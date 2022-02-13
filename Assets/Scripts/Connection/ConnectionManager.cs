using System.Threading.Tasks;
using SimonSays.Utils;
using Unity.Netcode;
using Unity.Services.Authentication;
using Unity.Services.Core;
using Unity.Services.Core.Environments;
using Unity.Services.Relay;
using Unity.Services.Relay.Models;
using UnityEngine;

namespace SimonSays.Connection
{
    public class ConnectionManager : Singleton<ConnectionManager>
    {
        public const int MAX_PLAYERS = 4;
        
        [SerializeField]
        private string environment = "production";
        [SerializeField]
        private int maxPLayers = MAX_PLAYERS;

        public UnityTransport Transport => NetworkManager.Singleton.gameObject.GetComponent<UnityTransport>();
        public bool IsRelayEnabled => Transport != null && Transport.Protocol == UnityTransport.ProtocolType.RelayUnityTransport;

        public ConnectionState State { get; private set; } = ConnectionState.Disconnected;

        private async Task Initialize()
        {
            await UnityServices.InitializeAsync(new InitializationOptions().SetEnvironmentName(environment));
            
            if (!AuthenticationService.Instance.IsSignedIn)
            {
                await AuthenticationService.Instance.SignInAnonymouslyAsync();
            }
        }

        public async Task<string> HostServer()
        {
            Debug.Log("Hosting server", this);

            await Initialize();
            
            var allocation = await Relay.Instance.CreateAllocationAsync(maxPLayers);
            var joinCode = await Relay.Instance.GetJoinCodeAsync(allocation.AllocationId);
            
            Transport.SetRelayServerData(allocation.RelayServer.IpV4, (ushort)allocation.RelayServer.Port, allocation.AllocationIdBytes,
                                         allocation.Key, allocation.ConnectionData);
            
            State = ConnectionState.Host;
            return joinCode;
        }
        
        public async void JoinServer(string joinCode)
        {
            Debug.Log($"Joining server {joinCode}", this);
            
            await Initialize();
            
            var allocation = await Relay.Instance.JoinAllocationAsync(joinCode);
            
            Transport.SetRelayServerData(allocation.RelayServer.IpV4, (ushort)allocation.RelayServer.Port, allocation.AllocationIdBytes,
                                         allocation.Key, allocation.ConnectionData, allocation.HostConnectionData);
            
            State = ConnectionState.Client;
        }
    }
}