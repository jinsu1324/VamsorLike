using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class BezierCurveTest : MonoBehaviour
{
    public GameObject go;

    public Vector3 P1;
    public Vector3 P2;
    public Vector3 P3;
    public Vector3 P4;

    [Range(0, 1)]
    public float Value;


    private void Update()
    {
        go.transform.position = BezierTest(P1, P2, P3, P4, Value);
    }


    public Vector3 BezierTest(Vector3 p1, Vector3 p2, Vector3 p3, Vector3 p4, float value)
    {
        Vector3 A = Vector3.Lerp(p1, p2, value);
        Vector3 B = Vector3.Lerp(p2, p3, value);
        Vector3 C = Vector3.Lerp(p3, p4, value);

        Vector3 D = Vector3.Lerp(A, B, value);
        Vector3 E = Vector3.Lerp(B, C, value);

        Vector3 F = Vector3.Lerp(D, E, value);

        return F;
    }
}

[CanEditMultipleObjects]
[CustomEditor(typeof(BezierCurveTest))]
public class BezierTestEditor : Editor
{
    private void OnSceneGUI()
    {
        BezierCurveTest generator = (BezierCurveTest)target;

        generator.P1 = Handles.PositionHandle(generator.P1, Quaternion.identity);
        generator.P2 = Handles.PositionHandle(generator.P2, Quaternion.identity);
        generator.P3 = Handles.PositionHandle(generator.P3, Quaternion.identity);
        generator.P4 = Handles.PositionHandle(generator.P4, Quaternion.identity);


        Handles.DrawLine(generator.P1, generator.P2);
        Handles.DrawLine(generator.P3, generator.P4);

        int count = 50;
        for (float i = 0; i < count; i++)
        {
            float value_Before = i / count;
            Vector3 before = generator.BezierTest(generator.P1, generator.P2, generator.P3, generator.P4, value_Before);

            float value_After = (i + 1) / count;
            Vector3 after = generator.BezierTest(generator.P1, generator.P2, generator.P3, generator.P4, value_After);

            Handles.DrawLine(before, after);
        }
    }
}
