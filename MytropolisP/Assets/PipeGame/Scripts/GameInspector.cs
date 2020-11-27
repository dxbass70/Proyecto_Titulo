﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

namespace MadFireOn
{
    public class GameInspector : MonoBehaviour
    {
        //array where all the pipe objects in the scene are stored
        private PipeScript[] pipeObj;
        //ref to level complete status
        public bool levelComplete = false;
        //ref to which level is on
        private int levelInd;
        public GameObject Ventanapuntaje;
        public Text TextPuntaje;
        public Text Textelectricidad;
        private int Puntaje = 0;
        private int Duracion = 30;
        private int Agua = 0;
        private int timeLeft;

        //Timer
        private float endTime;
        public Text TextTimer;

        void Awake()
        {
            //at start we want levelComplete false
            levelComplete = false;
        }

        // Use this for initialization
        void Start()
        {
            //get all the pipeObj and are stored in array
            pipeObj = FindObjectsOfType<PipeScript>();

            //Timer
            endTime = Time.time + Duracion;
            TextTimer.text = Duracion.ToString();

        }

        // Update is called once per frame
        void Update()
        {
            timeLeft = (int)(endTime - Time.time);            
            
            CheckForLevelComplete();

            //if level is complete
            if (levelComplete)
            {
                FinPartida(timeLeft);
                //se muestra la ventana de ganaste
            }

            
            else{
                Tiempo();
            }

        }
        //methode which loop throught all the pipes and check if each one variable
        //"completed" is true
        void CheckForLevelComplete()
        {
            for (int i = 0; i < pipeObj.Length; i++)
            {
                if (pipeObj[i].completed == false)
                {
                    Debug.Log("aun no");
                    Debug.Log(i);
                    levelComplete = false;
                    return;
                }
                
            }
            Debug.Log("Ahora si");
            levelComplete = true;
        }

        public void FinPartida(int Incremento){
            Puntaje += Incremento;
            Debug.Log("Fin de la partida");
            if (Ventanapuntaje.activeSelf == false){
                Ventanapuntaje.SetActive(true); // activa la ventana puntaje
                TextPuntaje.text = "Puntaje: " + Puntaje.ToString();
                Agua = Puntaje*10;
                Textelectricidad.text = Agua.ToString();
            }
        }

        void Tiempo(){
            if(timeLeft == 0){
                FinPartida(0);
                }
            if (timeLeft < 0){
                timeLeft = 0;
            }
            TextTimer.text = timeLeft.ToString();
        }
    }

    

}//namespace MadFireOn