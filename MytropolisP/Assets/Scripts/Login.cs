using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;
using UnityEngine.Networking;
using MySql.Data;
using MySql.Data.MySqlClient;
//using UnityEditorInternal;

[Serializable]
public class Usuario{
    public int id;
    public string loginame;
    public string password;
    public string nombre;

    public DateTime sesion_id;  //Id de la sesion
}
public class Login : MonoBehaviour
{
    #region VARIABLES

        [Header("login textFields")]

        public InputField usuarioInput; //Campo para ingresar el usuario
        public InputField contrasenaInput;  //Campo para ingresar la contraseña
        public GameObject textError;    //Texto de error en caso de que los datos sean incorrectos
        private Usuario usuario;    //objeto para almacenar los datos

        //[Header("Database Properties")]
        //private string Host = "66.97.47.164";
        //private string User = "ulearnet_web";
        //private string Password = "Uchile2020.";
        //private string Database = "ulearnet_reim_pilotaje";

        #endregion

        #region UNITY METHODS

        private void Start()
        {
            //Connect();
        }

        #endregion

        #region METHODS

        public void login()
        {
            textError.SetActive(false);
            //Se crea un objeto usuario para almacenar los datos
            usuario = new Usuario();
            usuario.loginame = usuarioInput.GetComponent<InputField>().text;
            usuario.password = contrasenaInput.GetComponent<InputField>().text;
            if (string.IsNullOrWhiteSpace(usuario.loginame) || string.IsNullOrWhiteSpace(usuario.password)) //en caso de que un campo este vacio no se hace la consulta
            {
                textError.SetActive(true);
                return;
            }
            try
            {
                using (MySqlConnection connection = new MySqlConnection(SystemSave.conexionDB.GetConnection()))
                {
                    connection.Open();
                    //Busca un usuario por su usuario y contraseña
                    string sqlQuery = "SELECT id,nombres FROM usuario WHERE username='"+usuario.loginame+"'and password='"+usuario.password+"'";
                    try
                    {
                        using (MySqlCommand command = new MySqlCommand(sqlQuery, connection))
                        {
                            using (MySqlDataReader reader = command.ExecuteReader())
                            {
                                if (reader.Read())
                                {
                                    //print("leyendo datos...");
                                    // Acceder a los datos de cada fila
                                    usuario.id = reader.GetInt32(0);
                                    usuario.nombre = reader.GetString(1);

                                    // Hacer algo con los datos obtenidos
                                    //Debug.Log("ID: " + usuario.id + ", nombres: " + usuario.nombre);
                                    SystemSave.usuario = usuario; //registramos los datos del usuario para futuras consultas en la sesion de juego
                                    //SystemSave.usuario.loginame = usuario.loginame;
                                    //print(SystemSave.usuario.id);
                                    //print(SystemSave.usuario.nombre);
                                    asigna_reim_alumno();   //Se ejecuta el registro de la sesion
                                    SceneManager.LoadScene("Ciudad");   //Cargo la siguiente escena del juego
                                    //Debug.Log("Ingresando a la ciudad");
                                }else
                                {
                                    //no se encontro usuario
                                    textError.SetActive(true);
                                }
                            }
                        }
                        
                    }
                    catch (MySqlException exception)
                    {
                        print(exception.Message);
                    }

                    
                    //print("MySQL - Closed Connection");
                    connection.Close();
                }
            }
            catch (MySqlException exception)
            {
                print(exception.Message);
            }
        }

        private void asigna_reim_alumno(){ //Registramos el inicio de sesion en la base de datos
            
            //generar los datos de la sesion
            DateTime date_inicio = DateTime.Now;    //hora de inicio de la sesion
            string sesionId = SystemSave.usuario.id + "-" + date_inicio.ToString("yyyy-MM-dd HH:mm:ss:fff");
            int periodo = 202401;   //periodo 1er Semestre 2024
            
            //guardado para actualizar mientras avanza la sesion
            SystemSave.asigna_reim_alumno = new Asigna_reim_alumno(sesionId, SystemSave.usuario.id, periodo, SystemSave.reim.id, date_inicio, date_inicio);
            
            //print("Registrado inicio de sesion....");
            
            try{
                using (MySqlConnection connection = new MySqlConnection(SystemSave.conexionDB.GetConnection())){
                    connection.Open();
                    string sqlQuery = "INSERT INTO asigna_reim_alumno (sesion_id, usuario_id, periodo_id, reim_id, datetime_inicio, datetime_termino) VALUES (@Valor1, @Valor2, @Valor3, @Valor4, @Valor5, @Valor6)";
                    try{
                        using (MySqlCommand command = new MySqlCommand(sqlQuery, connection)){
                            command.Parameters.AddWithValue("@Valor1", SystemSave.asigna_reim_alumno.sesion_id);  //Id de la sesion
                            command.Parameters.AddWithValue("@Valor2", SystemSave.asigna_reim_alumno.usuario_id);  //Id del usuario
                            command.Parameters.AddWithValue("@Valor3", SystemSave.asigna_reim_alumno.periodo_id);   //Id del periodo (1er semestre 2024)
                            command.Parameters.AddWithValue("@Valor4", SystemSave.asigna_reim_alumno.reim_id);   //id del REIM
                            command.Parameters.AddWithValue("@Valor5", SystemSave.asigna_reim_alumno.datetime_inicio);   //momento en que se inicia la sesion
                            command.Parameters.AddWithValue("@Valor6", SystemSave.asigna_reim_alumno.datetime_termino);  //por defecto la hora de termino es igual a la de inicio (se debe actualizar a medida que se usa el REIM)

                            int rowsAffected = command.ExecuteNonQuery();

                            Console.WriteLine($"{rowsAffected} fila(s) afectada(s).");
                        }
                            
                    }
                    catch (MySqlException exception){
                        print(exception.Message);
                    }
                    //print("MySQL - Closed Connection");
                    connection.Close();
                }
            }
            catch (MySqlException exception){
                print(exception.Message);
            }
        }

        #endregion
}
