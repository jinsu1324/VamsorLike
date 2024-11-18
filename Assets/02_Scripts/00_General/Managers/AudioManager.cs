using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Audio;

public enum SFXType
{
    Select
}

public enum BGMType
{
    Lobby_CampFire,
    Lobby_Bird,
    Lobby_BaseMusic,
    Play_BaseMusic
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
    private Transform _bgmPoolParent;                                           // BGM Ǯ �θ�
    [SerializeField]
    private int _maxBGMCount = 2;                                               // BGM ������ҽ� Ǯ���� ����
    private Queue<AudioSource> _bgmSourcePool = new Queue<AudioSource>();       // SFX ������ҽ� Ǯ
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
        FillBGMPool();
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

        // Ǯ ī��Ʈ�� 1���� �������� �� Ǯ���� ���� ����, �ƴϸ� Ǯ�� ä��� ���
        AudioSource bgmSource = _bgmSourcePool.Count > 0 ? _bgmSourcePool.Dequeue() : FillBGMPool();
        bgmSource.outputAudioMixerGroup = _bgmMixerGroup;
        bgmSource.clip = _bgmClipDict[bgmType];
        bgmSource.loop = true;
        bgmSource.gameObject.SetActive(true);
        bgmSource.Play();

    }

    /// <summary>
    /// BGM ������Ʈ Ǯ ä���
    /// </summary>
    private AudioSource FillBGMPool()
    {
        for (int i = 0; i < _maxBGMCount; i++)
        {
            AudioSource bgmSource = Instantiate(_bgmSource, _bgmPoolParent);
            bgmSource.outputAudioMixerGroup = _bgmMixerGroup;
            bgmSource.gameObject.SetActive(false);
            _bgmSourcePool.Enqueue(bgmSource);
        }

        return _bgmSourcePool.Dequeue();
    }

    /// <summary>
    /// ����� �ҽ� �ٽ� Ǯ�� ��ȯ
    /// </summary>
    private IEnumerator ReturnBGMAsync(AudioSource audioSource)
    {
        // ����� ����� Ŭ�� ���� ��ŭ ���
        yield return new WaitForSeconds(audioSource.clip.length);

        // Ǯ�� �ٽ� ��ȯ
        audioSource.gameObject.SetActive(false);
        _bgmSourcePool.Enqueue(audioSource);
    }


    /// <summary>
    /// 0~1�� �Ű��������� ���ú��� ��ȯ �� bgm�� ����
    /// </summary>
    /// <param name="volume"> volume�� 0~1�� ���� ���� </param>
    public void SetBGMVolume(float volume)
    {
        // ���ú��� ��ȯ (������) 
        float db = Mathf.Log10(volume) * 20; 

        // BGM�׷����� ������ ������� ������ ����
        _baseMixer.SetFloat("BGMVolume", db);
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
    /// 0~1�� �Ű��������� ���ú��� ��ȯ �� sfx�� ����
    /// </summary>
    /// <param name="volume"> volume�� 0~1�� ���� ���� </param>
    public void SetSFXVolume(AudioSource audioSource, float volume)
    {
        // ���ú��� ��ȯ (������) 
        float db = Mathf.Log10(volume) * 20;

        // SFX�׷����� ������ ������� ������ ����
        _baseMixer.SetFloat("SFXVolume", db);
    }
    #endregion
}
