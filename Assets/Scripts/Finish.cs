using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Finish : MonoBehaviour
{
    private AudioSource finishSource;

    private bool levelCompleted = false;

   private void Start()
    {
        finishSource = GetComponent<AudioSource>();
    }

    //Adding functionality to the finsih line flag
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Player" && !levelCompleted)
        {
            //Playing the finish line sound
            finishSource.Play();

            //Note that the level has been completed
            levelCompleted = true;

            //Calling the completeLevel funtion with a delay so palying feels more smooth
            Invoke("CompleteLevel", 2f);
        }
    }

    //Function wich loads the next scene
    private void CompleteLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
