using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]  // �� ��ũ��Ʈ�� ������ ��忡���� ����ǵ��� ����
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
        // �����Ϳ����� �����ϵ��� ����
#if UNITY_EDITOR
        UpdateGridObj();
#endif
    }

    private void UpdateGridObj()
    {
        if (!Application.isPlaying && Grid != null)
        {
            Vector3Int cellPosition = GetGridCellPos(transform.position);
            // ������Ʈ�� Grid�� ���� ����
            SettingGridPos(cellPosition);
        }
    }

    private void SettingGridPos(Vector3Int cellPos)
    {
        Vector3 adjustedPosition = GetAdjustedPosition(cellPos);

        // �θ� ������Ʈ�� ��ġ�� ����
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
        // ���� �Ʒ��� �������� ��ġ�� ����
        Vector3 adjustedPosition = Grid.GetCellCenterWorld(cellPosition);
        return adjustedPosition;
    }
}