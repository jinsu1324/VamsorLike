using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillDatas_Boomerang : DataListSO<SkillData_Boomerang>, ISkillDatas
{
    public DataListSO<Data> GetDataListSO<Data>() where Data : BaseData
    {
        return this as DataListSO<Data>;
    }
}
