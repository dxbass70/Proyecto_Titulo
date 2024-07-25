using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CtrlPause : MonoBehaviour
{
    bool isPaused = false;
    public GameObject TextoPausa;

	public void pauseGame()
	{
		if (isPaused) {
			Time.timeScale = 1;
            TextoPausa.SetActive(false);
			isPaused = false;
            
		} else {
			Time.timeScale = 0;
            TextoPausa.SetActive(true);
			isPaused = true;
		}
	}
}
