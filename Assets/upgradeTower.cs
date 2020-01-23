using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class upgradeTower : MonoBehaviour
{
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
        if (CanUpgradeMonster())
        {
            transform.parent.gameObject.GetComponent<PlaceMonster>().monster.GetComponent<MonsterData>().increaseLevel();
            AudioSource audioSource = gameObject.GetComponent<AudioSource>();
            audioSource.PlayOneShot(audioSource.clip);

            gameManager.Gold -= transform.parent.gameObject.GetComponent<PlaceMonster>().monster.GetComponent<MonsterData>().CurrentLevel.cost;
        }
    }

    private bool CanUpgradeMonster()
        {
            if (transform.parent.gameObject.GetComponent<PlaceMonster>().monster != null)
            {
                MonsterData monsterData = transform.parent.gameObject.GetComponent<PlaceMonster>().monster.GetComponent<MonsterData>();
                MonsterLevel nextLevel = monsterData.getNextLevel();
                if (nextLevel != null)
                {
                    return gameManager.Gold >= nextLevel.cost;
                }
            }
            return false;
        }
}
