using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Test : MonoBehaviour
{
    public Transform _fieldItem;            // �ʵ� ������
    public RectTransform _icon;             // ������ ������        
    public RectTransform _targetIcon;       // Ÿ�� ������
    public float _moveDuration = 2.0f;      // �̵��� �ɸ��� �ð�
    public Camera _uiCamera;                // UIī�޶�
   
    /// <summary>
    /// Start
    /// </summary>
    private void Start()
    {
        StartMove_IconToTargetIcon(_fieldItem, _icon, _targetIcon, _moveDuration, _uiCamera);
    }

    /// <summary>
    /// �������� Ÿ�پ����� ��ġ�� �̵��ϴ� �ڷ�ƾ ����
    /// </summary>
    public void StartMove_IconToTargetIcon(Transform fieldItem, RectTransform icon, RectTransform targetIcon, float moveDuration, Camera uiCamera)
    {
        StartCoroutine(Move_IconToTargetIcon(fieldItem, icon, targetIcon, moveDuration, uiCamera));
    }

    /// <summary>
    /// �������� Ÿ�پ����� ��ġ�� �̵�
    /// </summary>
    private IEnumerator Move_IconToTargetIcon(Transform fieldItem, RectTransform icon, RectTransform targetIcon, float moveDuration, Camera uiCamera)
    {
        // 1. �ʵ����� �ִ� �������� ���� ��ġ�� ��ũ�� ��ǥ�� ��ȯ
        Vector2 startScreenPosition = uiCamera.WorldToScreenPoint(fieldItem.position);

        // 2. UI Ÿ�� �������� ��ġ�� ��ũ�� ��ǥ�� ��ȯ
        Vector2 endScreenPosition = RectTransformUtility.WorldToScreenPoint(uiCamera, targetIcon.position);

        // 3. ��� �������� ����� �߰� ���� ���� (�߰����� ������ ���̷� ���� �ø�)
        Vector2 centerControlPoint =
            (startScreenPosition + endScreenPosition) / 2
            + Vector2.up * Random.Range(-800f, -1000f);

        // ��� �ð��� ������ ���� �ʱ�ȭ
        float time = 0f;

        // 4. moveDuration ���� �������� ���������� �̵�
        while (time < moveDuration)
        {
            // ��� �ð� ������Ʈ
            time += Time.deltaTime;

            // value �� 0���� 1���� �����ϸ�, 0�̸� ������ġ, 1�̸� ��ǥ ��ġ�� ����
            float value = time / moveDuration;

            // 5. ������ � �������� ���� ��ġ ��� (������ �̵�)
            // B(t) = (1-t)*(1-t)*P0   +   2*(1-t)*t*P1   +   t*t*P2      
            Vector2 currentScreenPosition =
                Mathf.Pow((1 - value), 2) * startScreenPosition +
                2 * (1 - value) * value * centerControlPoint +
                Mathf.Pow(value, 2) * endScreenPosition;

            // 6. ���� ��ũ�� ��ġ�� UI��ġ�� ��ȯ
            RectTransformUtility.ScreenPointToLocalPointInRectangle(
                targetIcon.parent as RectTransform,
                currentScreenPosition,
                uiCamera,
                out Vector2 uiPosition);

            // 7. icon�� UI��ġ ������Ʈ
            icon.GetComponent<RectTransform>().anchoredPosition = uiPosition;
         
            // �� �������� ����Ͽ� �ִϸ��̼� ȿ���� ����
            yield return null;
        }

        // 8. icon�� ���� ��ġ�� targetIcon ��ġ�� ����
        icon.GetComponent<RectTransform>().anchoredPosition = targetIcon.anchoredPosition;
    }
}