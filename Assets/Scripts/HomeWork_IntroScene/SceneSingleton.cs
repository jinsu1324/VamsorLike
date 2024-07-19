using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneSingleton<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T _instance;
    private static object _lock = new object();
    
    public static T Instance
    {
        get
        {
            lock (_lock)
            {
                if (_instance != null)
                    if (!_instance.gameObject.activeInHierarchy)
                        _instance = null;

                if (_instance == null)
                {
                    _instance = FindObjectOfType<T>();

                    if (_instance == null)
                        return null;
                }
            }

            return _instance;
        }
    }
}
