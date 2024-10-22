using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    #region �̱���_���̵� O
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

    public HeroID myHeroID { get; set; }              // ���� �̹� ���ӿ� ������ ���� ID


    /// <summary>
    /// �̹����ӿ������� ���õ� ���� ���� �� ����
    /// </summary>
    public void MyHeroIDSetting(HeroID selectHeroID)
    {
        myHeroID = selectHeroID;
    }
}
