using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseUIButton : MonoBehaviour
{
    /// <summary>
    /// 일시정지 버튼 누르면 호출
    /// </summary>
    public void OnClickPauseButton()
    {
        PlaySceneCanvas.Instance.PausePopup.OpenPopup();
    }
}
