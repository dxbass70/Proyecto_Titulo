using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Act4Salir : MonoBehaviour
{
    public GameObject LevelLoader;

    public void TransicionEscena(string NombreEscena)
    {
        SystemSave.Updateasigna_reim_alumno();  //actualiza la duracion de la sesion
        LevelLoader.SendMessage("LoadLevel", NombreEscena);
    }
}
