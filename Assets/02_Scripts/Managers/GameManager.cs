using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    #region 싱글톤_씬이동 O
    private static GameManager _instance;

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

    public static GameManager Instance
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

    public HeroID myHeroID { get; set; }              // 내가 이번 게임에 선택한 영웅 ID


    /// <summary>
    /// 이번게임영웅으로 선택된 영웅 셋팅 및 스폰
    /// </summary>
    public void MyHeroIDSetting(HeroID selectHeroID)
    {
        myHeroID = selectHeroID;
    }
}
