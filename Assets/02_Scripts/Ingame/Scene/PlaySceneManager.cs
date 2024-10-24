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

    public HeroObj MyHeroObj { get; set; }                       // ���� �̹� ���ӿ� ������ ����
    public bool IsGameStart { get; set; }                           // ���� ���� �Ǿ�����

    [SerializeField]
    public PlaySceneCanvas PlaySceneCanvas { get; set; }            // �÷��̾� ĵ����
    
    [SerializeField]
    public PlayAchivement PlayAchivement { get; set; }              // �÷��� ���, ����

    public int StageLevel { get; set; } = 1;                        // �������� ���� 
    public int MaxStageLevel { get; set; } = 4;                     // �ִ� �������� ����  
    public float StageLevelUpIntervelTime { get; set; } = 10.0f;    // �������� ������ ����

    private float _playTime = 0.0f;                                 // �÷���Ÿ��
    
   
    /// <summary>
    /// Start �Լ�
    /// </summary>
    private void Start()
    {
        MyHeroObjSetting();
    }

    /// <summary>
    /// Update �Լ�
    /// </summary>
    private void Update()
    {        
        PlayTimeCalculate_UIRefresh();
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
    /// �÷���Ÿ�� ��� + ����
    /// </summary>
    private void PlayTimeCalculate_UIRefresh()
    {
        if (IsGameStart == false)
            return;

        _playTime += Time.deltaTime;

        int minute = Mathf.FloorToInt(_playTime / 60F);
        int second = Mathf.FloorToInt(_playTime % 60F);

        PlaySceneCanvas.PlayTimeUI.RefreshUIText(minute, second);

        // �÷���Ÿ�� üũ�ؼ� �������� ������
        if (_playTime >= StageLevel * StageLevelUpIntervelTime)
            ChangeStageLevel();
    }

    /// <summary>
    /// ���ӽ��� bool �Ķ���ͷ� ����
    /// </summary>
    public void IsGameStartChange(bool state)
    {        
        IsGameStart = state;
    }    

    /// <summary>
    /// �������� ������
    /// </summary>
    public void ChangeStageLevel()
    {
        if (StageLevel >= MaxStageLevel)
        {
            Debug.Log("�ִ� �������� �����Դϴ�.");
            return;
        }

        StageLevel++;
        Debug.Log($"�������� ������! : {StageLevel}");

        if (StageLevel == 2)
        {
            EnemySpawner.Instance.BossSpawn();
        }        
    }


    public void DeadCheat()
    {
        MyHeroObj.Death();
    }
}
