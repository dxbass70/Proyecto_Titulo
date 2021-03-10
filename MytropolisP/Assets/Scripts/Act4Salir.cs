using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Act4Salir : MonoBehaviour
{
    public GameObject musica;
    public GameObject LevelLoader;

    public void TransicionEscena(string NombreEscena)
    {
        GameObject.FindGameObjectWithTag("Musica").GetComponent<MantenerMusica>().StopMusic();
        LevelLoader.SendMessage("LoadLevel", NombreEscena);
    }
}
