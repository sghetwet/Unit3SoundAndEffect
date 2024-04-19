using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SocialPlatforms.Impl;

public class PlayerController : MonoBehaviour
{
    private Rigidbody playerRb;
    public AudioClip jumpSound;
    public AudioClip crashSound;
    private AudioSource playerAudio;
    public  ParticleSystem explosionParticle;
    private Animator playerAnim;
    public ParticleSystem dirtParticle;
    public float jumpForce = 10;
    public float gravityModifier;
    public bool isOnGround = true;
    public bool gameOver;
    public bool canDoubleJump;
    public float dash = 2;
    public bool isdashing = false;
    private float animSpeed;
    public bool startGame = false;
    public float Score = 0;
    public GameObject[] obstacle;
    public MoveLeft backgroundSpeed;
    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        Physics.gravity *= gravityModifier;
        playerAnim = GetComponent<Animator>();
        playerAudio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        Score += 1 * Time.deltaTime;
        if (Input.GetKeyDown(KeyCode.Space) && isOnGround && !gameOver)
            // the exclemation is ONLY for boolians (i think)
        {
            playerRb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isOnGround = false;
            playerAnim.SetTrigger("Jump_trig");
            dirtParticle.Stop();
            playerAudio.PlayOneShot(jumpSound, 0.3f);
            canDoubleJump = true;
        }
        if (Input.GetKeyDown(KeyCode.Space) && playerRb.velocity.y > 0f && canDoubleJump && !gameOver)
        {
            canDoubleJump = false;
            playerRb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            playerAnim.SetTrigger("Jump_trig");
            playerAudio.PlayOneShot(jumpSound, 1);
        }
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            animSpeed = playerAnim.GetFloat("Speed_f") * dash;
            playerAnim.SetFloat("Speed_f", animSpeed);
            isdashing = true;
        }
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            playerAnim.SetFloat("Speed_f", animSpeed / dash);
            isdashing = false;
        }
    }
    private void OnCollisionEnter(Collision collision)
    {

        if (collision.gameObject.CompareTag("ground"))
        {
            isOnGround = true;
            dirtParticle.Play();
        } else if (collision.gameObject.CompareTag("obsticale"))
        {
            Debug.Log("Game Over!");
            gameOver = true;
            playerAnim.SetBool("Death_b", true);
            playerAnim.SetInteger("DeathType_int", 1);
            explosionParticle.Play();
            dirtParticle.Stop();
            playerAudio.PlayOneShot(crashSound, 1.0f);
            Debug.Log("score" + Score);
        }
    }
}
