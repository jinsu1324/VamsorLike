using Cinemachine;
using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class LobbyHero : SerializedMonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField]
    protected HeroID _heroID;                         // 이 영웅 ID
    public HeroData BaseHeroData { get; set; }        // 영웅 정보 데이터

    protected LobbyVirtualCamera _virtualCamera;      // 버추얼 카메라 받아올 변수
    protected Material _outlineMaterial;              // 아웃라인 마테리얼 담을 변수 
    protected Animator _animator;                     // 애니메이터 담을 변수
    protected Transform _cameraSnapPos;               // 카메라 스냅할 위치

    /// <summary>
    /// Start
    /// </summary>
    private void Start()
    {
        BaseHeroData = DataManager.Instance.HeroDatas.GetDataById(_heroID.ToString());

        _virtualCamera = FindObjectOfType<LobbyVirtualCamera>();
        _outlineMaterial = GetComponent<SpriteRenderer>().material;
        _animator = GetComponent<Animator>();
        _cameraSnapPos = transform.Find("CameraSnapPos_NameStringUse");
    }

    /// <summary>
    /// 클릭하면 실행되는 이벤트
    /// </summary>
    public void OnPointerClick(PointerEventData eventData)
    {
        // 영웅 선택 상태일때 호출되는 함수
        LobbySceneManager.Instance.Change_HeroSelectState(this, eventData);

    }

    /// <summary>
    /// 마우스가 올라가면 실행되는 이벤트
    /// </summary>
    public void OnPointerEnter(PointerEventData eventData)
    {
        OutlineMaterialONOFF("_OutlineThickness", 1.0f);
    }

    /// <summary>
    /// 마우스가 벗어나면 실행되는 이벤트
    /// </summary>
    public void OnPointerExit(PointerEventData eventData)
    {
        OutlineMaterialONOFF("_OutlineThickness", 0.0f);
    }

    /// <summary>
    /// 준비 애니메이션 실행 (공격한번 휘두르기)
    /// </summary>
    public void PlayAnimation_Ready()
    {
        _animator.SetTrigger("Ready");
    }

    /// <summary>
    /// 아웃라인 마테리얼 ON/OFF (value가 1이면 ON / 0 이면 OFF)
    /// </summary>
    public void OutlineMaterialONOFF(string paramName, float value)
    {
        _outlineMaterial.SetFloat(paramName, value);
    }
}
