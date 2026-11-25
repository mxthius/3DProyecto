using UnityEngine;

public class Move : MonoBehaviour
{
    public float velocidad = 5f;
    public Vector3 direccion;
    void Start()
    {
        Debug.Log(transform.position);
        transform.Translate(direccion * velocidad * Time.deltaTime);
        Debug.Log(transform.position);
    }
    //Frame por Frame 
    private void Update()
    {
        transform.Translate(Vector3.up * velocidad * Time.deltaTime);
    }

}