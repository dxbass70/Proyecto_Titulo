using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace CtrlActividad2{
public class CtrlActividad2 : MonoBehaviour
{
    private int Puntaje = 0;
    private int Electricidad = 0;
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
        endTime = Time.time + 30;
        TextMesh.text = "30";
    }

    // Update is called once per frame
    void Update()
    {
        int timeLeft = (int)(endTime - Time.time);
        if(timeLeft == 0.0f){
            FinPartida(0);
            endTime-=1; //evita que se repita la condicion mas de una vez
        }
        if (timeLeft >= 0) TextMesh.text = timeLeft.ToString();
    }

    public int FinPartida(int Incremento){
        Puntaje += Incremento;
        Debug.Log("Fin de la partida");
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
        return Electricidad;
        }
        return 0;
    }
}

}