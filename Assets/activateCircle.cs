using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class activateCircle : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnMouseEnter()
    {
        GetComponent<SpriteRenderer>().enabled = true;
    }

    void OnMouseExit()
    {
        GetComponent<SpriteRenderer>().enabled = false;
    }
}
