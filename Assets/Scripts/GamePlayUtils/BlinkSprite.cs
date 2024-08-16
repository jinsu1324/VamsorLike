using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ��������Ʈ �����̴� ����
public static class BlinkSprite
{
    // �ǰݽ� �����̱�
    public static IEnumerator Blink(SpriteRenderer spriteRenderer, float blinkTime)
    {
        spriteRenderer.color = Color.red;

        yield return new WaitForSeconds(blinkTime);

        spriteRenderer.color = Color.white;
    }
}
