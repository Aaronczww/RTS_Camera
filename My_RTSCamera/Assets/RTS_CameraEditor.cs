using System.Collections;
using System.Collections.Generic;

#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;

[CustomEditor(typeof(RTS_Camera))]
public class RTS_CameraEditor : Editor {

    private int select = 0;
    public override void OnInspectorGUI()
    {
        GUIStyle gUIStyle = new GUIStyle();
        gUIStyle.fontSize = 14;
        gUIStyle.fontStyle = FontStyle.Bold;

        GUIStyle ToggleStyle = new GUIStyle();
        ToggleStyle.fontSize = 12;
        ToggleStyle.fontStyle = FontStyle.Bold;

        string[] barResources = { "Movement", "Rotation", "Height"};

        RTS_Camera script = target as RTS_Camera;

        select = GUILayout.Toolbar(select,barResources);

        switch (select)
        {
            case 0:
                GUILayout.Label("Movement", gUIStyle);
                GUIContent gUIContent = new GUIContent();

                gUIContent.text = "Use Keyboard Input:";

                script.moveKeyboarInput_bool = EditorGUILayout.Toggle(gUIContent,script.moveKeyboarInput_bool);
                script.horizontalAxisName = EditorGUILayout.TextField("Horizontal Axis Name:", script.horizontalAxisName);
                script.verticalAxisName = EditorGUILayout.TextField("Vetical Axis Name:", script.verticalAxisName);
                script.keyboardMovementSpeed = EditorGUILayout.FloatField("Keyboard Movement Speed:", script.keyboardMovementSpeed);

                script.edgeInput_bool = EditorGUILayout.Toggle("Screen edge input:", script.edgeInput_bool);
                script.panningWithMouse_bool = EditorGUILayout.Toggle("Panning with mouse:", script.panningWithMouse_bool);
                script.mouseMovementSpeed = EditorGUILayout.FloatField("Mouse Movement Speed:", script.mouseMovementSpeed);
                script.limitMovement_bool = EditorGUILayout.Toggle("Limit movement:", script.limitMovement_bool);
                script.Boundary = EditorGUILayout.Vector3Field("Limit Boundary:", script.Boundary);
                break;

            case 1:
                GUILayout.Label("Rotation:", gUIStyle);

                script.rotationKeyboardInput_bool = EditorGUILayout.Toggle("Keyboard input:", script.rotationKeyboardInput_bool);
                script.RotateLeft = (KeyCode)EditorGUILayout.EnumPopup("Rotate Left", script.RotateLeft);
                script.RotateRight = (KeyCode)EditorGUILayout.EnumPopup("Rotate Right:", script.RotateRight);
                script.RotateUp = (KeyCode)EditorGUILayout.EnumPopup("Rotate Up", script.RotateUp);
                script.RotateDown = (KeyCode)EditorGUILayout.EnumPopup("Rotate Down", script.RotateDown);

                script.KeyboardRotateSensity = EditorGUILayout.FloatField("Keyboard Rotate Sensity:", script.KeyboardRotateSensity);

                script.rotationMouseInput_bool = EditorGUILayout.Toggle("Mouse input:", script.rotationMouseInput_bool);

                script.rotateXAxisName = EditorGUILayout.TextField("Rotate XAxis Name:", script.rotateXAxisName);
                script.rotateYAxisName = EditorGUILayout.TextField("Rotate YAxis Name:", script.rotateYAxisName);


                script.MouseRotateSensity = EditorGUILayout.FloatField("Mouse Rotate Sensity:", script.MouseRotateSensity);

                break;

            case 2:
                GUILayout.Label("Height:", gUIStyle);

                //script.autoHeight = EditorGUILayout.Toggle("Auto Height:", script.autoHeight);
                //script.heightDampening = EditorGUILayout.FloatField("Height dampening:", script.heightDampening);
                //script.groundMask = EditorGUILayout.LayerField("Ground Mask:", script.groundMask);

                script.Groundmask =(RTS_Camera.MaskLayer) EditorGUILayout.EnumFlagsField("Ground Mask", script.Groundmask);

                script.keyBoardZooming_bool = EditorGUILayout.Toggle("Keyboard zooming:", script.keyBoardZooming_bool);
                script.ZoomIn = (KeyCode)EditorGUILayout.EnumPopup("Zoom In:", script.ZoomIn);
                script.ZoomOut = (KeyCode)EditorGUILayout.EnumPopup("Zoom Out:", script.ZoomOut);

                script.keyBoardSensity = EditorGUILayout.FloatField("KeyBoard Sensity:", script.keyBoardSensity);

                script.scrollwheelZooming_bool = EditorGUILayout.Toggle("Scrollwheel zooming:", script.scrollwheelZooming_bool);
                script.scrollwheelSensity = EditorGUILayout.FloatField("scrollwheelSensity:", script.scrollwheelSensity);
                GUILayout.BeginHorizontal();
                script.maxHeight = EditorGUILayout.FloatField("max Height:", script.maxHeight);
                script.minHeight = EditorGUILayout.FloatField("min Height:", script.minHeight);
                GUILayout.EndHorizontal();
                break;
        }
    }
}
