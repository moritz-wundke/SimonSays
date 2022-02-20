using System.Collections.Generic;
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
        public bool IsInProgress => State == GameState.GeneratePattern || State == GameState.PlayPattern;
        public bool IsPostMatch => State == GameState.Post;

        private HashSet<PlayerController> players = new HashSet<PlayerController>();

        public void Start()
        {
            if (IsClient)
            {
                gameState.OnValueChanged += OnGameStateChangedInternal;
            }
        }

        public override void OnDestroy()
        {
            base.OnDestroy();
            if (IsClient)
            {
                gameState.OnValueChanged -= OnGameStateChangedInternal;
            }
        }
        
        public void AddPlayer(PlayerController player)
        {
            players.Add(player);
        }
        
        public void RemovePlayer(PlayerController player)
        {
            players.Remove(player);
        }

        private void DoGameStateChange(GameState newState)
        {
            if (!IsServer)
            {
                return;
            }

            var previousState = gameState.Value;
            gameState.Value = newState;
            OnGameStateChangedInternal(previousState, newState);
        }

        public void StartMatch()
        {
            if (IsPreMatch && IsServer)
            {
                DoGameStateChange(GameState.GeneratePattern);
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

        private void OnGameStateChangedInternal(GameState previous, GameState next)
        {
            Debug.Log($"Game Changed from {previous} to {next}");

            if (IsServer)
            {
                
            }
        }
    }
}