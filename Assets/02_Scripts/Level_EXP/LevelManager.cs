using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HeroLvExp
{
    public int EXP;
    public int Level;

    public HeroLvExp(int exp, int level)
    {
        EXP = exp;
        Level = level;
    }
}

public class LevelManager : SerializedMonoBehaviour
{
    #region 싱글톤_씬이동x
    private static LevelManager _instance;

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

    // 이번게임 영웅 레벨 경험치
    public HeroLvExp MyHeroLvExp { get; set; } = new HeroLvExp(0, 0);

    // 바닥에 떨어져있을 EXP 오브젝트
    [SerializeField]
    public EXPObject ExpObj { get; set; }

    // UI EXP 게이지바
    [SerializeField]
    public EXPBar EXPBar { get; set; }

    /// <summary>
    /// Start 함수
    /// </summary>
    private void Start()
    {
        EXPBar.Update_EXPBarInfos();

        MonsterObject.OnMonsterDeath += InstantiateEXPObj;
        EXPObject.OnGetExp += EXPUp;
    }

    /// <summary>
    /// 경험치 증가
    /// </summary>
    public void EXPUp()
    {
        MyHeroLvExp.EXP += 10;

        List<LevelData> levelDataList = DataManager.Instance.LevelDatas.LevelDataList;

        if (MyHeroLvExp.EXP >= levelDataList[MyHeroLvExp.Level].MaxExp)
        {
            LevelUp();
        }
    }

    /// <summary>
    /// 레벨 증가
    /// </summary>
    private void LevelUp()
    {
        MyHeroLvExp.Level++;
        MyHeroLvExp.EXP = 0; 

        EXPBar.Update_EXPBarInfos();

        PlaySceneManager.Instance.PlaySceneCanvas.SkillChoicePopupUI.OpenPopup();
        PlaySceneManager.Instance.IsGameStartChange(false);
    }

    /// <summary>
    /// 몬스터 죽으면 바닥에 EXP 오브젝트 생성
    /// </summary>
    private void InstantiateEXPObj(MonsterObject monsterObject)
    {
        Instantiate(ExpObj, monsterObject.transform.position, Quaternion.identity);
    }   

    /// <summary>
    /// 씬 전환되거나 오브젝트 파괴될 때 이벤트 제거
    /// </summary>
    public void OnDisable()
    {
        MonsterObject.OnMonsterDeath -= InstantiateEXPObj;
        EXPObject.OnGetExp -= EXPUp;
    }




    // 치트
    public void OnClickExpUpCheatButton()
    {
        EXPUp();
        EXPBar.Update_EXPBarInfos();
    }
}
