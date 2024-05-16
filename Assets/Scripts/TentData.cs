using UnityEngine;

public class TentData : MonoBehaviour
{
    [Multiline]
    public string question = "Sample Question";
    public string[] answers = {"A", "B", "C", "D"};
    public string correct = "A";
    private bool visited = false;
    public bool guarded = false;
    public GameObject trap;

    public bool Visited { get => visited; set { trap.SetActive(!value); visited = value; } }
}
