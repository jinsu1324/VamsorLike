using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 리소스 매니저
/// </summary>
public class ResourceManager : SerializedMonoBehaviour
{
    #region 싱글톤
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

    // 스킬 아이콘 딕셔너리
    [SerializeField]
    private Dictionary<SkillID, Sprite> _skillIconDict = new Dictionary<SkillID, Sprite>();


    /// <summary>
    /// 스킬 아이콘 가져오는 함수
    /// </summary>
    public Sprite GetSkillIcon(SkillID skillID)
    {
        return _skillIconDict[skillID];
    }
}
