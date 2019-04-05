using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moveGrid : MonoBehaviour
{
    public moveCamera viewCheck;
    public gameController selectCheck;

    public float upperLimit;
    public float lowerLimit;
    public float moveSpeed;

    private Vector3 originalPos;
    private Vector3 currentPos;
    private Vector3 targetPos;

    private Quaternion currentRot;
    private Quaternion targetRot;
    public float rotationSpeed;

    private bool objectMoving;
    private bool objectRotating;
    private bool sideLook;

    private float startTime;
    private float journeyLength;
    private float inv;
    private float currentDirection;
    private float currentDirectionY;

    // Start is called before the first frame update
    void Start()
    {
        currentPos = transform.position;
        targetPos = transform.position;
        originalPos = currentPos;
        inv = 1;
        sideLook = false;
        currentDirection = viewCheck.xRotate;
        currentDirection = viewCheck.yRotate;

        //rotateCheck = GameObject.Find("Layout").GetComponent<gameController>();
    }

    // Update is called once per frame
    void Update()
    {
        if(selectCheck != null && selectCheck.makeObjectsTransparent)
        {
            if ((viewCheck.xRotate >= -180 && viewCheck.xRotate < -45) || (viewCheck.xRotate >= 135 && viewCheck.xRotate <= 180))
                inv = -1;
            else
                inv = 1;

            if (Input.GetAxis("Mouse ScrollWheel") * inv >= 0.1 && !selectCheck.objectSelected && !objectMoving)
            {
                if (sideLook && currentPos.x < upperLimit)
                    targetPos = new Vector3(currentPos.x + 1, currentPos.y, currentPos.z);
                else if (!sideLook && currentPos.z < upperLimit)
                    targetPos = new Vector3(currentPos.x, currentPos.y, currentPos.z + 1);
                if(targetPos != transform.position)
                {
                    currentPos = transform.position;
                    startTime = Time.time;
                    journeyLength = Vector3.Distance(currentPos, targetPos);
                    objectMoving = true;
                } 
            }
            else if (Input.GetAxis("Mouse ScrollWheel") * inv <= -0.1 && !selectCheck.objectSelected && !objectMoving)
            {
                if (sideLook && currentPos.x > lowerLimit)
                    targetPos = new Vector3(currentPos.x - 1, currentPos.y, currentPos.z);
                else if (!sideLook && currentPos.z > lowerLimit)
                    targetPos = new Vector3(currentPos.x, currentPos.y, currentPos.z - 1);
                if (targetPos != transform.position)
                {
                    currentPos = transform.position;
                    startTime = Time.time;
                    journeyLength = Vector3.Distance(currentPos, targetPos);
                    objectMoving = true;
                }
            }
        }
        

        if (objectMoving)
        {
            float distCovered = (Time.time - startTime) * moveSpeed;

            float fracJourney = distCovered / journeyLength;
            transform.position = Vector3.Lerp(currentPos, targetPos, Mathf.Sin(fracJourney * (Mathf.PI * 0.5f)));
            if (fracJourney >= 1)
            {
                objectMoving = false;
                currentPos = transform.position;
            }

        }

        if (objectRotating)
        {
            float value = (Time.time - startTime) * rotationSpeed;
            transform.rotation = Quaternion.Lerp(currentRot, targetRot, Mathf.Sqrt(1 - (1-value) * (1 - value)));
            if (value >= 1)
            {
                objectRotating = false;
                currentRot = transform.rotation;
            }
        }

        if (viewCheck != null)
        {
            if(viewCheck.yRotate < currentDirectionY - 65) 
            {
                currentDirectionY -= 90;
                if (!objectRotating)
                {
                    targetRot = Quaternion.Euler(currentDirectionY, 0, 0);
                    if (targetRot != transform.rotation)
                    {
                        currentRot = transform.rotation;
                        startTime = Time.time;
                        journeyLength = Vector3.Distance(currentPos, targetPos);
                        objectRotating = true;
                    }
                }
            }
            else if(viewCheck.yRotate >= currentDirectionY + 65)
            {
                currentDirectionY += 90;
                if (!objectRotating)
                {
                    targetRot = Quaternion.Euler(currentDirectionY, 0, 0);
                    if (targetRot != transform.rotation)
                    {
                        currentRot = transform.rotation;
                        startTime = Time.time;
                        journeyLength = Vector3.Distance(currentPos, targetPos);
                        objectRotating = true;
                    }
                }
            }
            else if(currentDirectionY == 0 || currentDirectionY == 180 || currentDirectionY == -180)
            {
                if (viewCheck.xRotate < currentDirection - 45 || (currentDirection == 180 && viewCheck.xRotate < -currentDirection - 45))
                {
                    currentDirection -= 90;
                    if (currentDirection == -270)
                    {
                        currentDirection = 135;
                    }

                    if (!objectRotating)
                    {
                        targetRot = Quaternion.Euler(0, currentDirection, 0);
                        if (targetRot != transform.rotation)
                        {
                            currentRot = transform.rotation;
                            startTime = Time.time;
                            objectRotating = true;
                        }
                    }
                    //transform.rotation = Quaternion.Euler(0, 0, 0);
                    sideLook = false;
                }
                else if (viewCheck.xRotate >= currentDirection + 45 || (currentDirection == -180 && viewCheck.xRotate >= -currentDirection + 45))
                {
                    currentDirection += 90;
                    if (currentDirection == 270)
                    {
                        currentDirection = -135;
                    }

                    if (!objectRotating)
                    {
                        targetRot = Quaternion.Euler(0, currentDirection, 0);
                        if (targetRot != transform.rotation)
                        {
                            currentRot = transform.rotation;
                            startTime = Time.time;
                            objectRotating = true;
                        }
                    }
                    sideLook = true;
                }
            }
        }    
    }
}
