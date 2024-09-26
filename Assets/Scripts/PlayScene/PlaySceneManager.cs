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

    // ���� �̹� ���ӿ� ������ ����
    public static HeroObject ThisGameHeroObject { get; set; }

    // ���� �����ؼ� ���� ���� �Ǿ�����
    public bool IsGameStart { get; set; }

    // ���� ���� �˾�
    [SerializeField]
    public HeroSelectPopup HeroSelectPopup { get; set; }

    // ��ų ���� �˾�
    [SerializeField]
    public SkillChoicePopup SkillPopupUI { get; set; }

    // HP��
    [SerializeField]
    public HPBar HPBar { get; set; }    

    // ���� UI ĵ����
    [SerializeField]
    public Transform GuageBarsTF { get; set; }

    // ������ �ؽ�Ʈ UI ������Ʈ Ǯ
    [SerializeField]
    public ObjectPool DamageTextUIPool { get; set; }

    private void Start()
    {
        ScenePopupsInitialize();
    }

    // �� �˾��� ON OFF �ʱ�ȭ
    private void ScenePopupsInitialize()
    {
        HeroSelectPopup.OpenPopup();
        SkillPopupUI.CloseSkillPopup();
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
