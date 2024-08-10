using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GraphicResourceManager : SerializedMonoBehaviour
{
    [Title("Hero Images", bold: false)]
    // ����� ��������Ʈ ��ųʸ�
    [SerializeField]
    private Dictionary<HeroID, Sprite> _heroSpriteDict = new Dictionary<HeroID, Sprite>();
    public Dictionary<HeroID, Sprite> HeroSpriteDict { get { return _heroSpriteDict; } }
}
