using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    public float speed;
    private Rigidbody rb;

    public bool controllerInput;
    private Vector3 mouse_pos;
    private float angle;
    private float horizontal;
    private float vertical;

    private GameState gs;
    // Start is called before the first frame update
    void Start()
    {
        rb = this.GetComponent<Rigidbody>();
        gs = Camera.main.GetComponent<GameState>();
    }
    void Update()
    {
        
    }
    void FixedUpdate()
    {
        if (!gs.Paused)
        {
            if (gs.ControllerInput)
            {
                Move(Input.GetAxis("HorizontalJoy"), Input.GetAxis("VerticalJoy"));
                Look(Input.GetAxis("RightStick X"), Input.GetAxis("RightStick Y"));
            }
            else
            {
                Move(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
                Look();
            }
        }
    }

    void Move(float inputX, float inputY)
    {
        rb.velocity = new Vector3(-inputY*speed, 0f, inputX*speed);
    }
    void Look(float inputX, float inputY)
    {
        if (inputX !=0 || inputY != 0)
        {
            float angle = Mathf.Rad2Deg * Mathf.Atan(inputY / inputX);
            angle = (-angle + 90);
            if(inputX < 0)
            {
                angle += 180;
            }
            gameObject.transform.eulerAngles = new Vector3(0f, angle, 0f);
        }
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
