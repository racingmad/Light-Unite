using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class transparencyCheck : MonoBehaviour
{

    private gameController selectionCheck;

    public Renderer mat1;
    public Renderer mat2;

    private Rigidbody rb;

    void Start()
    {
        selectionCheck = GameObject.Find("Layout").gameObject.GetComponent<gameController>();
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (selectionCheck.makeObjectsTransparent)
        {
            if (selectionCheck.numberOfMoves > 0)
            {
                if (selectionCheck.draggingScreen)
                {
                    mat1.material.color = new Color(mat1.material.color.r, mat1.material.color.g, mat1.material.color.b, 1.0f);
                    mat2.material.color = new Color(mat2.material.color.r, mat2.material.color.g, mat2.material.color.b, 1.0f);
                    rb.detectCollisions = false;
                }
                else if (selectionCheck.sideView)
                {
                    if (transform.position.x >= selectionCheck.gridLayer - 0.25f && transform.position.x <= selectionCheck.gridLayer + 0.25f)
                    {
                        mat1.material.color = new Color(mat1.material.color.r, mat1.material.color.g, mat1.material.color.b, 1.0f);
                        mat2.material.color = new Color(mat2.material.color.r, mat2.material.color.g, mat2.material.color.b, 1.0f);
                        rb.detectCollisions = true;
                    }
                    else
                    {
                        mat1.material.color = new Color(mat1.material.color.r, mat1.material.color.g, mat1.material.color.b, 0.05f);
                        mat2.material.color = new Color(mat2.material.color.r, mat2.material.color.g, mat2.material.color.b, 0.0f);
                        rb.detectCollisions = false;
                    }
                }
                else
                {
                    if (transform.position.z >= selectionCheck.gridLayer - 0.25f && transform.position.z <= selectionCheck.gridLayer + 0.25f)
                    {
                        mat1.material.color = new Color(mat1.material.color.r, mat1.material.color.g, mat1.material.color.b, 1.0f);
                        mat2.material.color = new Color(mat2.material.color.r, mat2.material.color.g, mat2.material.color.b, 1.0f);
                        rb.detectCollisions = true;
                    }
                    else
                    {
                        mat1.material.color = new Color(mat1.material.color.r, mat1.material.color.g, mat1.material.color.b, 0.05f);
                        mat2.material.color = new Color(mat2.material.color.r, mat2.material.color.g, mat2.material.color.b, 0.0f);
                        rb.detectCollisions = false;
                    }
                }
                if (selectionCheck.objectSelected)
                    rb.detectCollisions = true;
            }
            else
            {
                mat1.material.color = new Color(mat1.material.color.r, mat1.material.color.g, mat1.material.color.b, 1.0f);
                mat2.material.color = new Color(mat2.material.color.r, mat2.material.color.g, mat2.material.color.b, 1.0f);
                rb.detectCollisions = true;
            }
        }
    }
}