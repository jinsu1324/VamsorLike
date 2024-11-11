using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Test : MonoBehaviour
{
    public RectTransform goldIconUI; // UI ��� ������ ��ġ
    public float moveDuration = 2.0f; // �̵��� �ɸ��� �ð�

    public GameObject itemIcon;
    public GameObject fieldItem;

    public Camera uiCamera;

   
    private void Start()
    {
        MoveToUI();
    }

    public void MoveToUI()
    {
        StartCoroutine(MoveToUIRoutine());
    }

    private IEnumerator MoveToUIRoutine()
    {
        // 1. �ʵ����� �ִ� ��� �������� ���� ��ġ�� ��ũ�� ��ǥ�� ��ȯ
        Vector2 startScreenPosition = uiCamera.WorldToScreenPoint(fieldItem.transform.position);

        // 2. UI ��� �������� ��ġ�� ��ũ�� ��ǥ�� ��ȯ
        Vector2 endScreenPosition = RectTransformUtility.WorldToScreenPoint(uiCamera, goldIconUI.position);


        // 3. ��� �������� ����� �߰� ���� ���� (���۰� �� ������ �߰����� ���� ���� �ø�)
        //Vector2 centerControlPoint = 
        //    (startScreenPosition + endScreenPosition) / 2 
        //    + Vector2.up * Random.Range(-1000f, 1000f); // ���̸� �������� ����


        Vector2 centerControlPoint =
            (startScreenPosition + endScreenPosition) / 2
            + Vector2.up * Random.Range(-800f, -1000f); // ���̸� �������� ����

        // ��� �ð��� ������ ���� �ʱ�ȭ
        float elapsed = 0f;

        // 4. moveDuration ���� �������� ���������� �̵�
        while (elapsed < moveDuration)
        {
            // ��� �ð� ������Ʈ
            elapsed += Time.deltaTime;

            // t �� 0���� 1���� �����ϸ�, 0�̸� ������ġ, 1�̸� ��ǥ ��ġ�� ����
            float value = elapsed / moveDuration;

            // 5. ������ � �������� ���� ��ġ ��� (������ �̵�)
            // B(t) = (1-t)*(1-t)*P0   +   2*(1-t)*t*P1   +   t*t*P2      
            Vector2 currentScreenPosition =
                Mathf.Pow((1 - value), 2) * startScreenPosition +
                2 * (1 - value) * value * centerControlPoint +
                Mathf.Pow(value, 2) * endScreenPosition;

            // 5. ���� ��ũ�� ��ġ�� ���� ��ǥ�� ��ȯ�Ͽ� ��� ������ ��ġ�� ������Ʈ
            itemIcon.transform.position = uiCamera.ScreenToWorldPoint(new Vector3(currentScreenPosition.x, currentScreenPosition.y, uiCamera.nearClipPlane));

            // �� �������� ����Ͽ� �ִϸ��̼� ȿ���� ����
            yield return null;
        }

        // 6. ���� ��ġ�� UI ��� ���������� ����
        itemIcon.transform.position = uiCamera.ScreenToWorldPoint(new Vector3(endScreenPosition.x, endScreenPosition.y, uiCamera.nearClipPlane));

        Debug.Log("����!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!");
    }
}