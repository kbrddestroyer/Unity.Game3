using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField, Range(0f, 10f)] private float smoothness;
    private Vector3 offset = new Vector3(0, 0, -10);
    private Vector3 velocity = Vector3.zero;
    public Transform player;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        
    }

    void Update()
    {
        Vector3 target = player.position + offset;
        transform.position = Vector3.SmoothDamp(transform.position, target, ref velocity, smoothness);
    }
}
