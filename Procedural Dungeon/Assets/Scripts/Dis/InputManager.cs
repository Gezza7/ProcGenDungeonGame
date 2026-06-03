using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{

    public PlayerInput input;
    public InputAction move;
    public InputAction mouse;
    public InputAction escape;
    public InputAction sprint;
    public InputAction attack;
    public PlayerMovement playerMove;
    public CameraMove camMove;
    public Inventory inventory;
    public float sprintSpeed = 100;
    public float walkSpeed = 50;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        move = input.actions.FindAction("Move");
        mouse = input.actions.FindAction("MoveMouse");
        escape = input.actions.FindAction("Escape");
        sprint = input.actions.FindAction("Sprint");
        attack = input.actions.FindAction("Attack");

        move.Enable();
        mouse.Enable();
        escape.Enable();
        sprint.Enable();
        attack.Enable();

        move.performed += movePlayer;
        move.canceled += stopPlayer;
        mouse.performed += moveMouse;
        escape.performed += escapeMenu;
        sprint.performed += sprinting;
        sprint.canceled += stopSprinting;
        attack.performed += attackPerformed;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void movePlayer(InputAction.CallbackContext context)
    {
        Vector2 moved = context.ReadValue<Vector2>();
        playerMove.moveDir = new Vector3(moved.x, 0, moved.y);
    }

    public void stopPlayer(InputAction.CallbackContext context)
    {
        playerMove.moveDir = Vector3.zero;
    }

    public void moveMouse(InputAction.CallbackContext context)
    {
        camMove.look = context.ReadValue<Vector2>();
    }

    public void escapeMenu(InputAction.CallbackContext context)
    {
        //menu stuff
    }

    public void sprinting(InputAction.CallbackContext context)
    {
        //if(Time.timeScale == 0) Time.timeScale =1;
        //else Time.timeScale = 0;
            playerMove.speed = sprintSpeed;
    }

    public void stopSprinting(InputAction.CallbackContext context)
    {
        playerMove.speed = walkSpeed;
    }

    public void attackPerformed(InputAction.CallbackContext context)
    {
        inventory.attack();
    }

}
