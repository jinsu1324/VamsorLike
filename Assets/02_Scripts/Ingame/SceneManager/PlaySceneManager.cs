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
    public AchivementManager AchivementManager { get; set; }       // �÷��� ���, ����
    
    [SerializeField]
    public EnemyManager EnemyManager { get; set; }                 // �� �Ŵ���

    [SerializeField]
    public EnemyFactory EnemyFactory { get; set; }                 // �� ���丮

    [SerializeField]
    public EnemySpawner EnemySpawner { get; set; }                 // �� ������

    [SerializeField]
    public WaveManager WaveManager { get; set; }                   // ���̺�Ŵ���

    [SerializeField]
    public SkillManager SkillManager { get; set; }                 // ��ų�Ŵ���

    [SerializeField]
    public EXPManager EXPManager { get; set; }                     // �����Ŵ���

    [SerializeField]
    public GoldManager GoldManager { get; set; }                   // ���Ŵ���

    [SerializeField]
    public ItemManager ItemManager { get; set; }                   // �����۸Ŵ���
        
    /// <summary>
    /// Start �Լ�
    /// </summary>
    private void Start()
    {
        MyHeroObjSetting();
    }

    /// <summary>
    /// �̹����ӿ������� ���õ� ���� ���� �� ����
    /// </summary>
    private void MyHeroObjSetting()
    {
        HeroID myHeroID = GameManager.Instance.myHeroID;

        // ������ ������ �̹������� �������� �Ҵ�
        MyHeroObj = ObjectManager.Instance.HeroObjectDict[myHeroID];

        // �̹��������� ���õ� ������ �ʵ忡 ����
        MyHeroObj = Instantiate(ObjectManager.Instance.HeroObjectDict[myHeroID], new Vector3(0, 0, 0), Quaternion.identity);

        // �̹��������� ���õ� ���� ������ ����
        MyHeroObj.DataSetting();

        // ����ٴ� ī�޶� ����
        Camera.main.GetComponent<CameraFollow>().SetFollowTarget(MyHeroObj);
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
