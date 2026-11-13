using System;
using UnityEngine;

public class Pipe : MonoBehaviour
{
    [SerializeField] float _speed = 0.5f;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += Vector3.left * Time.deltaTime * _speed;
    }

}
