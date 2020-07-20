using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameState : MonoBehaviour
{
    private bool paused;
    private bool controllerInput;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //***Checking if The keyboard or controller is being used****
        for (int i = 0; i < 20; i++)
        {
            if (Input.GetKeyDown("joystick 1 button " + i))
            {
                controllerInput = true;
            }
        }
        if (Input.GetAxis("HorizontalJoy") != 0 || Input.GetAxis("VerticalJoy") != 0 || Input.GetAxis("RightStick X") != 0 || Input.GetAxis("RightStick Y") != 0)
        {
            controllerInput = true;
        }
        else if (Input.GetAxis("Fire1") != 0f)
        {
            controllerInput = true;
        }

        if (Input.GetAxis("Mouse X") != 0 || Input.GetAxis("Mouse Y") != 0 || Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0)
        {
            controllerInput = false;
        }
        else if (Input.GetButton("Fire1"))
        {
            controllerInput = false;
        }
    }
    public bool ControllerInput
    {
        get
        {
            return controllerInput;
        }
        set
        {
            controllerInput = value;
        }
    }
    public bool Paused
    {
        get
        {
            return paused;
        }
        set
        {
            paused = value;
        }
    }

}
