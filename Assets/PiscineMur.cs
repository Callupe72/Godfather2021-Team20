using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PiscineMur : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log(collision.gameObject.tag);
        if(collision.gameObject.CompareTag("Bullet"))
            gameObject.GetComponent<Joint>().breakForce = 0;

        if (collision.gameObject.CompareTag("Baby"))
            gameObject.GetComponent<Joint>().breakForce = 0;
    }
}
