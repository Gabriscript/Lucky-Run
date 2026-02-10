using UnityEngine;

public class PlayerMotor : MonoBehaviour
{
    public float speed = 15.0f;
    public float laneSpeed = 10.0f;

    public bool isMoving = false;
    private float targetX = 0f;
    private float currentX = 0f;
    private Vector3 startPosition;


    private const float LEFT_LANE = -2.5f; 
    private const float RIGHT_LANE = 2.5f; 

    void Awake()
    {
        startPosition = new Vector3(LEFT_LANE, transform.position.y, 0f);
        currentX = LEFT_LANE;
        targetX = LEFT_LANE;
        transform.position = startPosition;
    }

    void Update()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime, Space.World);

        if (isMoving)
        {
            currentX = Mathf.MoveTowards(currentX, targetX, laneSpeed * Time.deltaTime);
            Vector3 newPos = transform.position;
            newPos.x = currentX;
            transform.position = newPos;

            if (Mathf.Abs(currentX - targetX) < 0.1f)
            {
                currentX = targetX;
                Vector3 finalPos = transform.position;
                finalPos.x = currentX;
                transform.position = finalPos;
                isMoving = false;
            }
        }
    }

    public void MoveTo(float direction)
    {
        if (isMoving) return; 

    
        float newTargetX = (direction < 0) ? LEFT_LANE : RIGHT_LANE;


        if (Mathf.Abs(currentX - newTargetX) < 0.1f) return;
   
        targetX = newTargetX;
        isMoving = true;
    }

    public void ResetPosition()
    {
        currentX = LEFT_LANE;
        targetX = LEFT_LANE;
        isMoving = false;
        transform.position = startPosition;
    }


    public bool IsAtLeftLane() => Mathf.Abs(currentX - LEFT_LANE) < 0.1f;
    public bool IsAtRightLane() => Mathf.Abs(currentX - RIGHT_LANE) < 0.1f;
    public float GetCurrentLanePosition() => currentX;

 
    public int GetCurrentLaneIndex()
    {
        return IsAtLeftLane() ? 0 : 1;
    }
}
