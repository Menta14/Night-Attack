using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TentManager : MonoBehaviour
{
    public TextMeshProUGUI question;
    public TextMeshProUGUI[] answers = new TextMeshProUGUI[4];
    private string correct;

    private void Start()
    {
        question.text = PlayerControllerScript.question;
        for (int i = 0; i <  answers.Length; i++)
        {
            answers[i].text = PlayerControllerScript.answers[i];
        }
        correct = PlayerControllerScript.correct;
        SceneManager.sceneLoaded += onSceneLoad;
    }

    private void Update()
    {
        if (SceneManager.loadedSceneCount == 2 && Input.GetMouseButtonDown(0))
        {
            Vector2 rayOrigin = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(rayOrigin, Vector2.zero);
            if (hit.collider != null)
            {
                TextMeshProUGUI comp = hit.collider.GetComponent<TextMeshProUGUI>();
                if (comp != null)
                {
                    string submit = comp.text;
                    if (submit == correct)
                        SceneManager.UnloadSceneAsync("Tent");
                    else
                        SceneManager.LoadScene("Game over", LoadSceneMode.Additive);
                }
            }
        }
    }

    private void onSceneLoad(Scene loadedScene, LoadSceneMode loadSceneMode)
    {
        GameObject canvas = GameObject.FindGameObjectWithTag("Unlinked");
        canvas.GetComponent<Canvas>().worldCamera = Camera.main;
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= onSceneLoad;
    }
}
