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
            babyRb.AddExplosionForce(bounceStrengh * 10, transform.position, explosionRadius, upwardModifier * 10);
        
        }
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
