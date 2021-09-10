using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SunMove : MonoBehaviour
{
    public Transform newPos;
    public Transform directionLight;
    float time;
    Transform player;

    void Start()
    {
        time = FindObjectOfType<Timer>().minutes * 60 + FindObjectOfType<Timer>().seconds;
        player = FindObjectOfType<PlayerWeapon>().transform;
        StartCoroutine(MovePos());
    }

    IEnumerator MovePos()
    {
        Vector3 currentPos = transform.position;
        float t = 0f;
        while (t < 1)
        {
            t += Time.deltaTime / time;
            transform.position = Vector3.Lerp(currentPos, newPos.position, t);
            directionLight.position = transform.position;
            directionLight.transform.LookAt(player.position);
            yield return null;
        }
    }
}
