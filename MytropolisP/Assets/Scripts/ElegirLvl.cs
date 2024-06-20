using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElegirLvl : MonoBehaviour
{
    public GameObject LevelLoader;
    private int Nivel;
    private string Escena;
    private void TransicionEscena(string NombreEscena)
    {
        SystemSave.Updateasigna_reim_alumno();  //actualiza la duracion de la sesion
        LevelLoader.SendMessage("LoadLevel", NombreEscena);
    }

    public void AccesoNivelAct1(){
        Nivel = Random.Range(1,8);  //Selecciona un nivel al azar de la actividad 1
        Escena = "Actividad1"+Nivel.ToString();
        TransicionEscena(Escena);

    }
}
