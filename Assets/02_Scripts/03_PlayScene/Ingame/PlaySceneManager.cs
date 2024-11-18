using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// PlayScene�� �ʿ��� �۷ι��� �͵� ������ : �̹����ӿ� �÷����ϰ��ִ� ���� / ���ӽ��� ���� / ���ӽ��� / �������� �˾� 
public class PlaySceneManager : SerializedMonoBehaviour
{
    #region �̱���_���̵�x
    private static PlaySceneManager _instance;

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            //DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    public static PlaySceneManager Instance
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

    public HeroObj MyHeroObj { get; set; }                          // ���� �̹� ���ӿ� ������ ����
    public bool IsGameStart { get; set; }                           // ���� ���� �Ǿ�����

    [Title("Managers")]
    [SerializeField]
    public EnemySpawner EnemySpawner { get; set; }                 // �� ������

    [SerializeField]
    public ItemSpawner ItemSpawner { get; set; }                   // ������ ������

    [SerializeField]
    public EnemyManager EnemyManager { get; set; }                 // �� �Ŵ���

    [SerializeField]
    public ItemManager ItemManager { get; set; }                   // ������ �Ŵ���

    [SerializeField]
    public WaveManager WaveManager { get; set; }                   // ���̺�Ŵ���

    [SerializeField]
    public SkillManager SkillManager { get; set; }                 // ��ų�Ŵ���

    [SerializeField]
    public LevelManager LevelManager { get; set; }                 // �����Ŵ���

    [SerializeField]
    public EffectManager EffectManager { get; set; }               // ����Ʈ�Ŵ���

    [SerializeField]
    public InfiniteMapController InfiniteMapController { get; set; }// ���Ѹ� ��Ʈ�ѷ�


    /// <summary>
    /// Start �Լ�
    /// </summary>
    private void Start()
    {
        // ���õ� ���� ����
        MyHeroObjSetting();

        // ���ӽ��� true ��
        IsGameStartChange(true);

        // ���̵� �ƿ�
        PlaySceneCanvas.Instance.FadeInOutController.FadeOut();

        // ����� ���
        AudioManager.Instance.PlayBGM(BGMType.PlayScnene);
        AudioManager.Instance.StartFadeInBGM(1f, 0.5f);
        AudioManager.Instance.SetSFXVolume(0.5f);
    }

    /// <summary>
    /// �̹����ӿ������� ���õ� ���� ���� �� ����
    /// </summary>
    private void MyHeroObjSetting()
    {
        // ���� �̹� ���ӿ� ������ ���� ID �޾ƿ�
        HeroID myHeroID = GameManager.Instance.myHeroID;

        // ������ ������ �̹������� �������� �Ҵ�
        MyHeroObj = ResourceManager.Instance.HeroObjectDict[myHeroID];

        // �̹��������� ���õ� ������ �ʵ忡 ����
        MyHeroObj = Instantiate(ResourceManager.Instance.HeroObjectDict[myHeroID], new Vector3(0, 0, 0), Quaternion.identity);

        // �̹��������� ���õ� ���� ������ ����
        MyHeroObj.DataSetting();

        // ����ٴ� ī�޶� ����
        Camera.main.GetComponent<CameraFollow>().SetFollowTarget(MyHeroObj);

        // �� �̴�
        InfiniteMapController.InitMap();
    }    

    /// <summary>
    /// ���ӽ��� bool �Ķ���ͷ� ����
    /// </summary>
    public void IsGameStartChange(bool state)
    {        
        IsGameStart = state;
    }  







    // ġƮ
    public void DeadCheat()
    {
        MyHeroObj.Death();
    }
}
