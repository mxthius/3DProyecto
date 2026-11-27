using System.Net.Sockets;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.XR;
using UnityEngine.UIElements;

public class PlayerController : MonoBehaviour
{

    //private Rigidbody;
    private CharacterController controller;
    private float speed = 25;
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

    public InputActionReference shoot;
    public InputActionReference interactuar;
    public GameObject bullet;
    public Transform bulletPosition;

    //------Variables de vida------//
    public int life = 5;
    //-------------------------------//


    // Start is called once before the first execution of Update after the MonoBehaviour is created

    private void OnEnable()
    {
        moveAction.action.Enable();
        salto.action.Enable();
        shoot.action.Enable();
        interactuar.action.Enable();
    }

    private void OnDisable()
    {
        moveAction.action.Disable();
        shoot.action.Disable();
        interactuar.action.Disable() ;

        //anim = GetComponentInChildren<Animator>();


    }


    void Start()
    {
        controller = GetComponent<CharacterController>();
        anim = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        //------------- Movimiento general -------------//
        moveInput = moveAction.action.ReadValue<Vector2>();

        Vector3 movimiento = transform.right * moveInput.x + transform.forward * moveInput.y;
        movimiento *= speed;

        //------------- Salto -------------//
        isGround = Physics.CheckSphere(groundcheck.position, grounRadius, groundLayer);

        if (isGround && verticalVelocity < 0f)
            verticalVelocity = -2f;

        if (salto.action.WasPressedThisFrame() && isGround)
        {
            verticalVelocity = jumpforce;
            anim.SetTrigger("Jump");
            Debug.Log("Salto confirmado");
        }

        verticalVelocity += gravedad * Time.deltaTime;
        movimiento.y = verticalVelocity;

        controller.Move(movimiento * Time.deltaTime);

        disparo();

        Interactuar();

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

    private void Interactuar()
    {
        //Funcion de disparo
        if (interactuar.action.WasPressedThisFrame() )
        {
            
            Debug.Log("Interactua 👁️👁️👁️");
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


    private void disparo()
    {
        //Funcion de disparo
        if (shoot.action.WasPressedThisFrame())
        {
            Instantiate(bullet, bulletPosition.position, bulletPosition.rotation);
            Debug.Log("DISPARO");
        }


    }


}
