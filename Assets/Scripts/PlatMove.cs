using System.Drawing;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class PlatMove : MonoBehaviour
{
    public Transform[] targets;
    public Transform target;
    public Transform origin;
    public float lerpFactor = .2f;
    public float speed = 2;
    public int targetIndex;
    public float dist = .01f;
    public bool canMove;
    public Vector3 movement;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        target = targets[0];
        canMove = false;
    }

    public void FixedUpdate()
    {
        if (canMove)
        {
            Vector3 dirToTarget = target.position - transform.position;
            movement = dirToTarget.normalized * speed * Time.deltaTime;
            Debug.DrawRay(transform.position, dirToTarget);
            transform.position += movement;
            //transform.position = Vector3.Lerp(transform.position, target.position, lerpFactor);

            float distToTarget = dirToTarget.magnitude;
            if (distToTarget < dist)
            {
                targetIndex = (targetIndex + 1) % targets.Length;
                target = targets[targetIndex];
            }
        }
        else
        {
            Vector3 dirToTarget = origin.position - transform.position;
            Vector3 movement = dirToTarget.normalized * speed * Time.deltaTime;
            Debug.DrawRay(transform.position, dirToTarget);
            transform.position += movement;
            //transform.position = Vector3.Lerp(transform.position, target.position, lerpFactor);

            float distToTarget = dirToTarget.magnitude;
            if (distToTarget < dist)
            {
                transform.position = origin.position;
            }
        }

    }

    private void OnCollisionEnter(Collision collision) 
    { 
        canMove = true;       
    }

    private void OnCollisionExit(Collision collision) 
    { 
        canMove = false;
    }

}
