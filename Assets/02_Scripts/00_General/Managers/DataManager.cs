using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : SerializedMonoBehaviour
{
    #region �̱���_���̵� O
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
    public HeroDatas HeroDatas { get; set; }                           // ���� ������

    [SerializeField]
    public LevelDatas LevelDatas { get; set; }                         // ���� ������

    [SerializeField]
    public WaveDatas WaveDatas { get; set; }                           // ���̺� ������

    [SerializeField]
    public MonsterDatas MonsterDatas { get; set; }                     // ���� ������

    [SerializeField]
    public BossDatas BossDatas { get; set; }                            // ���� ������

    [SerializeField]
    public SkillDatas_SlashAttack SkillDatas_SlashAttack { get; set; } // ������ ���� ��ų ������

    [SerializeField]
    public SkillDatas_Boomerang SkillDatas_Boomerang { get; set; }     // �θ޶� ��ų ������

    [SerializeField]
    public SkillDatas_Sniper SkillDatas_Sniper { get; set; }           // �������� ��ų ������




    private void Start()
    {
        //Debug.Log(_heroDatas.GetDataById(HeroID.Wizard.ToString()).Desc);
    }
}
