using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class gameController : MonoBehaviour
{
    [System.NonSerialized]
    public bool objectSelected;
    public int numberOfMoves;
    [System.NonSerialized]
    public bool resetObjects;

    [System.NonSerialized]
    public bool levelComplete;
    private bool waitingForNextLevel;
    [System.NonSerialized]
    public bool levelRotated;

    public Text moves;

    [System.NonSerialized]
    public float gridLayer;
    [System.NonSerialized]
    public bool draggingObject;
    [System.NonSerialized]
    public bool draggingScreen;
    [System.NonSerialized]
    public float currentRotation;

    [System.NonSerialized]
    public bool sideView;
    [System.NonSerialized]
    public bool topView;
    [System.NonSerialized]
    public bool mouseOverObject;

    public bool makeObjectsTransparent;

    [System.NonSerialized]
    public float mouseXInv;
    [System.NonSerialized]
    public float mouseYInv;
    [System.NonSerialized]
    public float mouseWheelInv;

    public AudioSource sfx;

    public Transform gridPos;

    public float endX;
    public float endY;
    public float endZ;

    private int initialMoves;

    //private Scene currentLevel;
    //private string[] levelName;
    //private float levelNumber;
    //private string nextLevel;
    private int nextLevelIndex;


    void Start()
    {
        draggingObject = false;
        levelComplete = false;
        objectSelected = false;
        sideView = false;
        mouseOverObject = false;
        initialMoves = numberOfMoves;

        mouseXInv = 1;
        mouseYInv = 1;
        mouseWheelInv = 1;

        nextLevelIndex = SceneManager.GetActiveScene().buildIndex + 1;
        //currentLevel = SceneManager.GetActiveScene();
        //levelName = currentLevel.name.Split(' ');
        //levelNumber = float.Parse(levelName[1]);
        //levelNumber++;
        //nextLevel = levelName[0] + ' ' + levelNumber;
        currentRotation = 0;
    }

    private void Update()
    {
        //Debug.Log("Dragging Object:" + draggingObject + " Object Selected: " + objectSelected);
        if(topView)
            gridLayer = gridPos.position.y;
        if (sideView)
            gridLayer = gridPos.position.x;
        else
            gridLayer = gridPos.position.z;

        if (resetObjects)
        {
            numberOfMoves = initialMoves;
            moves.text = "You've Failed! Try again";
        }

        if (levelComplete)
        {
            moves.text = "Cleared!";
            StartCoroutine(NextLevelDelay());
        }

        else if(!resetObjects && !levelComplete)
            moves.text = "Moves Left: " + numberOfMoves;

        if (objectSelected || Input.GetKeyDown(KeyCode.Q) || Input.GetKeyDown(KeyCode.E))
            levelRotated = false;
    }

    IEnumerator NextLevelDelay()
    {
        yield return new WaitForSeconds(2f);
        if (SceneManager.sceneCountInBuildSettings > nextLevelIndex)
            SceneManager.LoadScene(nextLevelIndex);
        else
            Application.Quit();
    }
}
