using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereMovement : MonoBehaviour
{
    private Transform ball;
    // Start is called before the first frame update
    void Start()
    {
        ball = gameObject.GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        //ball.Translate(new Vector3(0f, -4.0f, 0f));
    }
}
