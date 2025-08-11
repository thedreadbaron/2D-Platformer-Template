using UnityEngine;

public class EnableTarget : MonoBehaviour
{
    public GameObject enableTarget;

    void Start()
    {
        enableTarget.SetActive(true);
    }
}
