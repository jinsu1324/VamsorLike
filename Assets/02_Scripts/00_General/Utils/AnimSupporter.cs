using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimSupporter : MonoBehaviour
{
    /// <summary>
    /// �ִϸ��̼� Ŭ�� ���� �� ��ȯ���ִ� �Լ�
    /// </summary>
    public static float GetAnimationClipLength(string clipName, Animator animator)
    {
        // Animator�� RuntimeAnimatorController���� ��� �ִϸ��̼� Ŭ�� �˻�
        foreach (var clip in animator.runtimeAnimatorController.animationClips)
        {
            if (clip.name == clipName)
            {
                // ��ġ�ϴ� Ŭ���� ���̸� ��ȯ
                return clip.length;
            }
        }

        // ��ġ�ϴ� Ŭ���� ã�� ���ϸ� 0 ��ȯ
        Debug.Log("�ִϸ��̼� Ŭ���� ã�� ���߽��ϴ�.: " + clipName);
        return 0f;
    }
}
