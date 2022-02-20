using System.Threading.Tasks;
using SimonSays.Utils;
using Unity.Netcode;
using Unity.Services.Authentication;
using Unity.Services.Core;
using Unity.Services.Core.Environments;
using Unity.Services.Relay;
using Unity.Services.Vivox;
using UnityEngine;

namespace SimonSays.Connection
{
    public class ServiceManager : Singleton<ServiceManager>
    {
        private const int MAX_PLAYERS = 4;
        
        [SerializeField]
        private string environment = "production";

        public UnityTransport Transport => NetworkManager.Singleton.gameObject.GetComponent<UnityTransport>();
        public bool IsRelayEnabled => Transport != null && Transport.Protocol == UnityTransport.ProtocolType.RelayUnityTransport;
        
        private async Task Initialize()
        {
            await UnityServices.InitializeAsync(new InitializationOptions().SetEnvironmentName(environment));
            
            if (!AuthenticationService.Instance.IsSignedIn)
            {
                await AuthenticationService.Instance.SignInAnonymouslyAsync();
            }
            
            VivoxService.Instance.Initialize();
            // TODO:  https://docs.vivox.com/v5/general/unity/15_1_160000/en-us/Default.htm#Unity/vivox-unity-first-steps.htm%3FTocPath%3DVivox%2520Unity%2520SDK%2520documentation%7C_____1
        }

        public async Task<string> HostServer()
        {
            Debug.Log("Hosting server", this);

            await Initialize();
            
            var allocation = await Relay.Instance.CreateAllocationAsync(MAX_PLAYERS);
            var joinCode = await Relay.Instance.GetJoinCodeAsync(allocation.AllocationId);
            
            Transport.SetRelayServerData(allocation.RelayServer.IpV4, (ushort)allocation.RelayServer.Port, allocation.AllocationIdBytes,
                                         allocation.Key, allocation.ConnectionData);
            return joinCode;
        }
        
        public async Task JoinServer(string joinCode)
        {
            Debug.Log($"Joining server {joinCode}", this);
            
            await Initialize();
            
            var allocation = await Relay.Instance.JoinAllocationAsync(joinCode);
            
            Transport.SetRelayServerData(allocation.RelayServer.IpV4, (ushort)allocation.RelayServer.Port, allocation.AllocationIdBytes,
                                         allocation.Key, allocation.ConnectionData, allocation.HostConnectionData);
        }

        public void Disconnect()
        {
            Transport.DisconnectLocalClient();
            VivoxService.Instance.Client.Uninitialize();
        }
    }
}