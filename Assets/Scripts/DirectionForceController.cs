using UnityEngine;
using System.Collections;

public class DirectionForceController : MonoBehaviour {

    public float Speed;
    public float Angle_of_Forward_force;
    public bool bGrounded;
    public float MagnitudeMax;
    private Rigidbody Object_RB;
    private GameController GC;


    public Vector3 vStartVector;
    public Vector3 vEndVector;

    private LineTouchController LTC;
    // Use this for initialization
    void Start () {
        GC = GameObject.FindGameObjectWithTag("Player").GetComponent<GameController>();
        Object_RB = gameObject.GetComponent<Rigidbody>();
        vStartVector = Vector3.zero;
        vEndVector = Vector3.zero;
        bGrounded = true;

        LTC = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<LineTouchController>();
    }
	
	// Update is called once per frame
	void Update () {

        if (GC.GameStart)
        {
            mouseControls();
            //touchControls();
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

            Vector3 vForce = Quaternion.Euler(Angle_of_Forward_force, 1, 1) * vDirection;

            Object_RB.AddForce(Vector3.ClampMagnitude(vForce * Speed, MagnitudeMax));

            Debug.Log(Vector3.ClampMagnitude(vForce * Speed, MagnitudeMax).magnitude);

            bGrounded = false;
        }
        else if (Input.GetMouseButton(0)) //mouse held down
        {
            vEndVector = Input.mousePosition;
            float magnitude = Vector3.Distance(vStartVector, vEndVector);
            Vector3 vDirectionRot = vEndVector - vStartVector;
            Vector3 vForceRot = Quaternion.Euler(Angle_of_Forward_force, 0, 0) * vDirectionRot;
            gameObject.GetComponent<RotationByMagnitude>().rotationAngle = vForceRot;


        }
    }

    private void touchControls()
    {
        int nbTouches = Input.touchCount;

        if (nbTouches > 0)
        {
            Touch touch = Input.GetTouch(0);
            TouchPhase phase = touch.phase;

            switch (phase)
            {
                case TouchPhase.Began:
                    vStartVector = touch.position;
                    break;
                case TouchPhase.Moved:
                    vEndVector = Input.mousePosition;
                    float magnitude = Vector3.Distance(vStartVector, vEndVector);
                    Vector3 vDirectionRot = vEndVector - vStartVector;
                    Vector3 vForceRot = Quaternion.Euler(Angle_of_Forward_force, 0, 0) * vDirectionRot;
                    gameObject.GetComponent<RotationByMagnitude>().rotationAngle = vForceRot;
                    break;
                case TouchPhase.Stationary:
                    vEndVector = Input.mousePosition;
                    float magnitudeSta = Vector3.Distance(vStartVector, vEndVector);
                    Vector3 vDirectionRotSta = vEndVector - vStartVector;
                    Vector3 vForceRotSta = Quaternion.Euler(Angle_of_Forward_force, 0, 0) * vDirectionRotSta;
                    gameObject.GetComponent<RotationByMagnitude>().rotationAngle = vForceRotSta;
                    break;
                case TouchPhase.Ended:

                    vEndVector = touch.position;
                    Vector3 vDirection = vEndVector - vStartVector;
                    Vector3 vForce = Quaternion.Euler(Angle_of_Forward_force, 0, 0) * vDirection;

                    Object_RB.AddForce(vForce * Speed);
                    bGrounded = false;

                    break;
                case TouchPhase.Canceled:
                    print("Touch index " + touch.fingerId + " cancelled");
                    break;
            }

        }


    }
}
