using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerMotor : MonoBehaviour
{
    public PlayerInput input;
    public new Rigidbody rigidbody;
    public float MovementSpeed = 5f;
    public float JumpHeight = 10f;

    private Vector3 desiredPosition;
    private bool wantsMove = false;

    private void OnEnable()
    {
        input.Movement.Down += OnMoveDown;
        input.Movement.Hold += OnMoveHold;
        input.Movement.Up += OnMoveUp;

        input.Jump.Down += OnJumpDown;
        input.Jump.Hold += OnJumpHold;
        input.Jump.Up += OnJumpUp;
    }

    void OnJumpDown()
    {
        rigidbody.AddForce(Vector3.up * JumpHeight);
    }

    void OnJumpHold()
    {

    }

    void OnJumpUp()
    {

    }

    void OnMoveDown(Vector2 inputVector)
    {

    }

    void OnMoveHold(Vector2 inputVector)
    {
        wantsMove = true;
        desiredPosition = transform.position + new Vector3(inputVector.x, 0, inputVector.y) * MovementSpeed * Time.deltaTime;
        rigidbody.AddForce(new Vector3(inputVector.x, rigidbody.velocity.y, inputVector.y) * MovementSpeed * Time.deltaTime, ForceMode.Impulse);

    }

    void OnMoveUp(Vector2 inputVector)
    {
        wantsMove = false;
    }

    private void FixedUpdate()
    {
        if (wantsMove)
        {
            //rigidbody.MovePosition(desiredPosition);
        }
    }
}
