using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Corners : MonoBehaviour
{
    public float cornerRadius = 2f;
    public float timeBeforeKill = 1f;
    public List<BabyMovement> childs = new List<BabyMovement>();
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
                childs.Add(item.GetComponent<BabyMovement>());
                StartCoroutine(AreTheyStillHere(item.gameObject));
                //Score
            }
        }
    }

    IEnumerator AreTheyStillHere(GameObject baby)
    {
        yield return new WaitForSeconds(timeBeforeKill);
        GameObject eclair = Instantiate(eclairAll, transform.position, Quaternion.identity);
        eclair.GetComponent<ParticleSystem>().Play();
        Destroy(eclair, .5f);
        StartCoroutine(CameraShakes.Shake(.15f, .4f));
        Destroy(baby);
        //foreach (Collider item in Physics.OverlapSphere(transform.position, cornerRadius))
        //{
        //    for (int i = 0; i < childs.Count; i++)
        //    {
        //        if(item == childs[i])
        //        {
        //            Destroy(item.gameObject);
        //            GameObject eclair = Instantiate(eclairAll, transform.position, Quaternion.identity);
        //            eclair.GetComponent<ParticleSystem>().Play();
        //            AudioManager.instance.Play3DSound("Babyelect", transform.position);
        //            AudioManager.instance.Play3DSound("BabyDisparition", transform.position);
        //            FindObjectOfType<Spawn>().SpawnABaby();
        //            childs.Clear();

        //        }
        //    }
        //}

    }
}
