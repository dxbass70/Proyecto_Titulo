using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public enum TipoControl{
    Touch,
    Giroscopio
}

[RequireComponent(typeof(Rigidbody2D))]
public class CtrlAuto : MonoBehaviour
{
    public GameObject ActivityCtrl;
    public GameObject Ventanapuntaje;
    [Range(2f, 20f)]
    public float velocidadMovimiento = 2f;
    private Rigidbody2D rb2D;
    private int puntaje;
    public Text TextMesh;

    public TipoControl tipoControl;

    [Header ("Control Touch")]
    private Vector3 posInicial;
    private Vector3 position;
    private float width;
    private float height;


    // Start is called before the first frame update
    private void Start () {
        rb2D = GetComponent<Rigidbody2D>();

        width = (float)Screen.width / 2.0f;
        height = (float)Screen.height / 2.0f;

    }
    private void FixedUpdate () {
        if (Ventanapuntaje.activeSelf == false){
            if (tipoControl == TipoControl.Touch)
            {
                MoverTouch();
            }else if (tipoControl == TipoControl.Giroscopio)
            {
                MoverGiroscopio();
            }
            
        }
        else{
            rb2D.velocity = new Vector2(0,0);
        }
    }

    void OnTriggerEnter2D(Collider2D other){
        if(other.gameObject.tag == "Objetivo"){ //Al chocar con el objetivo, se termina la partida
            puntaje = (Convert.ToInt32(TextMesh.text));
            ActivityCtrl.SendMessage("FinPartida",puntaje);
        }
    }

    void MoverTouch(){
        // Handle screen touches.
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                posInicial = touch.position;
            }

            else if (touch.phase == TouchPhase.Moved)
            {
                Vector2 pos = new Vector2(0f, 0f);
                position = touch.position;

                //Comprobar direccion horizontal
                if(posInicial.x == position.x){
                    pos.x = 0;
                }else if (posInicial.x < position.x)    //X a la derecha
                {
                    pos.x = 1;
                }else if (posInicial.x > position.x)    //X a la izquierda
                {
                    pos.x = -1;
                }
                //Comprobar direccion vertical
                if(posInicial.y == position.y){
                    pos.y = 0;
                }else if (posInicial.y < position.y)    //X arriba
                {
                    pos.y = 1;
                }else if (posInicial.y > position.y)    //X abajo
                {
                    pos.y = -1;
                }
                posInicial = touch.position;

                Debug.Log("X: " + pos.x);
                Debug.Log("Y: " + pos.y);


                rb2D.velocity = new Vector2(pos.x * velocidadMovimiento, pos.y * velocidadMovimiento);
                

            }else if(touch.phase == TouchPhase.Ended)
            {
                rb2D.velocity = new Vector2(0,0);
            }
        }
        Rotar();
    }

    void MoverGiroscopio(){
        rb2D.velocity = new Vector2(Input.acceleration.x * velocidadMovimiento, Input.acceleration.y * velocidadMovimiento);
        Rotar();
    }

    void Rotar(){
        if (rb2D.velocity.x > 0){
            if (rb2D.velocity.y > 0)
            {
                if (rb2D.velocity.y > rb2D.velocity.x){ //Arriba
                    transform.rotation = Quaternion.Euler(Vector3.forward * 90f);
                }else if(rb2D.velocity.y < rb2D.velocity.x){    //Derecha
                    transform.rotation = Quaternion.Euler(Vector3.forward * 0f);
                }
            } else if(rb2D.velocity.y < 0){
                if ((rb2D.velocity.y * -1) > rb2D.velocity.x){ //Abajo
                    transform.rotation = Quaternion.Euler(Vector3.forward * 270f);
                }else if((rb2D.velocity.y * -1) < rb2D.velocity.x){    //Derecha
                    transform.rotation = Quaternion.Euler(Vector3.forward * 0f);
                }
            }  
        } else if (rb2D.velocity.x <0){
            if (rb2D.velocity.y > 0)
            {
                if (rb2D.velocity.y > (rb2D.velocity.x * -1)){ //Arriba
                    transform.rotation = Quaternion.Euler(Vector3.forward * 90f);
                }else if(rb2D.velocity.y < (rb2D.velocity.x * -1)){    //Izquierda
                    transform.rotation = Quaternion.Euler(Vector3.forward * 180f);
                }
            } else if(rb2D.velocity.y < 0){
                if ((rb2D.velocity.y * -1) > (rb2D.velocity.x * -1)){ //Abajo
                    transform.rotation = Quaternion.Euler(Vector3.forward * 270f);
                }else if((rb2D.velocity.y * -1) < (rb2D.velocity.x * -1)){    //Derecha
                    transform.rotation = Quaternion.Euler(Vector3.forward * 180f);
                }
            } 
        }
    }

    public void SelectInput(TipoControl input){
        tipoControl = input;
    }
}
