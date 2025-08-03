using UnityEngine;
using UnityEngine.InputSystem;

public class HeadTrackMouse : MonoBehaviour
{
    private RectTransform rectTransform;

    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    void Update()
    {
        Vector2 mousePos = Mouse.current.position.ReadValue();
        int screenWidth = Screen.width;

        if (mousePos.x > screenWidth / 2 && rectTransform.localScale.x != 1)
        {
            rectTransform.localScale = new Vector3 (1, 1, 1);
        }
        else if (mousePos.x <= screenWidth / 2 && rectTransform.localScale.x != -1)
        {
            rectTransform.localScale = new Vector3(-1, 1, 1);
        }
    }
}
