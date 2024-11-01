using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEditor;
using UnityEngine;

public class Test : SerializedMonoBehaviour
{

    #region ½Ì±ÛÅæ_¾ÀÀÌµ¿ O
    private static Test _instance;

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

    public static Test Instance
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


    //public HeroDatas HeroDatas;


    //public HeroData GetHeroData(HeroID id)
    //{
    //    string searchId = id.ToString();

    //    HeroData heroData = HeroDatas.DataList.Find(hero => hero.Id == searchId);
    //    return heroData;
    //}
}


