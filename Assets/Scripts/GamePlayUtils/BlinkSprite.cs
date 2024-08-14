using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlinkSprite
{
    // �ǰݽ� �����̱�
    public IEnumerator Blink(SpriteRenderer spriteRenderer, float blinkTime)
    {
        spriteRenderer.color = Color.red;

        yield return new WaitForSeconds(blinkTime);

        spriteRenderer.color = Color.white;
    }
}
