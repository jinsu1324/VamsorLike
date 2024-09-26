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

    // 내가 이번 게임에 선택한 영웅
    public static HeroObject ThisGameHeroObject { get; set; }

    // 영웅 선택해서 게임 시작 되었는지
    public bool IsGameStart { get; set; }

    // 영웅 선택 팝업
    [SerializeField]
    public HeroSelectPopup HeroSelectPopup { get; set; }

    // 스킬 선택 팝업
    [SerializeField]
    public SkillChoicePopup SkillPopupUI { get; set; }

    // HP바
    [SerializeField]
    public HPBar HPBar { get; set; }    

    // 메인 UI 캔버스
    [SerializeField]
    public Transform GuageBarsTF { get; set; }

    // 데미지 텍스트 UI 오브젝트 풀
    [SerializeField]
    public ObjectPool DamageTextUIPool { get; set; }

    private void Start()
    {
        ScenePopupsInitialize();
    }

    // 씬 팝업들 ON OFF 초기화
    private void ScenePopupsInitialize()
    {
        HeroSelectPopup.OpenPopup();
        SkillPopupUI.CloseSkillPopup();
    }

    // 게임시작 bool 파라미터로 변경
    public void IsGameStartChange(bool state)
    {        
        IsGameStart = state;
    }

    // 이번게임영웅으로 선택된 영웅 셋팅 및 스폰
    public void ThisGameHeroSetting(HeroID selectHeroID)
    {
        // 선택한 영웅을 이번게임의 영웅으로 할당
        ThisGameHeroObject = HeroManager.Instance.HeroObjectDict[selectHeroID];

        // 이번게임으로 선택된 영웅을 필드에 스폰
        ThisGameHeroObject = Instantiate(HeroManager.Instance.HeroObjectDict[selectHeroID]);

        // 이번게임으로 선택된 영웅 데이터 셋팅
        ThisGameHeroObject.DataSetting();
    }
}
