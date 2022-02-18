using Unity.Netcode;
using UnityEngine;

namespace SimonSays.Utils
{
    public class SceneNetworkSingleton<T> : NetworkBehaviour
        where T : SceneNetworkSingleton<T>
    {
        private static T _instance;

        public static T Instance
        {
            get
            {
                if (_instance != null)
                {
                    return _instance;
                }
                
                // Find in scene
                if (FindObjectsOfType(typeof(T)) is T[] objs && objs.Length > 0)
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