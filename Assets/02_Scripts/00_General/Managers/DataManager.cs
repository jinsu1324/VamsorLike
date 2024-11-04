using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : SerializedMonoBehaviour
{
    #region 싱글톤_씬이동 O
    private static DataManager _instance;

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

    public static DataManager Instance
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
    public HeroDatas HeroDatas { get; set; }                           // 영웅 데이터

    [SerializeField]
    public LevelDatas LevelDatas { get; set; }                         // 레벨 데이터

    [SerializeField]
    public WaveDatas WaveDatas { get; set; }                           // 웨이브 데이터

    [SerializeField]
    public MonsterDatas MonsterDatas { get; set; }                     // 몬스터 데이터

    [SerializeField]
    public BossDatas BossDatas { get; set; }                           // 보스 데이터

    [SerializeField]
    public SkillDatas SkillDatas { get; set; }                         // 스킬 데이터
}


