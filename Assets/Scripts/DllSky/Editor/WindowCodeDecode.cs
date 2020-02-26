using DllSky.Protection;
using UnityEditor;
using UnityEngine;

public class WindowCodeDecode : EditorWindow
{
    #region Variables
    private Vector2 scrollPosition;
    private string firstData;
    private string secondData;
    #endregion

    #region Unity methods
    private void OnGUI()
    {
        scrollPosition = GUILayout.BeginScrollView(scrollPosition);

            GUILayout.BeginVertical(new GUILayoutOption[] { GUILayout.ExpandHeight(true) });
                GUILayout.Label("Source:");            
                firstData = GUILayout.TextArea(firstData, new GUILayoutOption[] { GUILayout.ExpandHeight(true) });            
            GUILayout.EndVertical();

            if (GUILayout.Button("Code"))
                OnCode();
            if (GUILayout.Button("Decode"))
                OnDecode();

            GUILayout.BeginVertical(new GUILayoutOption[] { GUILayout.ExpandHeight(true) });
                GUILayout.Label("Result:");
                GUILayout.TextArea(secondData, new GUILayoutOption[] { GUILayout.ExpandHeight(true) });
            GUILayout.EndVertical();

        GUILayout.EndScrollView();
    }
    #endregion

    #region Private methods
    private void OnCode()
    {
        //Шифруем
        secondData = SimpleEncrypting.Encode(firstData);
    }

    private void OnDecode()
    {
        //Дешифруем
        secondData = SimpleEncrypting.Decode(firstData);
    }
    #endregion

    #region Tools
    [MenuItem("Tools/Utills/CodeDecode", priority = 100)]
    private static void OnShowCodeDecodeWindow()
    {
        GetWindow<WindowCodeDecode>(true, "Code/Decode", true);
    }
    #endregion
}
