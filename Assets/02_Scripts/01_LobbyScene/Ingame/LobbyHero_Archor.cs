using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobbyHero_Archor : LobbyHero
{
    public void PlaySFX_SlashSword()
    {
        AudioManager.Instance.PlaySFX(SFXType.HeroSelect_SlashSword);
    }
}
