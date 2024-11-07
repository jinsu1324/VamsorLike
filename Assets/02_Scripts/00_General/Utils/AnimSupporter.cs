using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimSupporter : MonoBehaviour
{
    /// <summary>
    /// 애니메이션 클립 길이 를 반환해주는 함수
    /// </summary>
    public static float GetAnimationClipLength(string clipName, Animator animator)
    {
        // Animator의 RuntimeAnimatorController에서 모든 애니메이션 클립 검색
        foreach (var clip in animator.runtimeAnimatorController.animationClips)
        {
            if (clip.name == clipName)
            {
                // 일치하는 클립의 길이를 반환
                return clip.length;
            }
        }

        // 일치하는 클립을 찾지 못하면 0 반환
        Debug.Log("애니메이션 클립을 찾지 못했습니다.: " + clipName);
        return 0f;
    }
}
