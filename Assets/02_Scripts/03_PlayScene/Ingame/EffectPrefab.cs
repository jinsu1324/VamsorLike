using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectPrefab : MonoBehaviour
{
    private EffectManager _effectManager;
    private string _effectName;

    /// <summary>
    /// ����Ʈ ������ �ʱ�ȭ
    /// </summary>
    public void Initialize(EffectManager effectManager, string effectName)
    {
        _effectManager = effectManager;
        _effectName = effectName;
    }

    /// <summary>
    /// ��ƼŬ ������ ��
    /// </summary>
    private void OnParticleSystemStopped()
    {
        _effectManager.BackPoolEffect(_effectName, this.gameObject);
    }
}
