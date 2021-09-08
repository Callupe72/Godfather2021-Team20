using PathCreation;
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
    public Transform seeWall;

    [Header("Condition")]
    [SerializeField] bool wantToDie = false;
    public GameObject nearestCorner = null;
    GameObject previousCorner = null;
    [SerializeField] bool hasToChangeDir;
    public float secondsBeforeChangingTargetMin = 0.5f;
    public float secondsBeforeChangingTargetMax = 3f;
    public float distanceToSeeCorner = 2f;
    float distanceActualDestination;
    public float speedIncreaseView = 10;
    public Phone phone;

    [Header("Stun")]
    [HideInInspector] public bool isStun;
    MeshRenderer meshRenderer;
    public bool goingToCenter;

    [Header("Bagarre")]
    public float timeBeforeDying = 5f;
    public CollisionResult collisionResult;
    public List<BabyMovement> babiesFight = new List<BabyMovement>();
    [HideInInspector] public bool doNotNeedToThink;
    Animator anim;

    [Header("Desk")]
    public PathCreator pathCreatorOnDesk;
    public PathCreator pathCreatorGoOnDesk;
    public float speedOnDesk = 5f;
    float distanceTravelled;

    [Header("FollowBall")]
    public float distanceFollowBall = 5f;
    public float speedFollowBall = 3f;
    Transform footballTransform;
    public enum CollisionResult
    {
        none,
        fight,
        carry,
        onDesk,
        followBall,
    }

    [Tooltip("Chance de 0 à la valeur")] [Range(0, 100)] public int fightPercentage = 20;
    [Tooltip("Chance de la valeur précédente à celle-ci")] [Range(0, 100)] public int carryPercentage = 50;

    void OnDrawGizmos()
    {
        Color alphaBlue = new Vector4(Color.blue.r, Color.blue.g, Color.blue.b, 0.25f);
        Gizmos.color = alphaBlue;
        Gizmos.DrawWireSphere(transform.position, distanceToSeeCorner);

        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, distanceFollowBall);

    }

    void Start()
    {
        anim = GetComponent<Animator>();
        meshRenderer = GetComponent<MeshRenderer>();
        RandomRotation();
        StartCoroutine(ChangingTarget());
        cornersAvailable = corners;
        phone = FindObjectOfType<Phone>();
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

        //anim.SetFloat("Speed", rb.velocity.magnitude);

        for (int i = 0; i < babiesFight.Count; i++)
        {
            if (babiesFight[i] == null)
                Die();
        }

        if (collisionResult != CollisionResult.followBall)
            DetectBall();

        if (!isStun)
        {
            switch (collisionResult)
            {
                case CollisionResult.none:
                    Movement();
                    GoToGround();
                    if (nearestCorner == null && collisionResult == CollisionResult.none)
                        distanceToSeeCorner += speedIncreaseView / 10 * Time.deltaTime;
                    break;
                case CollisionResult.fight:
                    if (babiesFight.Count == 0)
                        collisionResult = CollisionResult.none;
                    break;
                case CollisionResult.carry:
                    break;
                case CollisionResult.onDesk:
                    WalkOnDesk();
                    break;
                case CollisionResult.followBall:
                    RunningAfterBall();
                    break;
            }
        }

        if (transform.rotation.eulerAngles.x != 0)
        {
            transform.rotation = Quaternion.Euler(0, transform.rotation.eulerAngles.y, 0);
        }


        foreach (Collider item in Physics.OverlapSphere(seeWall.position, 1f))
        {
            if (item.gameObject.CompareTag("Wall"))
                RandomRotationOnAWall();
        }

    }

    void DetectBall()
    {
        foreach (Collider item in Physics.OverlapSphere(transform.position, distanceFollowBall))
        {
            if (item.gameObject.CompareTag("Football"))
            {
                if (item.GetComponent<Rigidbody>().velocity.magnitude > 5f)
                {
                    collisionResult = CollisionResult.followBall;
                    if (footballTransform == null)
                        footballTransform = item.transform;
                }
            }
        }
    }

    void RunningAfterBall()
    {
        transform.position = Vector3.MoveTowards(transform.position, footballTransform.position, Time.deltaTime * speedFollowBall * phone.actualModifierSpeed);
        transform.LookAt(footballTransform.position);
    }

    void Die()
    {
        Destroy(gameObject);
    }

    public void Hit(float stunTime)
    {
        if (collisionResult == CollisionResult.fight)
        {
            if (!doNotNeedToThink)
            {
                for (int i = 0; i < babiesFight.Count; i++)
                {
                    babiesFight[i].doNotNeedToThink = true;
                    babiesFight[i].collisionResult = CollisionResult.none;
                }
                collisionResult = CollisionResult.none;
            }
        }
        if (collisionResult == CollisionResult.onDesk)
            collisionResult = CollisionResult.none;
        babiesFight.Clear();
        previousCorner = nearestCorner;
        nearestCorner = null;
        doNotNeedToThink = false;
        StopAllCoroutines();
        isStun = true;
        StartCoroutine(HitTime(stunTime));
    }

    IEnumerator HitTime(float stunTime)
    {
        yield return new WaitForSeconds(stunTime);
        isStun = false;
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
    void RandomRotationOnAWall()
    {
        Vector3 transformEular = transform.rotation.eulerAngles;
        transform.rotation = Quaternion.Euler(new Vector3(transformEular.x, transform.rotation.eulerAngles.y + 180, transformEular.z));
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
        if (collisionResult != CollisionResult.none)
            return;

        if (!goingToCenter)
        {
            if (nearestCorner == null)
                rb.velocity = positionToGo * speed * phone.actualModifierSpeed;
            else
            {
                transform.position = Vector3.MoveTowards(transform.position, positionToGo, speedSeingCorner / 10 * Time.deltaTime * phone.actualModifierSpeed);
                transform.LookAt(positionToGo);
            }
        }
        else
        {
            rb.velocity = positionToGo * (speedSeingCorner / 10 * phone.actualModifierSpeed);
        }
    }

    void GoToGround()
    {
        RaycastHit hit;
        Debug.DrawRay(transform.position, Vector3.down * 100);
        if (Physics.Raycast(transform.position, Vector3.down, out hit, 100))
        {
            transform.position = new Vector3(transform.position.x, hit.point.y + 0.25f, transform.position.z);
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Baby"))
        {
            StopAllCoroutines();
            if (collisionResult != CollisionResult.none)
                return;

            if (nearestCorner != null || collision.gameObject.GetComponent<BabyMovement>().nearestCorner != null)
                return;

            if (collision.gameObject.GetComponent<BabyMovement>().collisionResult == CollisionResult.fight)
            {
                Fight(collision.gameObject);
            }
            else
            {
                if (transform.position.x + transform.position.z > collision.transform.position.x + collision.transform.position.z)
                {
                    int random = Random.Range(0, 100);

                    if (random < fightPercentage * phone.actualModifierFight)
                    {
                        Fight(collision.gameObject);
                    }
                    else if (random > fightPercentage * phone.actualModifierFight && random < carryPercentage * phone.actualModifierFight)
                    {
                        Carry(collision.gameObject);
                    }
                    else
                    {
                        if (nearestCorner == null)
                            WillIChangeTarget();
                    }
                }
            }
        }
    }

    void WalkOnDesk()
    {
        distanceTravelled += speed * Time.deltaTime;
        transform.position = pathCreatorGoOnDesk.path.GetPointAtDistance(distanceTravelled);
        transform.rotation = pathCreatorGoOnDesk.path.GetRotationAtDistance(distanceTravelled);
        transform.rotation = Quaternion.Euler(0, transform.rotation.eulerAngles.y + 180, 0);
        if (distanceTravelled > 8 && pathCreatorGoOnDesk != pathCreatorOnDesk)
        {
            distanceTravelled = 0;
            pathCreatorGoOnDesk = pathCreatorOnDesk;
        }
    }

    void Carry(GameObject other)
    {
        collisionResult = CollisionResult.carry;
        other.GetComponent<BabyMovement>().collisionResult = CollisionResult.carry;
    }

    void Fight(GameObject other)
    {
        collisionResult = CollisionResult.fight;
        babiesFight.Add(other.GetComponent<BabyMovement>());
        other.GetComponent<BabyMovement>().babiesFight.Add(GetComponent<BabyMovement>());
        other.GetComponent<BabyMovement>().collisionResult = CollisionResult.fight;
        transform.LookAt(other.transform.position);
        other.transform.LookAt(transform.position);
        StartCoroutine(FightDie(other));
    }

    IEnumerator FightDie(GameObject other)
    {
        yield return new WaitForSeconds(timeBeforeDying);
        FindObjectOfType<Spawn>().SpawnABaby();
        FindObjectOfType<Spawn>().SpawnABaby();
        Destroy(other);
        Destroy(gameObject);

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

        if (other.gameObject.CompareTag("Desk"))
        {
            collisionResult = CollisionResult.onDesk;
            pathCreatorOnDesk = GameObject.Find("OnDeskPath").GetComponent<PathCreator>();
            pathCreatorGoOnDesk = GameObject.Find("GoOnDeskPath").GetComponent<PathCreator>();
        }
    }
}
