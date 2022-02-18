using SimonSays.Utils;
using Unity.Netcode;

namespace SimonSays
{
    public class PlayersManager : SceneNetworkSingleton<PlayersManager>
    {
        private readonly NetworkVariable<int> players = new();
        public int Players => players.Value;

        public void Start()
        {
            NetworkManager.Singleton.OnClientConnectedCallback += OnClientConnected;
            NetworkManager.Singleton.OnClientDisconnectCallback += OnClientDisconnect;
            players.OnValueChanged += OnPlayerCountChanged;
        }

        public override void OnDestroy()
        {
            base.OnDestroy();
            NetworkManager.Singleton.OnClientConnectedCallback -= OnClientConnected;
            NetworkManager.Singleton.OnClientDisconnectCallback -= OnClientDisconnect;
            players.OnValueChanged -= OnPlayerCountChanged;
        }

        private void OnClientConnected(ulong id)
        {
            if (!IsServer)
            {
                return;
            }

            var oldValue = players.Value;
            players.Value++;
            OnPlayerCountChanged(oldValue, players.Value);
        }
        
        private void OnClientDisconnect(ulong id)
        {
            if (!IsServer)
            {
                return;
            }

            var oldValue = players.Value;
            players.Value--;
            OnPlayerCountChanged(oldValue, players.Value);
        }

        private void OnPlayerCountChanged(int previous, int next)
        {
            
        }
    }
}