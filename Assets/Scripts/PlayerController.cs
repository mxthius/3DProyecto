using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private CharacterController controller;
    private float speed = 25;
    public InputActionReference moveAction;
    public Vector2 moveInput;

    //Variables de salto
    private float gravedad = -50f;
    public float jumpforce = 8f;
    private float verticalVelocity;
    public InputActionReference salto;
    public Transform groundcheck;
    public float grounRadius = 0.3f;
    public LayerMask groundLayer;
    private bool isGround;

    public Animator anim;

    public InputActionReference shoot;
    public InputActionReference interactuar;
    public InputActionReference run;

    public GameObject bullet;

    public Transform bulletPosition;


    //------Variables de vida------//
    public int life = 5;

    private void OnEnable()
    {
        moveAction.action.Enable();
        salto.action.Enable();
        shoot.action.Enable();
        interactuar.action.Enable();
        run.action.Enable();
    }

    private void OnDisable()
    {
        moveAction.action.Disable();
        salto.action.Disable();
        shoot.action.Disable();
        interactuar.action.Disable();
        run.action.Disable();
    }

    void Start()
    {
        controller = GetComponent<CharacterController>();
        if (anim == null)
            anim = GetComponentInChildren<Animator>();
    }

    void Update()
    {
        //------------- Movimiento general -------------//
        moveInput = moveAction.action.ReadValue<Vector2>();

        Vector3 movimiento = transform.right * moveInput.x + transform.forward * moveInput.y;

        //Correr
        if (run.action.IsPressed())
        {

            Debug.Log("CORRIENDO");
            movimiento *= speed*2;
        }
        else
        {
            movimiento *= speed;
        }
        //------------- Salto -------------//
        isGround = controller.isGrounded ||
                   Physics.CheckSphere(groundcheck.position, grounRadius, groundLayer, QueryTriggerInteraction.Ignore);

        if (isGround && verticalVelocity < 0f)
            verticalVelocity = -2f;

        if (salto.action.WasPressedThisFrame())
        {
            Debug.Log("Botón de salto presionado. isGround = " + isGround);

            if (isGround)
            {
                verticalVelocity = jumpforce;
                if (anim != null) anim.SetTrigger("Jump");
            }
        }

        verticalVelocity += gravedad * Time.deltaTime;
        movimiento.y = verticalVelocity;

        controller.Move(movimiento * Time.deltaTime);

        disparo();
        Interactuar();
    }

    private void OnDrawGizmos()
    {
        if (groundcheck == null) return;
        Gizmos.color = Color.aliceBlue;
        Gizmos.DrawWireSphere(groundcheck.position, grounRadius);
    }

    private void OnTriggerEnter(Collider colision)
    {
        if (colision.CompareTag("Cube"))
        {
            life -= 1;
            Debug.Log("❤️" + life);
            Debug.Log("COLISION CUBO 😁😁😁");

            if (life <= 0)
            {
                Destroy(gameObject);
                Debug.Log("MORISTE 🩻🩻");
            }
        }
        else if (colision.CompareTag("Circle"))
        {
            Destroy(gameObject);
            Debug.Log("COLISION CIRCULO 😍😍😍");
        }
    }

    private void OnTriggerExit(Collider colision)
    {
        if (colision.CompareTag("Cube"))
            Debug.Log("SALE DEL  CUBO 👍👍👍👍");
        else if (colision.CompareTag("Circle"))
            Debug.Log("SALE DEL CIRCULO 😒😒😒");
    }

    private void Interactuar()
    {
        if (interactuar.action.WasPressedThisFrame())
            Debug.Log("Interactua 👁️👁️👁️");
    }

    private void disparo()
    {
        if (shoot.action.WasPressedThisFrame())
        {
            Instantiate(bullet, bulletPosition.position, bulletPosition.rotation);
            Debug.Log("DISPARO");
        }
    }
}