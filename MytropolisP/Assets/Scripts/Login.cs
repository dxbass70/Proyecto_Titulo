using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;
using UnityEngine.Networking;
using MySql.Data;
using MySql.Data.MySqlClient;

[Serializable]
public class Usuario{
    public int id;
    public string loginame;
    public string password;
    public string nombre;
}
public class Login : MonoBehaviour
{
    #region VARIABLES

        [Header("login textFields")]

        public InputField usuarioInput;
        public InputField contrasenaInput;
        public GameObject textError;
        private Usuario usuario;

        //[Header("Database Properties")]
        //private string Host = "66.97.47.164";
        //public string Host = "localhost";
        //private string User = "ulearnet_web";
        ////public string User = "root";
        //private string Password = "Uchile2020.";
        //public string Password = "password";
        //private string Database = "ulearnet_reim_pilotaje";
        //public string Database = "test";

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
            if (string.IsNullOrWhiteSpace(usuario.loginame) || string.IsNullOrWhiteSpace(usuario.password)) //en caso de que un campo este vacio no se ahce la consulta
            {
                //print("Nonono ta mal");
                textError.SetActive(true);
                return;
            }

            //Se construyen los datos para la conexion a la bbdd
            //MySqlConnectionStringBuilder builder = new MySqlConnectionStringBuilder();
            //builder.Server = Host;
            //builder.Port = 3306;
            //builder.UserID = User;
            //builder.Password = Password;
            //builder.Database = Database;

            try
            {
                //using (MySqlConnection connection = new MySqlConnection(builder.ToString()))
                using (MySqlConnection connection = new MySqlConnection(SystemSave.conexionDB.GetConnection()))
                {
                    connection.Open();
                    //print("MySQL - Opened Connection");
                    string sqlQuery = "SELECT id,nombres FROM usuario WHERE username='"+usuario.loginame+"'and password='"+usuario.password+"'";
                    try
                    {
                        //print("Try...");
                        using (MySqlCommand command = new MySqlCommand(sqlQuery, connection))
                        {
                            //print("MySqlCommand command...");
                            using (MySqlDataReader reader = command.ExecuteReader())
                            {
                                //print("MySqlDataReader reader...");
                                if (reader.Read())
                                {
                                    print("leyendo datos...");
                                    // Acceder a los datos de cada fila
                                    usuario.id = reader.GetInt32(0);
                                    usuario.nombre = reader.GetString(1);

                                    // Hacer algo con los datos obtenidos
                                    //Debug.Log("ID: " + usuario.id + ", nombres: " + usuario.nombre);
                                    SystemSave.usuario = usuario; //registramos los datos del usuario para futuras consultas en la sesion de juego
                                    //SystemSave.usuario.loginame = usuario.loginame;
                                    asigna_reim_alumno();
                                    SceneManager.LoadScene("Ciudad");
                                    Debug.Log("Ingresando a la ciudad");
                                }else
                                {
                                    //print("Nonono ta mal"); //no se encontro usuario
                                    textError.SetActive(true);
                                }
                            }
                        }
                        
                    }
                    catch (MySqlException exception)
                    {
                        print(exception.Message);
                    }

                    
                    print("MySQL - Closed Connection");
                    connection.Close();
                }
            }
            catch (MySqlException exception)
            {
                print(exception.Message);
            }
        }

        private void asigna_reim_alumno(){ //Registramos el inicio de sesion en la base de datos
            //print("Registrado inicio de sesion....(mentira aun no lo programo :b)");
            print("Registrado inicio de sesion....");
        }

        #endregion

    /*
    private void asigna_reim_alumno(){
        string sesion_id = SystemSave.usuario.id + "-" + SystemSave.reim.id + System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"); //ajustar a formato
        string inicio = System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        Asigna_reim_alumno datos = new Asigna_reim_alumno(sesion_id, SystemSave.usuario.id, 202101, SystemSave.reim.id, inicio, inicio);
        SystemSave.asigna_reim_alumno = datos;
        Debug.Log("Sesion "+SystemSave.asigna_reim_alumno.sesion_id+" registrada");
        SystemSave.Put_asigna_reim_alumno(SystemSave.asigna_reim_alumno, this);
    }
    */
}
