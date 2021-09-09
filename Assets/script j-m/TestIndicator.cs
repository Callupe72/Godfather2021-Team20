using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestIndicator : MonoBehaviour
{
    [SerializeField] float destroyTimer = 20.0f;
    void Start()
    {
        Invoke("Register", Random.Range(0, 8));  
    }
    void Register()
    {
        if (!DI_Systeme.CheckIfObjectInSight(this.transform))
        {
            DI_Systeme.CreateIndicator(this.transform);
        }
        Destroy(this.gameObject, destroyTimer);
    }

    
}
