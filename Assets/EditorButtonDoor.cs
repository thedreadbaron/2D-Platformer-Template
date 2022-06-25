using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EditorButtonDoor : MonoBehaviour
{
    public Transform door;
    public Transform button;

    void OnDrawGizmos () {
        Gizmos.color = Color.blue;
        Gizmos.DrawLine (door.position, button.position);
    }

    void OnDrawGizmosSelected () {
        Gizmos.color = Color.cyan;
        Gizmos.DrawLine (door.position, button.position);
    }
    
}
