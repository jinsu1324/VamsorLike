using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobbyHero_Warrior : LobbyHero
{
    public void PlaySFX_SlashSword()
    {
        AudioManager.Instance.PlaySFX(SFXType.HeroSelect_SlashSword);
    }
}
