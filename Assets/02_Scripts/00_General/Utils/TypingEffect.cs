using UnityEngine;
using TMPro;
using System.Collections;
using Sirenix.OdinInspector;

public class TypingEffect : MonoBehaviour
{
    [Title("Ÿ���� ȿ�� �� �ؽ�Ʈ", bold: false)]
    [SerializeField]
    private TextMeshProUGUI _text;              // TextMeshPro

    [Title("Ÿ���� �ӵ� (���� �������� ������)", bold: false)]
    [SerializeField]
    private float _typingSpeed = 0.1f;          // �� ���� ���� ������ ����

    private string fullText;                    // �ؽ�Ʈ ��ü�� ������ ����

    /// <summary>
    /// Start �Լ�
    /// </summary>
    private void Start()
    {
        fullText = _text.text;  // ��ü �ؽ�Ʈ ����
        _text.text = "";        // ó���� �� �ؽ�Ʈ�� ����
        StartCoroutine(TypeText());
    }

    /// <summary>
    /// Ÿ���� ȿ�� �ڷ�ƾ
    /// </summary>
    private IEnumerator TypeText()
    {
        for (int i = 0; i <= fullText.Length; i++)
        {
            _text.text = fullText.Substring(0, i);  // ���ڸ� �ѱ��ھ� text�� �־���
            yield return new WaitForSeconds(_typingSpeed); // Ÿ���� �ӵ���ŭ ���
        }
    }
}