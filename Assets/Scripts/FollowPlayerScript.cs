using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayerScript : MonoBehaviour
{
    public Transform player;

    private void Update()
    {
        transform.position = player.position + Vector3.back*10;
    }
}
