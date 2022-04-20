using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingleToneMaker<T> : MonoBehaviour where T : MonoBehaviour
{
    private static readonly object _padLock = new object();
    private static T instance = null;
    public static T Instance
    {
        get
        {
            lock (_padLock)
            {
                if (instance == null)
                {
                    //싱글톤 컴포넌트는 1개만 있어야하기에 여러개의 게임오브젝트에 들어있다면 그 자체로 이미 싱글톤이 아님
                    instance = GameObject.FindObjectOfType(typeof(T)) as T;
                    if (instance == null)
                    {
                        var singletonObject = new GameObject();
                        instance = singletonObject.AddComponent<T>();
                        singletonObject.name = typeof(T).ToString() + " (Singleton)";

                        DontDestroyOnLoad(singletonObject);
                    }
                }
                return instance;
            }
        }
    }

    private void OnDestroy()
    {
        Debug.Log("파괴되었음?");
    }
}
