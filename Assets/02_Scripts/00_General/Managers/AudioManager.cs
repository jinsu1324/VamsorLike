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
    #region 싱글톤_씬이동 O
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
    private AudioMixer _baseMixer;                                              // 베이스 오디오 믹서

    [Header("BGM")]
    [SerializeField]
    private AudioMixerGroup _bgmMixerGroup;                                     // BGM 오디오 믹서 그룹
    [SerializeField]
    private AudioSource _bgmSource;                                             // BGM 오디오소스
    [SerializeField]
    private Transform _bgmPoolParent;                                           // BGM 풀 부모
    [SerializeField]
    private int _maxBGMCount = 2;                                               // BGM 오디오소스 풀링할 갯수
    private Queue<AudioSource> _bgmSourcePool = new Queue<AudioSource>();       // SFX 오디오소스 풀
    [SerializeField]
    private Dictionary<BGMType, AudioClip> _bgmClipDict = new Dictionary<BGMType, AudioClip>(); // BGM 오디오 클립들

    [Header("SFX")]
    [SerializeField]
    private AudioMixerGroup _sfxMixerGroup;                                     // SFX 오디오 믹서 그룹
    [SerializeField]
    private AudioSource _sfxSource;                                             // SFX 오디오소스
    [SerializeField]
    private Transform _sfxPoolParent;                                           // SFX 풀 부모
    [SerializeField]
    private int _maxSFXCount = 20;                                              // SFX 오디오소스 풀링할 갯수
    private Queue<AudioSource> _sfxSourcePool = new Queue<AudioSource>();       // SFX 오디오소스 풀
    [SerializeField]
    private Dictionary<SFXType, AudioClip> _sfxClipDict = new Dictionary<SFXType, AudioClip>(); // SFX 오디오 클립들

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
    /// BGM 플레이
    /// </summary>
    public void PlayBGM(BGMType bgmType)
    {
        // bgm클립딕셔너리 값이 없으면 그냥 리턴
        if (_bgmClipDict.ContainsKey(bgmType) == false) 
            return;

        // 풀 카운트가 1개라도 차있으면 그 풀에서 빼서 쓰고, 아니면 풀을 채우고 사용
        AudioSource bgmSource = _bgmSourcePool.Count > 0 ? _bgmSourcePool.Dequeue() : FillBGMPool();
        bgmSource.outputAudioMixerGroup = _bgmMixerGroup;
        bgmSource.clip = _bgmClipDict[bgmType];
        bgmSource.loop = true;
        bgmSource.gameObject.SetActive(true);
        bgmSource.Play();

    }

    /// <summary>
    /// BGM 오브젝트 풀 채우기
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
    /// 오디오 소스 다시 풀에 반환
    /// </summary>
    private IEnumerator ReturnBGMAsync(AudioSource audioSource)
    {
        // 재생한 오디오 클립 길이 만큼 대기
        yield return new WaitForSeconds(audioSource.clip.length);

        // 풀로 다시 반환
        audioSource.gameObject.SetActive(false);
        _bgmSourcePool.Enqueue(audioSource);
    }


    /// <summary>
    /// 0~1의 매개변수값을 데시벨로 변환 후 bgm에 적용
    /// </summary>
    /// <param name="volume"> volume은 0~1의 값만 지정 </param>
    public void SetBGMVolume(float volume)
    {
        // 데시벨로 변환 (공식임) 
        float db = Mathf.Log10(volume) * 20; 

        // BGM그룹으로 설정된 오디오들 볼륨을 조절
        _baseMixer.SetFloat("BGMVolume", db);
    }
    #endregion

    #region SFX ================
    /// <summary>
    /// SFX 플레이
    /// </summary>
    public void PlaySFX(SFXType sfxType)
    {
        // sfx클립딕셔너리 값이 없으면 그냥 리턴
        if (_sfxClipDict.ContainsKey(sfxType) == false) 
            return;
        
        // 풀 카운트가 1개라도 차있으면 그 풀에서 빼서 쓰고, 아니면 풀을 채우고 사용
        AudioSource sfxSource = _sfxSourcePool.Count > 0 ? _sfxSourcePool.Dequeue() : FillSFXPool();
        sfxSource.outputAudioMixerGroup = _sfxMixerGroup;
        sfxSource.clip = _sfxClipDict[sfxType];
        sfxSource.gameObject.SetActive(true);
        sfxSource.Play();

        // 오디오 다 사용하고나면 반환하는 코루틴도 시작
        StartCoroutine(ReturnSFXAsync(sfxSource));
    }

    /// <summary>
    /// SFX 오브젝트 풀 채우기
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
    /// 오디오 소스 다시 풀에 반환
    /// </summary>
    private IEnumerator ReturnSFXAsync(AudioSource audioSource)
    {
        // 재생한 오디오 클립 길이 만큼 대기
        yield return new WaitForSeconds(audioSource.clip.length);

        // 풀로 다시 반환
        audioSource.gameObject.SetActive(false);
        _sfxSourcePool.Enqueue(audioSource);
    }

    /// <summary>
    /// 0~1의 매개변수값을 데시벨로 변환 후 sfx에 적용
    /// </summary>
    /// <param name="volume"> volume은 0~1의 값만 지정 </param>
    public void SetSFXVolume(AudioSource audioSource, float volume)
    {
        // 데시벨로 변환 (공식임) 
        float db = Mathf.Log10(volume) * 20;

        // SFX그룹으로 설정된 오디오들 볼륨을 조절
        _baseMixer.SetFloat("SFXVolume", db);
    }
    #endregion
}
