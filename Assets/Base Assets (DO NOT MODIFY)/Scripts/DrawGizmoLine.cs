using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawGizmoLine : MonoBehaviour
{

    public Transform objectOne;
    public Transform objectTwo;

    void OnDrawGizmos () {
        Gizmos.color = Color.blue;
        Gizmos.DrawLine (objectOne.position, objectTwo.position);
    }

    void OnDrawGizmosSelected () {
        Gizmos.color = Color.cyan;
        Gizmos.DrawLine (objectOne.position, objectTwo.position);
    }
    
}