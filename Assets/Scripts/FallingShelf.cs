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


    // Start is called before the first frame update
    void Start()
    {
        triggerTime = Random.Range(minTime, maxTime);
        Debug.Log(triggerTime);
        StartCoroutine(FallShelf());
    }

    IEnumerator FallShelf()
    {
        yield return new WaitForSeconds(triggerTime);
        rb.isKinematic = false;
    }
}
