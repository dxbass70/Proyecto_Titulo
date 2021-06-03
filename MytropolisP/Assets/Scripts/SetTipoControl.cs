using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetTipoControl : MonoBehaviour
{
    public GameObject canvas;
    public GameObject Auto;
    public bool isPaused = false;

    private void Awake() {
        StartCoroutine(CargandoNivel());  
    }

    public void SelectControlTouch(){
        Auto.GetComponent<CtrlAuto>().tipoControl = TipoControl.Touch;
        isPaused = false;
        Time.timeScale = 1;
        canvas.SetActive(false);
        
    }

    public void SelectControlGiro(){
        Auto.GetComponent<CtrlAuto>().tipoControl = TipoControl.Giroscopio;
        isPaused = false;
        Time.timeScale = 1;
        canvas.SetActive(false);
        
    }

    IEnumerator CargandoNivel(){
        yield return new WaitForSeconds(0.5f);
        isPaused = true;
        Time.timeScale = 0;
    }
}
