using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraZoom : MonoBehaviour
{
    [SerializeField]  private float zoomSpeed = 1.0f;
    [SerializeField] private float minOrtographicSize = 1.0f;
    [SerializeField] private float maxOrtographicSize = 35f;

    private Camera cam;

    private void Start()
    {
        cam = GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        float neworthographicSize = cam.orthographicSize + ( scroll * zoomSpeed );
        neworthographicSize = Mathf.Clamp(neworthographicSize, minOrtographicSize, maxOrtographicSize);
        cam.orthographicSize = neworthographicSize;
    }
}
