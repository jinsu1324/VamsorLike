using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CharacterResourcesManager : SerializedMonoBehaviour
{
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
