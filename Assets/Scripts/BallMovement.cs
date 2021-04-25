using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallMovement : MonoBehaviour
{
    private Rigidbody rigid;
    // Start is called before the first frame update
    void Start()
    {
        rigid = gameObject.GetComponentInChildren<Rigidbody>();
        rigid.AddForce(transform.right * 5000);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        rigid.AddForce(transform.right * 50);
    }
}
