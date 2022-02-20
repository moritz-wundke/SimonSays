using System;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;

namespace SimonSays.UI
{
    public class SimonLogger : MonoBehaviour
    {
        private const int MAX_LOGS = 20;
        
        [SerializeField] private TextMeshProUGUI textField;
        
        struct LogMessage
        {
            public string message;
            public string stack;
            public LogType type;
        }
        List<LogMessage> logList = new List<LogMessage>();
        
        private void Awake()
        {
            Application.logMessageReceived += LogCallback;
        }

        private void OnDestroy()
        {
            Application.logMessageReceived -= LogCallback;
        }

        private void LogCallback(string message, string stackTrace, LogType type)
        {
            logList.Add(new LogMessage { message = message, stack = stackTrace, type = type });
            if (logList.Count > MAX_LOGS)
            {
                logList.RemoveAt(0);
            }
            UpdateLogList();
        }

        private string GetLogColor(LogType type)
        {
            return type switch
            {
                LogType.Log => "green",
                LogType.Warning => "yellow",
                _ => "red"
            };
        }

        public void UpdateLogList()
        {
            var sb = new StringBuilder();

            foreach (var log in logList)
            {
                var color = GetLogColor(log.type);
                var stack = color.Equals("red") ? "\n"+log.stack.Split("\n")[0] : "";
                 sb.AppendLine($"<color=\"{color}\">{DateTime.Now.ToString("HH:mm:ss.fff")} {log.message}{stack}</color>");
            }
            
            textField.text = sb.ToString();
        }
    }
}