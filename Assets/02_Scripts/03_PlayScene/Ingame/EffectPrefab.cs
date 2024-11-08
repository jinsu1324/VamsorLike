using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectPrefab : MonoBehaviour
{
    private EffectManager _effectManager;
    private string _effectName;

    /// <summary>
    /// 이펙트 프리팹 초기화
    /// </summary>
    public void Initialize(EffectManager effectManager, string effectName)
    {
        _effectManager = effectManager;
        _effectName = effectName;
    }

    /// <summary>
    /// 파티클 끝났을 때
    /// </summary>
    private void OnParticleSystemStopped()
    {
        _effectManager.BackPoolEffect(_effectName, this.gameObject);
    }
}
