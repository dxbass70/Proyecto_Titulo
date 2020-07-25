using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CtrlGeneradorBasura : MonoBehaviour
{
    private Rigidbody2D rb2d;
    private string direccion = "izquierda";
    public float velocity = 2f;
    public GameObject BasuraPrefab;
    public float timerGenerador = 1.75f;
    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
            rb2d.velocity = Vector2.left * velocity;
        InvokeRepeating("CreateBasura", 1.0f, timerGenerador);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    void CreateBasura(){
        Instantiate(BasuraPrefab, transform.position, Quaternion.identity);
    }

    void OnTriggerEnter2D(Collider2D other){
        if(other.gameObject.tag == "Muro" & direccion=="izquierda"){ //Al chocar con un objeto de tag muro, cambia la direccion a la derecha
            rb2d = GetComponent<Rigidbody2D>();
            rb2d.velocity = Vector2.right * velocity;
            direccion = "derecha";
            Debug.Log("Cambio de direccion");
        }
        else if(other.gameObject.tag == "Muro" & direccion=="derecha"){ //Al chocar con un objeto de tag muro, cambia la direccion a la izquierda
            rb2d = GetComponent<Rigidbody2D>();
            rb2d.velocity = Vector2.left * velocity;
            direccion = "izquierda";
            Debug.Log("Cambio de direccion");
        }
    }


}
