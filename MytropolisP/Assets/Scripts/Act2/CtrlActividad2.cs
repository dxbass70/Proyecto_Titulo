using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace CtrlActividad2{
public class CtrlActividad2 : MonoBehaviour
{
    #region Variables:
    [Header ("Resultado Final")]
    private int Puntaje = 0;
    private int Electricidad = 0;
    private GameObject CtrlRecursos;
    public GameObject auto;
    public GameObject Ventanapuntaje;
    public GameObject SonidoDerrota;
    public GameObject SonidoVictoria;
    public Text TextVictoria;
    public Text TextPuntaje;
    public Text Textelectricidad;
    public GameObject GeneradorLaberinto;
    private int seed;
    public TipoControl tipoControl = TipoControl.Touch;
    [Header ("Tiempo Actividad")]
    private float endTime;
    public Text TextMesh;
    public int TiempoDisponible;
    public Tiempoxactividad tiempoxactividad = new Tiempoxactividad();
    #endregion

    int interval = 1; 
    float nextTime = 0;

    void Start()
    {   
        AddTiempoActividad();
        
        CtrlRecursos = GameObject.Find("CtrlRecursos");  
        endTime = Time.time + TiempoDisponible + 1;
        TextMesh.text = TiempoDisponible.ToString();
    }


    void Update()
    {
        int timeLeft = (int)(endTime - Time.time);
        if(timeLeft == 0.0f && Ventanapuntaje.activeSelf == false){
            TextMesh.text = timeLeft.ToString();
            FinPartida(0);
        }
        if (timeLeft >= 0 && Ventanapuntaje.activeSelf == false){
            TextMesh.text = timeLeft.ToString();
            if(tiempoxactividad.id_tiempoactividad != 0){
                if (Time.time >= nextTime) {
                    UpdateTiempoActividad();
                    nextTime += interval;
                }
            }
            
            //actualizar tiempoxactividad
        }
    }

    public int FinPartida(int Incremento){
        //actualizar tiempoxactividadfinal
        tipoControl = auto.GetComponent<CtrlAuto>().tipoControl;
        seed = GeneradorLaberinto.GetComponent<MazeGenerator>().seed; //Guardamos la semilla para saber el laberinto jugado
        Debug.Log("Seed: " + seed);
        TextMesh.gameObject.SetActive(false); //Se esconde el timer al acabar la partida
        Puntaje += Incremento;
        if(Incremento == 0){
            updatetiempoxactividadfinal(1);
            TextVictoria.text = "Intentalo otra vez";
            SonidoDerrota.GetComponent<AudioSource>().Play();
        }
        else{
            updatetiempoxactividadfinal(0);
            TextVictoria.text = "Ganaste";
            SonidoVictoria.GetComponent<AudioSource>().Play();
        }
        if (Ventanapuntaje.activeSelf == false){
        Ventanapuntaje.SetActive(true); // activa la ventana puntaje
        TextPuntaje.text = "Puntaje: " + Puntaje.ToString();
        Electricidad = Puntaje*10;
        Textelectricidad.text = Electricidad.ToString();
        //Se Guardan las monedas ganadas
        CtrlRecursos.SendMessage("SumarElect",Electricidad); //Se suma la Electricidad ganada
        CtrlRecursos.SendMessage("SavePlayer"); //Guarda los datos
        return Electricidad;
        }
        return 0;
    }

    private void AddTiempoActividad(){
        tiempoxactividad.id_tiempoactividad = 0;    //Id inicializado en 0
        tiempoxactividad.inicio = System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        //Debug.Log(tiempoxactividad.inicio);
        tiempoxactividad.final = tiempoxactividad.inicio;       // inicializamos con tiempo final = a inicial
        tiempoxactividad.causa = 0;                             //causa por defecto 0
        if(SystemSave.usuario != null){
            tiempoxactividad.usuario_id = SystemSave.usuario.id;
            tiempoxactividad.reim_id = SystemSave.reim.id;
            tiempoxactividad.actividad_id = SystemSave.actividad2.id;
            SystemSave.SaveTiempoActividad(tiempoxactividad, this);
        }
        
    }

    private void UpdateTiempoActividad(){
        tiempoxactividad.final = System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");   //actualizamos el tiempo final
        if(SystemSave.usuario != null){
            SystemSave.UpdateTiempoActividad(tiempoxactividad, this);
        }
        
    }

    private void updatetiempoxactividadfinal(int causa){
        tiempoxactividad.final = System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");   //actualizamos el tiempo final
        tiempoxactividad.causa = causa; //Actualizmos la causa para indicar como termino
        if(SystemSave.usuario != null){
            SystemSave.UpdateTiempoActividad(tiempoxactividad, this);   
        }
        
    }

    IEnumerator CargandoNivel(){
        yield return new WaitForSeconds(1f);
    }
}

}