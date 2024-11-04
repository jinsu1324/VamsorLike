using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISkillDatas
{
    DataListSO<Data> GetDataListSO<Data>() where Data : BaseData;
}
