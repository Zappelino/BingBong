using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Implentation of addition functions for scene management
using UnityEngine.SceneManagement;

public class PlayerLife : MonoBehaviour
{
    //Call the Ainmator and the Rigidbody of the Game Object (in this case its meant for the Player)
    private Rigidbody2D rb;
    private Animator anim;

    [SerializeField] private AudioSource deathSoundEffect;

    private void Start()
    {
        //Get the actual values for the Animator and the Rigidbody
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Check if there was a collosion with a trap
        if (collision.gameObject.CompareTag("Trap"))
        {
            Die();
        }
    }

    //Function to go through everything that happens when the player dies
    private void Die()
    {
        deathSoundEffect.Play();
        rb.bodyType = RigidbodyType2D.Static;
        anim.SetTrigger("death");
    }

    //Function to restart the current level
    private void RestarteLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
