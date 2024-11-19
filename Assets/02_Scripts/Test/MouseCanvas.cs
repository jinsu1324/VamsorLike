using UnityEngine;

public class MouseCanvas : MonoBehaviour
{
    public RectTransform cursorImage;

    private void Awake()
    {
        cursorImage.gameObject.SetActive(false);
    }

#if UNITY_EDITOR // Unity Editor������ ����
    void Start()
    {
        cursorImage.gameObject.SetActive(true);    
        Cursor.visible = false;
    }

    void Update()
    {
        Vector3 mousePosition = Input.mousePosition;
        cursorImage.position = mousePosition;
    }
#endif
}
