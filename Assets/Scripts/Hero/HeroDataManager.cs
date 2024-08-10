using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroDataManager : SerializedMonoBehaviour
{
    // 프로젝트의 히어로 ScriptableObject를 모두 받아와서 저장할 딕셔너리
    [SerializeField]
    private Dictionary<HeroID, HeroData> _heroDataDict = new Dictionary<HeroID, HeroData>();
    public Dictionary<HeroID, HeroData> HeroDataDict { get { return _heroDataDict; } set { _heroDataDict = value; } }
}
