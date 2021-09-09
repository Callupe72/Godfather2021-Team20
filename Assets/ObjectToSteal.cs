using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class ObjectToSteal : MonoBehaviour
{
    public bool canBeSteal;
    public float speedMultuplier = 1.25f;
    public float fightMultiplier = 1.25f;

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
