using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class ObjectToSteal : MonoBehaviour
{
    [HideInInspector] public bool canBeSteal;
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Desk"))
        {
            canBeSteal = true;
        }
    }
    void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Desk"))
        {
            canBeSteal = false;
        }
    }
}
