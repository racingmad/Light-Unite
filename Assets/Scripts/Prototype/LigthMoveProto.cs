using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LigthMoveProto : MonoBehaviour
{
    public float shotSpeed;

    private Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.velocity = RoundDirection(transform.up) * shotSpeed;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Object2")
        {
            Destroy(gameObject);
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Object2")
        {
            Destroy(gameObject);
        }
    }

    public Vector3 RoundDirection(Vector3 initial)
    {
        bool[] inverted;
        Vector3 compare;
        inverted = new bool[3];

        if (initial.x < 0)
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
