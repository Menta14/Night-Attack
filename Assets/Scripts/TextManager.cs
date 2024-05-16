using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TextManager : MonoBehaviour
{
    public AudioClip finalMusic;
    private List<TextMeshProUGUI> seq;
    private TextMeshProUGUI info;
    private int current;

    private void Start()
    {
        info = transform.Find("Canvas_static").GetChild(0).GetComponent<TextMeshProUGUI>();
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
                if (SceneManager.GetActiveScene().name == "Intro")
                    SceneManager.LoadScene("Level 1");
                else
                    SceneManager.LoadScene("Intro");
                return;
            }
            seq[current].enabled = false;
            seq[++current].enabled = true;
            if (current == seq.Count - 1 && SceneManager.GetActiveScene().name == "Ending")
            {
                info.text = "apasa [space] pentru a reincepe jocul\napasa [escape] pentru a iesi din joc";
                AudioSource audioSource = GetComponent<AudioSource>();
                audioSource.Stop();
                audioSource.loop = false;
                audioSource.PlayOneShot(finalMusic);
            }
        }
        else if (Input.GetKeyUp(KeyCode.Escape))
        {
            Application.Quit();
        }
    }
}
