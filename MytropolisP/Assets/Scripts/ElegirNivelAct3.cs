using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ElegirNivelAct3 : MonoBehaviour
{
    private int Nivel;
    private string Escena;
    private void TransicionEscena(string NombreEscena)
    {
        SceneManager.LoadScene(NombreEscena);
        Debug.Log("Ingresando a nivel " + Nivel.ToString());
    }

    public void AccesoNivel(){
        Nivel = Random.Range(1,3);  //Selecciona un nivel al azar de la actividad 3
        Escena = "Actividad3"+Nivel.ToString();
        TransicionEscena(Escena);

    }

}