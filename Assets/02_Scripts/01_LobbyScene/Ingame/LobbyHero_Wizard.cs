using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobbyHero_Wizard : LobbyHero
{
    public void PlaySFX_FireSpell()
    {
        AudioManager.Instance.PlaySFX(SFXType.HeroSelect_FireSpell);
    }
}
