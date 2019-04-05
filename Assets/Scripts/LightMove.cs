using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightMove : MonoBehaviour
{
    public float shotSpeed;

    private Rigidbody rb;
    private gameController boundaryCheck;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.velocity = RoundDirection(transform.up)*shotSpeed;
        boundaryCheck = transform.parent.gameObject.GetComponent<gameController>();
    }

    private void Update()
    {
        if(transform.position.x > (boundaryCheck.endX + 2) || transform.position.x < (-boundaryCheck.endX - 2)
            || transform.position.y > (boundaryCheck.endY + 2) || transform.position.y < (-boundaryCheck.endY - 2)
            || transform.position.z > (boundaryCheck.endZ + 2) || transform.position.z < (-boundaryCheck.endZ - 2))
        {
            rb.detectCollisions = false;
            boundaryCheck.resetObjects = true;
            gameObject.GetComponent<Renderer>().enabled = false;
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
            rb.Sleep();
        }
            
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Object2")
        {
            rb.detectCollisions = false;
            boundaryCheck.resetObjects = true;
            gameObject.GetComponent<Renderer>().enabled = false;
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
            rb.Sleep();
        }
            
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Object2")
        {
            boundaryCheck.levelComplete = true;
            gameObject.GetComponent<Renderer>().enabled = false;
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
            rb.Sleep();
        }      
    }

    public Vector3 RoundDirection(Vector3 initial)
    {
        bool[] inverted;
        Vector3 compare;
        inverted = new bool[3];

        if(initial.x < 0)
        {
            compare.x = initial.x * -1;
            inverted[0] = true;
        }     
        else
            compare.x = initial.x;

        if (initial.y < 0)
        {
            compare.y = initial.y * -1;
            inverted[1] = true;
        }
        else
            compare.y = initial.y;

        if (initial.z < 0)
        {
            compare.z = initial.z * -1;
            inverted[2] = true;
        }
        else
            compare.z = initial.z;


        if (compare.x >= compare.y && compare.x >= compare.z)
        {
            compare.y = 0.0f;
            compare.z = 0.0f;
            if (inverted[0])
                compare.x *= -1;
        }
        else if (compare.y >= compare.x && compare.y >= compare.z)
        {
            compare.x = 0.0f;
            compare.z = 0.0f;
            if (inverted[1])
                compare.y *= -1;
        }
        else if (compare.z >= compare.x && compare.z >= compare.y)
        {
            compare.x = 0.0f;
            compare.y = 0.0f;
            if (inverted[2])
                compare.z *= -1;
        }

        return compare.normalized;
    }


}
