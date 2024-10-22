using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySceneCanvas : SerializedMonoBehaviour
{
    [Title("Popups")]
    [SerializeField]
    public SkillChoicePopup SkillChoicePopupUI { get; set; }    // ��ų ���� �˾�    

    [Title("Views")]
    [SerializeField]
    public ObjectPool DamageTextsPool { get; set; }            // ������ �ؽ�Ʈ UI ������Ʈ Ǯ

    [SerializeField]
    public PlayTimeUI PlayTimeUI { get; set; }                  // �÷��� Ÿ�� UI

    [Title("Interactions")]
    [SerializeField]
    public Joystick JoystickUI { get; set; }                    // ���̽�ƽ


    /// <summary>
    /// Start �Լ�
    /// </summary>
    private void Start()
    {
        ScenePopupsInitialize();
    }

    /// <summary>
    /// �� �˾��� ON OFF �ʱ�ȭ
    /// </summary>
    private void ScenePopupsInitialize()
    {
        SkillChoicePopupUI.OpenSkillPopup();
    }
}
