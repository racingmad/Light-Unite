using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightShootProto : MonoBehaviour
{
    public new GameObject light;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            Instantiate(light, transform.position + RoundDirection(transform.up), transform.rotation);
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
