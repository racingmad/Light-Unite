using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moveCamera : MonoBehaviour
{
    public gameController resetCheck;

    [System.NonSerialized]
    public float xRotate;
    [System.NonSerialized]
    public float yRotate;

    private bool resetting;
    private bool resetX;
    private bool resetY;

    public float resetSpeed;
    //[Range(0f, 90f)]
    //public float setyRotateDest;

    private float xRotateDest;
    private float yRotateDest;

    private float invX;

    void Start()
    {
        invX = 1;

        xRotate = 0;
        yRotate = 0;

        resetX = false;
        resetY = false;

        xRotateDest = 0;
        yRotateDest = 0;
    }

    void Update()
    {
        if (yRotate < -90 || yRotate > 90)
            invX = -1;
        else
            invX = 1;

        if(resetCheck != null)
        {
            if (Input.GetMouseButton(0) && !resetCheck.objectSelected && !resetCheck.draggingObject && !resetCheck.levelComplete && resetCheck.numberOfMoves > 0)
            {
                xRotate += Input.GetAxis("Mouse X") * Time.deltaTime * 200 * invX;
                yRotate -= Input.GetAxis("Mouse Y") * Time.deltaTime * 200;
            }


            if (Input.GetKey(KeyCode.R) && !resetting)
            {
                resetX = true;
                resetY = true;
                resetCheck.mouseXInv = 1;
                resetCheck.mouseWheelInv = 1;
                xRotateDest = 0;
                yRotateDest = 0;
                resetting = true;
            }


            if (Input.GetMouseButtonUp(0) && xRotate >= -45 && xRotate < 45)
            {
                xRotateDest = 0;
                resetCheck.mouseXInv = 1;
                resetCheck.mouseWheelInv = 1;
                resetCheck.sideView = false;
                resetX = true;
                resetting = true;
            }
            else if (Input.GetMouseButtonUp(0) && xRotate >= 45 && xRotate < 135)
            {
                xRotateDest = 90;
                resetCheck.mouseXInv = -1;
                resetCheck.mouseWheelInv = 1;
                resetCheck.sideView = true;
                resetX = true;
                resetting = true;
            }
            else if (Input.GetMouseButtonUp(0) && xRotate >= 135 && xRotate <= 180)
            {
                xRotateDest = 180;
                resetCheck.mouseXInv = -1;
                resetCheck.mouseWheelInv = -1;
                resetCheck.sideView = false;
                resetX = true;
                resetting = true;
            }
            else if (Input.GetMouseButtonUp(0) && xRotate >= -180 && xRotate < -135)
            {
                xRotateDest = -180;
                resetCheck.mouseXInv = -1;
                resetCheck.mouseWheelInv = -1;
                resetCheck.sideView = false;
                resetX = true;
                resetting = true;
            }
            else if (Input.GetMouseButtonUp(0) && xRotate >= -135 && xRotate < -45)
            {
                xRotateDest = -90;
                resetCheck.sideView = true;
                resetCheck.mouseXInv = 1;
                resetCheck.mouseWheelInv = -1;
                resetX = true;
                resetting = true;
            }

            if (Input.GetMouseButtonUp(0) && yRotate >= -45 && yRotate < 45)
            {
                yRotateDest = 0;
                resetCheck.mouseYInv = 1;
                resetCheck.topView = false;
                resetY = true;
                resetting = true;
            }
            else if(Input.GetMouseButtonUp(0) && yRotate >= 45 && yRotate <= 135)
            {
                yRotateDest = 90;
                if(xRotate == -90 || xRotate == -180 || xRotate == 180)
                    resetCheck.mouseYInv = -1;
                else
                    resetCheck.mouseYInv = 1;
                resetCheck.mouseWheelInv = -1;
                resetCheck.topView = true;
                resetY = true;
                resetting = true;
            }
            else if (Input.GetMouseButtonUp(0) && yRotate >= 135 && yRotate <= 180)
            {
                yRotateDest = 180;
                resetCheck.mouseYInv = -1;
                if (xRotate == -90 || xRotate == -180 || xRotate == 180)
                    resetCheck.mouseWheelInv = 1;
                else
                    resetCheck.mouseWheelInv = -1;
                resetCheck.topView = false;
                resetY = true;
                resetting = true;
            }
            else if (Input.GetMouseButtonUp(0) && yRotate >= -180 && yRotate < -135)
            {
                yRotateDest = -180;
                resetCheck.mouseYInv = -1;
                if(xRotate == -90 || xRotate == -180 || xRotate == 180)
                    resetCheck.mouseWheelInv = 1;
                else
                    resetCheck.mouseWheelInv = -1;
                resetCheck.topView = false;
                resetY = true;
                resetting = true;
            }
            else if(Input.GetMouseButtonUp(0) && yRotate >= -135 && yRotate < -45)
            {
                yRotateDest = -90;
                if(xRotate == -90 || xRotate == -180 || xRotate == 180)
                    resetCheck.mouseYInv = 1;
                else
                    resetCheck.mouseYInv = -1;
                resetCheck.mouseWheelInv = 1;
                resetCheck.topView = true;
                resetY = true;
                resetting = true;
            }

            if (resetCheck.numberOfMoves <= 0)
            {
                xRotateDest = 0;
                yRotateDest = 0;
                resetCheck.mouseXInv = 1;
                resetCheck.mouseWheelInv = 1;
                resetCheck.sideView = false;
                resetCheck.topView = false;
                resetX = true;
                resetY = true;
                resetting = true;
            }

            if (resetting)
            {
                if (resetX)
                {
                    if (xRotate < xRotateDest)
                    {
                        xRotate += Time.deltaTime * resetSpeed;
                        if (xRotate >= xRotateDest)
                            xRotate = xRotateDest;
                    }
                    else if (xRotate > xRotateDest)
                    {
                        xRotate -= Time.deltaTime * resetSpeed;
                        if (xRotate <= xRotateDest)
                            xRotate = xRotateDest;
                    }
                    if (xRotate == xRotateDest)
                        resetX = false;
                }
                if (resetY)
                {
                    if (yRotate < yRotateDest)
                    {
                        yRotate += Time.deltaTime * resetSpeed;
                        if (yRotate >= yRotateDest)
                            yRotate = yRotateDest;
                    }
                    else if (yRotate > yRotateDest)
                    {
                        yRotate -= Time.deltaTime * resetSpeed;
                        if (yRotate <= yRotateDest)
                            yRotate = yRotateDest;
                    }
                    if (yRotate == yRotateDest)
                        resetY = false;
                }
                if (!resetX && !resetY)
                    resetting = false;
            }

            if (xRotate > 180)
                xRotate = -180;
            else if (xRotate < -180)
                xRotate = 180;

            if (yRotate > 180)
                yRotate = -180;
            else if (yRotate < -180)
                yRotate = 180;

            //if (yRotate > 90)
            //    yRotate = 90;
            //else if (yRotate < -90)
            //    yRotate = -90;

            transform.rotation = Quaternion.Euler(yRotate, xRotate, 0);
        }
        else
        {
            Debug.Log("No Game Controller Script Detected. Please reference to utilise camera script");
        }
    }
}
