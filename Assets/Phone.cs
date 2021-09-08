using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Phone : MonoBehaviour
{
    public float timeMinBeforeRing = 10f;
    public float timeMaxBeforeRing = 30f;
    void Start()
    {
        StartCoroutine(Ring());
    }
    IEnumerator Ring()
    {
        float randomTime = Random.Range(timeMinBeforeRing, timeMaxBeforeRing);
        yield return new WaitForSeconds(randomTime);

        StartRinging();
    }

    private void StartRinging()
    {
    }
}
