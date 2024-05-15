using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TextManager : MonoBehaviour
{
    public float animationSpeed = 2f;
    private List<TextMeshProUGUI> seq;
    private int current;

    private void Start()
    {
        seq = new List<TextMeshProUGUI>();
        Transform canvas = transform.Find("Canvas");
        for (int i = 0; i < canvas.childCount; i++) {
            seq.Add(canvas.GetChild(i).GetComponent<TextMeshProUGUI>());
            seq[i].enabled = false;
        }
        current = 0;
        seq[0].enabled = true;
    }

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.Space))
        {
            if (current == seq.Count - 1)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
                return;
            }
            seq[current].enabled = false;
            seq[++current].enabled = true;
        }
    }
}
