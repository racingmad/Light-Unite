using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class objectMovement : MonoBehaviour
{

    public Material sel;
    public float moveSpeed;
    //[System.NonSerialized]
    //public float gridLayer;

    [Range(1f, 0.01f)]
    public float mouseSensitivity;

    private GameObject audioSourceList;
    private AudioSource moveSource;
    public AudioClip move;

    private Vector3 originalPos;
    private Vector3 currentPos;
    private Vector3 targetPos;

    private bool updatePos;

    private bool selected;
    private gameController selectionCheck;
    private Material original;

    private bool objectMoving;
    private bool draggingScreen;
    //private bool mouseOverObject;

    private float startTime;
    private float journeyLength;

    private Rigidbody rb;

    void Start()
    {
        selected = false;
        transform.position = new Vector3(Mathf.Round(transform.position.x), Mathf.Round(transform.position.y), Mathf.Round(transform.position.z));
        currentPos = transform.position;
        targetPos = transform.position;
        originalPos = currentPos;
        selectionCheck = transform.parent.gameObject.GetComponent<gameController>();
        original = GetComponent<Renderer>().material;
        rb = GetComponent<Rigidbody>();
        audioSourceList = GameObject.Find("Audio Sources");
        AudioSource[] sources = audioSourceList.GetComponents<AudioSource>();
        moveSource = sources[0];
        if (move != null)
            moveSource.clip = move;
    }

    void OnMouseOver()
    {
        selectionCheck.mouseOverObject = true;
        if(original.color.a == 1.0f)
        {
            if (Input.GetMouseButtonUp(0) && !selectionCheck.objectSelected && !selectionCheck.draggingObject && !draggingScreen && selectionCheck.numberOfMoves > 0)
            {
                selected = true;
                if (selectionCheck.levelRotated)
                    selectionCheck.levelRotated = false;
                selectionCheck.objectSelected = true;
                selectionCheck.resetObjects = false;
                GetComponent<Renderer>().material = sel;
            }
            else if (Input.GetMouseButtonDown(0) && selectionCheck.objectSelected && selected && selectionCheck.numberOfMoves > 0)
                selectionCheck.draggingObject = true;
            else if (Input.GetMouseButtonUp(0) && selectionCheck.objectSelected && selected && selectionCheck.numberOfMoves > 0)
            {
                selected = false;
                selectionCheck.objectSelected = false;
                selectionCheck.draggingObject = false;
                GetComponent<Renderer>().material = original;
            }
        }  
    }

    void OnMouseExit()
    {
        //    selected = false;
        //    selectionCheck.objectSelected = false;
        //    GetComponent<Renderer>().material = original;
        selectionCheck.mouseOverObject = false;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !selectionCheck.objectSelected && !draggingScreen && !selectionCheck.mouseOverObject)
        {
            selectionCheck.draggingScreen = true;
            draggingScreen = true;
        }    
        else if ((Input.GetMouseButtonUp(0) && !selectionCheck.objectSelected && draggingScreen && !selectionCheck.mouseOverObject) || (Input.GetMouseButtonUp(0) && objectMoving))
        {
            selectionCheck.draggingScreen = false;
            draggingScreen = false;
        }

        if (selectionCheck.levelRotated && !updatePos)
        {
            currentPos = transform.position;
            updatePos = true;
        }
        else if (!selectionCheck.levelRotated && updatePos)
            updatePos = false;


        if (selected)
        {
            if (/*((Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W)) || */
                (Input.GetMouseButton(0) && Input.GetAxis("Mouse Y") * selectionCheck.mouseYInv >= mouseSensitivity)/*)*/)
            {
                Collider[] detector = Physics.OverlapSphere(new Vector3(transform.position.x, transform.position.y, transform.position.z), 0.1f);
                if(selectionCheck.topView && !selectionCheck.sideView && currentPos.z < selectionCheck.endZ)
                    detector = Physics.OverlapSphere(new Vector3(transform.position.x, transform.position.y, transform.position.z + 1), 0.1f);
                else if(((!selectionCheck.topView && !selectionCheck.sideView) || (!selectionCheck.topView && selectionCheck.sideView)) && currentPos.y < selectionCheck.endY)
                    detector = Physics.OverlapSphere(new Vector3(transform.position.x, transform.position.y + 1, transform.position.z), 0.1f);
                else if (selectionCheck.topView && selectionCheck.sideView && currentPos.x < selectionCheck.endX)
                    detector = Physics.OverlapSphere(new Vector3(transform.position.x + 1, transform.position.y, transform.position.z), 0.1f);
                if (detector.Length == 0)
                {
                    if(selectionCheck.topView && !selectionCheck.sideView)
                        targetPos = new Vector3(currentPos.x, currentPos.y, currentPos.z + 1);
                    else if(selectionCheck.topView && selectionCheck.sideView)
                        targetPos = new Vector3(currentPos.x + 1, currentPos.y, currentPos.z);
                    else
                        targetPos = new Vector3(currentPos.x, currentPos.y + 1, currentPos.z);
                    //transform.position = new Vector3(currentPos.x, currentPos.y + 1, currentPos.z);
                    currentPos = transform.position;
                    selected = false;
                    startTime = Time.time;
                    journeyLength = Vector3.Distance(currentPos, targetPos);
                    GetComponent<Renderer>().material = original;
                    if (move != null)
                        moveSource.Play();
                    objectMoving = true;
                }   
            }
            if (/*((Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S)) || */
                (Input.GetMouseButton(0) && Input.GetAxis("Mouse Y") * selectionCheck.mouseYInv <= -mouseSensitivity)/*)*/)
            {
                Collider[] detector = Physics.OverlapSphere(new Vector3(transform.position.x, transform.position.y, transform.position.z), 0.1f);
                if(selectionCheck.topView && !selectionCheck.sideView && currentPos.z > -selectionCheck.endZ)
                    detector = Physics.OverlapSphere(new Vector3(transform.position.x, transform.position.y, transform.position.z - 1), 0.1f);
                else if(((!selectionCheck.topView && !selectionCheck.sideView) || (!selectionCheck.topView && selectionCheck.sideView)) && currentPos.y > -selectionCheck.endY)
                    detector = Physics.OverlapSphere(new Vector3(transform.position.x, transform.position.y - 1, transform.position.z), 0.1f);
                else if (selectionCheck.topView && selectionCheck.sideView && currentPos.x > -selectionCheck.endX)
                    detector = Physics.OverlapSphere(new Vector3(transform.position.x - 1, transform.position.y, transform.position.z), 0.1f);
                if (detector.Length == 0)
                {
                    //transform.position = new Vector3(currentPos.x, currentPos.y - 1, currentPos.z);
                    if(selectionCheck.topView && !selectionCheck.sideView)
                        targetPos = new Vector3(currentPos.x, currentPos.y, currentPos.z - 1);
                    else if(selectionCheck.topView && selectionCheck.sideView)
                        targetPos = new Vector3(currentPos.x - 1, currentPos.y, currentPos.z);
                    else
                        targetPos = new Vector3(currentPos.x, currentPos.y - 1, currentPos.z);
                    currentPos = transform.position;
                    selected = false;
                    startTime = Time.time;
                    journeyLength = Vector3.Distance(currentPos, targetPos);
                    GetComponent<Renderer>().material = original;
                    if (move != null)
                        moveSource.Play();
                    objectMoving = true;
                }
            }
            if (/*((Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A)) || */
                (Input.GetMouseButton(0) && (Input.GetAxis("Mouse X") * selectionCheck.mouseXInv) <= -mouseSensitivity)/*)*/)
            {
                Collider[] detector = Physics.OverlapSphere(new Vector3(transform.position.x, transform.position.y, transform.position.z), 0.1f);
                if (selectionCheck.sideView && currentPos.z > -selectionCheck.endZ)
                {
                    detector = Physics.OverlapSphere(new Vector3(transform.position.x, transform.position.y, transform.position.z - 1), 0.1f);
                }  
                else if(!selectionCheck.sideView && currentPos.x > -selectionCheck.endX)
                    detector = Physics.OverlapSphere(new Vector3(transform.position.x - 1, transform.position.y, transform.position.z), 0.1f);
                if (detector.Length == 0)
                {
                    //transform.position = new Vector3(currentPos.x - 1, currentPos.y, currentPos.z);
                    if (selectionCheck.sideView)
                        targetPos = new Vector3(currentPos.x, currentPos.y, currentPos.z - 1);
                    else
                        targetPos = new Vector3(currentPos.x - 1, currentPos.y, currentPos.z);
                    currentPos = transform.position;
                    selected = false;
                    startTime = Time.time;
                    journeyLength = Vector3.Distance(currentPos, targetPos);
                    GetComponent<Renderer>().material = original;
                    if (move != null)
                        moveSource.Play();
                    objectMoving = true;
                }
            }
            if (/*((Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D)) || */
                (Input.GetMouseButton(0) && (Input.GetAxis("Mouse X") * selectionCheck.mouseXInv) >= mouseSensitivity)/*)*/)
            {
                Collider[] detector = Physics.OverlapSphere(new Vector3(transform.position.x, transform.position.y, transform.position.z), 0.1f);
                if (selectionCheck.sideView && currentPos.z < selectionCheck.endZ)
                {
                    detector = Physics.OverlapSphere(new Vector3(transform.position.x, transform.position.y, transform.position.z + 1), 0.1f);
                }
                else if(!selectionCheck.sideView && currentPos.x < selectionCheck.endX)
                    detector = Physics.OverlapSphere(new Vector3(transform.position.x + 1, transform.position.y, transform.position.z), 0.1f);
                if (detector.Length == 0)
                {
                    //transform.position = new Vector3(currentPos.x + 1, currentPos.y, currentPos.z);
                    if (selectionCheck.sideView)
                        targetPos = new Vector3(currentPos.x, currentPos.y, currentPos.z + 1);
                    else
                        targetPos = new Vector3(currentPos.x + 1, currentPos.y, currentPos.z);
                    currentPos = transform.position;
                    selected = false;
                    startTime = Time.time;
                    journeyLength = Vector3.Distance(currentPos, targetPos);
                    GetComponent<Renderer>().material = original;
                    if (move != null)
                        moveSource.Play();
                    objectMoving = true;
                }
            }
            if ((Input.GetAxis("Mouse ScrollWheel") * selectionCheck.mouseWheelInv) >= 0.1)
            {
                Collider[] detector = Physics.OverlapSphere(new Vector3(transform.position.x, transform.position.y, transform.position.z), 0.1f);
                Debug.Log(currentPos.y + " VS " + selectionCheck.endY);
                if(selectionCheck.topView && !selectionCheck.sideView && currentPos.y < selectionCheck.endY)
                {
                    detector = Physics.OverlapSphere(new Vector3(transform.position.x, transform.position.y + 1, transform.position.z), 0.1f);
                    Debug.Log("Check 1");
                }
                else if (selectionCheck.sideView && !selectionCheck.topView && currentPos.x < selectionCheck.endX)
                    detector = Physics.OverlapSphere(new Vector3(transform.position.x + 1, transform.position.y, transform.position.z), 0.1f);
                else if(!selectionCheck.sideView && !selectionCheck.topView && currentPos.z < selectionCheck.endZ)
                    detector = Physics.OverlapSphere(new Vector3(transform.position.x, transform.position.y, transform.position.z + 1), 0.1f);
                if (detector.Length == 0)
                {
                    //transform.position = new Vector3(currentPos.x + 1, currentPos.y, currentPos.z);7
                    if(selectionCheck.topView && !selectionCheck.sideView)
                        targetPos = new Vector3(currentPos.x, currentPos.y + 1, currentPos.z);
                    else if (selectionCheck.sideView)
                        targetPos = new Vector3(currentPos.x + 1, currentPos.y, currentPos.z);
                    else
                        targetPos = new Vector3(currentPos.x, currentPos.y, currentPos.z + 1);
                    currentPos = transform.position;
                    selected = false;
                    startTime = Time.time;
                    journeyLength = Vector3.Distance(currentPos, targetPos);
                    GetComponent<Renderer>().material = original;
                    if (move != null)
                        moveSource.Play();
                    objectMoving = true;
                }
            }
            if ((Input.GetAxis("Mouse ScrollWheel") * selectionCheck.mouseWheelInv) <= -0.1)
            {
                Collider[] detector = Physics.OverlapSphere(new Vector3(transform.position.x, transform.position.y, transform.position.z), 0.1f);
                if(selectionCheck.topView && !selectionCheck.sideView && currentPos.y > -selectionCheck.endY)
                {
                    detector = Physics.OverlapSphere(new Vector3(transform.position.x, transform.position.y - 1, transform.position.z), 0.1f);
                    Debug.Log("Check 2");
                }
                if (selectionCheck.sideView && !selectionCheck.topView && currentPos.x > -selectionCheck.endX)
                    detector = Physics.OverlapSphere(new Vector3(transform.position.x - 1, transform.position.y, transform.position.z), 0.1f);
                else if(!selectionCheck.sideView && !selectionCheck.topView && currentPos.z > -selectionCheck.endZ)
                    detector = Physics.OverlapSphere(new Vector3(transform.position.x, transform.position.y, transform.position.z - 1), 0.1f);
                if (detector.Length == 0)
                {
                    //transform.position = new Vector3(currentPos.x + 1, currentPos.y, currentPos.z);
                    if(selectionCheck.topView && !selectionCheck.sideView)
                        targetPos = new Vector3(currentPos.x, currentPos.y - 1, currentPos.z);
                    else if (selectionCheck.sideView)
                        targetPos = new Vector3(currentPos.x - 1, currentPos.y, currentPos.z);
                    else
                        targetPos = new Vector3(currentPos.x, currentPos.y, currentPos.z - 1);
                    currentPos = transform.position;
                    selected = false;
                    startTime = Time.time;
                    journeyLength = Vector3.Distance(currentPos, targetPos);
                    GetComponent<Renderer>().material = original;
                    if (move != null)
                        moveSource.Play();
                    objectMoving = true;
                }
            }

        }

        if (Input.GetMouseButtonUp(0) && selectionCheck.draggingObject)
            selectionCheck.draggingObject = false;


        if (selectionCheck.resetObjects && !objectMoving)
        {
            targetPos = originalPos;
            currentPos = transform.position;
            if(currentPos != targetPos)
            {
                startTime = Time.time;
                journeyLength = Vector3.Distance(currentPos, targetPos);
                objectMoving = true;
            }
        }
            

        if(objectMoving)
        {
           
            float distCovered = (Time.time - startTime) * moveSpeed;

            float fracJourney = distCovered / journeyLength;
            transform.position = Vector3.Lerp(currentPos, targetPos, Mathf.Sin(fracJourney * (Mathf.PI * 0.5f)));
            if (fracJourney >= 1)
            {
                objectMoving = false;
                currentPos = transform.position;
                selectionCheck.objectSelected = false;
                if (!selectionCheck.resetObjects)
                    selectionCheck.numberOfMoves--;
            }
                
        }


        if(selectionCheck.makeObjectsTransparent)
        {
            if (selectionCheck.numberOfMoves > 0)
            {
                if (selectionCheck.draggingScreen)
                {
                    original.color = new Color(original.color.r, original.color.g, original.color.b, 1.0f);
                    rb.detectCollisions = false;
                }
                else if (selectionCheck.sideView)
                {
                    if (transform.position.x >= selectionCheck.gridLayer - 0.25f && transform.position.x <= selectionCheck.gridLayer + 0.25f)
                    {
                        original.color = new Color(original.color.r, original.color.g, original.color.b, 1.0f);
                        rb.detectCollisions = true;

                    }
                    else
                    {
                        original.color = new Color(original.color.r, original.color.g, original.color.b, 0.05f);
                        rb.detectCollisions = false;
                    }
                }
                else
                {
                    if (transform.position.z >= selectionCheck.gridLayer - 0.25f && transform.position.z <= selectionCheck.gridLayer + 0.25f)
                    {
                        original.color = new Color(original.color.r, original.color.g, original.color.b, 1.0f);
                        rb.detectCollisions = true;

                    }
                    else
                    {
                        original.color = new Color(original.color.r, original.color.g, original.color.b, 0.05f);
                        rb.detectCollisions = false;
                    }
                }
                if (selectionCheck.objectSelected)
                    rb.detectCollisions = true;
            }
            else
            {
                original.color = new Color(original.color.r, original.color.g, original.color.b, 1.0f);
                rb.detectCollisions = true;
            }
        }
    }
}
