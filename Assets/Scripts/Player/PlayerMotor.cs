using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerMotor : MonoBehaviour
{
    public PlayerInput input;
    public Rigidbody rigidbody;
    public float speed = 5f;

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
        desiredPosition = transform.position + new Vector3(inputVector.x, 0, inputVector.y) * speed * Time.deltaTime;
    }

    void OnMoveUp(Vector2 inputVector)
    {
        wantsMove = false;
    }

    private void FixedUpdate()
    {
        if (wantsMove)
        {
            rigidbody.MovePosition(desiredPosition);
        }
    }
}
