using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroDataManager : SerializedMonoBehaviour
{
    // ������Ʈ�� ����� ScriptableObject�� ��� �޾ƿͼ� ������ ��ųʸ�
    [SerializeField]
    private Dictionary<HeroID, HeroData> _heroDataDict = new Dictionary<HeroID, HeroData>();
    public Dictionary<HeroID, HeroData> HeroDataDict { get { return _heroDataDict; } set { _heroDataDict = value; } }
}
