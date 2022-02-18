using SimonSays.Utils;
using Unity.Netcode;
using UnityEngine;

namespace SimonSays
{
    public class GameMode : SceneNetworkSingleton<GameMode>
    {
        private NetworkVariable<GameState> gameState = new NetworkVariable<GameState>();
        public GameState State => gameState.Value;

        public bool IsPreMatch => State == GameState.Pre;
        public bool IsInProgress => State == GameState.InProgress;
        public bool IsPostMatch => State == GameState.Post;

        public void Start()
        {
            if (IsClient)
            {
                gameState.OnValueChanged += OnGameStateChanged;
            }
        }

        public override void OnDestroy()
        {
            base.OnDestroy();
            if (IsClient)
            {
                gameState.OnValueChanged -= OnGameStateChanged;
            }
        }

        private void DoGameStateChange(GameState newState)
        {
            if (!IsServer)
            {
                return;
            }

            var previousState = gameState.Value;
            gameState.Value = newState;
            OnGameStateChanged(previousState, newState);
        }

        public void StartMatch()
        {
            if (IsPreMatch && IsServer)
            {
                DoGameStateChange(GameState.InProgress);
            }
        }

        public void EndMatch()
        {
            if (IsInProgress && IsServer)
            {
                DoGameStateChange(GameState.Post);
            }
        }

        public void RestartMatch()
        {
            if (IsServer)
            {
                DoGameStateChange(GameState.Pre);
            }
        }

        public void OnGameStateChanged(GameState previous, GameState next)
        {
            Debug.Log($"Game Changed from {previous} to {next}");
        }
    }
}