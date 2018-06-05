using UnityEngine;

public class ParallaxLayer : MonoBehaviour
{
    public float Slowdown = 0.5f;
    private Vector3 _lastPosition;

    void Awake()
    {
        _lastPosition = Camera.main.transform.position;
    }

    void LateUpdate()
    {
        var newPosition = Camera.main.transform.position;
        var diff = newPosition - _lastPosition;
        _lastPosition = newPosition;
        
        var myPos = transform.position;
        myPos += Slowdown * diff;
        transform.position = myPos;
    }
}