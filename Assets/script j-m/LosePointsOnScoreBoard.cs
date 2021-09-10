using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LosePointsOnScoreBoard : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.transform.name == "AllTheCoin")
        {
            KeepScore.Score -= 10;
            Destroy(gameObject);
        }
    }
}
