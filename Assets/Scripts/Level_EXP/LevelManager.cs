using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

// Ŭ������ �ϴϱ� �ǳ�...?
public class EXP
{
    public int currentEXP;
    public int NextEXP;
    public int Level;

    public EXP(int currentExp, int nextExp, int level)
    {
        currentEXP = currentExp;
        NextEXP = nextExp;
        Level = level;
    }
}

public class LevelManager : SerializedMonoBehaviour
{
    #region �̱���
    private static LevelManager _instance;

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

    public static LevelManager Instance
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


    private LevelData _heroLevelData;

    // ������ ���� EXP
    public EXP HeroExp { get; set; } = new EXP(0, 100, 0);



    // �ٴڿ� ���������� EXP ������Ʈ
    [SerializeField]
    public EXPObject ExpObj { get; set; }

    // UI EXP ��������
    [SerializeField]
    public EXPBar EXPBar { get; set; }

    private void Start()
    {
        // ���� 0 ������ �ʱ⼳��
        _heroLevelData = DataManager.Instance.LevelDatas.LevelDataList[0];





        EXPObject.OnGetEXP += EXPUp;

        MonsterObject.OnMonsterDeath += InstantiateEXPObj;

        EXPBarUpdate();
    }


    // EXP�� ������ ������Ʈ
    private void EXPBarUpdate()
    {
        EXPBar.EXPSliderUpdate(HeroExp);
        EXPBar.LevelTextUpdate(HeroExp);
    }

    // ����ġ ����
    private void EXPUp(EXP exp)
    {
        exp.currentEXP += 10;
        Debug.Log(exp.currentEXP + "   " + exp.NextEXP);

        if (exp.currentEXP >= exp.NextEXP)
        {
            LevelUp(exp);
        }
    }

    // ���� ����
    private void LevelUp(EXP exp)
    {
        exp.Level++;
        EXPBar.LevelTextUpdate(exp);
    }

    // ���� ������ �ٴڿ� EXP ������Ʈ ����
    private void InstantiateEXPObj(MonsterObject monsterObject)
    {
        Instantiate(ExpObj, monsterObject.transform.position, Quaternion.identity);
    }

}
