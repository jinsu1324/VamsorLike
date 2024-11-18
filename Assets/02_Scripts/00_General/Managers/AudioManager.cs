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

        // 오디오 소스에 클립 찾아넣고 플레이
        _bgmSource.outputAudioMixerGroup = _bgmMixerGroup;
        _bgmSource.clip = _bgmClipDict[bgmType];
        _bgmSource.loop = true;
        _bgmSource.Play();
    }

    /// <summary>
    /// 0~1의 매개변수값을 데시벨로 변환 후 bgm에 적용
    /// </summary>
    /// <param name="volume"> volume은 0~1의 값만 지정 </param>
    public void SetBGMVolume(float volume)
    {
        float db;

        if (volume <= 0)
        {
            // 음소거 처리 (AudioMixer의 최소값 (무음))
            db = -80f;
        }
        else
        {
            // 데시벨로 변환 (공식임) 
            db = Mathf.Log10(volume) * 20;
        }

        // BGM그룹으로 설정된 오디오들 볼륨을 조절
        _baseMixer.SetFloat("BGMVolume", db);
    }

    /// <summary>
    /// BGM 볼륨 점점 줄이기
    /// </summary>
    public void StartFadeOutBGM(float duration)
    {
        StartCoroutine(FadeOutBGM(duration));
    }

    /// <summary>
    /// BGM 볼륨 점점 키우기
    /// </summary>
    public void StartFadeInBGM(float duration, float targetVolume)
    {
        StartCoroutine(FadeInBGM(duration, targetVolume));
    }

    /// <summary>
    /// BGM 볼륨 점점 줄이기
    /// </summary>
    private IEnumerator FadeOutBGM(float duration)
    {
        float currentTime = 0;
        float startVolume = 0f;

        // 현재 볼륨 가져오기
        _baseMixer.GetFloat("BGMVolume", out float currentDB);
        startVolume = Mathf.Pow(10, currentDB / 20); // 데시벨에서 0~1 범위로 변환

        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            float newVolume = Mathf.Lerp(startVolume, 0f, currentTime / duration);
            SetBGMVolume(newVolume); // 볼륨 적용
            yield return null;
        }

        SetBGMVolume(0f); // 최종적으로 0으로 설정
    }

    /// <summary>
    /// BGM 볼륨 점점 키우기
    /// </summary>
    private IEnumerator FadeInBGM(float duration, float targetVolume)
    {
        float currentTime = 0;

        // 현재 볼륨 가져오기
        _baseMixer.GetFloat("BGMVolume", out float currentDB);
        float startVolume = Mathf.Pow(10, currentDB / 20); // 데시벨에서 0~1 범위로 변환

        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            float newVolume = Mathf.Lerp(startVolume, targetVolume, currentTime / duration);
            SetBGMVolume(newVolume); // 볼륨 적용
            yield return null;
        }

        SetBGMVolume(targetVolume); // 최종적으로 목표 볼륨으로 설정
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
    /// 전체 SFX들 풀로 다시 리턴
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
    /// 0~1의 매개변수값을 데시벨로 변환 후 sfx에 적용
    /// </summary>
    /// <param name="volume"> volume은 0~1의 값만 지정 </param>
    public void SetSFXVolume(float volume)
    {
        float db;

        if (volume <= 0)
        {
            // 음소거 처리 (AudioMixer의 최소값 (무음))
            db = -80f;
        }
        else
        {
            // 데시벨로 변환 (공식임) 
            db = Mathf.Log10(volume) * 20;
        }

        // SFX그룹으로 설정된 오디오들 볼륨을 조절
        _baseMixer.SetFloat("SFXVolume", db);
    }
    #endregion
}
