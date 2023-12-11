using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class MoveLeft : MonoBehaviour
{
    private float moveSpeed = 15f;
    private float normalMoveSpeed = 15f;
    private float dashSpeed = 30f;
    private PlayerController playerControllerscript;
    private float leftBound = -15f;
    // Start is called before the first frame update
    void Start()
    {
        playerControllerscript = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (playerControllerscript.IsIntro){
            moveSpeed = 0;
        } else {
            if(playerControllerscript.IsDashing){
                moveSpeed = dashSpeed;
                //Debug.Log("using dash speed");
            } else {
                moveSpeed = normalMoveSpeed;
            }
            if (playerControllerscript.gameOver == false){
                transform.Translate(Vector3.left * moveSpeed * Time.deltaTime);

            }
            if (transform.position.x < leftBound && gameObject.CompareTag("Obstacle")){
                Destroy(gameObject);
            }

        }
    }
}
