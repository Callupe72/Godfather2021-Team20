using System.Collections;
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

    private void Start()
    {
        wichCoin = Random.Range(0, coin.Count);
        coinPosition = coin[wichCoin].transform.position - transform.position;

    }

    // Update is called once per frame
    void Update()
    {
        coinPosition = coin[wichCoin].transform.position - transform.position; //Peut �tre en faire une liste pour qu'ils foncent sur un al�atoire
        coinPosition.Normalize();

        transform.LookAt(coin[wichCoin].transform);

        Vector3 dir = coinPosition;
        dir = new Vector3(dir.x, 0, dir.z);

        rb.velocity = dir * speed;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Coin")
        {
            Destroy(gameObject);
        }
    }
}
