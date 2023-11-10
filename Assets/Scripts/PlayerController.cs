using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody playerRB;
    private Animator playerAnim;
    public ParticleSystem explosionPart;
    public ParticleSystem dirtPart;
    private AudioSource playerAudio;
    public AudioClip crashSound;
    public AudioClip jumpSound;
    public float gravityModifier = 2f;
    public float jumpForce = 14.0f;
    public bool IsOnGround = true;
    public bool gameOver = false;
    public float soundFXVolume = 1.0f;
    
    // Start is called before the first frame update
    void Start()
    {
        playerAudio = GetComponent<AudioSource>();
        playerRB = GetComponent<Rigidbody>();
        playerAnim = GetComponent<Animator>();
        Physics.gravity *= gravityModifier;
    }

    // Update is called once per frame
    void Update()
    {
       
        if (Input.GetKeyDown(KeyCode.Space) && IsOnGround && !gameOver) {

            playerRB.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            IsOnGround = false;
            playerAnim.SetTrigger("Jump_trig");
            dirtPart.Stop();
            playerAudio.PlayOneShot(jumpSound,soundFXVolume);
        }
    }

    private void OnCollisionEnter (Collision collision){

        if (collision.gameObject.CompareTag("Ground")){
            IsOnGround = true;
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
