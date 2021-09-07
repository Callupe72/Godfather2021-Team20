using System.Collections.Generic;
using UnityEngine;

public class BabyMovement : MonoBehaviour
{
    [Header("Component")]
    [SerializeField] Rigidbody rb;
    public List<GameObject> coin = new List<GameObject>();
    int wichCoin;

    [Header("Movement")]
    [SerializeField] float speed;
    private Vector3 coinPosition;

    [Header("Condition")]
    [SerializeField] bool wantToDie = false;
    GameObject nearestCoin = null;
    [SerializeField] bool hasToChangeDir;

    private void Start()
    {
        coinPosition = Vector3.forward;

    }

    // Update is called once per frame
    void Update()
    {  
        rb.velocity = coinPosition * speed;

        //Si le bebe change de dir et veut die
        if (hasToChangeDir && wantToDie)
        {
            float coinTested;
            for (int i = 0; i < coin.Count; i++)//pour chaques coin test si il est plus proche que les autres
            {
                coinTested = Vector3.Distance(coin[i].transform.position, transform.position);
                nearestCoin = coin[wichCoin];
                if (coinTested < Vector3.Distance(nearestCoin.transform.position, transform.position))
                {
                    nearestCoin = coin[i];
                }

                hasToChangeDir = false;
            }
            rb.velocity = Vector3.zero;
            coinPosition = new Vector3 (nearestCoin.transform.position.x, nearestCoin.transform.position.y, nearestCoin.transform.position.z);
            transform.LookAt(coin[wichCoin].transform);
        }


        RaycastHit hit;
        Debug.DrawRay(transform.position, Vector3.down * 100);
        if (Physics.Raycast(transform.position, Vector3.down, out hit, 100))
        {
            transform.position = new Vector3(transform.position.x, hit.point.y + 1, transform.position.z);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Coin")
        {
            Destroy(gameObject);
        }
    }
}
