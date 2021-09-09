using System.Collections;
using System.Collections.Generic;
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
    Bloom bloom;
    Vignette vignette;

    public float distanceSound = 10f;

    public List<GameObject> weapon;

    MeshFilter meshFilter;
    MeshRenderer meshRenderer;
    GameObject bullet;

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, distanceSound);
    }

    void Start()
    {
        startingField = cam.fieldOfView;

        ChromaticAberration _chromAb;
        Bloom _bloom;
        Vignette _vignette;

        if (postProcess.profile.TryGet<ChromaticAberration>(out _chromAb))
        {
            chromaAb = _chromAb;
        }
        if (postProcess.profile.TryGet<Bloom>(out _bloom))
        {
            bloom = _bloom;
        }
        if (postProcess.profile.TryGet<Vignette>(out _vignette))
        {
            vignette = _vignette;
        }
        ChangeBall();
    }

    void ChangeBall()
    {
        bullet = Instantiate(bulletPrefab);
        bullet.transform.localScale = Vector3.zero;
        bullet.GetComponent<Rigidbody>().isKinematic = true;
        meshFilter = bullet.GetComponent<MeshFilter>();
        meshRenderer = bullet.GetComponent<MeshRenderer>();
        int num = Random.Range(0, weapon.Count);
        meshFilter.sharedMesh = weapon[num].GetComponent<MeshFilter>().sharedMesh;
        meshRenderer.sharedMaterial = weapon[num].GetComponent<MeshRenderer>().sharedMaterial;
    }

    void Update()
    {

        if (bullet)
        {
            GrowUp();
            bullet.transform.position = bulletSpawn.position;
            bullet.transform.rotation = bulletSpawn.rotation;
        }


        if (Input.GetMouseButton(0))
        {
            if (currentLoad < maxTimeLoad)
            {
                currentLoad += Time.deltaTime;
                cam.fieldOfView = startingField + (camMultiplier * currentLoad);
                chromaAb.intensity.value = currentLoad / 3;
                bloom.intensity.value = currentLoad / 2;
                vignette.intensity.value = currentLoad / 10 * 3;
            }
        }
        else if (Input.GetMouseButtonUp(0))
        {
            cam.fieldOfView = startingField;
            chromaAb.intensity.value = 0;
            bloom.intensity.value = 0;
            vignette.intensity.value = 0;
            Fire();
        }

    }

    void GrowUp()
    {
        float increase = 0.01f;

        if (bullet.transform.localScale.x < 0.3f)
        {
            bullet.transform.localScale += new Vector3(increase * 3f, bullet.transform.localScale.y, bullet.transform.localScale.z);
        }
        else
        {
            bullet.transform.localScale = new Vector3(0.3f, bullet.transform.localScale.y, bullet.transform.localScale.z);
        }
        if (bullet.transform.localScale.y < 0.25f)
        {
            bullet.transform.localScale += new Vector3(bullet.transform.localScale.x, increase * 2.5f, bullet.transform.localScale.z);
        }
        else
        {
            bullet.transform.localScale = new Vector3(bullet.transform.localScale.x, 0.25f, bullet.transform.localScale.z);
        }
        if (bullet.transform.localScale.z < 0.8f)
        {
            bullet.transform.localScale += new Vector3(bullet.transform.localScale.x, bullet.transform.localScale.y, increase * 8f);
        }
        else
        {
            bullet.transform.localScale = new Vector3(bullet.transform.localScale.x, bullet.transform.localScale.y, 0.8f);
        }
    }

    private void Fire()
    {

        //Physics.IgnoreCollision(bullet.GetComponent<Collider>(), bulletSpawn.parent.GetComponent<Collider>());
        bullet.GetComponent<TrailRenderer>().enabled = true;
        bullet.GetComponent<Rigidbody>().isKinematic = false;

        Vector3 rotation = bullet.transform.rotation.eulerAngles;

        bullet.transform.rotation = Quaternion.Euler(rotation.x, transform.eulerAngles.y, rotation.z);

        bullet.GetComponent<Rigidbody>().AddForce(bulletSpawn.forward * bulletSpeed * currentLoad, ForceMode.Impulse);
        currentLoad = 0;

        Destroy(bullet, lifeTime);

        bullet = null;

        StartCoroutine(Wait());
    }

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(0.5f);
        ChangeBall();
    }
}

