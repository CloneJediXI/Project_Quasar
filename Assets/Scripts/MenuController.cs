using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;

public class MenuController : MonoBehaviour
{
    private bool held;
    private GameState gs;
    public bool playableScene;
    // Start is called before the first frame update
    void Start()
    {
        gs = Camera.main.GetComponent<GameState>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (gs.Paused || !playableScene)
        {
            if (gs.ControllerInput)
            {
                Navigate(Input.GetAxis("HorizontalJoy"), Input.GetAxis("VerticalJoy"));
            }
        }
    }
    void Navigate(float inputX, float inputY)
    {
        AxisEventData data = new AxisEventData(EventSystem.current);
        MoveDirection direction;
        if (Mathf.Abs(inputX) > .5 || Mathf.Abs(inputY) > .5){
            if (!held){
                if (Mathf.Abs(inputX) > Mathf.Abs(inputY))
                {
                    if (inputX > 0){
                        direction = MoveDirection.Right;}
                    else{
                        direction = MoveDirection.Left;}
                }else{
                    if (inputY > 0){
                        direction = MoveDirection.Up;}
                    else{
                        direction = MoveDirection.Down;}
                }
                Debug.Log("Moved");
                held = true;
                data.moveDir = direction;
                data.selectedObject = EventSystem.current.currentSelectedGameObject;
                ExecuteEvents.Execute(data.selectedObject, data, ExecuteEvents.moveHandler);
            }
        }else if(Mathf.Abs(inputX) < .5 && Mathf.Abs(inputY) < .5)
        {
            if (held)
            {
                held = false;
            }
            
        }
    }
}
