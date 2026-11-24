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

    public Animator anim;

    //------Variables de vida------//
    public int life = 5;
    //-------------------------------//


    // Start is called once before the first execution of Update after the MonoBehaviour is created

    private void OnEnable()
    {
        moveAction.action.Enable();
        salto.action.Enable();
    }

    private void OnDisable()
    {
        moveAction.action.Disable();
        anim = GetComponentInChildren<Animator>();
    }


    void Start()
    {
        controller = GetComponent<CharacterController>();
        anim = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        moveInput = moveAction.action.ReadValue<Vector2>();
        //Debug.Log(moveInput);
        //if (moveInput.x != 0)
        //{
        //    float rotationaMOUNT = moveInput.x * turnspeed * Time.deltaTime;
        //    transform.Rotate(0f, rotationaMOUNT,0f);

        //}

        //Verificar si el jugador esta tocando el piso
        isGround = Physics.CheckSphere(groundcheck.position,grounRadius,groundLayer);

        if (salto.action.WasPressedThisFrame()&& isGround)
        {
           verticalVelocity = jumpforce;
            anim.SetTrigger("Jump");
            Debug.Log("Salto confirmado");
        }


        verticalVelocity += gravedad * Time.deltaTime;

        Vector3 direccion = transform.forward * moveInput.y *speed;
        direccion.y = verticalVelocity;
        controller.Move(direccion * speed * Time.deltaTime);

        anim.SetFloat("Move", moveInput.y);

    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.aliceBlue;
        Gizmos.DrawWireSphere(groundcheck.position,grounRadius);
    }

    
    private void OnTriggerEnter(Collider colision)
    {
        if(colision.CompareTag("Cube"))
        {
            life -= 1;

            Debug.Log("❤️" + life);

            //Destroy(colision.gameObject);
            Debug.Log("COLISION CUBO 😁😁😁");

            if(life <= 0)
            {
                Destroy(gameObject);
                Debug.Log("MORISTE 🩻🩻");
            }

        }
        else if(colision.CompareTag("Circle"))
        {
            Destroy(gameObject);
            Debug.Log("COLISION CIRCULO 😍😍😍");
        }



    }

    
    private void OnTriggerExit(Collider colision)
    {

        if (colision.CompareTag("Cube"))
        {

            Debug.Log("SALE DEL  CUBO 👍👍👍👍");
        }
        else if (colision.CompareTag("Circle"))
        {
            Debug.Log("SALE DEL CIRCULO 😒😒😒");
        }


    }


}
