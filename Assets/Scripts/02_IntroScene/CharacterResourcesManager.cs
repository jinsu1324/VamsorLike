using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CharacterResourcesManager : SerializedMonoBehaviour
{
    [SerializeField]
    public List<Sprite> HairList { get; set; }

    [SerializeField]
    public List<Sprite> FaceList { get; set; }

    [SerializeField]
    public List<Sprite> CostumeList { get; set; }
}
