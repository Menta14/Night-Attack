using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class NPCControllerScript : MonoBehaviour
{
    public GameObject wplist;
    public float speed;
    private List<Transform> waypoints;
    private int current;

    private void Start()
    {
        waypoints = wplist.GetComponentsInChildren<Transform>().ToList();
        waypoints = (from waypoint in waypoints where waypoint.name != wplist.name select waypoint).ToList();

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
            float randomNumber = Random.Range(0, waypoints.Count);
            int number = (int)Mathf.Floor(randomNumber);
            current = number;
            float angle = Mathf.Atan2(waypoints[current].position.y - transform.position.y, waypoints[current].position.x - transform.position.x) * Mathf.Rad2Deg;
            Quaternion targetRotation = Quaternion.Euler(new Vector3(0, 0, angle));
            transform.rotation = targetRotation;
        }
    }
}
