using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void InputEvent();
public delegate void MovementEvent(Vector2 inputVector);

public class PlayerInput : MonoBehaviour
{
    public int PlayerID;

    public MovementInput Movement;
    public GenericInput Jump;
    public GenericInput Action_1;
    public GenericInput Action_2;
    public GenericInput Action_3;

    void Awake()
    {
        Movement = new MovementInput(PlayerSpecificAxisName("Horizontal"), PlayerSpecificAxisName("Vertical"));
        Jump = new GenericInput(PlayerSpecificAxisName("Jump"));
        Action_1 = new GenericInput(PlayerSpecificAxisName("Action_1")); //Left Action
        Action_2 = new GenericInput(PlayerSpecificAxisName("Action_2")); //Top Action
        Action_3 = new GenericInput(PlayerSpecificAxisName("Action_3")); //Right Action
    }

    void Update()
    {
        CheckForInputs();
    }

    void CheckForInputs()
    {
        Movement.CheckInput();
        Jump.CheckInput();
        Action_1.CheckInput();
        Action_2.CheckInput();
        Action_3.CheckInput();
    }

    string PlayerSpecificAxisName(string axis)
    {
        return string.Format("Player_{0}_{1}", PlayerID, axis);
    }

}

public abstract class InputBase
{
    public string Axis;

    public InputEvent Down;
    public InputEvent Hold;
    public InputEvent Up;

    public virtual float CheckInput() { return 0; }
}

public class GenericInput : InputBase
{
    #region private variables
    private bool sentDownEvent;
    private bool sendUpEvent;
    #endregion

    public GenericInput(string axis)
    {
        this.Axis = axis;
    }

    public override float CheckInput()
    {
        float value = Input.GetAxisRaw(Axis);

        if (value > 0f)
        {
            sendUpEvent = true;
            if (!sentDownEvent)
            {
                if (Down != null)
                {
                    Down();
                }
                sentDownEvent = true;
            }
            else
            {
                if (Hold != null)
                {
                    Hold();
                }
            }
        }
        else
        {
            if (sendUpEvent)
            {
                //TODO: Possibly also send hold event?

                if (Up != null)
                {
                    Up();
                }
                sendUpEvent = false;
                sentDownEvent = false;
            }
        }

        return value;
    }
}

public class MovementInput
{
    public Vector2 inputVector = Vector2.zero;
    public bool normalize = true;

    public GenericInput Horizontal;
    public GenericInput Vertical;

    public MovementEvent Down;
    public MovementEvent Hold;
    public MovementEvent Up;

    #region private variables
    private bool sentDownEvent;
    private bool sendUpEvent;
    #endregion

    public MovementInput(string horizontalAxis, string verticalAxis, bool normalize = true)
    {
        Horizontal = new GenericInput(horizontalAxis);
        Vertical = new GenericInput(verticalAxis);
    }

    public void CheckInput()
    {
        inputVector.x = Horizontal.CheckInput();
        inputVector.y = Vertical.CheckInput();

        if (normalize && inputVector.magnitude > 1f){
            inputVector.Normalize();
        }

        if (inputVector.magnitude > 0f)
        {
            sendUpEvent = true;
            if (!sentDownEvent)
            {
                if (Down != null)
                {
                    Down(inputVector);
                }
                sentDownEvent = true;
            }
            else
            {
                if (Hold != null)
                {
                    Hold(inputVector);
                }
            }
        }
        else
        {
            if (sendUpEvent)
            {
                if (Up != null)
                {
                    Up(inputVector);
                }
                sendUpEvent = false;
                sentDownEvent = false;
            }
        }
    }
}


