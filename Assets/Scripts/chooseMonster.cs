using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class chooseMonster : MonoBehaviour
{
    public GameObject selectedMonster;
    private GameManagerBehavior gameManager;
    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManagerBehavior>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnMouseDown()
    {
        // this object was clicked - do something
        gameManager.selectedMonster = selectedMonster;
    }
}
