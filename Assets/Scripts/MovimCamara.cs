using UnityEngine;
using UnityEngine.InputSystem;

public class MovimCamara : MonoBehaviour
{
    public Transform Player;
    public float sensibilidadMouse = 0.01f;

    private float RotacionVertical = 0f;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        if (Mouse.current == null) return;

        Vector2 delta = Mouse.current.delta.ReadValue() * sensibilidadMouse;

        RotacionVertical -= delta.y;
        RotacionVertical = Mathf.Clamp(RotacionVertical, -80f, 80f);
        transform.localRotation = Quaternion.Euler(RotacionVertical, 0f, 0f);

        Player.Rotate(Vector3.up * delta.x);
    }
}