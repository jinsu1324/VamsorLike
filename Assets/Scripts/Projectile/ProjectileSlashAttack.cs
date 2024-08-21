using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileSlashAttack : Projectile
{
    private void Start()
    {
        AttackAfterDestroy(0.1f);
    }

    public void AttackAfterDestroy(float delay)
    {
        Destroy(this.gameObject, delay);
    }

}
