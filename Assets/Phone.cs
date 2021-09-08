using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Phone : MonoBehaviour
{
    public float timeMinBeforeRing = 20f;
    public float timeMaxBeforeRing = 30f;

    public float multiplierSpeedFactor = 1.25f;
    [HideInInspector] public float actualModifierSpeed = 1f;
    public float multiplierFightFactor = 1.25f;
    [HideInInspector] public float actualModifierFight = 1f;
    void Start()
    {
        actualModifierSpeed = 1;
        actualModifierFight = 1;
        StartCoroutine(Ring());
    }
    IEnumerator Ring()
    {
        float randomTime = Random.Range(timeMinBeforeRing, timeMaxBeforeRing);
        yield return new WaitForSeconds(randomTime);
        Debug.Log("Ring");
        StartRinging();
    }

    void StartRinging()
    {
        actualModifierSpeed = multiplierSpeedFactor;
        actualModifierFight = multiplierFightFactor;
    }

    public void Hit()
    {
        actualModifierSpeed = 1;
        actualModifierFight = 1;
        StartCoroutine(Ring());
    }
}