using UnityEngine;
using UnityEngine.InputSystem;

public class CameraControllerMenu : MonoBehaviour
{
    public float strength = 10f;
    
    private Camera camera;

    void Start()
    {
        camera = GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 mousePos = Mouse.current.position.ReadValue();
        int screenWidth = Screen.width;
        int screenHeight = Screen.height;

        camera.transform.position = new Vector3((-0.5f + (mousePos.x / screenWidth)) * strength, (-0.5f + (mousePos.y / screenHeight)) * strength, -100);
        camera.transform.LookAt(Vector3.zero);
    }
}
