using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// PlayScene에 필요한 글로벌한 것들 관리자 : 이번게임에 플레이하고있는 영웅 / 게임시작 여부 / 게임시작 / 영웅선택 팝업 
public class PlaySceneManager : SerializedMonoBehaviour
{
    #region 싱글톤
    private static PlaySceneManager _instance;

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    public static PlaySceneManager Instance
    {
        get
        {
            if (_instance == null)
            {
                return null;
            }

            return _instance;
        }
    }
    #endregion

    public static HeroObject ThisGameHeroObject { get; set; }   // 내가 이번 게임에 선택한 영웅
    public bool IsGameStart { get; set; }                       // 영웅 선택해서 게임 시작 되었는지

    [SerializeField]
    public HeroSelectPopup HeroSelectPopupUI { get; set; }      // 영웅 선택 팝업    

    [SerializeField]
    public SkillChoicePopup SkillChoicePopupUI { get; set; }    // 스킬 선택 팝업
    
    [SerializeField]
    public HPBar HPBarPrefab { get; set; }                      // HP바
    
    [SerializeField]
    public Transform HpBarParentTransform { get; set; }         // HP바 부모 트랜스폼

    [SerializeField]
    public ObjectPool DamageTextUIPool { get; set; }            // 데미지 텍스트 UI 오브젝트 풀
    
    [SerializeField]
    public Joystick JoystickUI { get; set; }                    // 조이스틱UI

    private void Start()
    {
        ScenePopupsInitialize();
    }

    /// <summary>
    /// 씬 팝업들 ON OFF 초기화
    /// </summary>
    private void ScenePopupsInitialize()
    {
        HeroSelectPopupUI.OpenPopup();
        SkillChoicePopupUI.CloseSkillPopup();
    }

    /// <summary>
    /// 게임시작 bool 파라미터로 변경
    /// </summary>
    public void IsGameStartChange(bool state)
    {        
        IsGameStart = state;
    }

    /// <summary>
    /// 이번게임영웅으로 선택된 영웅 셋팅 및 스폰
    /// </summary>
    public void ThisGameHeroSetting(HeroID selectHeroID)
    {
        // 선택한 영웅을 이번게임의 영웅으로 할당
        ThisGameHeroObject = HeroManager.Instance.HeroObjectDict[selectHeroID];

        // 이번게임으로 선택된 영웅을 필드에 스폰
        ThisGameHeroObject = Instantiate(HeroManager.Instance.HeroObjectDict[selectHeroID], new Vector3(0, 0, 0), Quaternion.identity);

        // 이번게임으로 선택된 영웅 데이터 셋팅
        ThisGameHeroObject.DataSetting();
    }
}
