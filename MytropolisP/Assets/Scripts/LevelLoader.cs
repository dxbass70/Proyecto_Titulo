using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class LevelLoader : MonoBehaviour
{
    public Animator transition;
    public float transitiontime = 1f;

    public void LoadNextLevel(string NombreEscena){
        StartCoroutine(LoadLevel(NombreEscena));        
    }

    IEnumerator LoadLevel(string NombreEscena){
        transition.SetTrigger("Start");

        yield return new WaitForSeconds(transitiontime);

        SceneManager.LoadScene(NombreEscena);

    }

}
