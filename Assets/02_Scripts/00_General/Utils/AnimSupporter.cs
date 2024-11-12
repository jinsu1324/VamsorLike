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
        // �������̵� ��Ʈ�ѷ� üũ�ؼ� ������ �������̵� ��Ʈ�ѷ�, ������ �׳� animator
        AnimatorOverrideController overrideController = animator.runtimeAnimatorController as AnimatorOverrideController;
        RuntimeAnimatorController controller = overrideController != null ? 
            overrideController.runtimeAnimatorController : animator.runtimeAnimatorController;

        // Animator�� RuntimeAnimatorController���� ��� �ִϸ��̼� Ŭ�� �˻�
        foreach (var clip in controller.animationClips)
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
