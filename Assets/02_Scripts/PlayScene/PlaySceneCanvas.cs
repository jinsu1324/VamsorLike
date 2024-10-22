using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySceneCanvas : SerializedMonoBehaviour
{
    [Title("Popups")]
    [SerializeField]
    public SkillChoicePopup SkillChoicePopupUI { get; set; }    // 스킬 선택 팝업    
    [SerializeField]
    public ResultPopup ResultPopupUI { get; set; }              // 결과 팝업

    [Title("Views")]
    [SerializeField]
    public ObjectPool DamageTextsPool { get; set; }            // 데미지 텍스트 UI 오브젝트 풀

    [SerializeField]
    public PlayTimeUI PlayTimeUI { get; set; }                  // 플레이 타임 UI

    [Title("Interactions")]
    [SerializeField]
    public Joystick JoystickUI { get; set; }                    // 조이스틱


    /// <summary>
    /// Start 함수
    /// </summary>
    private void Start()
    {
        PopupsInitialize();
    }

    /// <summary>
    /// 씬 팝업들 ON OFF 초기화
    /// </summary>
    private void PopupsInitialize()
    {
        SkillChoicePopupUI.OpenPopup();
        ResultPopupUI.ClosePopup();
    }
}
