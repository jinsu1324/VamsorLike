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
    public HeroLvExp MyHeroLvExp { get; set; } = new HeroLvExp(0, 1);   // 이번게임 영웅 레벨 경험치

    private Queue<int> _levelUpQueue = new Queue<int>();                // 레벨업 처리 누적할 Queue
    private bool _isSkillSelectPopupOpen = false;                       // 팝업 열렸는지 확인할 변수

    /// <summary>
    /// Start 함수
    /// </summary>
    private void Start()
    {
        PlaySceneCanvas.Instance.EXPBarUI.Update_EXPBarInfos();
    }

    /// <summary>
    /// 경험치를 획득할 때 호출되는 함수
    /// </summary>
    public void EXPUp(int amount)
    {
        MyHeroLvExp.EXP += amount;
        PlaySceneCanvas.Instance.EXPBarUI.Update_EXPBarInfos();

        CheckLevelUp();
    }

    /// <summary>
    /// 경험치가 일정 기준을 넘으면 레벨업 처리
    /// </summary>
    private void CheckLevelUp()
    {
        // 레벨업 할수있으면 계속 반복해서 레벨업 후 Queue에 넣음
        LevelData currentLevelData = DataManager.Instance.LevelDatas.GetDataById(MyHeroLvExp.Level.ToString());
        while (MyHeroLvExp.EXP >= currentLevelData.MaxExp)
        {
            MyHeroLvExp.Level++;
            MyHeroLvExp.EXP -= currentLevelData.MaxExp;
            _levelUpQueue.Enqueue(MyHeroLvExp.Level);

            currentLevelData = DataManager.Instance.LevelDatas.GetDataById(MyHeroLvExp.Level.ToString());
        }

        // 레벨업 팝업을 아직 열지 않았다면 큐 처리 시작
        if (_isSkillSelectPopupOpen == false && _levelUpQueue.Count > 0)
        {
            StartCoroutine(ProcessLevelUpQueue());
        }
    }

    /// <summary>
    /// 레벨업 큐 처리 코루틴
    /// </summary>
    private IEnumerator ProcessLevelUpQueue()
    {
        // Queue Count가 0이 될때까지 반복
        while (_levelUpQueue.Count > 0)
        {
            _isSkillSelectPopupOpen = true;
            int level = _levelUpQueue.Dequeue();

            // 팝업 열기
            PlaySceneCanvas.Instance.SkillSelectPopup.OpenPopup();

            // 주어진 조건이 true일때까지 기다림 (팝업이 닫히기를 기다림)
            yield return new WaitUntil(() => _isSkillSelectPopupOpen == false);
        }
    }

    /// <summary>
    /// 팝업이 닫힐 때 IsSkillSelectPopupOpen을 false로 만들어주는 함수
    /// </summary>
    public void ChangeFalse_IsSkillSelectPopupOpen()
    {
        _isSkillSelectPopupOpen = false;
    }










    // 치트
    public void OnClickExpUpCheatButton()
    {
        EXPUp(100);
        PlaySceneCanvas.Instance.EXPBarUI.Update_EXPBarInfos();
    }
}
