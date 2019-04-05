using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shootBlastEffect : MonoBehaviour
{
    public gameController shootCheck;

    private ParticleSystem shoot;

    private bool played;

    void Start()
    {
        shoot = GetComponent<ParticleSystem>();
    }

    void Update()
    {
        if (shootCheck.numberOfMoves == 0 && !played)
        {
            shoot.Play();
            played = true;
        }
        else if (shootCheck.numberOfMoves != 0 && played)
        {
            played = false;
        }
    }
}
