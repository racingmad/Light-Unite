using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class LightShoot : MonoBehaviour
{
    public new GameObject light;
    public Transform parent;

    private gameController shootCheck;
    private bool lightShot;
    private GameObject level;

    private GameObject audioSourceList;
    private AudioSource shootSource;
    public AudioClip shoot;


    private void Start()
    {
        lightShot = false;
        level = GameObject.Find("Layout");
        //shootCheck = transform.parent.gameObject.GetComponent<gameController>();
        shootCheck = level.GetComponent<gameController>();
        audioSourceList = GameObject.Find("Audio Sources");
        AudioSource[] sources = audioSourceList.GetComponents<AudioSource>();
        shootSource = sources[1];
        if (shoot != null)
            shootSource.clip = shoot;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            shootCheck.numberOfMoves = 0;

        if(shootCheck.numberOfMoves <= 0 && !lightShot)
        {
            Instantiate(light, transform.position + RoundDirection(transform.up), transform.rotation, parent);
            if(shoot != null)
                shootSource.Play();
            lightShot = true;
        }

        if (shootCheck.resetObjects)
            lightShot = false;
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
