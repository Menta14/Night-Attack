using UnityEngine;

public class TentData : MonoBehaviour
{
    [Multiline]
    public string question = "Sample Question";
    public string[] answers = {"A", "B", "C", "D"};
    public string correct = "A";
    public bool visited = false;
}
