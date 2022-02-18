using UnityEngine;

namespace SimonSays.Utils
{
    public static class ClipboardExtension
    {
        public static void CopyToClipboard(this string str)
        {
            GUIUtility.systemCopyBuffer = str;
        }
        
        public static string CopyFromClipboard() => GUIUtility.systemCopyBuffer;
    }
}