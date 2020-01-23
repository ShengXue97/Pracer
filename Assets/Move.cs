
using UnityEngine;
using System.Collections;

public class Move : MonoBehaviour
{
    public bool mouseOver = false;
    private Vector3 mousePosition;
    public float moveSpeed = 0.8f;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0) && mouseOver)
        {
            mousePosition = Input.mousePosition;
            mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
            transform.position = Vector2.Lerp(transform.position, mousePosition, moveSpeed);
        }

    }

    // The mesh goes red when the mouse is over it...
    void OnMouseEnter()
    {
        mouseOver = true;
    }

    // The mesh goes red when the mouse is over it...
    void OnMousOver()
    {
        mouseOver = true;
    }

    // ...and the mesh finally turns white when the mouse moves away.
    void OnMouseExit()
    {
        StartCoroutine(ExecuteAfterTime());
    }

    IEnumerator ExecuteAfterTime()
    {
        yield return new WaitForSeconds(0.2f);

        // Code to execute after the delay
        mouseOver = false;
    }
}
