using System;
using UnityEngine;
public class Mysingleton<T>:MonoBehaviour where T:MonoBehaviour{

    private static T m_instance;
    public static T Instance{
        get{

            if(m_instance == null){
                m_instance = FindObjectOfType<T>();
                if(m_instance == null) m_instance = new GameObject(typeof(T).Name).AddComponent<T>();
            }
            return m_instance;
        }
    }
    void Awake() {
         DontDestroyOnLoad(gameObject);
    }
}