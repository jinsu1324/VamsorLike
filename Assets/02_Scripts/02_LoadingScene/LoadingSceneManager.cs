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
    private UnityEngine.UI.Slider _progressBar; // ���α׷��� ��

    [SerializeField]
    private TextMeshProUGUI _progressText;      // ���α׷��� �ؽ�Ʈ

    [SerializeField]
    private float _fakeProgressSpeed = 0.5f;    // ���� ���α׷��� �ӵ�


    /// <summary>
    /// Start �Լ�
    /// </summary>
    private void Start()
    {
        StartCoroutine(LoadSceneWithProgress());
    }

    /// <summary>
    /// �� �ε� �� ���α׷��� ǥ�� �ڷ�ƾ
    /// </summary>
    private IEnumerator LoadSceneWithProgress()
    {
        // �� �񵿱� �ε� ����
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(SceneLoader.LoadSceneName);

        // �ڵ� �� Ȱ��ȭ ��Ȱ��ȭ
        asyncOperation.allowSceneActivation = false;

        // ���� ���α׷��� �ʱ�ȭ
        float fakeProgress = 0.0f;  

        // �ε� ���α׷��� ������Ʈ
        while (!asyncOperation.isDone)
        {
            // ���� progress�� �ö󰡴� ���� ���� ���α׷��� õõ�� ����
            if (fakeProgress < asyncOperation.progress)
            {
                fakeProgress += _fakeProgressSpeed * Time.deltaTime;
                
                // ���� progress �̻� �Ѿ�� ����
                fakeProgress = Mathf.Min(fakeProgress, asyncOperation.progress); 
            }

            // ���α׷��� �� ������Ʈ
            _progressBar.value = fakeProgress;
            _progressText.text = (Mathf.FloorToInt(fakeProgress * 100)).ToString() + "%";

            //// progress ���� 0 ~ 0.9 ���̿��� ����
            //_progressBar.value = asyncOperation.progress;
            //_progressText.text = (asyncOperation.progress * 100).ToString() + "%";

            // �ε��� ���� �Ϸ�� ���¿���
            if (asyncOperation.progress >= 0.9f && fakeProgress >= 0.9f)
            {
                // �ణ�� ����
                // yield return new WaitForSeconds(1.0f);  

                // �ٸ� 100%�� ������Ʈ
                _progressBar.value = 1.0f;
                _progressText.text = "100%";

                // �� Ȱ��ȭ
                asyncOperation.allowSceneActivation = true; 
            }

            yield return null;
        }
    }
}