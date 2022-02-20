using SimonSays.Connection;
using SimonSays.Utils;
using UnityEngine;
using UnityEngine.UI;

namespace SimonSays.UI
{
    public class UIGameController : SceneSingleton<UIGameController>
    {
        [SerializeField]
        private Button disconnect;

        [SerializeField] private UIPlayer ownerPlayer;
        [SerializeField] private UIPlayer player1;
        [SerializeField] private UIPlayer player2;
        [SerializeField] private UIPlayer player3;
        
        private void Awake()
        {
            disconnect.onClick.AddListener(() =>
            {
                ConnectionManager.Instance.Disconnect();
            });
            gameObject.SetActive(false);
            ConnectionManager.Instance.OnConnectionStateChanged.AddListener(OnConnectionStateChanged);
        }

        private void OnDestroy()
        {
            ConnectionManager.Instance.OnConnectionStateChanged.RemoveListener(OnConnectionStateChanged);
        }

        private void OnConnectionStateChanged()
        {
            gameObject.SetActive(ConnectionManager.Instance.State != ConnectionState.Disconnected);

            ownerPlayer.gameObject.SetActive(false);
            player1.gameObject.SetActive(false);
            player2.gameObject.SetActive(false);
            player3.gameObject.SetActive(false);
        }

        public void OnPlayerSpawned(PlayerController player)
        {
            if (player.IsOwner)
            {
                ownerPlayer.Init(player);
            }
            else if (player1.IsFree)
            {
                player1.Init(player);
            }
            else if (player2.IsFree)
            {
                player2.Init(player);
            }
            else if (player2.IsFree)
            {
                player2.Init(player);
            }
        }

    }
}