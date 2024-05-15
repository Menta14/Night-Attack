using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class NPCControllerScript : MonoBehaviour
{
    public Transform[] waypoints;
    public float speed;
    private int current;

    private void Start()
    {
        transform.position = waypoints[0].position;
        float angle = Mathf.Atan2(waypoints[1].position.y - waypoints[0].position.y, waypoints[1].position.x - waypoints[0].position.x) * Mathf.Rad2Deg;
        Quaternion targetRotation = Quaternion.Euler(new Vector3(0, 0, angle));
        transform.rotation = targetRotation;
        current = 1;
    }

    private void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, waypoints[current].position, speed*Time.deltaTime);
        if (Vector3.Distance(transform.position, waypoints[current].position) < .001f)
        {   
            current = (current + 1) % waypoints.Length;
            float angle = Mathf.Atan2(waypoints[current].position.y - transform.position.y, waypoints[current].position.x - transform.position.x) * Mathf.Rad2Deg;
            Quaternion targetRotation = Quaternion.Euler(new Vector3(0, 0, angle));
            transform.rotation = targetRotation;
        }
    }
}
