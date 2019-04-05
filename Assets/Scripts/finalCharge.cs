using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class finalCharge : MonoBehaviour
{
    public gameController chargeCheck;

    private ParticleSystem charge;

    private bool playing;

    void Start()
    {
        charge = GetComponent<ParticleSystem>();
        charge.Stop();
    }

    void Update()
    {
        if(chargeCheck.numberOfMoves == 1 && !playing)
        {
            charge.Play();
            playing = true;
        }
        else if(chargeCheck.numberOfMoves != 1 && playing)
        {
            charge.Stop();
            playing = false;
        }
    }


}
