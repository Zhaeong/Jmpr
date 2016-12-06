using UnityEngine;
using System.Collections;

public class DirectionForceController : MonoBehaviour {

    private Rigidbody Object_RB;
    public float Speed;

    Vector3 vStartVector;
    Vector3 vEndVector; 
    // Use this for initialization
    void Start () {
        Object_RB = gameObject.GetComponent<Rigidbody>();
        vStartVector = Vector3.zero;
        vEndVector = Vector3.zero;

    }
	
	// Update is called once per frame
	void Update () {        

        if (Input.GetMouseButtonDown(0))
        {
            vStartVector = Input.mousePosition;
        }

        if (Input.GetMouseButtonUp(0))
        {
            vEndVector = Input.mousePosition;
            Vector3 vDirection = vEndVector - vStartVector;
            Object_RB.AddForce(vDirection * Speed);
        }
        Debug.Log("startvec:" + vStartVector + " Endvec:" + vEndVector);
    }


}
