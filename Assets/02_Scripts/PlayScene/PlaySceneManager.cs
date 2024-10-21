using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// PlayScene�� �ʿ��� �۷ι��� �͵� ������ : �̹����ӿ� �÷����ϰ��ִ� ���� / ���ӽ��� ���� / ���ӽ��� / �������� �˾� 
public class PlaySceneManager : SerializedMonoBehaviour
{
    #region �̱���
    private static PlaySceneManager _instance;

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

    public static PlaySceneManager Instance
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

    public static HeroObject ThisGameHeroObject { get; set; }   // ���� �̹� ���ӿ� ������ ����
    public bool IsGameStart { get; set; }                       // ���� �����ؼ� ���� ���� �Ǿ�����

    [SerializeField]
    public PlaySceneCanvas PlaySceneCanvas { get; set; }      // �÷��̾� ĵ���� 


    /// <summary>
    /// ���ӽ��� bool �Ķ���ͷ� ����
    /// </summary>
    public void IsGameStartChange(bool state)
    {        
        IsGameStart = state;
    }

    /// <summary>
    /// �̹����ӿ������� ���õ� ���� ���� �� ����
    /// </summary>
    public void ThisGameHeroSetting(HeroID selectHeroID)
    {
        // ������ ������ �̹������� �������� �Ҵ�
        ThisGameHeroObject = HeroManager.Instance.HeroObjectDict[selectHeroID];

        // �̹��������� ���õ� ������ �ʵ忡 ����
        ThisGameHeroObject = Instantiate(HeroManager.Instance.HeroObjectDict[selectHeroID], new Vector3(0, 0, 0), Quaternion.identity);

        // �̹��������� ���õ� ���� ������ ����
        ThisGameHeroObject.DataSetting();
    }
}
