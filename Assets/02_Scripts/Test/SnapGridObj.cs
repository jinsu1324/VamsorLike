using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]  // 이 스크립트가 에디터 모드에서도 실행되도록 설정
public class SnapGridObj : MonoBehaviour
{
    private Grid grid;

    public Grid Grid
    {
        get
        {
            if (grid == null)
                grid = GetComponentInParent<Grid>();

            return grid;
        }
    }

    void Update()
    {
        // 에디터에서만 동작하도록 설정
#if UNITY_EDITOR
        UpdateGridObj();
#endif
    }

    private void UpdateGridObj()
    {
        if (!Application.isPlaying && Grid != null)
        {
            Vector3Int cellPosition = GetGridCellPos(transform.position);
            // 오브젝트를 Grid에 맞춰 스냅
            SettingGridPos(cellPosition);
        }
    }

    private void SettingGridPos(Vector3Int cellPos)
    {
        Vector3 adjustedPosition = GetAdjustedPosition(cellPos);

        // 부모 오브젝트의 위치를 조정
        transform.position = adjustedPosition;
    }

    private Vector3Int GetGridCellPos(Vector3 pos)
    {
        Vector3Int returnpos = Grid.WorldToCell(pos);
        returnpos.z = 0;
        return returnpos;
    }

    private Vector3 GetAdjustedPosition(Vector3Int cellPosition)
    {
        // 왼쪽 아래를 기준으로 위치를 조정
        Vector3 adjustedPosition = Grid.GetCellCenterWorld(cellPosition);
        return adjustedPosition;
    }
}