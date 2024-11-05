using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapTileController : MonoBehaviour
{
    /// <summary>
    /// ī�޶� collider�� Ÿ�Ͽ��� ����� �� (�̵� ���� Ÿ�Ͽ��� �����)
    /// </summary>
    private void OnTriggerExit2D(Collider2D collision)
    {
        // ī�޶� �޾ƿ���
        Camera camera = collision.gameObject.GetComponent<Camera>();
        if (camera == null)
            return;

        // ī�޶�, Ÿ�� ���� ���
        Vector3 direction = camera.transform.position - transform.position;

        // �ݶ��̴��� ����� ��, ī�޶� �� Ÿ���� ����ʿ� ��ġ�ϴ��� �Ǻ�
        float dirX = direction.x < 0 ? -1 : 1;
        float dirY = direction.y < 0 ? -1 : 1;

        // �����¿� ��������� �Ǻ��ϰ� �ڽ�(Ÿ��)�� �̵�
        if (Mathf.Abs(direction.x) > Mathf.Abs(direction.y))
            transform.Translate(Vector3.right * dirX * 128);
        else
            transform.Translate(Vector3.up * dirY * 128);
    }
}
