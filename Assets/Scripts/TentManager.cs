using System.Collections;
using System.Threading;
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
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
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
                        SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene());
                    else
                        SceneManager.LoadScene("Level 1");
                }
            }
        }
    }
}
