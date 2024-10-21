using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ���ҽ� �Ŵ���
/// </summary>
public class ResourceManager : SerializedMonoBehaviour
{
    #region �̱���
    private static ResourceManager _instance;

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

    public static ResourceManager Instance
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

    // ��ų ������ ��ųʸ�
    [SerializeField]
    private Dictionary<SkillID, Sprite> _skillIconDict = new Dictionary<SkillID, Sprite>();


    /// <summary>
    /// ��ų ������ �������� �Լ�
    /// </summary>
    public Sprite GetSkillIcon(SkillID skillID)
    {
        return _skillIconDict[skillID];
    }
}
