using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CtrlActividad1 : MonoBehaviour
{
    private int Puntaje = 0;
    private int Monedas = 0;
    private int Vidas = 3;
    private GameObject CtrlRecursos;
    public GameObject GeneradorBasura;
    public GameObject Ventanapuntaje;
    public GameObject SonidoDerrota;
    public GameObject MusicaFondo;
    public Text TextPuntaje;
    public Text PuntajeJuego;
    public Text Textmonedas;
    public lives vida_canvas;
    // Start is called before the first frame update
    void Start()
    {
        CtrlRecursos = GameObject.Find("CtrlRecursos");        
    }

    // Update is called once per frame
    void Update()
    {
        if(Vidas==0){
            MusicaFondo.GetComponent<AudioSource>().Stop();
            SonidoDerrota.GetComponent<AudioSource>().Play();
            Destroy(GeneradorBasura);
            if (Ventanapuntaje.activeSelf == false){
            Ventanapuntaje.SetActive(true); // activa la ventana puntaje
            TextPuntaje.text = "Puntaje: " + Puntaje.ToString();
            Monedas = Puntaje*10;
            Textmonedas.text = Monedas.ToString();
            Vidas--;//se setean las vidas a -1 para evitar que se repita la funcion
            //Se Guardan las monedas ganadas
            CtrlRecursos.SendMessage("SumarMonedas",Monedas); //Se suman las monedas ganadas
            CtrlRecursos.SendMessage("SavePlayer"); //Guarda los datos
        }
        }
        PuntajeJuego.text = "Puntaje: " + Puntaje.ToString();
    }

    public void IncrementarPuntos(){
        Puntaje++;
        //Debug.Log(Puntaje);
    }

    public void DisminuirVidas(){
        if(Vidas==-1){

        }
        else{
            Vidas--;
            vida_canvas.CambioVida((int)Vidas);
            //Debug.Log(Vidas);
            }
        
    }
}
