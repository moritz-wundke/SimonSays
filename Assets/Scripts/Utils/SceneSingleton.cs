using System;
using UnityEngine;

namespace SimonSays.Utils
{
    public class SceneSingleton<T> : MonoBehaviour
        where T : SceneSingleton<T>
    {
        private static T _instance;

        private void Awake()
        {
            if (_instance == null)
            {
                _instance = (T)this;
            }
            else
            {
                Destroy(gameObject);
            }
        }

        public static T Instance
        {
            get
            {
                if (_instance != null)
                {
                    return _instance;
                }
                
                // Find in scene
                if (FindObjectsOfType(typeof(T), true) is T[] objs && objs.Length > 0)
                {
                    _instance = objs[0];
                    return _instance;
                }
                
                // Spawn in case we have to
                var obj = new GameObject
                {
                    name = typeof(T).Name
                };
                _instance = obj.AddComponent<T>();

                return _instance;
            }
        }
    }
}