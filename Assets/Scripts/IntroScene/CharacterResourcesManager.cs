using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CharacterResourcesManager : MonoBehaviour
{
    #region ½Ì±ÛÅæ ÀÏ¹Ý¹æ½Ä
    private static CharacterResourcesManager _instance;

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

    public static CharacterResourcesManager Instance
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

    [SerializeField]
    private List<Sprite> _hairList = new List<Sprite>();
    public List<Sprite> HairList { get { return _hairList; }}

    [SerializeField]
    private List<Sprite> _faceList = new List<Sprite>();
    public List<Sprite> FaceList { get { return _faceList; } }

    [SerializeField]
    private List<Sprite> _costumeList = new List<Sprite>();
    public List<Sprite> CostumeList { get { return _costumeList; } }
}
