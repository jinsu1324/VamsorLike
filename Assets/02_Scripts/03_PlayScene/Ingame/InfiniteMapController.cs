using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NavMeshPlus.Components;
using UnityEngine.UIElements;

public class InfiniteMapController : MonoBehaviour
{
    [SerializeField]
    private List<ObjectPool> _tileObjectPoolList;   // 타일 오브젝트 풀 리스트

    [SerializeField]
    private Grid _parentGrid;                       // 부모가 될 그리드

    [SerializeField]
    private NavMeshSurface _surface;                // NavMeshSurface

    private GameObject[,] _tiles;                   // 생성된 타일들 담을 2차원 배열
    private Vector3 _lastPlayerPosition;            // 타일 재배치 되었을 때 플레이어 위치 저장할 변수

    private int _gridWidth = 3;                     // 가로 타일 개수
    private int _gridHeight = 3;                    // 세로 타일 개수
    private float _tileSize = 10f;                  // 타일 한 변의 길이
    private float _gridScale = 2f;                  // 그리드 스케일

    /// <summary>
    /// Update
    /// </summary>
    private void Update()
    {
        TileMove();
    }

    /// <summary>
    /// 맵 초기화
    /// </summary>
    public void InitMap()
    {
        _tileSize = _tileSize * _gridScale;

        InitialTileSpawn();
        _lastPlayerPosition = PlaySceneManager.Instance.MyHeroObj.transform.position;

        TileMove(true);
    }

    /// <summary>
    /// 초기 타일배치
    /// </summary>
    private void InitialTileSpawn()
    {
        // gridWidth, gridHeight 크기의 타일 2차원 배열 생성
        _tiles = new GameObject[_gridWidth, _gridHeight];

        // 2차원 배열 크기만큼 타일 생성
        for (int x = 0; x < _gridWidth; x++)
        {
            for (int y = 0; y < _gridHeight; y++)
            {
                // 타일 위치 설정
                Vector3 position = new Vector3(x * _tileSize, y * _tileSize, 0);
                
                // 랜덤타일 픽 및 셋팅, 스폰
                SpawnRandomTile(x, y, position);
            }
        }
    }

    /// <summary>
    /// 타일 움직이기
    /// </summary>
    private void TileMove(bool isFirstMove = false)
    {
        Vector3 playerPosition = PlaySceneManager.Instance.MyHeroObj.transform.position;

        // 플레이어가 일정이상 움직이지 않았으면 + 처음 움직이는게 아니라면 그냥 리턴 (처음 움직일때만 밑에 코드 실행가능)
        if ((Vector3.Distance(_lastPlayerPosition, playerPosition) < _tileSize / _gridWidth) && isFirstMove == false)
            return;

        // 타일 재배치 되었을때 플레이어 위치 저장
        _lastPlayerPosition = playerPosition;

        // 타일들 검사 후 재배치
        for (int x = 0; x < _gridWidth; x++)
        {
            for (int y = 0; y < _gridHeight; y++)
            {
                Vector3 tilePosition = _tiles[x, y].transform.position;

                bool isTileMoved = false;

                // 플레이어의 x 위치와 비교하여 타일을 반대쪽으로 이동
                if (Mathf.Abs(playerPosition.x - tilePosition.x) > ((_tileSize * _gridWidth) / 2))
                {
                    tilePosition.x += _tileSize * _gridWidth * Mathf.Sign(playerPosition.x - tilePosition.x);
                    isTileMoved = true;
                }

                // 플레이어의 y 위치와 비교하여 타일을 반대쪽으로 이동
                if (Mathf.Abs(playerPosition.y - tilePosition.y) > ((_tileSize * _gridWidth) / 2))
                {
                    tilePosition.y += _tileSize * _gridWidth * Mathf.Sign(playerPosition.y - tilePosition.y);
                    isTileMoved = true;
                }

                // 타일 움직여도 되면 기존타일 제거하고 새로운 랜덤타일 생성
                if (isTileMoved)
                {
                    // 기존 타일 제거
                    _tiles[x, y].GetComponent<ObjectPoolObject>().BackTrans();

                    // 새로운 랜덤타일 픽 및 셋팅, 스폰
                    SpawnRandomTile(x, y, tilePosition);
                }
            }
        }

        _surface.BuildNavMesh();
    }

    /// <summary>
    /// 랜덤 타일 반환 (타일 오브젝트 풀들 중에서 랜덤으로)
    /// </summary>
    private GameObject GetRandomTile()
    {
        ObjectPool tileObjectPool = _tileObjectPoolList[Random.Range(0, _tileObjectPoolList.Count)];
        GameObject tile = tileObjectPool.GetObj();

        return tile;
    }

    /// <summary>
    /// 랜덤타일 스폰 함수
    /// </summary>
    private void SpawnRandomTile(int x, int y, Vector3 pos)
    {
        // 랜덤 픽
        GameObject tilePrefab = GetRandomTile();

        // 셋팅
        tilePrefab.transform.position = pos;
        tilePrefab.transform.SetParent(_parentGrid.transform);

        // 타일 생성하고 배열에 저장 (부모는 그리드로 설정)
        _tiles[x, y] = tilePrefab;
    }
}
