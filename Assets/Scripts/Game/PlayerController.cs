using SimonSays.UI;
using Unity.Netcode;

namespace SimonSays
{
    public class PlayerController : NetworkBehaviour
    {
        private void Start()
        {
            if (IsClient)
            {
                UIGameController.Instance.OnPlayerSpawned(this);
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