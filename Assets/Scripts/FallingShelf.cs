using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingShelf : MonoBehaviour
{
    [Header("Component")]
    [SerializeField] Rigidbody rb;

    [Header("Conditions")]
    [SerializeField] float maxTime;
    [SerializeField] float minTime;
    float triggerTime;

    void Start()
    {
        triggerTime = Random.Range(minTime, maxTime);
        Debug.Log(triggerTime);
        StartCoroutine(FallShelf());
    }

    void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Bullet"))
        {
            StopAllCoroutines();
            rb.isKinematic = false;
        }
    }

    IEnumerator FallShelf()
    {
        yield return new WaitForSeconds(triggerTime);
        rb.isKinematic = false;
    }
}
