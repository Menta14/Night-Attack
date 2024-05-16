using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class NPCControllerScript : MonoBehaviour
{
    public GameObject wplist;
    public float minSpeed = .5f, maxSpeed = 2f;
    private List<Transform> waypoints;
    private int current;
    private float speed;

    private int newIndex()
    {
        return Mathf.FloorToInt(Random.Range(0, waypoints.Count));
    }

    private float newSpeed()
    {
        return Random.Range(minSpeed, maxSpeed);
    }

    private void Start()
    {
        GameObject[] npcs = GameObject.FindGameObjectsWithTag("NPCbody");
        Collider2D curr = GetComponent<Collider2D>();
        foreach (GameObject body in npcs)
        {
            if (body == gameObject)
                continue;
            Physics2D.IgnoreCollision(curr, body.GetComponent<Collider2D>());
        }
        waypoints = wplist.GetComponentsInChildren<Transform>().ToList();
        waypoints = (from waypoint in waypoints where waypoint.name != wplist.name select waypoint).ToList();
        current = newIndex();
        transform.position = waypoints[current].position;
        current = (current + 1)%waypoints.Count;
        Vector3 directionToTarget = waypoints[current].position - transform.position;
        float targetAngle = Mathf.Atan2(directionToTarget.y,directionToTarget.x)*Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, targetAngle);
        speed = newSpeed();
        StartCoroutine(Patrol());
    }

    private IEnumerator Patrol()
    {
        while (true)
        {
            transform.position = Vector3.MoveTowards(transform.position, waypoints[current].position, speed * Time.deltaTime);
            if (Vector3.Distance(transform.position, waypoints[current].position) < .001f)
            {
                current = newIndex();
                yield return StartCoroutine(Turn(waypoints[current]));
                speed = newSpeed();
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
