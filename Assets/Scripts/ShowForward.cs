using UnityEngine;
using System.Collections;

public class ShowForward : MonoBehaviour
{
    public Color ForwardColor = Color.red;
    public Color UpColor = Color.green;
    public int LineLength = 1000;
	
	void Update () {
        var forward = transform.TransformDirection(Vector3.forward) * LineLength;
        Debug.DrawRay(transform.position, forward, ForwardColor);
        var upward = transform.TransformDirection(Vector3.up) * LineLength;
        Debug.DrawRay(transform.position, upward, UpColor);
    }
}
