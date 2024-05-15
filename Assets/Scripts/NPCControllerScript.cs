using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class NPCControllerScript : MonoBehaviour
{
    public GameObject wplist;
    public float minSpeed = .5f, maxSpeed = 2f;
    private List<Transform> waypoints;
    private int current;
    private float speed;

    private void Start()
    {
        waypoints = wplist.GetComponentsInChildren<Transform>().ToList();
        waypoints = (from waypoint in waypoints where waypoint.name != wplist.name select waypoint).ToList();
        transform.position = waypoints[0].position;
        Vector3 directionToTarget = waypoints[1].position - waypoints[0].position;
        float targetAngle = Mathf.Atan2(directionToTarget.y,directionToTarget.x)*Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, targetAngle);
        current = 1;
        speed = Random.Range(minSpeed, maxSpeed);
        StartCoroutine(Patrol());
    }

    private IEnumerator Patrol()
    {
        while (true)
        {
            transform.position = Vector3.MoveTowards(transform.position, waypoints[current].position, speed * Time.deltaTime);
            if (Vector3.Distance(transform.position, waypoints[current].position) < .001f)
            {
                float randomNumber = Random.Range(0, waypoints.Count);
                current = (int)Mathf.Floor(randomNumber);
                yield return StartCoroutine(Turn(waypoints[current]));
                speed = Random.Range(minSpeed, maxSpeed);
            }
            yield return null;
        }
    }

    private IEnumerator Turn(Transform target)
    {
        Vector3 directionToTarget = target.position - transform.position;
        float targetAngle = Mathf.Atan2(directionToTarget.y, directionToTarget.x) * Mathf.Rad2Deg;
        float startAngle = transform.eulerAngles.z;
        float elapsedTime = 0f;
        while (elapsedTime < speed)
        {
            float angle = Mathf.LerpAngle(startAngle, targetAngle, elapsedTime / speed);
            transform.rotation = Quaternion.Euler(0, 0, angle);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        transform.rotation = Quaternion.Euler(0, 0, targetAngle);
    }
}
