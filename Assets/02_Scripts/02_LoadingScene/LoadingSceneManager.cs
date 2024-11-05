using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class LoadingSceneManager : MonoBehaviour
{
    [SerializeField]
    private UnityEngine.UI.Slider _progressBar; // 프로그레스 바

    [SerializeField]
    private TextMeshProUGUI _progressText;      // 프로그레스 텍스트

    [SerializeField]
    private float _fakeProgressSpeed = 0.5f;    // 가상 프로그레스 속도


    /// <summary>
    /// Start 함수
    /// </summary>
    private void Start()
    {
        StartCoroutine(LoadSceneWithProgress());
    }

    /// <summary>
    /// 씬 로드 및 프로그레스 표시 코루틴
    /// </summary>
    private IEnumerator LoadSceneWithProgress()
    {
        // 씬 비동기 로드 시작
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(SceneLoader.LoadSceneName);

        // 자동 씬 활성화 비활성화
        asyncOperation.allowSceneActivation = false;

        // 가상 프로그레스 초기화
        float fakeProgress = 0.0f;  

        // 로딩 프로그레스 업데이트
        while (!asyncOperation.isDone)
        {
            // 실제 progress가 올라가는 동안 가상 프로그레스 천천히 증가
            if (fakeProgress < asyncOperation.progress)
            {
                fakeProgress += _fakeProgressSpeed * Time.deltaTime;
                
                // 실제 progress 이상 넘어가지 않음
                fakeProgress = Mathf.Min(fakeProgress, asyncOperation.progress); 
            }

            // 프로그레스 바 업데이트
            _progressBar.value = fakeProgress;
            _progressText.text = (Mathf.FloorToInt(fakeProgress * 100)).ToString() + "%";

            //// progress 값이 0 ~ 0.9 사이에서 증가
            //_progressBar.value = asyncOperation.progress;
            //_progressText.text = (asyncOperation.progress * 100).ToString() + "%";

            // 로딩이 거의 완료된 상태에서
            if (asyncOperation.progress >= 0.9f && fakeProgress >= 0.9f)
            {
                // 약간의 지연
                // yield return new WaitForSeconds(1.0f);  

                // 바를 100%로 업데이트
                _progressBar.value = 1.0f;
                _progressText.text = "100%";

                // 씬 활성화
                asyncOperation.allowSceneActivation = true; 
            }

            yield return null;
        }
    }
}