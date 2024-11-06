using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NavMeshPlus.Components;
using UnityEngine.UIElements;

public class InfiniteMapController : MonoBehaviour
{
    [SerializeField]
    private List<ObjectPool> _tileObjectPoolList;   // Ÿ�� ������Ʈ Ǯ ����Ʈ

    [SerializeField]
    private Grid _parentGrid;                       // �θ� �� �׸���

    [SerializeField]
    private NavMeshSurface _surface;                // NavMeshSurface

    private GameObject[,] _tiles;                   // ������ Ÿ�ϵ� ���� 2���� �迭
    private Vector3 _lastPlayerPosition;            // Ÿ�� ���ġ �Ǿ��� �� �÷��̾� ��ġ ������ ����

    private int _gridWidth = 3;                     // ���� Ÿ�� ����
    private int _gridHeight = 3;                    // ���� Ÿ�� ����
    private float _tileSize = 10f;                  // Ÿ�� �� ���� ����
    private float _gridScale = 2f;                  // �׸��� ������

    /// <summary>
    /// Update
    /// </summary>
    private void Update()
    {
        TileMove();
    }

    /// <summary>
    /// �� �ʱ�ȭ
    /// </summary>
    public void InitMap()
    {
        _tileSize = _tileSize * _gridScale;

        InitialTileSpawn();
        _lastPlayerPosition = PlaySceneManager.Instance.MyHeroObj.transform.position;

        TileMove(true);
    }

    /// <summary>
    /// �ʱ� Ÿ�Ϲ�ġ
    /// </summary>
    private void InitialTileSpawn()
    {
        // gridWidth, gridHeight ũ���� Ÿ�� 2���� �迭 ����
        _tiles = new GameObject[_gridWidth, _gridHeight];

        // 2���� �迭 ũ�⸸ŭ Ÿ�� ����
        for (int x = 0; x < _gridWidth; x++)
        {
            for (int y = 0; y < _gridHeight; y++)
            {
                // Ÿ�� ��ġ ����
                Vector3 position = new Vector3(x * _tileSize, y * _tileSize, 0);
                
                // ����Ÿ�� �� �� ����, ����
                SpawnRandomTile(x, y, position);
            }
        }
    }

    /// <summary>
    /// Ÿ�� �����̱�
    /// </summary>
    private void TileMove(bool isFirstMove = false)
    {
        Vector3 playerPosition = PlaySceneManager.Instance.MyHeroObj.transform.position;

        // �÷��̾ �����̻� �������� �ʾ����� + ó�� �����̴°� �ƴ϶�� �׳� ���� (ó�� �����϶��� �ؿ� �ڵ� ���డ��)
        if ((Vector3.Distance(_lastPlayerPosition, playerPosition) < _tileSize / _gridWidth) && isFirstMove == false)
            return;

        // Ÿ�� ���ġ �Ǿ����� �÷��̾� ��ġ ����
        _lastPlayerPosition = playerPosition;

        // Ÿ�ϵ� �˻� �� ���ġ
        for (int x = 0; x < _gridWidth; x++)
        {
            for (int y = 0; y < _gridHeight; y++)
            {
                Vector3 tilePosition = _tiles[x, y].transform.position;

                bool isTileMoved = false;

                // �÷��̾��� x ��ġ�� ���Ͽ� Ÿ���� �ݴ������� �̵�
                if (Mathf.Abs(playerPosition.x - tilePosition.x) > ((_tileSize * _gridWidth) / 2))
                {
                    tilePosition.x += _tileSize * _gridWidth * Mathf.Sign(playerPosition.x - tilePosition.x);
                    isTileMoved = true;
                }

                // �÷��̾��� y ��ġ�� ���Ͽ� Ÿ���� �ݴ������� �̵�
                if (Mathf.Abs(playerPosition.y - tilePosition.y) > ((_tileSize * _gridWidth) / 2))
                {
                    tilePosition.y += _tileSize * _gridWidth * Mathf.Sign(playerPosition.y - tilePosition.y);
                    isTileMoved = true;
                }

                // Ÿ�� �������� �Ǹ� ����Ÿ�� �����ϰ� ���ο� ����Ÿ�� ����
                if (isTileMoved)
                {
                    // ���� Ÿ�� ����
                    _tiles[x, y].GetComponent<ObjectPoolObject>().BackTrans();

                    // ���ο� ����Ÿ�� �� �� ����, ����
                    SpawnRandomTile(x, y, tilePosition);
                }
            }
        }

        _surface.BuildNavMesh();
    }

    /// <summary>
    /// ���� Ÿ�� ��ȯ (Ÿ�� ������Ʈ Ǯ�� �߿��� ��������)
    /// </summary>
    private GameObject GetRandomTile()
    {
        ObjectPool tileObjectPool = _tileObjectPoolList[Random.Range(0, _tileObjectPoolList.Count)];
        GameObject tile = tileObjectPool.GetObj();

        return tile;
    }

    /// <summary>
    /// ����Ÿ�� ���� �Լ�
    /// </summary>
    private void SpawnRandomTile(int x, int y, Vector3 pos)
    {
        // ���� ��
        GameObject tilePrefab = GetRandomTile();

        // ����
        tilePrefab.transform.position = pos;
        tilePrefab.transform.SetParent(_parentGrid.transform);

        // Ÿ�� �����ϰ� �迭�� ���� (�θ�� �׸���� ����)
        _tiles[x, y] = tilePrefab;
    }
}
