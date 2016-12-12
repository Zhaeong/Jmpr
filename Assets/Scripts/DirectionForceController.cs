using UnityEngine;
using System.Collections;

public class DirectionForceController : MonoBehaviour {

    public float Speed;
    public bool bGrounded;
    public GameObject Particles, Particles2, Particles3;
    private Rigidbody Object_RB;


    Vector3 vStartVector;
    Vector3 vEndVector; 
    // Use this for initialization
    void Start () {
        Object_RB = gameObject.GetComponent<Rigidbody>();
        vStartVector = Vector3.zero;
        vEndVector = Vector3.zero;
        bGrounded = true;
    }
	
	// Update is called once per frame
	void Update () {

        if (Input.GetMouseButtonDown(0))
        {
            vStartVector = Input.mousePosition;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            vEndVector = Input.mousePosition;
            Vector3 vDirection = vEndVector - vStartVector;
            Vector3 vForce = Quaternion.Euler(45, 0, 0) * vDirection;
            if (bGrounded)
            {
                Object_RB.AddForce(vForce * Speed);
                Instantiate(Particles, transform.position, new Quaternion(0, 0, 0, 90));
                Instantiate(Particles2, transform.position, new Quaternion(0, 0, 0, 90));
                Instantiate(Particles3, transform.position, new Quaternion(0, 0, 0, 90));
                bGrounded = false;

            }

            //Debug.Log("startvec:" + vStartVector + " Endvec:" + vEndVector + "Forcevec: " + vForce + "mousedirVector:" + vDirection);
        }
        else if (Input.GetMouseButton(0)) //mouse held down
        {
            vEndVector = Input.mousePosition;
            float magnitude = Vector3.Distance(vStartVector, vEndVector);
            GameObject icoSphere = GameObject.FindGameObjectWithTag("IcoSphere");
            icoSphere.GetComponent<RotationByMagnitude>().MagnitudeofVelocity = magnitude;
            icoSphere.GetComponent<RotationByMagnitude>().rotationAngle = vEndVector - vStartVector;

        }
    }
}
