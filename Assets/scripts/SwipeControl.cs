using UnityEngine;

public class SwipeControl : MonoBehaviour
{
    Vector2 start, end;
    public Transform player;
    public PlayerMotor playerMotor;

    void Update()
    {
        // mouse
        if (Input.GetMouseButtonDown(0))
            start = Input.mousePosition;
        else if (Input.GetMouseButtonUp(0))
        {
            end = Input.mousePosition;
            Vector2 swipe = end - start;
            if (swipe.magnitude > 50f)
            {
                if (Mathf.Abs(swipe.x) > Mathf.Abs(swipe.y))
                {
                    if (swipe.x > 0)
                        Turn("right");
                    else
                        Turn("left");
                }
            }
        }



        //keyboard
        if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
            Turn("right");
        else if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
            Turn("left");


        // touch
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began)
                start = touch.position;
            else if (touch.phase == TouchPhase.Ended)
            {
                end = touch.position;
                Vector2 swipe = end - start;
                if (swipe.magnitude > 50f)
                {
                    if (Mathf.Abs(swipe.x) > Mathf.Abs(swipe.y))
                    {
                        if (swipe.x > 0)
                            Turn("right");
                        else
                            Turn("left");
                    }
                }
            }
        }
    }

    void Turn(string direction)
    {
        if (playerMotor != null && !playerMotor.isMoving)
            if (direction == "right")
                playerMotor.MoveTo(5f);
            else
                playerMotor.MoveTo(-5f);        
    }
}


