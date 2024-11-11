using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CautionView : MonoBehaviour
{
    /// <summary>
    /// view ÄÑ±â
    /// </summary>
    public void OpenCautionView()
    {
        gameObject.SetActive(true);
    }

    /// <summary>
    /// view ²ô±â
    /// </summary>
    public void CloseCautionView()
    {
        gameObject.SetActive(false);
    }


}
