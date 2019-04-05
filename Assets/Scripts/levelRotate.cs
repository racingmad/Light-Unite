using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class levelRotate : MonoBehaviour
{
    private gameController moveCheck;

    private bool levelRotating;

    private Quaternion currentRot;
    private Quaternion targetRot;

    private float startTime;
    private float journeyLength;
    private float reverseRot;

    public float rotationSpeed;

    // Start is called before the first frame update
    void Start()
    {
        moveCheck = GetComponent<gameController>();
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.E) && !moveCheck.objectSelected && moveCheck.numberOfMoves > 0 && !levelRotating)
        {
            if (moveCheck.resetObjects)
                moveCheck.resetObjects = false;
            reverseRot += 90;
            targetRot = Quaternion.Euler(0, 0, -reverseRot);
            if (targetRot != transform.rotation)
            {
                currentRot = transform.rotation;
                startTime = Time.time;
                levelRotating = true;
            }
            //transform.Rotate(0, 0, -90);   
        }
        else if (Input.GetKeyDown(KeyCode.Q) && !moveCheck.objectSelected && moveCheck.numberOfMoves > 0 && !levelRotating)
        {
            if (moveCheck.resetObjects)
                moveCheck.resetObjects = false;
            //transform.Rotate(0, 0, 90);
            reverseRot += -90;
            targetRot = Quaternion.Euler(0, 0, -reverseRot);
            if (targetRot != transform.rotation)
            {
                currentRot = transform.rotation;
                startTime = Time.time;
                levelRotating = true;
            }
        }

        if (levelRotating)
        {
            float value = (Time.time - startTime) * rotationSpeed;
            transform.rotation = Quaternion.Lerp(currentRot, targetRot, Mathf.Sqrt(1 - (1 - value) * (1 - value)));
            if (value >= 1)
            {
                levelRotating = false;
                currentRot = transform.rotation;
                if(!moveCheck.resetObjects)
                    moveCheck.numberOfMoves--;
                else
                    moveCheck.resetObjects = false;
                moveCheck.levelRotated = true;
            }
        }

        if (moveCheck.resetObjects && !levelRotating && reverseRot != 0)
        {
            //if ((Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.Q))
            //transform.Rotate(0, 0, moveCheck.reverseRot);
            reverseRot = 0;
            targetRot = Quaternion.Euler(0, 0, reverseRot);
            if (targetRot != transform.rotation)
            {
                currentRot = transform.rotation;
                startTime = Time.time;
                levelRotating = true;
            }
        }
    }
}
