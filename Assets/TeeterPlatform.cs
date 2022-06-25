using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeeterPlatform : MonoBehaviour
{
    public SpringJoint2D springJoint;

    void OnTriggerEnter2D(Collider2D other)
    {
        springJoint.frequency = 0.02f;
    }

    void OnTriggerExit2D(Collider2D other)
    {
        springJoint.frequency = 0.2f;
    }
}
