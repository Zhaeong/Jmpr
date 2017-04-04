using UnityEngine;
using System.Collections;

public class DirectionForceController : MonoBehaviour {

    public float Speed;
    public float Angle_of_Forward_force;
    public bool bGrounded;
    //public GameObject Particles, Particles2, Particles3;
    private Rigidbody Object_RB;
    private GameController GC;


    Vector3 vStartVector;
    Vector3 vEndVector; 
    // Use this for initialization
    void Start () {
        GC = GameObject.FindGameObjectWithTag("Player").GetComponent<GameController>();
        Object_RB = gameObject.GetComponent<Rigidbody>();
        vStartVector = Vector3.zero;
        vEndVector = Vector3.zero;
        bGrounded = true;
    }
	
	// Update is called once per frame
	void Update () {

        if (GC.GameStart)
        {
            mouseControls();
            touchControls();
        }        

    }

    private void mouseControls()
    {
        if (Input.GetMouseButtonDown(0))
        {
            vStartVector = Input.mousePosition;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            vEndVector = Input.mousePosition;
            Vector3 vDirection = vEndVector - vStartVector;
            Vector3 vForce = Quaternion.Euler(Angle_of_Forward_force, 0, 0) * vDirection;
            //if (bGrounded)
            //{
            Object_RB.AddForce(vForce * Speed);
            //Instantiate(Particles, transform.position, new Quaternion(0, 0, 0, 90));
            //Instantiate(Particles2, transform.position, new Quaternion(0, 0, 0, 90));
            //Instantiate(Particles3, transform.position, new Quaternion(0, 0, 0, 90));
            bGrounded = false;

            //}

        }
        else if (Input.GetMouseButton(0)) //mouse held down
        {
            vEndVector = Input.mousePosition;
            float magnitude = Vector3.Distance(vStartVector, vEndVector);
            GameObject icoSphere = GameObject.FindGameObjectWithTag("IcoSphere");
            icoSphere.GetComponent<RotationByMagnitude>().MagnitudeofVelocity = magnitude;
            Vector3 vDirectionRot = vEndVector - vStartVector;
            Vector3 vForceRot = Quaternion.Euler(Angle_of_Forward_force, 0, 0) * vDirectionRot;
            icoSphere.GetComponent<RotationByMagnitude>().rotationAngle = vForceRot;

        }
    }

    private void touchControls()
    {
        int nbTouches = Input.touchCount;

        if (nbTouches > 0)
        {
            /*
            for (int i = 0; i < nbTouches; i++)
            {
                Touch touch = Input.GetTouch(i);

                TouchPhase phase = touch.phase;

                switch (phase)
                {
                    case TouchPhase.Began:
                        print("New touch detected at position " + touch.position + " , index " + touch.fingerId);
                        break;
                    case TouchPhase.Moved:
                        print("Touch index " + touch.fingerId + " has moved by " + touch.deltaPosition);
                        break;
                    case TouchPhase.Stationary:
                        print("Touch index " + touch.fingerId + " is stationary at position " + touch.position);
                        break;
                    case TouchPhase.Ended:
                        print("Touch index " + touch.fingerId + " ended at position " + touch.position);
                        break;
                    case TouchPhase.Canceled:
                        print("Touch index " + touch.fingerId + " cancelled");
                        break;
                }
            }
            */

            Touch touch = Input.GetTouch(0);
            TouchPhase phase = touch.phase;

            switch (phase)
            {
                case TouchPhase.Began:
                    //print("New touch detected at position " + touch.position + " , index " + touch.fingerId);
                    vStartVector = touch.position;
                    break;
                case TouchPhase.Moved:
                    //print("Touch index " + touch.fingerId + " has moved by " + touch.deltaPosition);
                    vEndVector = Input.mousePosition;
                    float magnitude = Vector3.Distance(vStartVector, vEndVector);
                    GameObject icoSphere = GameObject.FindGameObjectWithTag("IcoSphere");
                    icoSphere.GetComponent<RotationByMagnitude>().MagnitudeofVelocity = magnitude;
                    Vector3 vDirectionRot = vEndVector - vStartVector;
                    Vector3 vForceRot = Quaternion.Euler(Angle_of_Forward_force, 0, 0) * vDirectionRot;
                    icoSphere.GetComponent<RotationByMagnitude>().rotationAngle = vForceRot;
                    break;
                case TouchPhase.Stationary:
                    //print("Touch index " + touch.fingerId + " is stationary at position " + touch.position);
                    vEndVector = Input.mousePosition;
                    float magnitudeSta = Vector3.Distance(vStartVector, vEndVector);
                    GameObject icoSphereSta = GameObject.FindGameObjectWithTag("IcoSphere");
                    icoSphereSta.GetComponent<RotationByMagnitude>().MagnitudeofVelocity = magnitudeSta;
                    Vector3 vDirectionRotSta = vEndVector - vStartVector;
                    Vector3 vForceRotSta = Quaternion.Euler(Angle_of_Forward_force, 0, 0) * vDirectionRotSta;
                    icoSphereSta.GetComponent<RotationByMagnitude>().rotationAngle = vForceRotSta;
                    break;
                case TouchPhase.Ended:
                    //print("Touch index " + touch.fingerId + " ended at position " + touch.position);

                    vEndVector = touch.position;
                    Vector3 vDirection = vEndVector - vStartVector;
                    Vector3 vForce = Quaternion.Euler(Angle_of_Forward_force, 0, 0) * vDirection;
                    //if (bGrounded)
                    //{
                    Object_RB.AddForce(vForce * Speed);
                    //Instantiate(Particles, transform.position, new Quaternion(0, 0, 0, 90));
                    //Instantiate(Particles2, transform.position, new Quaternion(0, 0, 0, 90));
                    //Instantiate(Particles3, transform.position, new Quaternion(0, 0, 0, 90));
                    bGrounded = false;

                    //}
                    break;
                case TouchPhase.Canceled:
                    print("Touch index " + touch.fingerId + " cancelled");
                    break;
            }

        }


    }
}
