using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CautionView : MonoBehaviour
{
    /// <summary>
    /// view �ѱ�
    /// </summary>
    public void OpenCautionView()
    {
        gameObject.SetActive(true);
    }

    /// <summary>
    /// view ����
    /// </summary>
    public void CloseCautionView()
    {
        gameObject.SetActive(false);
    }


}
