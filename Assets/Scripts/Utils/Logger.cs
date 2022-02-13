using TMPro;
using UnityEngine;

namespace SimonSays.Utils
{
    public class Logger : Singleton<Logger>
    {
        [SerializeField]
        private TextMeshProUGUI debugAreaText = null;
    }
}