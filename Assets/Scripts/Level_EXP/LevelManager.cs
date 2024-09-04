using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

// 클래스로 하니까 되네...?
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


    private LevelData _heroLevelData;

    // 마스터 영웅 EXP
    public EXP HeroExp { get; set; } = new EXP(0, 100, 0);



    // 바닥에 떨어져있을 EXP 오브젝트
    [SerializeField]
    public EXPObject ExpObj { get; set; }

    // UI EXP 게이지바
    [SerializeField]
    public EXPBar EXPBar { get; set; }

    private void Start()
    {
        // 레벨 0 값으로 초기설정
        _heroLevelData = DataManager.Instance.LevelDatas.LevelDataList[0];





        EXPObject.OnGetEXP += EXPUp;

        MonsterObject.OnMonsterDeath += InstantiateEXPObj;

        EXPBarUpdate();
    }


    // EXP바 정보들 업데이트
    private void EXPBarUpdate()
    {
        EXPBar.EXPSliderUpdate(HeroExp);
        EXPBar.LevelTextUpdate(HeroExp);
    }

    // 경험치 증가
    private void EXPUp(EXP exp)
    {
        exp.currentEXP += 10;
        Debug.Log(exp.currentEXP + "   " + exp.NextEXP);

        if (exp.currentEXP >= exp.NextEXP)
        {
            LevelUp(exp);
        }
    }

    // 레벨 증가
    private void LevelUp(EXP exp)
    {
        exp.Level++;
        EXPBar.LevelTextUpdate(exp);
    }

    // 몬스터 죽으면 바닥에 EXP 오브젝트 생성
    private void InstantiateEXPObj(MonsterObject monsterObject)
    {
        Instantiate(ExpObj, monsterObject.transform.position, Quaternion.identity);
    }

}
