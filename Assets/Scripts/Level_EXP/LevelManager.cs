using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ThisGameHeroLevelData
{
    public int CurrentEXP;
    public int CurrentLevel;

    public ThisGameHeroLevelData(int currentExp, int currentLevel)
    {
        CurrentEXP = currentExp;
        CurrentLevel = currentLevel;
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

    // �̹����� ���� ���� ����ġ ������
    public ThisGameHeroLevelData ThisGameHeroLevelData { get; set; } = new ThisGameHeroLevelData(0, 0);

    // �ٴڿ� ���������� EXP ������Ʈ
    [SerializeField]
    public EXPObject ExpObj { get; set; }

    // UI EXP ��������
    [SerializeField]
    public EXPBar EXPBar { get; set; }


    private void Start()
    {
        EXPBar.UpdateEXPBarInfos();

        MonsterObject.OnMonsterDeath += InstantiateEXPObj;
        EXPObject.OnGetExp += EXPUp;

    }


    // ����ġ ����
    public void EXPUp()
    {
        ThisGameHeroLevelData.CurrentEXP += 10;

        if (ThisGameHeroLevelData.CurrentEXP >= DataManager.Instance.LevelDatas.LevelDataList[ThisGameHeroLevelData.CurrentLevel].MaxExp)
        {
            LevelUp();
        }
    }


    // ���� ����
    private void LevelUp()
    {
        ThisGameHeroLevelData.CurrentLevel++;
        ThisGameHeroLevelData.CurrentEXP = 0; 

        EXPBar.UpdateEXPBarInfos();
    }


    // ���� ������ �ٴڿ� EXP ������Ʈ ����
    private void InstantiateEXPObj(MonsterObject monsterObject)
    {
        Instantiate(ExpObj, monsterObject.transform.position, Quaternion.identity);
    }

}
