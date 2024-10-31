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

    private string _showAnimClipName = "LobbyHeroStatPopup_Show";   // �˾� �ѱ� �ִϸ��̼� �̸� string
    private float _showAnimClipLegth;                               // Ŭ�� ���� ���� ����


    /// <summary>
    /// Start �Լ�
    /// </summary>
    private void Start()
    {
        _animator = GetComponent<Animator>();
        _showAnimClipLegth = GetAnimationClipLengh(_showAnimClipName);
    }

    /// <summary>
    /// UI ������ �ʱ�ȭ
    /// </summary>
    public void Initialized(HeroData heroData)
    {
        _nameText.text = heroData.Name;
        _descText.text = heroData.Desc;      
    }

    /// <summary>
    /// �˾� �Ѵ� �ִϸ��̼� ���� + �˾� �ѱ�
    /// </summary>
    public void ShowAnim_PopupON(HeroData heroData)
    {
        Initialized(heroData);
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
        
        Invoke("PopupOFF", _showAnimClipLegth);
    }

    /// <summary>
    /// Exit ��ư ������ ȣ��Ǵ� �Լ�
    /// </summary>
    public void OnClickExitButton()
    {
        LobbySceneManager.Instance.Change_DefaultState();
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

    /// <summary>
    /// �ִϸ��̼� Ŭ�� ���� �� ��ȯ���ִ� �Խ�
    /// </summary>
    private float GetAnimationClipLengh(string clipName)
    {
        // Animator�� RuntimeAnimatorController���� ��� �ִϸ��̼� Ŭ�� �˻�
        foreach (var clip in _animator.runtimeAnimatorController.animationClips)
        {
            if (clip.name == clipName)
            {
                // ��ġ�ϴ� Ŭ���� ���̸� ��ȯ
                return clip.length;
            }
        }

        // ��ġ�ϴ� Ŭ���� ã�� ���ϸ� 0 ��ȯ
        Debug.Log("�ִϸ��̼� Ŭ���� ã�� ���߽��ϴ�.: " + clipName);
        return 0f;
    }
}