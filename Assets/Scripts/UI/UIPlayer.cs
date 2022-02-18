using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace SimonSays.UI
{
    public class UIPlayer : UIBehaviour
    {
        [SerializeField] private Button leftButton;
        [SerializeField] private Button rightButton;
        [SerializeField] private Button muteButton;

        private PlayerController playerController;

        public bool IsFree => playerController != null;
        
        public void Init(PlayerController player)
        {
            playerController = player;
        }

        protected override void Awake()
        {
            base.Awake();
            
            leftButton.onClick.AddListener(LeftArrow);
            rightButton.onClick.AddListener(RightArrow);
            muteButton.onClick.AddListener(Mute);
        }

        private void LeftArrow()
        {
            if (playerController == null || !playerController.IsOwner)
            {
                return;
            }
            playerController.DoArrow(true);
        }
        
        private void RightArrow()
        {
            if (playerController == null || !playerController.IsOwner)
            {
                return;
            }
            playerController.DoArrow(false);
        }
        
        private void Mute()
        {
            if (playerController == null)
            {
                return;
            }
            playerController.Mute();
        }
    }
}