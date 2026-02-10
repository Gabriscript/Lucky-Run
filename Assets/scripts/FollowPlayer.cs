using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    Transform playerTransform;
    private float fixedX;
    private float fixedY;
    private float zOffset;

    void Start()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player)
            playerTransform = player.transform;

        fixedX = transform.position.x;
        fixedY = transform.position.y;
        zOffset = transform.position.z - playerTransform.position.z;
    }

    void LateUpdate()
    {
        transform.position = new Vector3(fixedX, fixedY, playerTransform.position.z + zOffset);
    }
}