using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerControllerScript : MonoBehaviour
{
    public float speed = 2f;
    private SpriteRenderer spr;
    private bool hidden = false;

    void Start()
    {
        GameObject[] wps = GameObject.FindGameObjectsWithTag("Waypoint");
        foreach (GameObject wp in wps)
        {
            wp.GetComponent<SpriteRenderer>().enabled = false;
        }
    }
    void FixedUpdate()
    {
        transform.position += new Vector3(Input.GetAxis("Horizontal")*speed*Time.fixedDeltaTime, Input.GetAxis("Vertical")*speed*Time.fixedDeltaTime, 0);
        spr = gameObject.GetComponent<SpriteRenderer>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Tent")
        {
            spr.color = Color.black;
            hidden = true;
            return;
        }
        if (collision.gameObject.tag == "Guard")
        {
            if (hidden)
                return;
            SceneManager.LoadScene(0);
            return;
        }
        if (collision.gameObject.tag == "Exit")
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Tent")
        {
            spr.color = Color.yellow;
            hidden = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "NPCbody")
        {
            Destroy(collision.gameObject);
        }
    }
}
