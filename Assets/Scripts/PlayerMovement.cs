using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    private bool paused;
    public float speed;
    private Rigidbody rb;

    public Text joystickInfo;
    private string debugString;

    public bool controllerInput;
    private Vector3 mouse_pos;
    private Vector3 mouse_world;
    private float angle;

    public Transform lookAtTarget;
    // Start is called before the first frame update
    void Start()
    {
        rb = this.GetComponent<Rigidbody>();
        paused = false;
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        //***Checking if The keyboard or controller is being used****
        for (int i = 0; i < 20; i++)
        {
            if (Input.GetKeyDown("joystick 1 button " + i))
            {
                controllerInput = true;
            }
        }
        if(Input.GetAxis("HorizontalJoy") != 0 || Input.GetAxis("VerticalJoy") != 0 || Input.GetAxis("RightStick X") != 0 || Input.GetAxis("RightStick Y") != 0){
            controllerInput = true;
        }else if(Input.GetAxis("Fire1") != 0f)
        {
            controllerInput = true;
        }

        if(Input.GetAxis("Mouse X") != 0 || Input.GetAxis("Mouse Y") != 0 || Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0)
        {
            controllerInput = false;
        }else if (Input.GetButton("Fire1"))
        {
            controllerInput = false;
        }
        //************************************
        debugString = "";
        if (!paused)
        {
            if (controllerInput)
            {
                
                Move(Input.GetAxis("HorizontalJoy"), Input.GetAxis("VerticalJoy"));
                Look(Input.GetAxis("RightStick X"), Input.GetAxis("RightStick Y"));
            }
            else
            {
                
                Move(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
                Look();
            }
            
            //Steady your self
            //gameObject.transform.eulerAngles = new Vector3(0f, gameObject.transform.eulerAngles.y, 0f);
        }
        joystickInfo.text = debugString;
    }
    void LateFixedUpdate()
    {

    }
    void Move(float inputX, float inputY)
    {
        debugString += "L_Joy_X : " + inputX+"\n";
        debugString += "L_Joy_Y : " + inputY + "\n";
        rb.velocity = new Vector3(-inputY*speed, 0f, inputX*speed);
        debugString += "----------\n";
    }
    void Look(float inputX, float inputY)
    {
        debugString += "R_Joy_X : " + inputX + "\n";
        debugString += "R_Joy_Y : " + inputY + "\n";
        if (inputX !=0 || inputY != 0)
        {
            float angle = Mathf.Rad2Deg * Mathf.Atan(inputY / inputX);
            angle = (-angle + 90);
            if(inputX < 0)
            {
                angle += 180;
            }
            debugString += "X/Y : " + (inputY / inputX) + "\n";
            debugString += "Look angle : " + angle + "\n";
            gameObject.transform.eulerAngles = new Vector3(0f, angle, 0f);
        }
        else
        {
            debugString += "X/Y : " + "N/A" + "\n";
            debugString += "Look angle : " + "N/A" + "\n";
        }
        debugString += "----------\n";

    }
    void Look()
    {
        mouse_pos = Input.mousePosition;

        mouse_pos.z = 1f; //Must pe positive to be converted by Screen to World Point
        mouse_world = Camera.main.ScreenToWorldPoint(mouse_pos);
        debugString += "World_Mouse_X : " + mouse_world.x + "\n";
        debugString += "World_Mouse_Y : " + mouse_world.y + "\n";
        debugString += "World_Mouse_Z : " + mouse_world.z + "\n";

        lookAtTarget.position = new Vector3(mouse_world.x, 1f, mouse_world.y);
        
        transform.LookAt(mouse_world);
        transform.Rotate(new Vector3(0f, 90f, 90f));
    }
}
