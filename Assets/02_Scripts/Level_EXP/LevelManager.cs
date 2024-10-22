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
    #region �̱���_���̵�x
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

    // �̹����� ���� ���� ����ġ
    public HeroLvExp MyHeroLvExp { get; set; } = new HeroLvExp(0, 0);

    // �ٴڿ� ���������� EXP ������Ʈ
    [SerializeField]
    public EXPObject ExpObj { get; set; }

    // UI EXP ��������
    [SerializeField]
    public EXPBar EXPBar { get; set; }

    /// <summary>
    /// Start �Լ�
    /// </summary>
    private void Start()
    {
        EXPBar.Update_EXPBarInfos();

        MonsterObject.OnMonsterDeath += InstantiateEXPObj;
        EXPObject.OnGetExp += EXPUp;
    }

    /// <summary>
    /// ����ġ ����
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
    /// ���� ����
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
    /// ���� ������ �ٴڿ� EXP ������Ʈ ����
    /// </summary>
    private void InstantiateEXPObj(MonsterObject monsterObject)
    {
        Instantiate(ExpObj, monsterObject.transform.position, Quaternion.identity);
    }   

    /// <summary>
    /// �� ��ȯ�ǰų� ������Ʈ �ı��� �� �̺�Ʈ ����
    /// </summary>
    public void OnDisable()
    {
        MonsterObject.OnMonsterDeath -= InstantiateEXPObj;
        EXPObject.OnGetExp -= EXPUp;
    }




    // ġƮ
    public void OnClickExpUpCheatButton()
    {
        EXPUp();
        EXPBar.Update_EXPBarInfos();
    }
}
