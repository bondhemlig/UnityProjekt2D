using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{

    public Transform target;
    public Vector3 offset = new Vector3 (0, 0, -10f);
    public float smoothing = 1.0f;

    void LateUpdate()
    {
        Vector3 newPosition = Vector3.Lerp(transform.position, target.position + offset, smoothing * Time.deltaTime);
        newPosition = new Vector3(newPosition.x, newPosition.y, newPosition.z);

        transform.position = newPosition;

                



    }



}
