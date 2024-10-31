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

    private string _showAnimClipName = "LobbyHeroStatPopup_Show";   // 팝업 켜기 애니메이션 이름 string
    private float _showAnimClipLegth;                               // 클립 길이 담을 변수


    /// <summary>
    /// Start 함수
    /// </summary>
    private void Start()
    {
        _animator = GetComponent<Animator>();
        _showAnimClipLegth = GetAnimationClipLengh(_showAnimClipName);
    }

    /// <summary>
    /// UI 정보들 초기화
    /// </summary>
    public void Initialized(HeroData heroData)
    {
        _nameText.text = heroData.Name;
        _descText.text = heroData.Desc;      
    }

    /// <summary>
    /// 팝업 켜는 애니메이션 실행 + 팝업 켜기
    /// </summary>
    public void ShowAnim_PopupON(HeroData heroData)
    {
        Initialized(heroData);
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
        
        Invoke("PopupOFF", _showAnimClipLegth);
    }

    /// <summary>
    /// Exit 버튼 누르면 호출되는 함수
    /// </summary>
    public void OnClickExitButton()
    {
        LobbySceneManager.Instance.Change_DefaultState();
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

    /// <summary>
    /// 애니메이션 클릭 길이 를 반환해주는 함스
    /// </summary>
    private float GetAnimationClipLengh(string clipName)
    {
        // Animator의 RuntimeAnimatorController에서 모든 애니메이션 클립 검색
        foreach (var clip in _animator.runtimeAnimatorController.animationClips)
        {
            if (clip.name == clipName)
            {
                // 일치하는 클립의 길이를 반환
                return clip.length;
            }
        }

        // 일치하는 클립을 찾지 못하면 0 반환
        Debug.Log("애니메이션 클립을 찾지 못했습니다.: " + clipName);
        return 0f;
    }
}