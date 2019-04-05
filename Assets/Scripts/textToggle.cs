using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class textToggle : MonoBehaviour
{
    public RawImage objectIcon;
    public Text objectText;
    public visibiltyTrigger buttonParent;

    private bool textMoving;

    private float startTime;
    private float journeyLength;

    private Vector3 currentPos1, currentPos2;
    private Vector3 targetPos1, targetPos2;

    private void Start()
    {
        if(!buttonParent.isVisible)
        {
            if(objectIcon != null)
                objectIcon.CrossFadeAlpha(0, 0.00001f, false);
            if(objectText != null)
                objectText.CrossFadeAlpha(0, 0.00001f, false);
        }
    }

    private void Update()
    {
        if(buttonParent != null)
        {
            if(buttonParent.isVisible) 
            {
                if(objectIcon != null)
                    objectIcon.CrossFadeAlpha(1, 0.1f, false);
                if(objectText != null)
                    objectText.CrossFadeAlpha(1, 0.1f, false);
            }
            else
            {
                if (objectIcon != null)
                    objectIcon.CrossFadeAlpha(0, 0.1f, false);
                if (objectText != null)
                    objectText.CrossFadeAlpha(0, 0.1f, false);
            }
        }

        //if ()
        //{
        //    targetPos1 = 
        //    currentPos1 = objectIcon.anch;
        //    if (currentPos1 != targetPos1)
        //    {
        //        startTime = Time.time;
        //        journeyLength = Vector3.Distance(currentPos, targetPos);
        //        textMoving = true;
        //    }
        //}


        //if (textMoving)
        //{

        //    float distCovered = (Time.time - startTime) * moveSpeed;

        //    float fracJourney = distCovered / journeyLength;
        //    transform.position = Vector3.Lerp(currentPos, targetPos, Mathf.Sin(fracJourney * (Mathf.PI * 0.5f)));
        //    if (fracJourney >= 1)
        //    {
        //        objectMoving = false;
        //        currentPos = transform.position;
        //        selectionCheck.objectSelected = false;
        //        if (!selectionCheck.resetObjects)
        //            selectionCheck.numberOfMoves--;
        //    }

        //}
    }
}