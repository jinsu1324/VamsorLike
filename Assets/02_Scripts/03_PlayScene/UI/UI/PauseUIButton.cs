using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseUIButton : MonoBehaviour
{
    /// <summary>
    /// �Ͻ����� ��ư ������ ȣ��
    /// </summary>
    public void OnClickPauseButton()
    {
        PlaySceneCanvas.Instance.PausePopup.OpenPopup();
    }
}
