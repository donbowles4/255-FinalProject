using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterCollision : MonoBehaviour
{
    public int scoreIncrement = 10; //score per barrel
    public string targetTag = "Collectible"; //placeholder tag for col check

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag(targetTag))
        {
            ScoreManager scoreManager = FindObjectOfType<ScoreManager>();
            if (scoreManager != null)
            {
                scoreManager.AddScore(scoreIncrement);
            }

            //destroy barrel
            Destroy(collision.gameObject);
        }
    }
}
