using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySceneCanvas : SerializedMonoBehaviour
{
    [Title("Popups")]
    [SerializeField]
    public SkillSelectPopup SkillSelectPopup { get; set; }    // 스킬 선택 팝업    
    [SerializeField]
    public ResultPopup ResultPopup { get; set; }              // 결과 팝업

    [Title("UIs")]
    [SerializeField]
    public ObjectPool DamageTextsPool { get; set; }            // 데미지 텍스트 UI 오브젝트 풀

    [SerializeField]
    public PlayTimeUI PlayTimeUI { get; set; }                  // 플레이 타임 UI

    [SerializeField]
    public SkillInvenUI SkillInvenUI { get; set; }              // 스킬 인벤토리 UI

    [SerializeField]
    public BossHPBarUI BossHPBarUI { get; set; }                // 보스 HP 바 UI

    [Title("Interactions")]
    [SerializeField]
    public Joystick Joystick { get; set; }                    // 조이스틱


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
        SkillSelectPopup.OpenPopup();
        ResultPopup.ClosePopup();
    }
}
