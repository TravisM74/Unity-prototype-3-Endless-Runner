using System;
using System.Collections;
using System.Collections.Generic;

using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class PlayerController : MonoBehaviour
{
    private Rigidbody playerRB;
    private Animator playerAnim;
    public ParticleSystem explosionPart;
    public ParticleSystem dirtPart;
    private AudioSource playerAudio;
    public AudioClip crashSound;
    public AudioClip jumpSound;

    public TextMeshProUGUI scoreText;
    public float gravityModifier = 2f;
    public float jumpForce = 14.0f;
    public bool IsOnGround = true;
    public bool IsDoubleJumpUsed = false;
    public bool gameOver = false;
    public float soundFXVolume = 1.0f;
    public bool IsDashing = false;
    public bool IsIntro = true;
    private int playerScore = 0; 

    private Vector3 gamePoint;
    private Vector3 startPosition = new Vector3(-5,0,0);
    private float walkSpeed = 1.1f;

    
    // Start is called before the first frame update
    void Start()
    {
        gamePoint = transform.position;
        transform.position = startPosition;
        playerScore = 0;
        playerAudio = GetComponent<AudioSource>();
        playerRB = GetComponent<Rigidbody>();
        playerAnim = GetComponent<Animator>();
        Physics.gravity *= gravityModifier;
    }

    // Update is called once per frame
    void Update()
    {
        if (IsIntro) {
            playerAnim.SetFloat("Speed_f", .4f);
            //Debug.Log(transform.position.x + "   " + gamePoint.x);
            dirtPart.Stop();
            if (transform.position.x  < gamePoint.x){
                transform.position += gamePoint * walkSpeed * Time.deltaTime;

            } else {
                IsIntro = false;
                playerAnim.SetFloat("Speed_f", .9f);
            }

        } else {
            
            PlayerJumpTest();
            PlayerDashTest();
            PlayerScoring();
        }
        
    }
    private void PlayerDashTest(){
        if (Input.GetKey(KeyCode.D) && IsOnGround){
            IsDashing = true;
            
            //Debug.Log("Dashing !");
        } else {
            IsDashing = false;
        }
    }
    private void PlayerJumpTest(){
         if (Input.GetKeyDown(KeyCode.Space) && (IsOnGround || !IsDoubleJumpUsed) && !gameOver) {

                playerRB.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
                if (!IsOnGround && !IsDoubleJumpUsed){
                    IsDoubleJumpUsed = true;
                }
                IsOnGround = false;
                playerAnim.SetTrigger("Jump_trig");
                dirtPart.Stop();
                playerAudio.PlayOneShot(jumpSound,soundFXVolume);
            }
    }
    
    private void PlayerScoring(){
        if (!gameOver && !IsIntro){
            //basic point
            playerScore++;
            // in air point
            if (!IsOnGround){
                playerScore++;
            }
            //dashing bonus point
            if (IsDashing){
                playerScore++;
            }
            scoreText.text = "Score : " + playerScore;

        }

    }
    private void OnCollisionEnter (Collision collision){

        if (collision.gameObject.CompareTag("Ground")){
            IsOnGround = true;
            IsDoubleJumpUsed = false;
            dirtPart.Play();
        } else if (collision.gameObject.CompareTag("Obstacle")){
            gameOver = true;
            playerAnim.SetInteger("DeathType_int", 1);
            playerAnim.SetBool("Death_b", true);
            Debug.Log("GameOver");
            explosionPart.Play();
            dirtPart.Stop();
            playerAudio.PlayOneShot(crashSound, soundFXVolume);
        }
       
        
    } 
}
