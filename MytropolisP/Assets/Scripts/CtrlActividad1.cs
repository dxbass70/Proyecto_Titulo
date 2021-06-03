using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CtrlActividad1 : MonoBehaviour
{
    private int Puntaje = 0;
    private int Monedas = 0;
    private int Vidas = 3;
    private float horaInicio;
    private float Duracion;   //Cuanto tiempo dura en la actividad
    private GameObject CtrlRecursos;
    public GameObject GeneradorBasura;
    public GameObject Ventanapuntaje;
    public GameObject SonidoDerrota;
    public Text TextPuntaje;
    public Text PuntajeJuego;
    public Text Textmonedas;
    public lives vida_canvas;

    public Tiempoxactividad tiempoxactividad = new Tiempoxactividad();
    int interval = 1; 
    float nextTime = 0;
    // Start is called before the first frame update
    void Start()
    {
        AddTiempoActividad();
        horaInicio = Time.time;
        CtrlRecursos = GameObject.Find("CtrlRecursos");        
    }

    // Update is called once per frame
    void Update()
    {
        if(Vidas==0){
            updatetiempoxactividadfinal(1);
            Duracion = (Time.time - horaInicio);
            Duracion = (int) Duracion;
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
        else if (Vidas>0){
            if(tiempoxactividad.id_tiempoactividad != 0){
                if (Time.time >= nextTime) {
                    UpdateTiempoActividad();
                    nextTime += interval;
                }
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

    private void AddTiempoActividad(){
        tiempoxactividad.id_tiempoactividad = 0;    //Id inicializado en 0
        tiempoxactividad.inicio = System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        Debug.Log(tiempoxactividad.inicio);
        tiempoxactividad.final = tiempoxactividad.inicio;       // inicializamos con tiempo final = a inicial
        tiempoxactividad.causa = 0;                             //causa por defecto 0
        tiempoxactividad.usuario_id = SystemSave.usuario.id;
        tiempoxactividad.reim_id = SystemSave.reim.id;
        tiempoxactividad.actividad_id = SystemSave.actividad1.id;
        SystemSave.SaveTiempoActividad(tiempoxactividad, this);
    }

    private void UpdateTiempoActividad(){
        tiempoxactividad.final = System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");   //actualizamos el tiempo final
        SystemSave.UpdateTiempoActividad(tiempoxactividad, this);
    }

    private void updatetiempoxactividadfinal(int causa){
        tiempoxactividad.final = System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");   //actualizamos el tiempo final
        tiempoxactividad.causa = causa; //Actualizmos la causa para indicar como termino
        SystemSave.UpdateTiempoActividad(tiempoxactividad, this);   
    }
}
