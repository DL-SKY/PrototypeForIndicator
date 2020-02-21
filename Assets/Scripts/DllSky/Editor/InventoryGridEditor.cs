using UnityEditor;
using UnityEngine;

namespace DllSky.Components
{
    [CustomEditor(typeof(InventoryGrid))]
    public class InventoryGridEditor : Editor
    {
        #region Variables
        private InventoryGrid _target;

        private MonoScript dataScript;
        private SerializedProperty dataTransform;
        #endregion

        #region Unity methods
        private void OnEnable()
        {
            _target = target as InventoryGrid;

            dataScript = MonoScript.FromMonoBehaviour(_target);
            dataTransform = serializedObject.FindProperty("parent");
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            EditorGUI.BeginDisabledGroup(true);
            EditorGUILayout.ObjectField("Script:", dataScript, typeof(MonoScript), false);
            EditorGUI.EndDisabledGroup();

            EditorGUILayout.Space();
            GUILayout.Label("Settings", EditorStyles.boldLabel);

            _target.showType = (EnumInventoryGridShowType)EditorGUILayout.EnumPopup("Type Showing Items", _target.showType);

            EditorGUI.BeginDisabledGroup(_target.showType != EnumInventoryGridShowType.PerFewItems);
            _target.itemsPerFrame = EditorGUILayout.IntField("Items Per Frame", _target.itemsPerFrame);
            _target.itemsPerFrame = Mathf.Max(1, _target.itemsPerFrame);
            EditorGUI.EndDisabledGroup();

            EditorGUILayout.Space();
            GUILayout.Label("Items", EditorStyles.boldLabel);

            EditorGUI.BeginChangeCheck();
            EditorGUILayout.PropertyField(dataTransform);
            if (EditorGUI.EndChangeCheck())
                serializedObject.ApplyModifiedProperties();

            _target.prefab = (GameObject)EditorGUILayout.ObjectField("Item Prefab", _target.prefab, typeof(GameObject), false);            


            //GUILayout.Label("Base Settings", EditorStyles.boldLabel); //Подпись к полю ввода текста
            //myString = EditorGUILayout.TextField("Text Field", myString); //Поле ввода текста

            //groupEnabled = EditorGUILayout.BeginToggleGroup("Optional Settings", groupEnabled); //Выключаемая группа элементов
            //myBool = EditorGUILayout.Toggle("Toggle", myBool); //Галочка-выключатель
            //myFloat = EditorGUILayout.Slider("Slider", myFloat, -3, 3); //Слайдэр
            //EditorGUILayout.EndToggleGroup(); //Конец группы элементов
        }
        #endregion

        #region Private methods
        #endregion
    }
}
