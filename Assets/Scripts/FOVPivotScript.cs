using UnityEngine;

public class FOVPivotScript : MonoBehaviour
{
    public Transform target;
    void Start()
    {
        transform.parent = target;
        
    }
}
