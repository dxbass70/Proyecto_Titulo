using UnityEngine;
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
        public bool done = false;
        //ref to which level is on
        private int levelInd;
        private GameObject CtrlRecursos;
        public GameObject Ventanapuntaje;
        public GameObject SonidoDerrota;
        public GameObject SonidoVictoria;
        public Text TextVictoria;
        public Text TextPuntaje;
        public Text Textelectricidad;
        private int Puntaje = 0;
        private int Duracion = 30;
        private int Agua = 0;
        private int timeLeft;

        //Timer
        private float endTime;
        public Text TextTimer;

        //guardardatos
        public Tiempoxactividad tiempoxactividad = new Tiempoxactividad();
        int interval = 1; 
        float nextTime = 0;

        void Awake()
        {
            //at start we want levelComplete false
            levelComplete = false;
            done = false;
        }

        // Use this for initialization
        void Start()
        {
            AddTiempoActividad();            
            CtrlRecursos = GameObject.Find("CtrlRecursos"); 
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
                if(done){ 
                }
                else{
                    FinPartida(timeLeft);
                    done = true;
                }
                //se muestra la ventana de ganaste
            }

            
            else{
                if (Ventanapuntaje.activeSelf == false){
                    Tiempo();
                    if(tiempoxactividad.id_tiempoactividad != 0){
                        if (Time.time >= nextTime) {
                            UpdateTiempoActividad();
                            nextTime += interval;
                        }
                    }
                } 
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
                    levelComplete = false;
                    return;
                }
                
            }
            levelComplete = true;
        }

        public void FinPartida(int Incremento){
            Puntaje += Incremento;
            if(Incremento == 0){
                updatetiempoxactividadfinal(1);
                TextVictoria.text = "Inténtalo otra vez";
                SonidoDerrota.GetComponent<AudioSource>().Play();
            }
            else{
                updatetiempoxactividadfinal(0);
                TextVictoria.text = "Ganaste";
                SonidoVictoria.GetComponent<AudioSource>().Play();
            }
            if (Ventanapuntaje.activeSelf == false){
                Ventanapuntaje.SetActive(true); // activa la ventana puntaje
                TextPuntaje.text = "Puntaje: " + Puntaje.ToString();
                Agua = Puntaje*10;
                Textelectricidad.text = Agua.ToString();
                //Se Guardan las monedas ganadas
                Debug.Log(Agua.ToString());
                CtrlRecursos.SendMessage("SumarAgua",Agua); //Se suma la Electricidad ganada
                CtrlRecursos.SendMessage("SavePlayer"); //Guarda los datos
            }
        }

        void Tiempo(){
            if(timeLeft == 0){
                FinPartida(0);
                endTime-=1;
                }
            if (timeLeft >= 0){
                TextTimer.text = timeLeft.ToString();
            }
        }

        private void AddTiempoActividad(){
        tiempoxactividad.id_tiempoactividad = 0;    //Id inicializado en 0
        tiempoxactividad.inicio = System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        Debug.Log(tiempoxactividad.inicio);
        tiempoxactividad.final = tiempoxactividad.inicio;       // inicializamos con tiempo final = a inicial
        tiempoxactividad.causa = 0;                             //causa por defecto 0
        tiempoxactividad.usuario_id = SystemSave.usuario.id;
        tiempoxactividad.reim_id = SystemSave.reim.id;
        tiempoxactividad.actividad_id = SystemSave.actividad3.id;
        //SystemSave.SaveTiempoActividad(tiempoxactividad, this);
        }

        private void UpdateTiempoActividad(){
            tiempoxactividad.final = System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");   //actualizamos el tiempo final
            //SystemSave.UpdateTiempoActividad(tiempoxactividad, this);
        }

        private void updatetiempoxactividadfinal(int causa){
            tiempoxactividad.final = System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");   //actualizamos el tiempo final
            tiempoxactividad.causa = causa; //Actualizmos la causa para indicar como termino
            //SystemSave.UpdateTiempoActividad(tiempoxactividad, this);   
        }
    }

    

}//namespace MadFireOn