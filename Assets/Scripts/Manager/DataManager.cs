using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 게임에 필요한 데이터들(스크립터블 오브젝트 등) 딕셔너리 가지고있는 매니저
public class DataManager : SerializedMonoBehaviour
{
    [Title("데이터 딕셔너리들", bold: false)]
    // 영웅 데이터 딕셔너리
    [SerializeField]
    public Dictionary<HeroID, HeroData> HeroDataDict { get; set; } = new Dictionary<HeroID, HeroData>();

    // 몬스터 데이터 딕셔너리
    [SerializeField]
    public Dictionary<MonsterID, MonsterData> MonsterDataDict { get; set; } = new Dictionary<MonsterID, MonsterData>();

    // 스킬 데이터 딕셔너리
    [SerializeField]
    public Dictionary<SkillID, SkillData> SkillDataDict { get; set; } = new Dictionary<SkillID, SkillData>();
}
