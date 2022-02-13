using UnityEngine;

namespace SimonSays.Data
{
    /// <summary>
    /// Data storage of all the valid strings used to create a player's name.
    /// Currently names are a two word combination in Adjective-Noun Combo (e.g. Happy Apple)
    /// From: https://github.com/Unity-Technologies/com.unity.multiplayer.samples.coop/blob/main/Assets/BossRoom/Scripts/Client/Data/NameGenerationData.cs
    /// </summary>
    /// 
    [CreateAssetMenu(menuName = "GameData/NameGeneration", order = 2)]
    public class PlayerNameGenerator : ScriptableObject
    {
        [Tooltip("The list of all possible strings the game can use as the first word of a player name")]
        public string[] FirstWordList;

        [Tooltip("The list of all possible strings the game can use as the second word in a player name")]
        public string[] SecondWordList;
    }
}