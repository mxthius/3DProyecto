using System.Net.Sockets;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.XR;
using UnityEngine.UIElements;

public class PlayerController : MonoBehaviour
{

    //private Rigidbody;
    private CharacterController controller;
    public float speed;
    public float turnspeed;
    public InputActionReference moveAction;
    public Vector2 moveInput;
    private string varchar;

    //Variables de salto
    private float gravedad = -25f;
    public float jumpforce = 8f;
    private float verticalVelocity;
    public InputActionReference salto;
    public Transform groundcheck;
    public float grounRadius;
    public LayerMask groundLayer;
    private bool isGround;


    // Start is called once before the first execution of Update after the MonoBehaviour is created

    private void OnEnable()
    {
        moveAction.action.Enable();
        salto.action.Enable();
    }

    private void OnDisable()
    {
        moveAction.action.Disable();
    }


    void Start()
    {
        controller = GetComponent<CharacterController>();
        
    }

    // Update is called once per frame
    void Update()
    {
        moveInput = moveAction.action.ReadValue<Vector2>();
        Debug.Log(moveInput);
        if (moveInput.x != 0)
        {
            float rotationaMOUNT = moveInput.x * turnspeed * Time.deltaTime;
            transform.Rotate(0f, rotationaMOUNT,0f);

        }

        //Verificar si el jugador esta tocando el piso
        isGround = Physics.CheckSphere(groundcheck.position,grounRadius,groundLayer);

        if (salto.action.WasPressedThisFrame()&& isGround)
        {
           verticalVelocity = jumpforce;
        }


        verticalVelocity += gravedad * Time.deltaTime;

        Vector3 direccion = transform.forward * moveInput.y *speed;
        direccion.y = verticalVelocity;
        controller.Move(direccion * speed * Time.deltaTime);

    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.aliceBlue;
        Gizmos.DrawWireSphere(groundcheck.position,grounRadius);
    }

}
