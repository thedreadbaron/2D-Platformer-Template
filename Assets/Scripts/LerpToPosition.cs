using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LerpToPosition : MonoBehaviour
{
    Vector2 positionToMoveTo;
    Vector2 startPosition;
    Transform StartPoint;
    Transform EndPoint;
    Rigidbody2D rb;
    public float duration = 5f;
    
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        StartPoint = GetComponent<Transform>();
        EndPoint = this.gameObject.transform.GetChild(0);
        startPosition = new Vector2(StartPoint.transform.position.x, StartPoint.transform.position.y);
        positionToMoveTo = new Vector2(EndPoint.transform.position.x, EndPoint.transform.position.y);
        transform.GetChild(0).gameObject.SetActive(false);
        StartCoroutine(LerpPosition(positionToMoveTo));
    }

    IEnumerator LerpPosition(Vector2 targetPosition)
    {
        float time = 0;
        while (time < duration)
        {
            float t = time / duration;
            t = t * t * (3f - 2f * t);
            rb.transform.position = Vector2.Lerp(startPosition, targetPosition, t);
            time += Time.deltaTime;
            yield return null;
        }
        rb.transform.position = targetPosition;
        StartCoroutine(LerpPosition2(startPosition));
    }

    IEnumerator LerpPosition2(Vector2 targetPosition)
    {
        float time = 0;
        while (time < duration)
        {
            float t = time / duration;
            t = t * t * (3f - 2f * t);
            rb.transform.position = Vector2.Lerp(positionToMoveTo, targetPosition, t);
            time += Time.deltaTime;
            yield return null;
        }
        rb.transform.position = targetPosition;
        StartCoroutine(LerpPosition(positionToMoveTo));
    }
    
}
