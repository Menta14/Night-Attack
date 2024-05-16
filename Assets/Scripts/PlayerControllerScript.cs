using JetBrains.Annotations;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerControllerScript : MonoBehaviour
{
    public float speed = 2f;
    private SpriteRenderer spr;
    private bool hidden = false;
    public static string question;
    public static string[] answers;
    public static string correct;
    void Start()
    {
        GameObject[] wps = GameObject.FindGameObjectsWithTag("Waypoint");
        foreach (GameObject wp in wps)
        {
            wp.GetComponent<SpriteRenderer>().enabled = false;
        }
        SceneManager.sceneUnloaded += onSceneUnloaded;
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
            TentData data = collision.gameObject.GetComponent<TentData>();
            if (!data.visited)
            {
                data.visited = true;
                question = data.question;
                answers = data.answers;
                correct = data.correct;
                transform.position = collision.gameObject.GetComponent<Renderer>().bounds.center;
                StartCoroutine(EnterTent());
            }
        }
        if (collision.gameObject.tag == "Guard")
        {
            if (hidden)
                return;
            SceneManager.LoadScene(1);
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

    private void onSceneUnloaded(Scene unloadedScene)
    {
        foreach (GameObject obj in SceneManager.GetActiveScene().GetRootGameObjects())
        {
            obj.SetActive(true);
        }
    }

    private void OnDestroy()
    {
        SceneManager.sceneUnloaded -= onSceneUnloaded;
    }

    private IEnumerator EnterTent()
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("Tent", LoadSceneMode.Additive);

        while (!asyncLoad.isDone)
        {
            yield return null;
        }
        foreach (GameObject obj in SceneManager.GetActiveScene().GetRootGameObjects())
        {
            obj.SetActive(false);
        }
        SceneManager.SetActiveScene(SceneManager.GetSceneByName("Tent"));
    }
}
