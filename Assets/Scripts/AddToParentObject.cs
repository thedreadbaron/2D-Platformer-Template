using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]

public class AddToParentObject : MonoBehaviour
{
    public string parentName;
    private GameObject parentObject;

    void Awake()
    {
        parentObject = GameObject.Find(parentName);
        if (transform.parent != parentObject.transform)
        {
            transform.parent = parentObject.transform;
        }
    }  
}
