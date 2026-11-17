using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.XR;

public class CharacterController : MonoBehaviour
{

    //private Rigidbody;
    private CharacterController controller;
    public float speed;
    public InputActionReference moveAction;
    public Vector3 moveInput;
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


        moveInput = moveAction.action.ReadValue<Vector3>();
        Debug.Log(moveInput);
    }
}
