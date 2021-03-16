using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace CtrlActividad2{
public class CtrlActividad2 : MonoBehaviour
{
    private int Puntaje = 0;
    private int Electricidad = 0;
    private GameObject CtrlRecursos;
    public GameObject Ventanapuntaje;
    public GameObject SonidoDerrota;
    public GameObject SonidoVictoria;
    public GameObject MusicaFondo;
    public Text TextVictoria;
    public Text TextPuntaje;
    public Text Textelectricidad;
    private float endTime;
    public Text TextMesh;
    // Start is called before the first frame update
    void Start()
    {
        CtrlRecursos = GameObject.Find("CtrlRecursos");  
        endTime = Time.time + 60;
        TextMesh.text = "60";
    }

    // Update is called once per frame
    void Update()
    {
        int timeLeft = (int)(endTime - Time.time);
        if(timeLeft == 0.0f && Ventanapuntaje.activeSelf == false){
            FinPartida(0);
            endTime-=1; //evita que se repita la condicion mas de una vez
        }
        if (timeLeft >= 0 && Ventanapuntaje.activeSelf == false){
            TextMesh.text = timeLeft.ToString();
        }
    }

    public int FinPartida(int Incremento){
        Puntaje += Incremento;
        if(Incremento == 0){
            TextVictoria.text = "Intentalo otra vez";
            MusicaFondo.GetComponent<AudioSource>().Stop();
            SonidoDerrota.GetComponent<AudioSource>().Play();
        }
        else{
            TextVictoria.text = "Ganaste";
            MusicaFondo.GetComponent<AudioSource>().Stop();
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
}

}