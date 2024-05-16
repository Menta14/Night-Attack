using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.SceneManagement;

public class PlayerControllerScript : MonoBehaviour
{
    public float speed = 2f;
    private float actualSpeed = 2f;
    private SpriteRenderer spr;
    private bool hidden = false;
    public static string question;
    public static string[] answers;
    public static string correct;
    private Vector3 snap2D = new Vector3(1, 1, 0);
    void Start()
    {
        actualSpeed = speed;
        GameObject[] wps = GameObject.FindGameObjectsWithTag("Waypoint");
        foreach (GameObject wp in wps)
        {
            wp.GetComponent<SpriteRenderer>().enabled = false;
        }
        SceneManager.sceneLoaded += onSceneLoad;
        SceneManager.sceneUnloaded += onSceneUnload;
    }
    void FixedUpdate()
    {
        transform.position += new Vector3(Input.GetAxis("Horizontal")*actualSpeed*Time.fixedDeltaTime, Input.GetAxis("Vertical")*actualSpeed*Time.fixedDeltaTime, 0);
        spr = gameObject.GetComponent<SpriteRenderer>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Tent")
        {
            spr.color = Color.black;
            hidden = true;
            TentData data = collision.gameObject.GetComponent<TentData>();
            if (!data.guarded)
                return;
            if (!data.Visited)
            {
                data.Visited = true;
                question = data.question;
                answers = data.answers;
                correct = data.correct;
                transform.position = collision.gameObject.GetComponent<Renderer>().bounds.center;
                SceneManager.LoadScene("Tent", LoadSceneMode.Additive);
            }
        }
        if (collision.gameObject.tag == "Guard")
        {
            if (hidden)
                return;
            SceneManager.LoadScene("Game over", LoadSceneMode.Additive);
            return;
        }
        if (collision.gameObject.tag == "Exit")
        {
            SceneManager.LoadScene("Ending");
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Tent")
        {
            spr.color = Color.yellow;
            hidden = false;
            GameObject trap = collision.GetComponent<TentData>().trap;
            if (trap != null)
                trap.SetActive(false);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "NPCbody")
        {
            Destroy(collision.gameObject);
        }
    }

    private void onSceneLoad(Scene loadedScene, LoadSceneMode loadSceneMode)
    {
        GameObject unlinkedCanvas = GameObject.FindGameObjectWithTag("Unlinked");
        unlinkedCanvas.GetComponent<Canvas>().worldCamera = Camera.main;
        if (loadedScene.name == "Tent")
            GameObject.Find("Question_light").transform.position = Vector3.Scale(Camera.main.transform.position, snap2D);

        if (loadedScene.name == "Game over")
        {
            spr.color = Color.black;
            actualSpeed = 0f;
            GameObject[] npcs = GameObject.FindGameObjectsWithTag("NPCbody");
            Collider2D curr = GetComponent<Collider2D>();
            foreach (GameObject body in npcs)
            {
                if (body == gameObject)
                    continue;
                Physics2D.IgnoreCollision(curr, body.GetComponent<Collider2D>());
                Physics2D.IgnoreCollision(curr, body.transform.GetChild(0).GetComponent<Collider2D>());
            }
        }
        else
            actualSpeed = 0f;
    }

    private void onSceneUnload(Scene unloadedScene)
    {
        actualSpeed = speed;
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= onSceneLoad;
        SceneManager.sceneUnloaded -= onSceneUnload;
    }
}
