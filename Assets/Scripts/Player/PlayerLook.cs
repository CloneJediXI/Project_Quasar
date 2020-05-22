using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLook : MonoBehaviour
{
    private PlayerMovement pm;
    private Vector3 mouse_pos;
    private Vector3 mouse_world;
    // Start is called before the first frame update
    void Start()
    {
        pm = this.GetComponent<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {

            if (pm.controllerInput)
            {
                Look(Input.GetAxis("RightStick X"), Input.GetAxis("RightStick Y"));
            }
            else
            {
                Look();
            }

            //Steady your self
            //gameObject.transform.eulerAngles = new Vector3(0f, gameObject.transform.eulerAngles.y, 0f);
        
    }
    void Look(float inputX, float inputY)
    {
        if (inputX != 0 || inputY != 0)
        {
            float angle = Mathf.Rad2Deg * Mathf.Atan(inputY / inputX);
            angle = (-angle + 90);
            if (inputX < 0)
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
        mouse_world = Camera.main.ScreenToWorldPoint(mouse_pos);

        transform.LookAt(mouse_world);
        transform.Rotate(new Vector3(0f, 90f, 90f));
    }
}
