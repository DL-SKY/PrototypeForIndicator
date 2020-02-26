using UnityEngine;
using UnityEditor;

public class AnimatorContextMenu : EditorWindow
{
    [MenuItem ("CONTEXT/Animator/New animation")]
    public static void NewAnimationContext (MenuCommand command)
    {
        Animator context = (Animator)command.context;
        //var anim = Selection.activeObject;

        NewAnimation (context.runtimeAnimatorController);
    }

    [MenuItem ("Assets/Animation/New")]
    public static void NewAnimationContext2 ()
    {
        var context = Selection.activeObject;

        NewAnimation (context);
    }

    // Validate the menu item defined by the function above.
    // The menu item will be disabled if this function returns false.
    [MenuItem ("Assets/Animation/New", true)]
    private static bool NewAnimationContext2Validator ()
    {
        return Selection.activeObject is RuntimeAnimatorController;
    }

    private static void NewAnimation(Object animator)
    {
        // Add an animation clip to it
        AnimationClip animationClip = new AnimationClip ();
        animationClip.name = "New Clip";
        AssetDatabase.AddObjectToAsset (animationClip, animator);

        // Reimport the asset after adding an object.
        // Otherwise the change only shows up when saving the project
        AssetDatabase.ImportAsset (AssetDatabase.GetAssetPath (animationClip));
    }

    [MenuItem ("Assets/Animation/Rename")]
    public static void RenameAnimation ()
    {
        var window = EditorWindow.GetWindow<AnimatorContextMenu> (true, "Rename animation clip");
        window.ShowUtility ();
    }

    private string newName;
    private void OnGUI ()
    {
        var context = Selection.activeObject;

        if (string.IsNullOrEmpty(newName))
            newName = context.name;

        GUILayout.BeginVertical ();
        newName = EditorGUILayout.TextField (newName);
        GUILayout.BeginHorizontal ();
        {
            if (GUILayout.Button ("Save"))
            {
                context.name = newName;
                AssetDatabase.ImportAsset (AssetDatabase.GetAssetPath (context));

                this.Close ();
            }
            if (GUILayout.Button ("Quit"))
                this.Close ();
        }
        GUILayout.EndHorizontal ();
        GUILayout.EndVertical ();
    }

    [MenuItem ("Assets/Animation/Rename", true)]
    private static bool RenameAnimationValidator ()
    {
        return Selection.activeObject is AnimationClip;
    }

    [MenuItem ("Assets/Animation/Delete")]
    public static void DeleteAnimation()
    {
        var context = Selection.activeObject;

        DestroyImmediate (context, true);
        AssetDatabase.SaveAssets ();
    }
    
    [MenuItem ("Assets/Animation/Delete", true)]
    private static bool DeleteAnimationValidator ()
    {
        return Selection.activeObject is AnimationClip;
    }
}