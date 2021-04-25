using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Transform ball;
    private Vector3 displacement;
    private CharacterController charControl;
    private int movementSpeed = 10;
    [SerializeField] private bool isPlayerOne;
    public bool movementEnabled = true;
    // Start is called before the first frame update
    void Start()
    {
        ball = gameObject.GetComponentInChildren<Transform>();
        charControl = gameObject.GetComponentInChildren<CharacterController>();
        displacement = new Vector3(0, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        if(movementEnabled)
        {
            if (isPlayerOne)
            {
                displacement.x = Input.GetAxis("Horizontal") * movementSpeed;
                displacement.z = Input.GetAxis("Vertical") * movementSpeed;
            }
            else
            {
                displacement.x = Input.GetAxis("Horizontal2") * movementSpeed;
                displacement.z = Input.GetAxis("Vertical2") * movementSpeed;
            }
            if (charControl.isGrounded)
                displacement.y = 0;
            else
                displacement.y -= 9.81f * Time.deltaTime;
            displacement = transform.TransformDirection(displacement);
            charControl.Move(displacement * Time.deltaTime);
        }
    }
}
