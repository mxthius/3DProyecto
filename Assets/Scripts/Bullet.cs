using UnityEngine;

public class Nullet : MonoBehaviour
{

    private Rigidbody rb;
    public int force;
    public int friccion;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.AddForce(transform.forward* force, ForceMode.Impulse);
        Destroy(gameObject, 2f);
    }

    // Update is called once per frame
    void Update()
    {
        //rb.AddForce(transform.forward * force, ForceMode.Impulse);


    }


    private void OnTriggerEnter(Collider colision)
    {
        if (colision.CompareTag("Enemigo"))
        {

            Destroy(colision.gameObject);
            Debug.Log("COLISION CON ENEMIGO 😎😎");

        }
    }


}
