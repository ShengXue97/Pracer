/*
 * Copyright (c) 2017 Razeware LLC
 * 
 * Permission is hereby granted, free of charge, to any person obtaining a copy
 * of this software and associated documentation files (the "Software"), to deal
 * in the Software without restriction, including without limitation the rights
 * to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
 * copies of the Software, and to permit persons to whom the Software is
 * furnished to do so, subject to the following conditions:
 * 
 * The above copyright notice and this permission notice shall be included in
 * all copies or substantial portions of the Software.
 *
 * Notwithstanding the foregoing, you may not use, copy, modify, merge, publish, 
 * distribute, sublicense, create a derivative work, and/or sell copies of the 
 * Software in any work that is designed, intended, or marketed for pedagogical or 
 * instructional purposes related to programming, coding, application development, 
 * or information technology.  Permission for such use, copying, modification,
 * merger, publication, distribution, sublicensing, creation of derivative works, 
 * or sale is expressly withheld.
 *    
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
 * IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
 * FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
 * AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
 * LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
 * OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
 * THE SOFTWARE.
 */

using UnityEngine;
using System.Collections;
using Fungus;
[System.Serializable]
public class Wave
{
    public string[] enemies;
    public float spawnInterval = 2;
}

public class SpawnEnemy : MonoBehaviour
{
    public GameObject[] OpenSpots;
    public GameObject[] Backgrounds;
    public Flowchart flowchart;
    public GameObject flowchartObj;

    public GameObject normalEnemy;
    public GameObject fireEnemy;
    public GameObject waterEnemy;
    public GameObject grassEnemy;
    public GameObject enemyPrefab;

    public GameObject[] waypoints;
    public GameObject testEnemyPrefab;

    public Wave[] waves;
    public int timeBetweenWaves = 5;

    private GameManagerBehavior gameManager;

    private float lastSpawnTime;
    public int enemiesSpawned = 0;
    public int currentWaveIndex = 1;
    public bool waterDialogueStarted = false;

    public bool d2aStarted = false;
    public bool d3aStarted = false;
    // Use this for initialization
    void Start()
    {
        lastSpawnTime = Time.time;
        gameManager = GameObject.Find("GameManager").GetComponent<GameManagerBehavior>();

        flowchart = gameManager.flowchart;
        flowchartObj = gameManager.flowchartObj;
        Backgrounds = gameManager.Backgrounds;
    }

    // Update is called once per frame
    void Update()
    {
        // 1
        int currentWave = gameManager.Wave;
        if (currentWaveIndex == 2 && enemiesSpawned == 0 && !d2aStarted)
        {
            d2aStarted = true;
            Fungus.Flowchart.BroadcastFungusMessage("2a");
        }

        if (currentWaveIndex == 3 && enemiesSpawned == 0 && !d3aStarted && GameObject.FindGameObjectWithTag("Enemy") == null)
        {
            d3aStarted = true;
            Fungus.Flowchart.BroadcastFungusMessage("3a");
        }
        if (currentWave < waves.Length)
        {
            
            // 2
            float timeInterval = Time.time - lastSpawnTime;
            float spawnInterval = waves[currentWave].spawnInterval;
            if (((enemiesSpawned == 0 && timeInterval > timeBetweenWaves) ||
                 timeInterval > spawnInterval) &&
                enemiesSpawned < waves[currentWave].enemies.Length)
            {
                // 3  
                lastSpawnTime = Time.time;
                enemyPrefab = normalEnemy;
                if (waves[currentWave].enemies[enemiesSpawned] == "normal")
                {
                    enemyPrefab = normalEnemy;
                } else if (waves[currentWave].enemies[enemiesSpawned] == "water")
                {
                    enemyPrefab = waterEnemy;
                } else if (waves[currentWave].enemies[enemiesSpawned] == "fire")
                {
                    enemyPrefab = fireEnemy;
                } else if (waves[currentWave].enemies[enemiesSpawned] == "grass")
                {
                    enemyPrefab = grassEnemy;
                }

                //Pause if don't allow to spawn the enemy yet
                if (!flowchart.GetBooleanVariable("canSpawnFire") && enemiesSpawned == 0 && currentWaveIndex == 2)
                {

                } else if(enemiesSpawned == 4 && currentWaveIndex == 2 && GameObject.FindGameObjectWithTag("Enemy") == null && !waterDialogueStarted)
                {
                    print("can start water dia");
                    waterDialogueStarted = true;
                    Fungus.Flowchart.BroadcastFungusMessage("2b");
                } else if (!flowchart.GetBooleanVariable("canSpawnWater") && enemiesSpawned == 4 && currentWaveIndex == 2)
                {

                }
                else if (!flowchart.GetBooleanVariable("canSpawnGrass") && enemiesSpawned == 0 && currentWaveIndex == 3)
                {

                }
                else
                {
                    GameObject newEnemy = (GameObject)Instantiate(enemyPrefab);
                    newEnemy.GetComponent<MoveEnemy>().waypoints = gameManager.AllWayPoints[currentWaveIndex - 1].waypoints;
                    enemiesSpawned++;
                }
                
            }
            // 4 
            if (enemiesSpawned == waves[currentWaveIndex - 1].enemies.Length &&
                GameObject.FindGameObjectWithTag("Enemy") == null)
            {
                currentWaveIndex++;
                gameManager.Wave++;
                gameManager.Gold = Mathf.RoundToInt(gameManager.Gold * 1.1f);
                enemiesSpawned = 0;
                lastSpawnTime = Time.time;

                GameObject OpenSpotsListPrev = OpenSpots[currentWaveIndex - 2];
                GameObject OpenSpotsListNext = OpenSpots[currentWaveIndex - 1];
                OpenSpotsListPrev.SetActive(false);
                OpenSpotsListNext.SetActive(true);

                GameObject BackgroundPrev = Backgrounds[currentWaveIndex - 2];
                GameObject BackgroundNext = Backgrounds[currentWaveIndex - 1];
                BackgroundPrev.SetActive(false);
                BackgroundNext.SetActive(true);
            }
            // 5 
        }
        else
        {
            gameManager.gameOver = true;
            GameObject gameOverText = GameObject.FindGameObjectWithTag("GameWon");
            gameOverText.GetComponent<Animator>().SetBool("gameOver", true);
        }
    }

}
