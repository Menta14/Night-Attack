using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    public AudioClip bgmusic;
    public AudioClip gameOver;
    public AudioClip correct;
    private AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.loop = true;
        audioSource.volume = .1f;
        audioSource.clip = bgmusic;
        audioSource.Play();
        SceneManager.sceneLoaded += onSceneLoad;
        SceneManager.sceneUnloaded += onSceneUnload;
    }

    private void onSceneLoad(Scene loadedScene, LoadSceneMode loadSceneMode)
    {
        if (loadedScene.name == "Game over")
            audioSource.PlayOneShot(gameOver);
    }

    private void onSceneUnload(Scene unloadedScene)
    {
        if (unloadedScene.name == "Tent")
            audioSource.PlayOneShot(correct);
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= onSceneLoad;
        SceneManager.sceneUnloaded -= onSceneUnload;
    }
}
