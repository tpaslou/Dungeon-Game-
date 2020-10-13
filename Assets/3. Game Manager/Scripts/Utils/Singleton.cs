using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : Singleton<T>
{
    public static T instance;

    public static T Instance
    {
        get { return instance; }
        //no set to protect access
    }

    public static bool IsInitialized
    {
        get { return instance != null; }
    }
    
    //protected = accesible , virtual = can be overwritten
    protected virtual void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("[Singleton] Trying to instantiate a second instance of a singleton class.");
        }
        else
        {
            instance = (T)this;
            
        }
    }

    protected virtual void OnDestroy()
    {
        if (instance == this)
        {
            instance = null;
        }
    }
}
