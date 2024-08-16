using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ���ӿ� �ʿ��� �۷ι��ϰ� �������ִ� �Ŵ����� ����
public class Managers : SerializedMonoBehaviour
{
    #region �̱���
    private static Managers _instance;

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    public static Managers Instance
    {
        get
        {
            if (_instance == null)
            {
                return null;
            }

            return _instance;
        }
    }
    #endregion

    [Title("�Ŵ�����", bold: false)]
    // ������Ʈ �Ŵ���
    [SerializeField]
    public ObjectManager ObjectManager { get; set; }

    // ������ �Ŵ���
    [SerializeField]
    public DataManager DataManager { get; set; }    
}
