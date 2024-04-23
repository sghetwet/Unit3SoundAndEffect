using System.Collections;
using System.Collections.Generic;
using UnityEditor.ShaderKeywordFilter;
using UnityEngine;

public class PlayerControllerX : MonoBehaviour
{
    public bool gameOver;

    public float floatForce;
    private float gravityModifier = 1.5f;
    private Rigidbody playerRb;

    public ParticleSystem explosionParticle;
    public ParticleSystem fireworksParticle;

    private AudioSource playerAudio;
    public AudioClip moneySound;
    public AudioClip explodeSound;
    public bool isLowEnough;


    // Start is called before the first frame update
    void Start()
    {

        //ALWAYS make sure to add this code most likely
        playerRb = GetComponent<Rigidbody>();
        Physics.gravity *= gravityModifier;
        playerAudio = GetComponent<AudioSource>();

        // Apply a small upward force at the start of the game
        playerRb.AddForce(Vector3.up * 5, ForceMode.Impulse);

    }

    // Update is called once per frame
    void Update()
    {
        // While space is pressed and player is low enough, float up
        if (Input.GetKey(KeyCode.Space) && !gameOver)
        {
            //you add forcemode.impulse for it to go up, like it does in the void start, and by deltatime so it isn't uncontrollable
            playerRb.AddForce(Vector3.up * floatForce * Time.deltaTime, ForceMode.Impulse);
            if (transform.position.y > 13)
            {
                isLowEnough = true;
                if (isLowEnough == true)
                {
                    playerRb.velocity = Vector3.zero;
                }else if (transform.position.y < 13) { isLowEnough = false; }
                if (Input.GetKey(KeyCode.Space) && !gameOver && isLowEnough)
                {
                    playerRb.AddForce(Vector3.up * Time.deltaTime * floatForce, ForceMode.Impulse);
                }
                if (transform.position.y < 0 && !gameOver)
                {

                }
                {

                }
            }
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        // if player collides with bomb, explode and set gameOver to true
        if (other.gameObject.CompareTag("Bomb"))
        {
            explosionParticle.Play();
            playerAudio.PlayOneShot(explodeSound, 1.0f);
            gameOver = true;
            Debug.Log("Game Over!");
            Destroy(other.gameObject);
        } 

        // if player collides with money, fireworks
        else if (other.gameObject.CompareTag("Money"))
        {
            fireworksParticle.Play();
            playerAudio.PlayOneShot(moneySound, 1.0f);
            Destroy(other.gameObject);

        }

    }

}
