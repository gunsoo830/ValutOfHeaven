using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingleToneBehaviour<T> : MonoBehaviour where T : MonoBehaviour
{

    [SerializeField] protected static T instance;

    public static bool InstanceExists => instance != null;

    protected virtual void Awake()
    {
        if (instance != null && instance != this)
        {
            OnDestroy();
            return;
        }
        instance = this as T;

        DontDestroyOnLoad(instance.gameObject);
    }

    public static T Call
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<T>();
            }

            return instance;
        }
    }

    public static bool TryCall(out T callInstance)
    {
        callInstance = Call;
        return callInstance != null;
    }

    protected virtual void OnDestroy()
    {
        if (this.gameObject != null)
        {
            Destroy(this.gameObject);
        }
    }
}
