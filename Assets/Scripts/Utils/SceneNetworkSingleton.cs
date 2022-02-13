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