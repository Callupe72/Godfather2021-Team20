using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn : MonoBehaviour
{
    [SerializeField] Transform allcoin;
    [Header("Component")]
    [SerializeField] GameObject baby;
    [SerializeField] GameObject[] spawnPoint;
    public int babyNumber = 5;

    [Header("position")]
    int wichspawnPoint;

    void Start()
    {
        for (int i = 0; i < 5; i++)
        {
            SpawnABaby();
        }
    }

    void Update()
    {
        if (FindObjectsOfType<BabyMovement>().Length < babyNumber)
            SpawnABaby();
    }

    public void SpawnABaby()
    {
        wichspawnPoint = Random.Range(0, spawnPoint.Length);
        GameObject babyInstance = Instantiate(baby, spawnPoint[wichspawnPoint].transform.position, spawnPoint[wichspawnPoint].transform.rotation);
        AudioManager.instance.Play3DSound("BabyPop", transform.position);

        BabyMovement babyMove = babyInstance.GetComponent<BabyMovement>();
        babyMove.corners.Clear();

        for (int i = 0; i < allcoin.transform.childCount; i++)
        {
            babyMove.corners.Add(allcoin.transform.GetChild(i).transform.gameObject);
        }
    }
}
