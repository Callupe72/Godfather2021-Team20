using UnityEngine;

public class BulletBehaviour : MonoBehaviour
{
    public enum BallType
    {
        bouncy,
        destroyable,
    }

    public BallType ballType;

    public float bounceStrengh = 10f;
    public float explosionRadius = 0.5f;
    public float upwardModifier = 10f;
    public float stunTime = 5f;

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Baby"))
        {
            other.gameObject.GetComponent<BabyMovement>().Hit(stunTime);
            Rigidbody babyRb = other.gameObject.GetComponent<Rigidbody>();
            babyRb.velocity = Vector3.zero;
            babyRb.AddExplosionForce(bounceStrengh * 10 * babyRb.velocity.magnitude, transform.position, explosionRadius, upwardModifier * 10 * babyRb.velocity.magnitude);
        }
        else if (other.gameObject.CompareTag("Phone"))
        {
            other.gameObject.GetComponent<Phone>().Hit();
        }
        /*else if (other.gameObject.CompareTag("Piscine"))
        {
            if (other.gameObject.GetComponent<Joint>() != null)
            {
                other.gameObject.GetComponent<Joint>().breakForce = 0;
            }
        }*/
        switch (ballType)
        {
            case BallType.destroyable:
                Destroy(gameObject);
                break;
            case BallType.bouncy:
                break;
        }

    }
}
