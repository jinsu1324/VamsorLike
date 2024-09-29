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

    // �� �˾��� ON OFF �ʱ�ȭ
    private void ScenePopupsInitialize()
    {
        HeroSelectPopupUI.OpenPopup();
        SkillChoicePopupUI.CloseSkillPopup();
    }

    // ���ӽ��� bool �Ķ���ͷ� ����
    public void IsGameStartChange(bool state)
    {        
        IsGameStart = state;
    }

    // �̹����ӿ������� ���õ� ���� ���� �� ����
    public void ThisGameHeroSetting(HeroID selectHeroID)
    {
        // ������ ������ �̹������� �������� �Ҵ�
        ThisGameHeroObject = HeroManager.Instance.HeroObjectDict[selectHeroID];

        // �̹��������� ���õ� ������ �ʵ忡 ����
        ThisGameHeroObject = Instantiate(HeroManager.Instance.HeroObjectDict[selectHeroID]);

        // �̹��������� ���õ� ���� ������ ����
        ThisGameHeroObject.DataSetting();
    }
}
