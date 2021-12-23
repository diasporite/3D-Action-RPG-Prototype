using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    public float speed = -3f;

    void Update()
    {
        transform.position += speed * Vector3.right * Time.deltaTime;
    }
}
