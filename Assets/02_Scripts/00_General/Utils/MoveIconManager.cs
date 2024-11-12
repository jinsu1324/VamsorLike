using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class MoveIconManager : ObjectPool
{
    private Camera _mainCamera;                          // ����ī�޶�
    private Camera _uiCamera;                            // UIī�޶�

    [Title("General")]
    [SerializeField]
    private float _moveDuration = 2.0f;                  // �̵��� �ɸ��� �ð�

    [Title("Gold Move")]
    [SerializeField]
    private RectTransform _targetGoldIcon;               // Ÿ�� ��������
  
    /// <summary>
    /// Start
    /// </summary>
    private void Start()
    {
        _mainCamera = Camera.main;
        _uiCamera = PlaySceneCanvas.Instance.UICamera;
    }

    /// <summary>
    /// �������� Ÿ�پ����� ��ġ�� �̵��ϴ� �ڷ�ƾ ����
    /// </summary>
    public void StartMove_IconToTargetIcon(Transform fieldItem)
    {
        StartCoroutine(Move_IconToTargetIcon(fieldItem, _targetGoldIcon, _moveDuration, _uiCamera, _mainCamera));
    }

    /// <summary>
    /// �������� Ÿ�پ����� ��ġ�� �̵�
    /// </summary>
    private IEnumerator Move_IconToTargetIcon(Transform fieldItem, RectTransform targetIcon, float moveDuration, Camera uiCamera, Camera mainCamera)
    {
        // 0. �������� ����ϱ� ���� Ǯ���� ��������
        GameObject icon = GetObj();

        // 1. �ʵ����� �ִ� �������� ���� ��ġ�� Main Camera ��ũ�� ��ǥ�� ��ȯ
        Vector2 startScreenPosition = mainCamera.WorldToScreenPoint(fieldItem.position);

        // 2. startScreenPosition�� UI Camera ������ Viewport Point�� ��ȯ ��, �ٽ� UI Camera�� Screen Point�� ��ȯ
        Vector3 viewportPosition = mainCamera.ScreenToViewportPoint(startScreenPosition);
        startScreenPosition = uiCamera.ViewportToScreenPoint(viewportPosition);

        // 3. UI Ÿ�� �������� ��ġ�� UI Camera ������ ��ũ�� ��ǥ�� ��ȯ
        Vector2 endScreenPosition = RectTransformUtility.WorldToScreenPoint(uiCamera, targetIcon.position);

        // 4. ��� �������� ����� �߰� ���� ���� (�߰����� ������ ���̷� ���� �ø�)
        Vector2 centerControlPoint =
            (startScreenPosition + endScreenPosition) / 2
            + Vector2.up * Random.Range(200f, 500f);

        // ��� �ð��� ������ ���� �ʱ�ȭ
        float time = 0f;

        // 5. moveDuration ���� �������� ���������� �̵�
        while (time < moveDuration)
        {
            // ��� �ð� ������Ʈ
            time += Time.deltaTime;

            // value �� 0���� 1���� �����ϸ�, 0�̸� ������ġ, 1�̸� ��ǥ ��ġ�� ����
            float value = time / moveDuration;

            // 6. ������ � �������� ���� ��ġ ��� (������ �̵�)
            // B(t) = (1-t)*(1-t)*P0   +   2*(1-t)*t*P1   +   t*t*P2      
            Vector2 currentScreenPosition =
                Mathf.Pow((1 - value), 2) * startScreenPosition +
                2 * (1 - value) * value * centerControlPoint +
                Mathf.Pow(value, 2) * endScreenPosition;

            // 7.���� ��ũ�� ��ġ�� UI������ġ�� ��ȯ
            RectTransformUtility.ScreenPointToLocalPointInRectangle(
                targetIcon.parent as RectTransform,
                currentScreenPosition,
                uiCamera,
                out Vector2 uiPosition);

            // 8. icon�� UI��ġ ������Ʈ
            icon.GetComponent<RectTransform>().anchoredPosition = uiPosition;

            // �� �������� ����Ͽ� �ִϸ��̼� ȿ���� ����
            yield return null;
        }

        // 9. icon�� ���� ��ġ�� targetIcon ��ġ�� ����
        icon.GetComponent<RectTransform>().anchoredPosition = targetIcon.anchoredPosition;

        // 10. �� �̵������� Ǯ�� ����������
        icon.gameObject.GetComponent<ObjectPoolObject>().BackTrans();
    }
}
