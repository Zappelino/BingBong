using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemCollector : MonoBehaviour
{
    //Creat counting variable
    private int cherries = 0;

    //Referenz to text displaying the counter
    [SerializeField] private Text cherriesText;

    //Adding option to add sound effect
    [SerializeField] private AudioSource collectionSoundEffect;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Cherry"))
        {
            //Play collection sound effect
            collectionSoundEffect.Play();

            //Delet collected item
            Destroy(collision.gameObject);

            //Add +1 to the item counter
            cherries++; //is short hand for "cherries = cherries +1" 
            
            //Update the text displaying the counter
            cherriesText.text = "Cherries: " + cherries;
        }
    }
}
