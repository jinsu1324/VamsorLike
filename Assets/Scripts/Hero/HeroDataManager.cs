using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroDataManager : SerializedMonoBehaviour
{
    // ������Ʈ�� ���� ScriptableObject�� ��� �޾ƿͼ� ������ ��ųʸ�
    [SerializeField]
    private Dictionary<HeroID, HeroData> _heroDataDict = new Dictionary<HeroID, HeroData>();
    public Dictionary<HeroID, HeroData> HeroDataDict { get { return _heroDataDict; } set { _heroDataDict = value; } }

    // ���� �����յ� ��ųʸ�
    [SerializeField]
    private Dictionary<HeroID, HeroObject> _heroObjectDict = new Dictionary<HeroID, HeroObject>();
    public Dictionary<HeroID, HeroObject> HeroObjectDict { get { return _heroObjectDict; } set { _heroObjectDict = value; } }
}
