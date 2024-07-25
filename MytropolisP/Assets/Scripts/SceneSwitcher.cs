﻿
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneSwitcher : MonoBehaviour
{
    public void TransicionEscena(string NombreEscena)
    {
        SystemSave.Updateasigna_reim_alumno();  //actualiza la duracion de la sesion
        SceneManager.LoadScene(NombreEscena);
        //Debug.Log("Transicion a " + NombreEscena);
    }

}
