using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using Unity.Mathematics;
using UnityEngine;

public class GrapplingHook : MonoBehaviour
{
    [SerializeField] private float grappleLength;
    [SerializeField] private LayerMask grappleLayer;
    [SerializeField] private LineRenderer rope;
    [SerializeField] private Object r;
    [SerializeField] private float maxDistance = 300f;
    [SerializeField] private AudioClip grappleFlySound;
    [SerializeField] private ParticleSystem hitParticles;

    private AudioSource playerAudioSource;
    private Vector3 grapplePoint;
    private DistanceJoint2D joint;
    private float minDepth = -Mathf.Infinity;
    private float maxDepth = Mathf.Infinity;


    private PlayerState playerState;

    // Start is called before the first frame update
    void Start()
    {
        joint = gameObject.GetComponent<DistanceJoint2D>();
        joint.enabled = false;
        rope.enabled = false;
        playerState = gameObject.GetComponent<PlayerState>();
        playerAudioSource = gameObject.GetComponent<AudioSource>();

    }

    // Update is called once per frame



    void Update()
    {
        if(Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1))

        {

            Camera cam = Camera.main;
            
            Vector3 mousePos = Input.mousePosition;
            
            mousePos.z = 0f; // Set the Z position?

            Vector3 startPosition = transform.position;
            Vector3 worldPointTarget = cam.ScreenToWorldPoint(mousePos);
            Vector3 direction = startPosition - worldPointTarget;
            direction = Vector3.Normalize(direction);
            
            RaycastHit2D hit = Physics2D.Raycast(
                origin: startPosition, 
                direction: -direction, 
                distance: math.clamp(Vector3.Distance(startPosition, worldPointTarget), -maxDistance, maxDistance),
                layerMask: grappleLayer,
                minDepth : minDepth,
                maxDepth : maxDepth
            );
            Debug.DrawRay(startPosition, direction * 1000f, Color.cyan, 1f);
      
            if(hit.collider !=null)
            {
                
                //hur varierar jag pitchen?
                playerAudioSource.PlayOneShot(grappleFlySound, 0.5f);



                playerState.moveSpeed += 100;
                grapplePoint = hit.point;
                grapplePoint.z = 0;
                joint.connectedAnchor = grapplePoint;
                joint.enabled = true;
                joint.distance = grappleLength;
                rope.SetPosition(0, grapplePoint);
                rope.SetPosition(1, transform.position);
                rope.enabled = true;
                Instantiate(hitParticles, hit.point, Quaternion.identity);
            }
        }

        if(Input.GetMouseButtonUp(0) || Input.GetMouseButtonUp(1))
        {
           if (joint.enabled)
            {
                playerState.moveSpeed -= 100;
            }
            joint.enabled = false;
           rope.enabled = false;
        }

        if(rope.enabled == true)
        {
            rope.SetPosition(1, transform.position);
        }
    }
}

/*
print(hit.collider);

RaycastHit2D hit2 = Physics2D.Linecast(startPosition, worldPointTarget, grappleLayer.value, minDepth, maxDepth);
Debug.DrawLine(startPosition, worldPointTarget, Color.red, 5f);
print(hit2.collider);

Ray ray = new Ray(startPosition, direction);
RaycastHit2D rayIntersection = Physics2D.GetRayIntersection(ray, 1000f, grappleLayer.value);

print(rayIntersection.collider);

*/