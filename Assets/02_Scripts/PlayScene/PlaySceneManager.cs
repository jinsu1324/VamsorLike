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
    public HeroSelectPopup HeroSelectPopupUI { get; set; }      // ���� ���� �˾�    

    [SerializeField]
    public SkillChoicePopup SkillChoicePopupUI { get; set; }    // ��ų ���� �˾�
    
    [SerializeField]
    public HPBar HPBarPrefab { get; set; }                      // HP��
    
    [SerializeField]
    public Transform HpBarParentTransform { get; set; }         // HP�� �θ� Ʈ������

    [SerializeField]
    public ObjectPool DamageTextUIPool { get; set; }            // ������ �ؽ�Ʈ UI ������Ʈ Ǯ
    
    [SerializeField]
    public Joystick JoystickUI { get; set; }                    // ���̽�ƽUI

    private void Start()
    {
        ScenePopupsInitialize();
    }

    /// <summary>
    /// �� �˾��� ON OFF �ʱ�ȭ
    /// </summary>
    private void ScenePopupsInitialize()
    {
        HeroSelectPopupUI.OpenPopup();
        SkillChoicePopupUI.CloseSkillPopup();
    }

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
