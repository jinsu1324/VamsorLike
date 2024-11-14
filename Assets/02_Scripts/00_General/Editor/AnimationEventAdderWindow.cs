using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
using UnityEditorInternal;
#endif

public class AnimationEventAdderWindow : EditorWindow
{
    private ReorderableList animationClipList;
    private List<AnimationClip> animationClips = new List<AnimationClip>();
    private string functionName = "OnEvent";
    private float frameRate = 60f;
    private int frameIndex = 0;
    private string parameterType = "float";
    private string stringParameter = "";
    private int intParameter = 0;
    private float floatParameter = 0f;

    [MenuItem("StudioSogae/Animation/Animation Event Adder")]
    public static void ShowWindow()
    {
        GetWindow<AnimationEventAdderWindow>("Animation Event Adder").minSize = new Vector2(400, 300);
    }

    private void OnEnable()
    {
        animationClipList = new ReorderableList(animationClips, typeof(AnimationClip), true, true, true, true);

        animationClipList.drawHeaderCallback = (Rect rect) =>
        {
            EditorGUI.LabelField(rect, "Animation Clips");
        };

        animationClipList.drawElementCallback = (Rect rect, int index, bool isActive, bool isFocused) =>
        {
            animationClips[index] = (AnimationClip)EditorGUI.ObjectField(
                new Rect(rect.x, rect.y, rect.width, EditorGUIUtility.singleLineHeight),
                animationClips[index], typeof(AnimationClip), false);
        };

        animationClipList.elementHeight = EditorGUIUtility.singleLineHeight + 6;
    }

    private void OnGUI()
    {
        GUILayout.Label("Add Animation Event", EditorStyles.boldLabel);

        animationClipList.DoLayoutList();

        functionName = EditorGUILayout.TextField("Function Name", functionName);
        frameRate = EditorGUILayout.FloatField("Frame Rate", frameRate);
        frameIndex = EditorGUILayout.IntField("Frame Index", frameIndex);

        parameterType = EditorGUILayout.Popup("Parameter Type", parameterType == "string" ? 0 : parameterType == "int" ? 1 : 2, new string[] { "string", "int", "float" }) switch
        {
            0 => "string",
            1 => "int",
            2 => "float",
            _ => parameterType
        };

        switch (parameterType)
        {
            case "string":
                stringParameter = EditorGUILayout.TextField("String Parameter", stringParameter);
                break;
            case "int":
                intParameter = EditorGUILayout.IntField("Int Parameter", intParameter);
                break;
            case "float":
                floatParameter = EditorGUILayout.FloatField("Float Parameter", floatParameter);
                break;
        }

        // Handle Drag and Drop
        Rect dropArea = GUILayoutUtility.GetRect(0.0f, 50.0f, GUILayout.ExpandWidth(true));
        GUI.Box(dropArea, "Drag and Drop Animation Clips Here");
        HandleDragAndDrop(dropArea);

        if (GUILayout.Button("Add Event"))
        {
            if (animationClips.Count > 0)
            {
                foreach (var clip in animationClips)
                {
                    if (clip != null)
                    {
                        AddEventToClip(clip, functionName, frameRate, frameIndex, parameterType, stringParameter, intParameter, floatParameter);
                    }
                }
            }
            else
            {
                EditorUtility.DisplayDialog("Error", "Please add at least one Animation Clip.", "OK");
            }
        }
    }

    private void HandleDragAndDrop(Rect dropArea)
    {
        Event evt = Event.current;
        switch (evt.type)
        {
            case EventType.DragUpdated:
            case EventType.DragPerform:
                if (!dropArea.Contains(evt.mousePosition))
                    return;

                DragAndDrop.visualMode = DragAndDropVisualMode.Copy;
                if (evt.type == EventType.DragPerform)
                {
                    DragAndDrop.AcceptDrag();

                    foreach (Object draggedObject in DragAndDrop.objectReferences)
                    {
                        if (draggedObject is AnimationClip)
                        {
                            animationClips.Add((AnimationClip)draggedObject);
                        }
                    }
                    Repaint();
                }
                break;
        }
    }

    private void AddEventToClip(AnimationClip clip, string functionName, float frameRate, int frameIndex, string parameterType, string stringParameter, int intParameter, float floatParameter)
    {
        AnimationEvent animationEvent = new AnimationEvent
        {
            time = frameIndex / frameRate,
            functionName = functionName
        };

        switch (parameterType)
        {
            case "string":
                animationEvent.stringParameter = stringParameter;
                break;
            case "int":
                animationEvent.intParameter = intParameter;
                break;
            case "float":
                animationEvent.floatParameter = floatParameter;
                break;
        }

        AnimationEvent[] events = AnimationUtility.GetAnimationEvents(clip);
        List<AnimationEvent> eventList = new List<AnimationEvent>(events) { animationEvent };
        AnimationUtility.SetAnimationEvents(clip, eventList.ToArray());

        EditorUtility.SetDirty(clip);
        AssetDatabase.SaveAssets();
        EditorUtility.DisplayDialog("Success", "Animation event added!", "OK");
    }
}
