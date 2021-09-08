using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class PlayerWeapon : MonoBehaviour
{
    public GameObject bulletPrefab;

    public Transform bulletSpawn;

    public float bulletSpeed = 30;

    public float lifeTime = 3;

    public float maxTimeLoad = 1f;
    public Transform gun;
    float currentLoad;

    [Header("Camera")]
    public Camera cam;
    public float camMultiplier = 10f;
    float startingField;

    [Header("PostProcess")]
    public Volume postProcess;
    ChromaticAberration chromaAb;


    void Start()
    {
        startingField = cam.fieldOfView;

        ChromaticAberration chromAb;

        if ( postProcess.profile.TryGet<ChromaticAberration>(out chromAb))
        {
            chromaAb = chromAb;
        }
        
    }
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            if (currentLoad < maxTimeLoad)
            {
                currentLoad += Time.deltaTime;
                cam.fieldOfView = startingField + (camMultiplier * currentLoad);
                chromaAb.intensity.value = currentLoad / 10;
            }
        }
        else if (Input.GetMouseButtonUp(0))
        {
            cam.fieldOfView = startingField;
            chromaAb.intensity.value = 0;
            Fire();
        }
    }

    private void Fire()
    {
        GameObject bullet = Instantiate(bulletPrefab);

        //Physics.IgnoreCollision(bullet.GetComponent<Collider>(), bulletSpawn.parent.GetComponent<Collider>());

        bullet.transform.position = bulletSpawn.position;

        Vector3 rotation = bullet.transform.rotation.eulerAngles;

        bullet.transform.rotation = Quaternion.Euler(rotation.x, transform.eulerAngles.y, rotation.z);

        bullet.GetComponent<Rigidbody>().AddForce(bulletSpawn.forward * bulletSpeed * currentLoad, ForceMode.Impulse);
        currentLoad = 0;

        Destroy(bullet, lifeTime);

    }
}
