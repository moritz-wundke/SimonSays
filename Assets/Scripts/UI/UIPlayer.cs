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
        [SerializeField] private CanvasGroup canvasGroup;

        private PlayerController playerController;

        public bool IsFree => playerController != null;
        
        public void Init(PlayerController player)
        {
            gameObject.SetActive(true);
            canvasGroup.alpha = 1;
            playerController = player;
            playerController.OnEliminated.AddListener(OnEliminated);
        }

        protected override void Awake()
        {
            base.Awake();
            
            leftButton.onClick.AddListener(LeftArrow);
            rightButton.onClick.AddListener(RightArrow);
            muteButton.onClick.AddListener(Mute);
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
            playerController.OnEliminated.RemoveListener(OnEliminated);
        }

        private void OnEliminated()
        {
            canvasGroup.alpha = 0.5f;
        }

        private void LeftArrow()
        {
            if (playerController == null || !playerController.IsOwner || playerController.IsEliminated)
            {
                return;
            }
            playerController.DoArrow(true);
        }
        
        private void RightArrow()
        {
            if (playerController == null || !playerController.IsOwner || playerController.IsEliminated)
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