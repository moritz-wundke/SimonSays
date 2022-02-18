using System;
using System.Threading.Tasks;
using SimonSays.Utils;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.Events;

namespace SimonSays.Connection
{
    public class ConnectionManager : Singleton<ConnectionManager>
    {
        public string JoinCode { get; private set; }
        public ConnectionState State { get; private set; } = ConnectionState.Disconnected;

        public UnityEvent OnConnectionStateChanged = new UnityEvent();

        private void Start()
        {
            DontDestroyOnLoad(NetworkManager.Singleton);
            
            // STATUS TYPE CALLBACKS
            NetworkManager.Singleton.OnClientConnectedCallback += (id) =>
            {
                Debug.Log($"{id} just connected...");
            };
        }

        public async void StartServer(bool host = true)
        {
            if (State != ConnectionState.Disconnected)
            {
                throw new Exception("Already in a match");
            }

            JoinCode = await ServiceManager.Instance.HostServer();
            if (host)
            {
                Debug.Log(NetworkManager.Singleton.StartHost() ? "Starting server as host" : "Can not start host");
            }
            else
            {
                Debug.Log(NetworkManager.Singleton.StartServer() ? "Starting dedicated server" : "Can not start host");
            }
           
            State = ConnectionState.Host;
            
            
            // Connection state changed
            OnConnectionStateChanged.Invoke();
        }

        public async void Join(string joinCode)
        {
            if (State != ConnectionState.Disconnected)
            {
                throw new Exception("Already in a match");
            }

            JoinCode = joinCode;
            await ServiceManager.Instance.JoinServer(JoinCode);
            Debug.Log(NetworkManager.Singleton.StartClient() ? "Hosting!" : "Can not start host");
            State = ConnectionState.Client;
            
            // Connection state changed
            OnConnectionStateChanged.Invoke();
        }

        public void Disconnect()
        {
            if (State == ConnectionState.Disconnected)
            {
                throw new Exception("Not connected");
            }
            NetworkManager.Singleton.Shutdown();
            ServiceManager.Instance.Disconnect();
            
            State = ConnectionState.Disconnected;

            // Connection state changed
            OnConnectionStateChanged.Invoke();
        }
    }
}