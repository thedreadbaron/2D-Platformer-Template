using UnityEngine;

public class SetSizeToSceenWidth : MonoBehaviour
{
    private RectTransform rectTransform;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        int screenWidth = Screen.width;
        int screenHeight = Screen.height;
        rectTransform.sizeDelta = new Vector2(screenWidth, screenHeight);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
