using System;
using SimonSays.Connection;
using SimonSays.Utils;
using UnityEngine;
using UnityEngine.UI;

namespace SimonSays.UI
{
    public class UIMenuController : SceneSingleton<UIMenuController>
    {
        [SerializeField]
        private Button host;
        
        [SerializeField]
        private Button dedicated;

        [SerializeField]
        private Button join;
        
        [SerializeField]
        private InputField joinCodeInput;

        private void Awake()
        {
            host.onClick.AddListener(() =>
            {
                ConnectionManager.Instance.StartServer(true);
            });
            
            dedicated.onClick.AddListener(() =>
            {
                ConnectionManager.Instance.StartServer(false);
            });
            
            join.onClick.AddListener(() =>
            {
                if (string.IsNullOrEmpty(joinCodeInput.text))
                {
                    Debug.Log("Please enter a valid join code");
                    return;
                }
                ConnectionManager.Instance.Join(joinCodeInput.text);
            });
            
            ConnectionManager.Instance.OnConnectionStateChanged.AddListener(OnConnectionStateChanged);
        }

        private void OnDestroy()
        {
            ConnectionManager.Instance.OnConnectionStateChanged.RemoveListener(OnConnectionStateChanged);
        }

        private void OnConnectionStateChanged()
        {
            gameObject.SetActive(ConnectionManager.Instance.State == ConnectionState.Disconnected);
        }
    }
}