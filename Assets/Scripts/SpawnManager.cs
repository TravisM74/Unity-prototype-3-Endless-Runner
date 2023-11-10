using System.Collections;
using UnityEngine;



public class SpawnManager : MonoBehaviour
{
    public GameObject obstaclePrefab;

    // create a player controller variable
    private PlayerController playerCotrollerScript;
    private Vector3 spawnPos = new Vector3(25, 0, 0); 
    private float spawnDelay = 2;
    public float spawnInterval = 2;
    // Start is called before the first frame update
    void Start()
    {
        // asign the variable by finding the game object and getting it component 
        playerCotrollerScript = GameObject.Find("Player").GetComponent<PlayerController>();
        InvokeRepeating("SpawnObstacle", spawnDelay, spawnInterval);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void SpawnObstacle(){
        if (playerCotrollerScript.gameOver == false){
            Instantiate(obstaclePrefab, spawnPos, obstaclePrefab.transform.rotation);
        }
    }
}
