using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Corners : MonoBehaviour
{
    public float cornerRadius = 2f;
    public float timeBeforeKill = 1f;

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
                Destroy(item);
                item.GetComponent<Rigidbody>().isKinematic = true;
                AudioManager.instance.StopSound("BabyDie");
                AudioManager.instance.Play3DSound("BabyDie", transform.position);
                Destroy(item.gameObject, timeBeforeKill);
                FindObjectOfType<Spawn>().SpawnABaby();
                //Score
            }
        }
    }
}
