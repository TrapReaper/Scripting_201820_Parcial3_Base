using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolEnemies : MonoBehaviour {

    public Transform[] spawnPoints;

    private GameObject[] enemies;

    public GameObject prefabEnemy;

    public GameManager myGameManager;

    // Use this for initialization
    void Start () {

        CrearEnemigos();
		
	}

    public void CrearEnemigos()
    {
        for (int i = 0; i < 4; i++)
        {

            int randomNum = Random.Range(0, spawnPoints.Length);
            GameObject tempEnemy = Instantiate(prefabEnemy, spawnPoints[randomNum].position, spawnPoints[randomNum].rotation);
            tempEnemy.GetComponent<Movimiento>().goals = spawnPoints;
            tempEnemy.GetComponent<Enemy1>().myGameManager = myGameManager;
        }
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
