using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Corners : MonoBehaviour
{
    public float cornerRadius = 2f;
    public float timeBeforeKill = 1f;
    [SerializeField] GameObject eclairAll;
    public CameraShakes CameraShakes;

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, cornerRadius);
    }

    void Update()
    {
        foreach (Collider item in Physics.OverlapSphere(transform.position, cornerRadius))
        {
            if (item.gameObject.CompareTag("Baby"))
            {
                if (!item.GetComponent<BabyMovement>().willIDie)
                {
                    item.GetComponent<BabyMovement>().willIDie = true;
                    AudioManager.instance.Play3DSound("BabyElect", transform.position);
                    if (!item.GetComponent<TestIndicator>())
                        item.gameObject.AddComponent<TestIndicator>();
                    item.GetComponent<TestIndicator>().destroyTimer = timeBeforeKill / 5;
                    item.GetComponent<TestIndicator>().Register();
                    StartCoroutine(AreTheyStillHere(item.gameObject));
                    //Score
                }
            }
        }
    }

    IEnumerator AreTheyStillHere(GameObject baby)
    {
        yield return new WaitForSeconds(timeBeforeKill);
        foreach (Collider item in Physics.OverlapSphere(transform.position, cornerRadius))
        {
            if (item.gameObject.CompareTag("Baby"))
            {
                if (item.GetComponent<BabyMovement>().willIDie)
                {
                    AudioManager.instance.Play3DSound("BabyElect", item.transform.position);
                    GameObject eclair = Instantiate(eclairAll, transform.position, Quaternion.identity);
                    eclair.GetComponent<ParticleSystem>().Play();
                    Destroy(eclair, .5f);
                    item.GetComponent<BabyMovement>().Die();
                }
            }
        }

    }
}
