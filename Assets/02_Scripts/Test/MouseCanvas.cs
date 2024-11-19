using UnityEngine;

public class MouseCanvas : MonoBehaviour
{
    public RectTransform cursorImage;

#if UNITY_EDITOR // Unity Editor에서만 실행
    void Start()
    {
        Cursor.visible = false;
    }

    void Update()
    {
        Vector3 mousePosition = Input.mousePosition;
        cursorImage.position = mousePosition;
    }
#endif
}
