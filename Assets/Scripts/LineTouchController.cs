using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineTouchController : MonoBehaviour {

    public GameObject LineObjStart, LineObjEnd, LineObjMid;
    public Vector3 mousepos;
    public Vector3 worldPos;

    

    private LineRenderer LR;

    void Start()
    {
        LR = gameObject.GetComponent<LineRenderer>();
        LR.enabled = false;
        
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            LR.enabled = true;
            mousepos = Input.mousePosition;

            Vector3 WorldTransform = new Vector3(mousepos.x, mousepos.y, 3);

            worldPos = Camera.main.ScreenToWorldPoint(WorldTransform);

            LineObjStart.transform.position = worldPos;           
        }

        if (Input.GetMouseButton(0))
        {
            mousepos = Input.mousePosition;

            Vector3 WorldTransform = new Vector3(mousepos.x, mousepos.y, 3);

            worldPos = Camera.main.ScreenToWorldPoint(WorldTransform);

            LineObjEnd.transform.position = worldPos;
            Debug.Log(Vector3.Distance(LineObjStart.transform.position, LineObjEnd.transform.position));

            if (Vector3.Distance(LineObjStart.transform.position, LineObjEnd.transform.position) > 1)
            {
                LineObjMid.transform.position = LineObjStart.transform.position + (LineObjEnd.transform.position - LineObjStart.transform.position).normalized;


                LR.SetPosition(0, LineObjStart.transform.position);
                LR.SetPosition(1, LineObjMid.transform.position);
                //LR.SetPosition(2, LineObjEnd.transform.position);
            }
            else
            {
                LineObjMid.transform.position = worldPos;
                LR.SetPosition(0, LineObjStart.transform.position);
                LR.SetPosition(1, LineObjMid.transform.position);
                //LR.SetPosition(2, LineObjEnd.transform.position);
            }

            
        }
        if (Input.GetMouseButtonUp(0))
        {
            LR.enabled = false;
        }
    }
}

