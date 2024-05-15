using UnityEngine;

public class FollowScript : MonoBehaviour
{
    public Transform player;

    void FixedUpdate()
    {
        transform.position = new Vector3(player.position.x, player.position.y, transform.position.z);
    }
}
