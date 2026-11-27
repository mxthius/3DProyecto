using UnityEngine;
using UnityEngine.InputSystem;

public class MovimCamara : MonoBehaviour
{
    public Transform Player;
    public float sensibilidadMouse = 0.01f;

    private float rotacionVertical = 0f;
    private float rotacionHorizontal = 0f;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        if (Player != null)
            rotacionHorizontal = Player.eulerAngles.y;
    }

    // LateUpdate: corre después de todos los Update, incluido el movimiento del Player
    void LateUpdate()
    {
        if (Mouse.current == null || Player == null) return;

        Vector2 delta = Mouse.current.delta.ReadValue() * sensibilidadMouse;

        // Evita saltos bruscos (por ejemplo al recuperar el foco de la ventana)
        delta = Vector2.ClampMagnitude(delta, 10f);

        rotacionVertical -= delta.y;
        rotacionVertical = Mathf.Clamp(rotacionVertical, -80f, 80f);
        transform.localRotation = Quaternion.Euler(rotacionVertical, 0f, 0f);

        rotacionHorizontal += delta.x;
        Player.rotation = Quaternion.Euler(0f, rotacionHorizontal, 0f);
    }
}