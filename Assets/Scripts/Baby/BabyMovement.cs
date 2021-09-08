using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BabyMovement : MonoBehaviour
{
    [Header("Component")]
    [SerializeField] Rigidbody rb;
    public List<GameObject> corners = new List<GameObject>();
    public List<GameObject> cornersAvailable = new List<GameObject>();
    int wichCoin;

    [Header("Movement")]
    [SerializeField] float speed = 3f;
    [SerializeField] float speedSeingCorner = 3f;
    private Vector3 positionToGo;

    [Header("Condition")]
    [SerializeField] bool wantToDie = false;
    GameObject nearestCorner = null;
    GameObject previousCorner = null;
    [SerializeField] bool hasToChangeDir;
    public float secondsBeforeChangingTargetMin = 0.5f;
    public float secondsBeforeChangingTargetMax = 3f;
    public float distanceToSeeCorner = 2f;
    float distanceActualDestination;
    public float speedIncreaseView = 10;

    [Header("Stun")]
    [HideInInspector] public bool isStun;
    public Material stunMat;
    public Material originalMat;
    public Material seeACorner;
    MeshRenderer meshRenderer;
    public bool goingToCenter;

    void OnDrawGizmos()
    {
        Color alphaBlue = new Vector4(Color.blue.r, Color.blue.g, Color.blue.b, 0.25f);
        Gizmos.color = alphaBlue;
        Gizmos.DrawWireSphere(transform.position, distanceToSeeCorner);
    }

    void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        RandomRotation();
        StartCoroutine(ChangingTarget());
        cornersAvailable = corners;
    }

    void Update()
    {
        //Si le bebe change de dir et veut die
        //if (hasToChangeDir && wantToDie)
        //{
        //    float coinTested;
        //    for (int i = 0; i < coin.Count; i++)//pour chaques coin test si il est plus proche que les autres
        //    {
        //        coinTested = Vector3.Distance(coin[i].transform.position, transform.position);
        //        nearestCoin = coin[wichCoin];
        //        if (coinTested < Vector3.Distance(nearestCoin.transform.position, transform.position))
        //        {
        //            nearestCoin = coin[i];
        //        }

        //        hasToChangeDir = false;
        //    }
        //    rb.velocity = Vector3.zero;
        //    coinPosition = new Vector3 (nearestCoin.transform.position.x, nearestCoin.transform.position.y, nearestCoin.transform.position.z);
        //    transform.LookAt(coin[wichCoin].transform);
        //}

        if (!isStun)
        {
            Movement();
            GoToGround();
            if (nearestCorner == null)
                distanceToSeeCorner += speedIncreaseView / 10 * Time.deltaTime;
        }
    }

    public void Hit(float stunTime)
    {
        previousCorner = nearestCorner;
        nearestCorner = null;
        meshRenderer.material = stunMat;
        StopAllCoroutines();
        isStun = true;
        StartCoroutine(HitTime(stunTime));
    }

    IEnumerator HitTime(float stunTime)
    {
        yield return new WaitForSeconds(stunTime);
        isStun = false;
        meshRenderer.material = originalMat;
        if (nearestCorner == null)
            WillIChangeTarget();
        else
            GoToCenter();
    }

    void GoToCenter()
    {
        StopAllCoroutines();
        goingToCenter = true;
        Vector3 oppositeDir = -transform.position;
        positionToGo = new Vector3(oppositeDir.x, transform.position.y, oppositeDir.z) * (speedSeingCorner / 10);
    }

    void RandomRotation()
    {
        float yRotation = Random.Range(0, 360);
        Vector3 transformEular = transform.rotation.eulerAngles;
        transform.rotation = Quaternion.Euler(new Vector3(transformEular.x, yRotation, transformEular.z));
        positionToGo = transform.TransformDirection(Vector3.forward);
    }

    IEnumerator ChangingTarget()
    {
        yield return new WaitForSeconds(Random.Range(secondsBeforeChangingTargetMin, secondsBeforeChangingTargetMax));
        WillIChangeTarget();
    }

    void WillIChangeTarget()
    {
        if (nearestCorner != null)
        {
            distanceActualDestination = Vector3.Distance(nearestCorner.transform.position, transform.position);
            for (int i = 0; i < cornersAvailable.Count; i++)
            {

                //If he finds closer
                if (Vector3.Distance(cornersAvailable[i].transform.position, transform.position) > distanceActualDestination)
                {
                    nearestCorner = cornersAvailable[i];
                    positionToGo = nearestCorner.transform.position;
                    cornersAvailable.RemoveAt(i);
                    if (previousCorner != null)
                    {
                        cornersAvailable.Add(previousCorner);
                        previousCorner = null;
                    }
                    meshRenderer.material = seeACorner;
                    return;
                }
            }
        }
        else
        {
            for (int i = 0; i < cornersAvailable.Count; i++)
            {
                if (Vector3.Distance(cornersAvailable[i].transform.position, transform.position) < distanceToSeeCorner)
                {
                    nearestCorner = cornersAvailable[i];
                    positionToGo = nearestCorner.transform.position;
                    cornersAvailable.RemoveAt(i);
                    if (previousCorner != null)
                    {
                        cornersAvailable.Add(previousCorner);
                        previousCorner = null;
                    }
                    meshRenderer.material = seeACorner;
                    return;
                }
            }
        }
        if (nearestCorner == null)
        {
            RandomRotation();
            StartCoroutine(ChangingTarget());
        }
    }

    void Movement()
    {
        if (!goingToCenter)
        {
            if (nearestCorner == null)
                rb.velocity = positionToGo * speed;
            else
                rb.velocity = positionToGo * (speedSeingCorner / 10);
        }
        else
        {
            rb.velocity = positionToGo * (speedSeingCorner / 10);
        }
    }

    void GoToGround()
    {
        RaycastHit hit;
        Debug.DrawRay(transform.position, Vector3.down * 100);
        if (Physics.Raycast(transform.position, Vector3.down, out hit, 100))
        {
            transform.position = new Vector3(transform.position.x, hit.point.y + 1, transform.position.z);
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Wall") || collision.gameObject.CompareTag("Baby"))
        {
            if (nearestCorner == null)
                WillIChangeTarget();
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (goingToCenter)
        {
            if (other.gameObject.CompareTag("Center"))
            {
                goingToCenter = false;
                WillIChangeTarget();
            }
        }
    }
}
