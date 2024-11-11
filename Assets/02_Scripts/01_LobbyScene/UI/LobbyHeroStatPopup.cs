using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LobbyHeroStatPopup : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI _nameText;              // 이름 텍스트

    [SerializeField]
    private TextMeshProUGUI _descText;              // 설명 텍스트

    [SerializeField]
    private Slider _atkSlider;                      // 공격력 슬라이더

    [SerializeField]
    private Slider _hpSlider;                       // 체력 슬라이더

    [SerializeField]
    private Slider _speedSlider;                    // 이동속도 슬라이더

    [SerializeField]
    private Button _exitButton;                     // 나가기 버튼

    [SerializeField]
    private Button _gameStartButton;                // 게임 시작 버튼

    [SerializeField]
    private Animator _animator;                     // 애니메이터 담을 변수

    private HeroID _selectedHeroID;                 // 선택된 영웅 ID
    private LobbyHero _selectedLobbyHero;           // 선택된 영웅 정보

    private string _showAnimClipName = "LobbyHeroStatPopup_Show";   // 팝업 켜기 애니메이션 이름 string
    private float _showAnimClipLength;                               // 클립 길이 담을 변수


    /// <summary>
    /// Start 함수
    /// </summary>
    private void Start()
    {
        _animator = GetComponent<Animator>();
        _showAnimClipLength = AnimSupporter.GetAnimationClipLength(_showAnimClipName, _animator);
    }

    /// <summary>
    /// UI 정보들 초기화
    /// </summary>
    public void Initialized(LobbyHero lobbyHero)
    {
        _selectedLobbyHero = lobbyHero;

        HeroData heroData = _selectedLobbyHero.BaseHeroData;

        _nameText.text = heroData.Name;
        _descText.text = heroData.Desc;
        _atkSlider.value = (heroData.Atk / 500.0f) * 5.0f;
        _hpSlider.value = (heroData.MaxHp / 500.0f) * 5.0f;
        _speedSlider.value = (heroData.Speed / 10.0f) * 5.0f;

        _selectedHeroID = (HeroID)Enum.Parse(typeof(HeroID), heroData.ID);
    }

    /// <summary>
    /// 팝업 켜는 애니메이션 실행 + 팝업 켜기
    /// </summary>
    public void ShowAnim_PopupON(LobbyHero lobbyHero)
    {
        Initialized(lobbyHero);
        PopupON();

        _animator.SetFloat("Speed", 1.0f);
        _animator.Play(_showAnimClipName, 0, 0f);
    }

    /// <summary>
    /// 팝업 숨기는 애니메이션 실행 + 팝업 끄기
    /// </summary>
    public void HideAnim_PopupOFF()
    {
        _animator.SetFloat("Speed", -1.0f);
        _animator.Play(_showAnimClipName, 0, 1f);
        
        Invoke("PopupOFF", _showAnimClipLength);
    }

    /// <summary>
    /// Exit 버튼 누르면 호출되는 함수
    /// </summary>
    public void OnClickExitButton()
    {
        LobbySceneManager.Instance.Change_DefaultState();
    }

    /// <summary>
    /// GameStart 버튼 누르면 호출되는 함수
    /// </summary>
    public void OnClickGameStartButton()
    {
        StartCoroutine(GameStart());
    }

    /// <summary>
    /// 게임 시작 코루틴
    /// </summary>
    private IEnumerator GameStart()
    {
        // 내가 이번 게임에 선택한 영웅 ID 셋팅
        GameManager.Instance.MyHeroIDSetting(_selectedHeroID);

        // 캐릭터 애니메이션
        _selectedLobbyHero.PlayAnimation_Ready();

        // 대기
        yield return new WaitForSeconds(0.5f);

        // 로딩 애니메이션
        LobbySceneCanvas.Instance.FadeInOutController.FadeIn();

        // 대기
        yield return new WaitForSeconds(1.0f);

        // 씬 전환
        SceneLoader.LoadScene("03_PlayScene");
    }

    /// <summary>
    /// 팝업 켜기
    /// </summary>
    private void PopupON()
    {
        gameObject.SetActive(true);
    }

    /// <summary>
    /// 팝업 끄기
    /// </summary>
    private void PopupOFF()
    {
        gameObject.SetActive(false);
    }
}