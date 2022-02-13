using UnityEngine;

namespace SimonSays.Utils
{
    public class Singleton<T> : MonoBehaviour
        where T : Singleton<T>
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
                DontDestroyOnLoad(_instance);

                return _instance;
            }
        }
    }
}