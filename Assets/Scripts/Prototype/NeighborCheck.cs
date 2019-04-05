using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NeighborCheck : MonoBehaviour
{
    //[System.NonSerialized]
    public bool hasLeftNeighbor;

    //[System.NonSerialized]
    public bool hasRightNeighbor;

    //[System.NonSerialized]
    public bool hasNeighborBelow;

    //[System.NonSerialized]
    public bool hasNeighborAbove;

    //[System.NonSerialized]
    public bool hasNeighborTopLeft;

    //[System.NonSerialized]
    public bool hasNeighborTopRight;

    //[System.NonSerialized]
    public bool hasNeighborBottomLeft;

    //[System.NonSerialized]
    public bool hasNeighborBottomRight;

    void Start()
    {
        hasLeftNeighbor = false;
        hasRightNeighbor = false;
        hasNeighborBelow = false;
        hasNeighborAbove = false;
        hasNeighborTopLeft = false;
        hasNeighborTopRight = false;
        hasNeighborBottomLeft = false;
        hasNeighborBottomRight = false;

        float pointUp = transform.position.y + 1;
        float pointDown = transform.position.y - 1;
        float pointLeft = transform.position.x - 1;
        float pointRight = transform.position.y + 1;

        Collider[] topRight = Physics.OverlapSphere(new Vector3(pointRight, pointUp, transform.position.z), 0.1f);
        if (topRight.Length > 0)
            hasNeighborTopRight = true;

        Collider[] topLeft = Physics.OverlapSphere(new Vector3(pointLeft, pointUp, transform.position.z), 0.1f);
        if (topLeft.Length > 0)
            hasNeighborTopLeft = true;

        Collider[] bottomRight = Physics.OverlapSphere(new Vector3(pointRight, pointDown, transform.position.z), 0.1f);
        if (bottomRight.Length > 0)
            hasNeighborTopRight = true;

        Collider[] bottomLeft = Physics.OverlapSphere(new Vector3(pointLeft, pointDown, transform.position.z), 0.1f);
        if (bottomLeft.Length > 0)
            hasNeighborTopLeft = true;
    }

    void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.tag == "Object" && collision.transform.position.y == gameObject.transform.position.y
            && collision.transform.position.x > gameObject.transform.position.x)
        {
            hasRightNeighbor = true;
            if (collision.transform.GetComponent<NeighborCheck>() != null && collision.transform.GetComponent<NeighborCheck>().hasNeighborAbove)
                hasNeighborTopRight = true;
        }

        if (collision.gameObject.tag == "Object" && collision.transform.position.y == gameObject.transform.position.y
            && collision.transform.position.x < gameObject.transform.position.x)
        {
            hasLeftNeighbor = true;
            if (collision.transform.GetComponent<NeighborCheck>() != null && collision.transform.GetComponent<NeighborCheck>().hasNeighborAbove)
                hasNeighborTopLeft = true;
        }
            
        if (collision.gameObject.tag == "Object" && collision.transform.position.x == gameObject.transform.position.x
            && collision.transform.position.y < gameObject.transform.position.y)
        {
            hasNeighborBelow = true;
            if (collision.transform.GetComponent<NeighborCheck>() != null && collision.transform.GetComponent<NeighborCheck>().hasRightNeighbor)
                hasNeighborBottomRight = true;
            else if (collision.transform.GetComponent<NeighborCheck>() != null && collision.transform.GetComponent<NeighborCheck>().hasLeftNeighbor)
                hasNeighborBottomLeft = true;
        }
           

        if (collision.gameObject.tag == "Object" && collision.transform.position.x == gameObject.transform.position.x
            && collision.transform.position.y > gameObject.transform.position.y)
            hasNeighborAbove = true;
    }
}
