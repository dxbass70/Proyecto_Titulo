using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

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


    // Start is called before the first frame update
    private void Start () {
        rb2D = GetComponent<Rigidbody2D>();  
    }
    private void FixedUpdate () {
        if (Ventanapuntaje.activeSelf == false){
            mover();
        }
        else{
            rb2D.velocity = new Vector2(0,0);
        }
    }

    void OnTriggerEnter2D(Collider2D other){
        if(other.gameObject.tag == "Objetivo"){ //Al chocar con un objeto de tag basura, la basura se destruye
            puntaje = (Convert.ToInt32(TextMesh.text));
            ActivityCtrl.SendMessage("FinPartida",puntaje);
        }
    }

    void mover(){
        if(Input.acceleration.x > 0){
            if(Input.acceleration.y > 0){
                if(Input.acceleration.x >= Input.acceleration.y){
                    transform.localEulerAngles = new Vector3 (0,0,0);
                }else{
                    transform.localEulerAngles = new Vector3 (0,0,90);
                }
            }
            else if(Input.acceleration.y < 0){
                if(Input.acceleration.x >= (Input.acceleration.y * -1)){
                    transform.localEulerAngles  = new Vector3 (0,0,0);
                }else{
                    transform.localEulerAngles = new Vector3 (0,0,270);
                }
            }
        }
        else if(Input.acceleration.x < 0){
            if(Input.acceleration.y > 0){
                if(Input.acceleration.x >= (Input.acceleration.y * -1)){
                    transform.localEulerAngles = new Vector3 (0,0,180);
                }else if(Input.acceleration.x < Input.acceleration.y){
                    transform.localEulerAngles = new Vector3 (0,0,90);
                }
            }
            else{
                if(Input.acceleration.x >= Input.acceleration.y){
                    transform.localEulerAngles = new Vector3 (0,0,180);
                }else{
                    transform.localEulerAngles = new Vector3 (0,0,270);
                }
            }
        }

        
        rb2D.velocity = new Vector2(Input.acceleration.x * velocidadMovimiento, Input.acceleration.y * velocidadMovimiento);
    }
}
