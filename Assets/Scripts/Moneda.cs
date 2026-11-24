using UnityEngine;

public class Moneda : MonoBehaviour
{
    private int monedas;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void OnTriggerEnter(Collider colision)
    {

        if (colision.CompareTag("Player"))
        {
            monedas -= 1;

            Debug.Log("🪙" + monedas);

        }


    }

}
