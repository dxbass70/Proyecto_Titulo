using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CtrlActividad1 : MonoBehaviour
{
    private int Puntaje = 0;
    private int Monedas = 0;
    private int Vidas = 3;
    public GameObject GeneradorBasura;
    public GameObject Ventanapuntaje;
    public Text TextPuntaje;
    public Text Textmonedas;
    public lives vida_canvas;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Vidas==0){
            Debug.Log("Fin de la partida");
            Destroy(GeneradorBasura);
            if (Ventanapuntaje.activeSelf == false){
            Ventanapuntaje.SetActive(true); // activa la ventana puntaje
            TextPuntaje.text = "Puntaje: " + Puntaje.ToString();
            Monedas = Puntaje*10;
            Textmonedas.text = Monedas.ToString();
            Debug.Log("Activada ventana" + Ventanapuntaje.name);
            Vidas--;//se setean las vidas a -1 para evitar que se repita la funcion
        }
        }
    }

    public void IncrementarPuntos(){
        Puntaje++;
        Debug.Log(Puntaje);
    }

    public void DisminuirVidas(){
        if(Vidas==-1){

        }
        else{
            Vidas--;
            vida_canvas.CambioVida((int)Vidas);
            Debug.Log(Vidas);
            }
        
    }
}
