using SimonSays.UI;
using Unity.Netcode;
using UnityEngine.Events;

namespace SimonSays
{
    public class PlayerController : NetworkBehaviour
    {
        private NetworkVariable<bool> eliminated = new NetworkVariable<bool>();
        public bool IsEliminated => eliminated.Value;

        public UnityEvent OnEliminated = new UnityEvent();
        
        private void Start()
        {
            if (IsClient)
            {
                UIGameController.Instance.OnPlayerSpawned(this);
            }
            else if (IsServer)
            {
                GameMode.Instance.AddPlayer(this);
            }
        }

        public override void OnDestroy()
        {
            base.OnDestroy();
            if (IsServer)
            {
                GameMode.Instance.RemovePlayer(this);
            }
        }

        public void DoArrow(bool left)
        {
            if (IsOwner)
            {
                DoArrowActionServerRpc(left);
            }
            DoArrowAction(left);
        }

        private void DoArrowAction(bool left)
        {
            
        }

        [ServerRpc]
        private void DoArrowActionServerRpc(bool left)
        {
            DoArrowActionClientRpc(left);
        }
        
        [ClientRpc]
        private void DoArrowActionClientRpc(bool left)
        {
            DoArrowAction(left);
        }

        public void Mute()
        {
            if (IsOwner)
            {
                // Mute mic
            }
            else
            {
                // Mute player voice
            }
        }
    }
}