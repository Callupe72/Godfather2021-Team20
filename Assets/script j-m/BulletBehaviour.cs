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
    public float stunTime;

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Baby"))
        {
            Debug.Log("touch");
            Rigidbody babyRb = other.gameObject.GetComponent<Rigidbody>();
            babyRb.AddExplosionForce(bounceStrengh * 1000, transform.position, 1f, bounceStrengh*1000);
        
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
