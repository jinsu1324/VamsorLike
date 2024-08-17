using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 스프라이트 깜빡이는 역할
public class BlinkSprite
{
    // 피격시 깜빡이기
    public IEnumerator Blink(SpriteRenderer spriteRenderer, float blinkTime)
    {
        spriteRenderer.color = Color.red;

        yield return new WaitForSeconds(blinkTime);

        spriteRenderer.color = Color.white;
    }
}
