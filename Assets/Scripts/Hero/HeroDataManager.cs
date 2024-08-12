using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroDataManager : SerializedMonoBehaviour
{
    // ÇÁ·ÎÁ§Æ®ÀÇ ¿µ¿õ ScriptableObject¸¦ ¸ðµÎ ¹Þ¾Æ¿Í¼­ ÀúÀåÇÒ µñ¼Å³Ê¸®
    [SerializeField]
    private Dictionary<HeroID, HeroData> _heroDataDict = new Dictionary<HeroID, HeroData>();
    public Dictionary<HeroID, HeroData> HeroDataDict { get { return _heroDataDict; } set { _heroDataDict = value; } }

    // ¿µ¿õ ÇÁ¸®ÆÕµé µñ¼Å³Ê¸®
    [SerializeField]
    private Dictionary<HeroID, HeroObject> _heroObjectDict = new Dictionary<HeroID, HeroObject>();
    public Dictionary<HeroID, HeroObject> HeroObjectDict { get { return _heroObjectDict; } set { _heroObjectDict = value; } }
}
