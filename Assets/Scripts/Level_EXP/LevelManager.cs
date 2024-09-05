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
    #region 싱글톤
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

    // 이번게임 영웅 레벨 경험치 데이터
    public ThisGameHeroLevelData ThisGameHeroLevelData { get; set; } = new ThisGameHeroLevelData(0, 0);

    // 바닥에 떨어져있을 EXP 오브젝트
    [SerializeField]
    public EXPObject ExpObj { get; set; }

    // UI EXP 게이지바
    [SerializeField]
    public EXPBar EXPBar { get; set; }


    private void Start()
    {
        EXPBar.UpdateEXPBarInfos();

        MonsterObject.OnMonsterDeath += InstantiateEXPObj;
        EXPObject.OnGetExp += EXPUp;

    }


    // 경험치 증가
    public void EXPUp()
    {
        ThisGameHeroLevelData.CurrentEXP += 10;

        if (ThisGameHeroLevelData.CurrentEXP >= DataManager.Instance.LevelDatas.LevelDataList[ThisGameHeroLevelData.CurrentLevel].MaxExp)
        {
            LevelUp();
        }
    }


    // 레벨 증가
    private void LevelUp()
    {
        ThisGameHeroLevelData.CurrentLevel++;
        ThisGameHeroLevelData.CurrentEXP = 0; 

        EXPBar.UpdateEXPBarInfos();
    }


    // 몬스터 죽으면 바닥에 EXP 오브젝트 생성
    private void InstantiateEXPObj(MonsterObject monsterObject)
    {
        Instantiate(ExpObj, monsterObject.transform.position, Quaternion.identity);
    }

}
