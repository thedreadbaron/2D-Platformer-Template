using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantiateTest : MonoBehaviour
{
    public GameObject objectToInstantiate;
    public GameObject parent;
    // Start is called before the first frame update
    void Start()
    {
        Instantiate(objectToInstantiate, new Vector3(10, 5, 0), Quaternion.identity, parent.transform);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
