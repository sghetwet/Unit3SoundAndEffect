using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

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
        if (Input.GetKeyDown(KeyCode.Space) && isOnGround && !gameOver)
            // the exclemation is ONLY for boolians (i think)
        {
            playerRb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isOnGround = false;
            playerAnim.SetTrigger("Jump_trig");
            dirtParticle.Stop();
            playerAudio.PlayOneShot(jumpSound, 0.3f);
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
        }
    }
}
