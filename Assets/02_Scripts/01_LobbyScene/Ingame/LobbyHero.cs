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
    protected HeroID _heroID;                         // �� ���� ID
    public HeroData BaseHeroData { get; set; }        // ���� ���� ������

    protected LobbyVirtualCamera _virtualCamera;      // ���߾� ī�޶� �޾ƿ� ����
    protected Material _outlineMaterial;              // �ƿ����� ���׸��� ���� ���� 
    protected Animator _animator;                     // �ִϸ����� ���� ����
    protected Transform _cameraSnapPos;               // ī�޶� ������ ��ġ

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
    /// Ŭ���ϸ� ����Ǵ� �̺�Ʈ
    /// </summary>
    public void OnPointerClick(PointerEventData eventData)
    {
        // ���� ���� �����϶� ȣ��Ǵ� �Լ�
        LobbySceneManager.Instance.Change_HeroSelectState(this, eventData);

    }

    /// <summary>
    /// ���콺�� �ö󰡸� ����Ǵ� �̺�Ʈ
    /// </summary>
    public void OnPointerEnter(PointerEventData eventData)
    {
        OutlineMaterialONOFF("_OutlineThickness", 1.0f);
    }

    /// <summary>
    /// ���콺�� ����� ����Ǵ� �̺�Ʈ
    /// </summary>
    public void OnPointerExit(PointerEventData eventData)
    {
        OutlineMaterialONOFF("_OutlineThickness", 0.0f);
    }

    /// <summary>
    /// �غ� �ִϸ��̼� ���� (�����ѹ� �ֵθ���)
    /// </summary>
    public void PlayAnimation_Ready()
    {
        _animator.SetTrigger("Ready");
    }

    /// <summary>
    /// �ƿ����� ���׸��� ON/OFF (value�� 1�̸� ON / 0 �̸� OFF)
    /// </summary>
    public void OutlineMaterialONOFF(string paramName, float value)
    {
        _outlineMaterial.SetFloat(paramName, value);
    }
}
