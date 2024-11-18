using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Audio;

public enum SFXType
{
    ButtonClick,
    CampFire,
    GameOver,
    GetEXPItem,
    GetHealItem,
    GetMagnetItem,
    HeroDamaged,
    HeroSelect_FireSpell,
    HeroSelect_SlashSword,
    HeroSelectComplete,
    Hit,
    LevelUp,
    Magic_Attack,
    Magic_Hit,
    MagicWideArea_Hit,
    Melee_Attack,
    Melee_Hit,
    HeroMovement,
    GetGoldItem
}

public enum BGMType
{
    LobbyScene,
    PlayScnene
}

public class AudioManager : SerializedMonoBehaviour
{
    #region �̱���_���̵� O
    private static AudioManager _instance;

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

    public static AudioManager Instance
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

    [SerializeField]
    private AudioMixer _baseMixer;                                              // ���̽� ����� �ͼ�

    [Header("BGM")]
    [SerializeField]
    private AudioMixerGroup _bgmMixerGroup;                                     // BGM ����� �ͼ� �׷�
    [SerializeField]
    private AudioSource _bgmSource;                                             // BGM ������ҽ�
    [SerializeField]
    private Dictionary<BGMType, AudioClip> _bgmClipDict = new Dictionary<BGMType, AudioClip>(); // BGM ����� Ŭ����

    [Header("SFX")]
    [SerializeField]
    private AudioMixerGroup _sfxMixerGroup;                                     // SFX ����� �ͼ� �׷�
    [SerializeField]
    private AudioSource _sfxSource;                                             // SFX ������ҽ�
    [SerializeField]
    private Transform _sfxPoolParent;                                           // SFX Ǯ �θ�
    [SerializeField]
    private int _maxSFXCount = 20;                                              // SFX ������ҽ� Ǯ���� ����
    private Queue<AudioSource> _sfxSourcePool = new Queue<AudioSource>();       // SFX ������ҽ� Ǯ
    [SerializeField]
    private Dictionary<SFXType, AudioClip> _sfxClipDict = new Dictionary<SFXType, AudioClip>(); // SFX ����� Ŭ����

    /// <summary>
    /// OnEnable
    /// </summary>
    private void OnEnable()
    {
        FillSFXPool();
    }

    #region BGM ================
    /// <summary>
    /// BGM �÷���
    /// </summary>
    public void PlayBGM(BGMType bgmType)
    {
        // bgmŬ����ųʸ� ���� ������ �׳� ����
        if (_bgmClipDict.ContainsKey(bgmType) == false) 
            return;

        // ����� �ҽ��� Ŭ�� ã�Ƴְ� �÷���
        _bgmSource.outputAudioMixerGroup = _bgmMixerGroup;
        _bgmSource.clip = _bgmClipDict[bgmType];
        _bgmSource.loop = true;
        _bgmSource.Play();
    }

    /// <summary>
    /// 0~1�� �Ű��������� ���ú��� ��ȯ �� bgm�� ����
    /// </summary>
    /// <param name="volume"> volume�� 0~1�� ���� ���� </param>
    public void SetBGMVolume(float volume)
    {
        float db;

        if (volume <= 0)
        {
            // ���Ұ� ó�� (AudioMixer�� �ּҰ� (����))
            db = -80f;
        }
        else
        {
            // ���ú��� ��ȯ (������) 
            db = Mathf.Log10(volume) * 20;
        }

        // BGM�׷����� ������ ������� ������ ����
        _baseMixer.SetFloat("BGMVolume", db);
    }

    /// <summary>
    /// BGM ���� ���� ���̱�
    /// </summary>
    public void StartFadeOutBGM(float duration)
    {
        StartCoroutine(FadeOutBGM(duration));
    }

    /// <summary>
    /// BGM ���� ���� Ű���
    /// </summary>
    public void StartFadeInBGM(float duration, float targetVolume)
    {
        StartCoroutine(FadeInBGM(duration, targetVolume));
    }

    /// <summary>
    /// BGM ���� ���� ���̱�
    /// </summary>
    private IEnumerator FadeOutBGM(float duration)
    {
        float currentTime = 0;
        float startVolume = 0f;

        // ���� ���� ��������
        _baseMixer.GetFloat("BGMVolume", out float currentDB);
        startVolume = Mathf.Pow(10, currentDB / 20); // ���ú����� 0~1 ������ ��ȯ

        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            float newVolume = Mathf.Lerp(startVolume, 0f, currentTime / duration);
            SetBGMVolume(newVolume); // ���� ����
            yield return null;
        }

        SetBGMVolume(0f); // ���������� 0���� ����
    }

    /// <summary>
    /// BGM ���� ���� Ű���
    /// </summary>
    private IEnumerator FadeInBGM(float duration, float targetVolume)
    {
        float currentTime = 0;

        // ���� ���� ��������
        _baseMixer.GetFloat("BGMVolume", out float currentDB);
        float startVolume = Mathf.Pow(10, currentDB / 20); // ���ú����� 0~1 ������ ��ȯ

        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            float newVolume = Mathf.Lerp(startVolume, targetVolume, currentTime / duration);
            SetBGMVolume(newVolume); // ���� ����
            yield return null;
        }

        SetBGMVolume(targetVolume); // ���������� ��ǥ �������� ����
    }
    #endregion

    #region SFX ================
    /// <summary>
    /// SFX �÷���
    /// </summary>
    public void PlaySFX(SFXType sfxType)
    {
        // sfxŬ����ųʸ� ���� ������ �׳� ����
        if (_sfxClipDict.ContainsKey(sfxType) == false) 
            return;
        
        // Ǯ ī��Ʈ�� 1���� �������� �� Ǯ���� ���� ����, �ƴϸ� Ǯ�� ä��� ���
        AudioSource sfxSource = _sfxSourcePool.Count > 0 ? _sfxSourcePool.Dequeue() : FillSFXPool();
        sfxSource.outputAudioMixerGroup = _sfxMixerGroup;
        sfxSource.clip = _sfxClipDict[sfxType];
        sfxSource.gameObject.SetActive(true);
        sfxSource.Play();
       
        // ����� �� ����ϰ��� ��ȯ�ϴ� �ڷ�ƾ�� ����
        StartCoroutine(ReturnSFXAsync(sfxSource));
    }
        
    /// <summary>
    /// SFX ������Ʈ Ǯ ä���
    /// </summary>
    private AudioSource FillSFXPool()
    {
        for (int i = 0; i < _maxSFXCount; i++)
        {
            AudioSource sfxSource = Instantiate(_sfxSource, _sfxPoolParent);
            sfxSource.outputAudioMixerGroup= _sfxMixerGroup;
            sfxSource.gameObject.SetActive(false);
            _sfxSourcePool.Enqueue(sfxSource);
        }

        return _sfxSourcePool.Dequeue();
    }

    /// <summary>
    /// ����� �ҽ� �ٽ� Ǯ�� ��ȯ
    /// </summary>
    private IEnumerator ReturnSFXAsync(AudioSource audioSource)
    {
        // ����� ����� Ŭ�� ���� ��ŭ ���
        yield return new WaitForSeconds(audioSource.clip.length);

        // Ǯ�� �ٽ� ��ȯ
        audioSource.gameObject.SetActive(false);
        _sfxSourcePool.Enqueue(audioSource);
    }

    /// <summary>
    /// ��ü SFX�� Ǯ�� �ٽ� ����
    /// </summary>
    public void AllReturnSFXPool()
    {
        int childCount = _sfxPoolParent.childCount;

        for (int i = childCount - 1; i >= 0; i--)
        {
            AudioSource child = _sfxPoolParent.GetChild(i).GetComponent<AudioSource>();

            child.gameObject.SetActive(false);
            _sfxSourcePool.Enqueue(child);
        }
    }

    /// <summary>
    /// 0~1�� �Ű��������� ���ú��� ��ȯ �� sfx�� ����
    /// </summary>
    /// <param name="volume"> volume�� 0~1�� ���� ���� </param>
    public void SetSFXVolume(float volume)
    {
        float db;

        if (volume <= 0)
        {
            // ���Ұ� ó�� (AudioMixer�� �ּҰ� (����))
            db = -80f;
        }
        else
        {
            // ���ú��� ��ȯ (������) 
            db = Mathf.Log10(volume) * 20;
        }

        // SFX�׷����� ������ ������� ������ ����
        _baseMixer.SetFloat("SFXVolume", db);
    }
    #endregion
}
