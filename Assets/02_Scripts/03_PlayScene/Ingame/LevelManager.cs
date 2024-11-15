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
    public HeroLvExp MyHeroLvExp { get; set; } = new HeroLvExp(0, 1);   // �̹����� ���� ���� ����ġ

    private Queue<int> _levelUpQueue = new Queue<int>();                // ������ ó�� ������ Queue
    private bool _isSkillSelectPopupOpen = false;                       // �˾� ���ȴ��� Ȯ���� ����

    /// <summary>
    /// Start �Լ�
    /// </summary>
    private void Start()
    {
        PlaySceneCanvas.Instance.EXPBarUI.Update_EXPBarInfos();
    }

    /// <summary>
    /// ����ġ�� ȹ���� �� ȣ��Ǵ� �Լ�
    /// </summary>
    public void EXPUp(int amount)
    {
        MyHeroLvExp.EXP += amount;
        PlaySceneCanvas.Instance.EXPBarUI.Update_EXPBarInfos();

        CheckLevelUp();
    }

    /// <summary>
    /// ����ġ�� ���� ������ ������ ������ ó��
    /// </summary>
    private void CheckLevelUp()
    {
        // ������ �Ҽ������� ��� �ݺ��ؼ� ������ �� Queue�� ����
        LevelData currentLevelData = DataManager.Instance.LevelDatas.GetDataById(MyHeroLvExp.Level.ToString());
        while (MyHeroLvExp.EXP >= currentLevelData.MaxExp)
        {
            MyHeroLvExp.Level++;
            MyHeroLvExp.EXP -= currentLevelData.MaxExp;
            _levelUpQueue.Enqueue(MyHeroLvExp.Level);

            currentLevelData = DataManager.Instance.LevelDatas.GetDataById(MyHeroLvExp.Level.ToString());
        }

        // ������ �˾��� ���� ���� �ʾҴٸ� ť ó�� ����
        if (_isSkillSelectPopupOpen == false && _levelUpQueue.Count > 0)
        {
            StartCoroutine(ProcessLevelUpQueue());
        }
    }

    /// <summary>
    /// ������ ť ó�� �ڷ�ƾ
    /// </summary>
    private IEnumerator ProcessLevelUpQueue()
    {
        // Queue Count�� 0�� �ɶ����� �ݺ�
        while (_levelUpQueue.Count > 0)
        {
            _isSkillSelectPopupOpen = true;
            int level = _levelUpQueue.Dequeue();

            // �˾� ����
            PlaySceneCanvas.Instance.SkillSelectPopup.OpenPopup();

            // �־��� ������ true�϶����� ��ٸ� (�˾��� �����⸦ ��ٸ�)
            yield return new WaitUntil(() => _isSkillSelectPopupOpen == false);
        }
    }

    /// <summary>
    /// �˾��� ���� �� IsSkillSelectPopupOpen�� false�� ������ִ� �Լ�
    /// </summary>
    public void ChangeFalse_IsSkillSelectPopupOpen()
    {
        _isSkillSelectPopupOpen = false;
    }










    // ġƮ
    public void OnClickExpUpCheatButton()
    {
        EXPUp(100);
        PlaySceneCanvas.Instance.EXPBarUI.Update_EXPBarInfos();
    }
}
