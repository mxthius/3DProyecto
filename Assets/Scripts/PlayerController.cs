using System.Net.Sockets;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.XR;

public class PlayerController : MonoBehaviour
{

    //private Rigidbody;
    public CharacterController controller;
    public float speed;
    public float turnspeed;
    public InputActionReference moveAction;
    public Vector2 moveInput;
    public string varchar;
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    private void OnEnable()
    {
        moveAction.action.Enable();
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        //transform


        moveInput = moveAction.action.ReadValue<Vector2>();
        Debug.Log(moveInput);
        if (moveInput.x != 0)
        {
            float rotationaMOUNT = moveInput.x * turnspeed * Time.deltaTime;
            transform.Rotate(0f, rotationaMOUNT,0f);

        }

        Vector3 direccion = transform.forward * moveInput.y;
        controller.Move(direccion * speed * Time.deltaTime);

    }
}
