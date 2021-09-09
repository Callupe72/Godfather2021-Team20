using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestIndicator : MonoBehaviour
{
    [SerializeField] public float destroyTimer = 20.0f;
    void OnEnable()
    {
     //   Invoke("Register", Random.Range(0, 8));  
    }
    public void Register()
    {
        Destroy(this, destroyTimer);
        if (!DI_Systeme.CheckIfObjectInSight(this.transform))
        {
            DI_Systeme.CreateIndicator(this.transform);
        }
    }
}
