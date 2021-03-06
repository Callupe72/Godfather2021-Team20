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

    [SerializeField] private Animator animator;

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

        foreach (BabyMovement babies in FindObjectsOfType<BabyMovement>())
        {
            int i = Random.Range(0, 3);
            AudioManager.instance.Play3DSound("BabyLaught" + i, babies.transform.position);
        }

        StartRinging();
    }

    void StartRinging()
    {        
        actualModifierSpeed = multiplierSpeedFactor;
        actualModifierFight = multiplierFightFactor;
        AudioManager.instance.Play3DSound("PhoneRinging", transform.position);
        animator.SetBool("isRingging", true);        
    }

    public void Hit()
    {
        AudioManager.instance.StopSound("PhoneRinging");
        animator.SetBool("isRingging", false);
        actualModifierSpeed = 1;
        actualModifierFight = 1;
        StartCoroutine(Ring());
    }
}