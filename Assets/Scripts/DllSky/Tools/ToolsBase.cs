using UnityEditor;
using UnityEngine;

public class ToolsBase : MonoBehaviour
{
    [MenuItem("Tools/Save/Open data folder", priority = 10000)]
    private static void OpenDataFolder()
    {
        System.Diagnostics.Process.Start(Application.persistentDataPath);
        Debug.Log("<color=#FFD800>[ToolsBase]</color> Open " + Application.persistentDataPath);
    }
}
