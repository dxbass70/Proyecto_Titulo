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

        [Header("Database Properties")]
        //public string Host = "66.97.47.164";
        public string Host = "localhost";
        //public string User = "ulearnet_web";
        public string User = "root";
        //public string Password = "Uchile2020.";
        public string Password = "password";
        //public string Database = "ulearnet_reim_pilotaje";
        public string Database = "test";

        #endregion

        #region UNITY METHODS

        private void Start()
        {
            Connect();
        }

        #endregion

        #region METHODS

        private void Connect()
        {
            MySqlConnectionStringBuilder builder = new MySqlConnectionStringBuilder();
            builder.Server = Host;
            builder.Port = 3306;
            builder.UserID = User;
            builder.Password = Password;
            builder.Database = Database;

            try
            {
                using (MySqlConnection connection = new MySqlConnection(builder.ToString()))
                {
                    connection.Open();
                    print("MySQL - Opened Connection");
                }
            }
            catch (MySqlException exception)
            {
                print(exception.Message);
            }
        }

        #endregion
    /*
    public InputField usuarioInput;
    public InputField contrasenaInput;

    private string connectionString;

    private void Start()
    {
        // Establecer la cadena de conexión a la base de datos MySQL
        //connectionString = "DRIVER={MySQL ODBC 8.3.0 Unicode Driver};server=66.97.47.164;database=ulearnet_reim_pilotaje;Port=3306;user=ulearnet_web;password=Uchile2020.";
        //connectionString = "server=66.97.47.164;Port=3306;database=ulearnet_reim_pilotaje;user=ulearnet_web;password=Uchile2020.";
        connectionString = "server=localhost;port=3306;database=test;user=root;password=root";
        ConsultarDatos();
    }*/
    
    /*
    public void login(){
        Usuario usuario;
        usuario = new Usuario();
        usuario.loginame = usuarioInput.GetComponent<InputField>().text;
        usuario.password = contrasenaInput.GetComponent<InputField>().text;
        //StartCoroutine(Post(usuario));
        
    }

    IEnumerator Post(Usuario usuario){
        //string urlAPI = "http://localhost:3002/api/login";
        string urlAPI = "http://66.97.47.164:3306/api/login";
        var jsonData = JsonUtility.ToJson(usuario);

        using (UnityWebRequest www = UnityWebRequest.Post(urlAPI, jsonData)){
            www.SetRequestHeader("content-type", "application/Json");
            www.uploadHandler.contentType = "application/Json";
            www.uploadHandler = new UploadHandlerRaw(System.Text.Encoding.UTF8.GetBytes(jsonData));
            yield return www.SendWebRequest();
            if(www.isNetworkError){
                Debug.LogError(www.error);
            }else{
                if(www.isDone){
                    var result = System.Text.Encoding.UTF8.GetString(www.downloadHandler.data);
                    if(result != null){
                        var usuarios = JsonUtility.FromJson<Usuario>(result);
                        SystemSave.usuario = usuarios;
                        SystemSave.usuario.loginame = usuario.loginame;
                        //asigna_reim_alumno();
                        SceneManager.LoadScene("Ciudad");
                        Debug.Log("Ingresando a la ciudad");
                    }
                }
            }

        }

    }

    private void asigna_reim_alumno(){
        string sesion_id = SystemSave.usuario.id + "-" + SystemSave.reim.id + System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"); //ajustar a formato
        string inicio = System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        Asigna_reim_alumno datos = new Asigna_reim_alumno(sesion_id, SystemSave.usuario.id, 202101, SystemSave.reim.id, inicio, inicio);
        SystemSave.asigna_reim_alumno = datos;
        Debug.Log("Sesion "+SystemSave.asigna_reim_alumno.sesion_id+" registrada");
        SystemSave.Put_asigna_reim_alumno(SystemSave.asigna_reim_alumno, this);
    }
    */
    

    /*
    public void ConsultarDatos()
    {
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            ;
            Debug.Log("Host" + System.Net.Dns.GetHostName());
            connection.Open();
            print("ODBC - Opened Connection");

            string sqlQuery = "SELECT * FROM usuario WHERE username='Jmorales' ";

            using (MySqlCommand command = new MySqlCommand(sqlQuery, connection))
            {
                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        // Acceder a los datos de cada fila
                        int id = reader.GetInt32(0);
                        string nombre = reader.GetString(1);
                        int apellido_paterno = reader.GetInt32(2);

                        // Hacer algo con los datos obtenidos
                        Debug.Log("ID: " + id + ", Nombre: " + nombre + ", Apellido: " + apellido_paterno);
                    }
                }
            }

            connection.Close();
        }
    }
    */
}
