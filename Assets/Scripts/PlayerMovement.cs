using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Vector3 displacement;
    private Vector3 startingPosition;
    private CharacterController charControl;
    private int movementSpeed = 10;
    public bool isPlayerOne = true;
    public bool movementEnabled = true;
    // Start is called before the first frame update
    void Start()
    {
        charControl = gameObject.GetComponentInChildren<CharacterController>();
        displacement = new Vector3(0, 0, 0);
        startingPosition = transform.position;
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

    public void resetPosition()
    {
        transform.position = startingPosition;
    }
}
