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
    public Vector3 mouse_pos;
    private Vector3 mouse_world;
    public float angle;
    public float horizontal;
    public float vertical;

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
        //This assumes the player is always in the middle of the screen

        //Step 1: Find the mouse position relative to the player (Middle of the screen)
        horizontal = mouse_pos.x - (Screen.width / 2f);
        vertical = mouse_pos.y - (Screen.height / 2f);
        
        //Step 2: Find the angle from the center of the screen to the mouse
        angle = Mathf.Rad2Deg * Mathf.Atan(vertical / horizontal);

        //Step 3: Rotate the unit circle so 0 is vertical, account for ArcTan's blind spot
        angle = (-angle + 90);
        if (horizontal < 0)
        {
            angle += 180;
        }

        //Step 4: Rotate Player 
        gameObject.transform.eulerAngles = new Vector3(0f, angle, 0f);

    }
}
