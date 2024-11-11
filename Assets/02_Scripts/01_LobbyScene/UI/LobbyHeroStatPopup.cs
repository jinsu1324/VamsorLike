using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LobbyHeroStatPopup : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI _nameText;              // �̸� �ؽ�Ʈ

    [SerializeField]
    private TextMeshProUGUI _descText;              // ���� �ؽ�Ʈ

    [SerializeField]
    private Slider _atkSlider;                      // ���ݷ� �����̴�

    [SerializeField]
    private Slider _hpSlider;                       // ü�� �����̴�

    [SerializeField]
    private Slider _speedSlider;                    // �̵��ӵ� �����̴�

    [SerializeField]
    private Button _exitButton;                     // ������ ��ư

    [SerializeField]
    private Button _gameStartButton;                // ���� ���� ��ư

    [SerializeField]
    private Animator _animator;                     // �ִϸ����� ���� ����

    private HeroID _selectedHeroID;                 // ���õ� ���� ID
    private LobbyHero _selectedLobbyHero;           // ���õ� ���� ����

    private string _showAnimClipName = "LobbyHeroStatPopup_Show";   // �˾� �ѱ� �ִϸ��̼� �̸� string
    private float _showAnimClipLength;                               // Ŭ�� ���� ���� ����


    /// <summary>
    /// Start �Լ�
    /// </summary>
    private void Start()
    {
        _animator = GetComponent<Animator>();
        _showAnimClipLength = AnimSupporter.GetAnimationClipLength(_showAnimClipName, _animator);
    }

    /// <summary>
    /// UI ������ �ʱ�ȭ
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
    /// �˾� �Ѵ� �ִϸ��̼� ���� + �˾� �ѱ�
    /// </summary>
    public void ShowAnim_PopupON(LobbyHero lobbyHero)
    {
        Initialized(lobbyHero);
        PopupON();

        _animator.SetFloat("Speed", 1.0f);
        _animator.Play(_showAnimClipName, 0, 0f);
    }

    /// <summary>
    /// �˾� ����� �ִϸ��̼� ���� + �˾� ����
    /// </summary>
    public void HideAnim_PopupOFF()
    {
        _animator.SetFloat("Speed", -1.0f);
        _animator.Play(_showAnimClipName, 0, 1f);
        
        Invoke("PopupOFF", _showAnimClipLength);
    }

    /// <summary>
    /// Exit ��ư ������ ȣ��Ǵ� �Լ�
    /// </summary>
    public void OnClickExitButton()
    {
        LobbySceneManager.Instance.Change_DefaultState();
    }

    /// <summary>
    /// GameStart ��ư ������ ȣ��Ǵ� �Լ�
    /// </summary>
    public void OnClickGameStartButton()
    {
        StartCoroutine(GameStart());
    }

    /// <summary>
    /// ���� ���� �ڷ�ƾ
    /// </summary>
    private IEnumerator GameStart()
    {
        // ���� �̹� ���ӿ� ������ ���� ID ����
        GameManager.Instance.MyHeroIDSetting(_selectedHeroID);

        // ĳ���� �ִϸ��̼�
        _selectedLobbyHero.PlayAnimation_Ready();

        // ���
        yield return new WaitForSeconds(0.5f);

        // �ε� �ִϸ��̼�
        LobbySceneCanvas.Instance.FadeInOutController.FadeIn();

        // ���
        yield return new WaitForSeconds(1.0f);

        // �� ��ȯ
        SceneLoader.LoadScene("03_PlayScene");
    }

    /// <summary>
    /// �˾� �ѱ�
    /// </summary>
    private void PopupON()
    {
        gameObject.SetActive(true);
    }

    /// <summary>
    /// �˾� ����
    /// </summary>
    private void PopupOFF()
    {
        gameObject.SetActive(false);
    }
}