using SimonSays.Connection;
using SimonSays.Utils;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace SimonSays.UI
{
    [RequireComponent(typeof(Text))]
    public class JoinCodeLabel : MonoBehaviour, IPointerClickHandler
    {
        [SerializeField] private Text textField;
        
        private void Awake()
        {
            ConnectionManager.Instance.OnConnectionStateChanged.AddListener(OnConnectionStateChanged);
        }

        private void OnDestroy()
        {
            ConnectionManager.Instance.OnConnectionStateChanged.RemoveListener(OnConnectionStateChanged);
        }

        private void OnConnectionStateChanged()
        {
            textField.text = $"Click to copy join code: {ConnectionManager.Instance.JoinCode}";
        }
        
        public void OnPointerClick(PointerEventData eventData)
        {
            ConnectionManager.Instance.JoinCode.CopyToClipboard();
        }
    }
}