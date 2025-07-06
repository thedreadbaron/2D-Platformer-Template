using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class EditorLineUpdate : MonoBehaviour
{
    public LineRenderer line;
    public Transform StartPoint;
    public Transform EndPoint;
 
    void Awake()
    {
        line.SetPosition(0, StartPoint.localPosition);
    }

    void Update()
    {
        line.SetPosition(1, EndPoint.localPosition);
    }
}
