using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class chargeIntensity : MonoBehaviour
{
    private Animator gradient;
    private bool flashing;
    public gameController speedCheck;

    public Material test;

    void Start()
    {
        gradient = GetComponent<Animator>();
        flashing = true;
    }

    void Update()
    {
        if (speedCheck.numberOfMoves > 0)
        {
            gradient.SetBool("stopped", false);
            if (!flashing)
            {
                flashing = true;
            }
            gradient.SetFloat("speed", 1f / speedCheck.numberOfMoves);
        }
        else
        {
            //gradient.speed = 0.0f;
            gradient.SetBool("stopped", true);
            flashing = false;
        }
    }
}
