using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public bool isGrounded;
    public bool cantGoRight;
    public bool cantGoLeft;
    private bool positionSwitched;
    private bool fallSwitched;
    private bool posSwitchCollision;

    private Coroutine fall;

    void Update()
    {
        Vector3 currentPos = transform.position;
        if (currentPos.x >= 3)
        {
            float reversePos = currentPos.x * -1;
            Collider[] detector = Physics.OverlapSphere(new Vector3(reversePos, transform.position.y, transform.position.z), 0.1f);
            if (detector.Length > 0)
            {
                cantGoRight = true;
                posSwitchCollision = true;
            }    
        }
        else if (currentPos.x <= -3)
        {
            float reversePos = currentPos.x * -1;
            Collider[] detector = Physics.OverlapSphere(new Vector3(reversePos, transform.position.y, transform.position.z), 0.1f);
            if (detector.Length > 0)
            {
                cantGoLeft = true;
                posSwitchCollision = true;
            }
        }

        if (Input.GetKeyUp(KeyCode.A) && isGrounded && !cantGoLeft)
        {
            if (positionSwitched)
                positionSwitched = false;
            if(posSwitchCollision)
            {
                posSwitchCollision = false;
                cantGoRight = false;
            }
            if (currentPos.x <= -3)
            {
                gameObject.transform.position = new Vector3(Mathf.Round(currentPos.x) * -1, Mathf.Round(currentPos.y), Mathf.Round(currentPos.z));
                positionSwitched = true;
            }
            else
                gameObject.transform.position = new Vector3(Mathf.Round(currentPos.x) - 1, Mathf.Round(currentPos.y), Mathf.Round(currentPos.z));
        }       
        else if (Input.GetKeyUp(KeyCode.D) && isGrounded && !cantGoRight)
        {
            if (positionSwitched)
                positionSwitched = false;
            if (posSwitchCollision)
            {
                posSwitchCollision = false;
                cantGoLeft = false;
            }
            if (currentPos.x >= 3)
            {
                gameObject.transform.position = new Vector3(Mathf.Round(currentPos.x) * -1, Mathf.Round(currentPos.y), Mathf.Round(currentPos.z));
                positionSwitched = true;
            }
            else
                gameObject.transform.position = new Vector3(Mathf.Round(currentPos.x) + 1, Mathf.Round(currentPos.y), Mathf.Round(currentPos.z));
        }    
        else
            gameObject.transform.position = new Vector3(Mathf.Round(currentPos.x), Mathf.Round(currentPos.y), Mathf.Round(currentPos.z));

        if (fall == null && !isGrounded)
        {
            if (currentPos.y <= -3)
            {
                float reversePos = currentPos.y * -1;
                Collider[] detector = Physics.OverlapSphere(new Vector3(transform.position.x, reversePos, transform.position.z), 0.1f);
                if (detector.Length > 0)
                    isGrounded = true;
                else
                    fall = StartCoroutine(PlayerDrop());
            }
            else
                fall = StartCoroutine(PlayerDrop());
        }  
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Object")
        {
            if (gameObject.transform.position.y <= collision.transform.position.y)
            isGrounded = false;
            if (gameObject.transform.position.y == collision.transform.position.y
                && collision.transform.position.x > gameObject.transform.position.x)
            {
                cantGoRight = true;
            }
            else if (gameObject.transform.position.y == collision.transform.position.y
                && collision.transform.position.x < gameObject.transform.position.x)
            {
                cantGoLeft = true;
            }


            if (collision.transform.GetComponent<NeighborCheck>() != null && gameObject.transform.position.y > collision.transform.position.y && positionSwitched)
            {
                if (gameObject.transform.position.x == collision.transform.position.x)
                    isGrounded = true;
                else if ((gameObject.transform.position.x < collision.transform.position.x && collision.transform.GetComponent<NeighborCheck>().hasLeftNeighbor)
                || (gameObject.transform.position.x > collision.transform.position.x && collision.transform.GetComponent<NeighborCheck>().hasRightNeighbor))
                    isGrounded = true;
                else if ((gameObject.transform.position.x < collision.transform.position.x && !collision.transform.GetComponent<NeighborCheck>().hasLeftNeighbor)
                || (gameObject.transform.position.x > collision.transform.position.x && !collision.transform.GetComponent<NeighborCheck>().hasRightNeighbor))
                    isGrounded = false;
            }
        }   
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.tag == "Object" && collision.transform.GetComponent<NeighborCheck>() != null && gameObject.transform.position.y > collision.transform.position.y
           && gameObject.transform.position.x > collision.transform.position.x - (collision.transform.localScale.x * 0.9)
           && gameObject.transform.position.x < collision.transform.position.x + (collision.transform.localScale.x * 0.9))
        {
            isGrounded = true;
            if (collision.transform.GetComponent<NeighborCheck>().hasNeighborTopLeft)
                cantGoLeft = true;
            else
                cantGoLeft = false;
            if (collision.transform.GetComponent<NeighborCheck>().hasNeighborTopRight)
                cantGoRight = true;
            else
                cantGoRight = false;
        }
        else if (collision.transform.GetComponent<NeighborCheck>() != null && collision.gameObject.tag == "Object" && gameObject.transform.position.y > collision.transform.position.y)
        {
            if ((gameObject.transform.position.x > collision.transform.position.x && collision.transform.GetComponent<NeighborCheck>().hasRightNeighbor) 
                || (gameObject.transform.position.x > collision.transform.position.x && collision.transform.GetComponent<NeighborCheck>().hasNeighborBottomRight))
                isGrounded = true;
            else if ((gameObject.transform.position.x < collision.transform.position.x && collision.transform.GetComponent<NeighborCheck>().hasLeftNeighbor) 
                || (gameObject.transform.position.x < collision.transform.position.x && collision.transform.GetComponent<NeighborCheck>().hasNeighborBottomLeft))
                isGrounded = true;
            else
                isGrounded = false;
        }
        else if (isGrounded && collision.transform.GetComponent<NeighborCheck>() != null && collision.gameObject.tag == "Object" && gameObject.transform.position.y <= collision.transform.position.y)
        {
            if (collision.transform.position.x < gameObject.transform.position.x && collision.transform.GetComponent<NeighborCheck>().hasNeighborBelow)
                cantGoLeft = true;
            if (collision.transform.position.x > gameObject.transform.position.x && collision.transform.GetComponent<NeighborCheck>().hasNeighborBelow)
                cantGoRight = true;
        }
        else if(gameObject.transform.position.y == collision.transform.position.y
                && collision.transform.position.x > gameObject.transform.position.x)
                cantGoRight = true;
        else if (gameObject.transform.position.y == collision.transform.position.y
                && collision.transform.position.x < gameObject.transform.position.x)
                cantGoLeft = true;
    }

    private void OnCollisionExit(Collision collision)
    {
        if(collision.gameObject.tag == "Object")
        {
            if (gameObject.transform.position.y == collision.transform.position.y
                && collision.transform.position.x > gameObject.transform.position.x)
            {
                if (positionSwitched)
                    cantGoLeft = false;
                else
                    cantGoRight = false;
            }
            else if (gameObject.transform.position.y == collision.transform.position.y
                    && collision.transform.position.x < gameObject.transform.position.x)
            {
                if (positionSwitched)
                    cantGoRight = false;
                else
                    cantGoLeft = false;
            }
   //         else if (gameObject.transform.position.y > collision.transform.position.y && positionSwitched) { }
   //             isGrounded = true;
        }
    }

    IEnumerator PlayerDrop()
    {
        if (fallSwitched)
            fallSwitched = false;
        Vector3 currentPos = transform.position;
        if(currentPos.y <= -3)
        {
            gameObject.transform.position = new Vector3(Mathf.Round(currentPos.x), Mathf.Round(currentPos.y) * -1, Mathf.Round(currentPos.z));
            fallSwitched = true;
        }   
        else
            gameObject.transform.position = new Vector3(Mathf.Round(currentPos.x), Mathf.Round(currentPos.y) - 1, Mathf.Round(currentPos.z));
        yield return new WaitForSeconds(0.05f);

        fall = null;
    }
}
