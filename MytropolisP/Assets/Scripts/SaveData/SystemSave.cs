using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Collections.Generic;
using System.Collections;
using MySql.Data;
using MySql.Data.MySqlClient;



[Serializable]
public class DBConnection{
    private string Host;
    private string User;
    private string Password;
    private string Database;
    public DBConnection(string Host, string User, string Password, string Database){
        this.Host = Host;
        this.User = User;
        this.Password = Password;
        this.Database = Database;
    }

    public string GetConnection(){
        MySqlConnectionStringBuilder builder = new MySqlConnectionStringBuilder();
        builder.Server = Host;
        builder.Port = 3306;
        builder.UserID = User;
        builder.Password = Password;
        builder.Database = Database;
        return builder.ToString();
    }
}
public class Reim{
    public int id;
    public string nombre;

    public Reim(int id, string nombre){
        this.id = id;
        this.nombre = nombre;
    }
}
[Serializable]
public class Actividad{
    public int id;
    public string nombre;
    public int id_reim;

    public Actividad(int id, string nombre, int id_reim){
        this.id = id;
        this.nombre = nombre;
        this.id_reim = id_reim;
    }
}

[Serializable]
public class Tiempoxactividad{
    public int id_tiempoactividad;
    public string inicio;
    public string final;
    public int causa;
    public int usuario_id;
    public int reim_id;
    public int actividad_id;
}

[Serializable]
public class dibujo_reim{
    public int id_imagenes_reim;
    public string sesion_id;
    public int usuario_id;
    public int reim_id;
    public int actividad_id;
    public byte[] imagen;
}

public class Elemento   //clase para almacenar los elementos importantes del reim
{
    public int id;
    public string nombre;

    public Elemento(int id, string nombre){
        this.id = id;
        this.nombre = nombre;
    }
    
}

[Serializable]
public class Asigna_reim_alumno{
    public string sesion_id;
    public int usuario_id;
    public int periodo_id;
    public int reim_id;
    public string datetime_inicio;
    public string datetime_termino;

    public Asigna_reim_alumno(string sesion_id, int usuario_id, int periodo_id, int reim_id, string datetime_inicio, string datetime_termino){
        this.sesion_id = sesion_id;
        this.usuario_id = usuario_id;
        this.periodo_id = periodo_id;
        this.reim_id = reim_id;
        this.datetime_inicio = datetime_inicio;
        this.datetime_termino = datetime_termino;
    }
}
public static class SystemSave{
    //Datos conexion a la bbdd
    public static DBConnection conexionDB = new DBConnection("66.97.47.164", "ulearnet_web", "Uchile2020.", "ulearnet_reim_pilotaje");

    //Datos de la sesion
    public static Usuario usuario;  //Guardamos los datos del usuario para futuras consultas

    //Datos del Reim
    
    public static Asigna_reim_alumno asigna_reim_alumno;
    public static Reim reim = new Reim(1006, "Mytropolis");  //Guardamos los datos del reim para futuras consultas
    public static Actividad Ciudad = new Actividad(230114, "Ciudad", 1006);
    public static Actividad actividad1 = new Actividad(230115, "Atrapa la basura", 1006);
    public static Actividad actividad2 = new Actividad(230116, "Laberinto en la ciudad", 1006);
    public static Actividad actividad3 = new Actividad(230117, "Conecta la tuberia", 1006);
    public static Actividad actividad4 = new Actividad(230118, "La Galeria", 1006);
    public static Actividad actividadColab = new Actividad(230119, "Tablon de anuncios", 1006);

    //Elementos importantes del reim
    public static Elemento Ulearcoin = new Elemento(900, "Ulearncoin");
    public static Elemento Agua = new Elemento(3016, "Agua");
    public static Elemento Electricidad = new Elemento(6660157, "Electricidad");

    public static void SavePlayer (CtrlRecursos recursos){
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/player.sav";
        FileStream stream = new FileStream(path, FileMode.Create);

        PlayerData data = new PlayerData(recursos);

        formatter.Serialize(stream, data);
        stream.Close();
    }

    public static PlayerData LoadPlayer (){
        string path = Application.persistentDataPath + "/player.sav";
        if (File.Exists(path)){
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            PlayerData data = formatter.Deserialize(stream) as PlayerData;
            stream.Close();

            return data;
        } else{
            Debug.LogError("Archivo de guardado no encontrado en " + path);
            return null;
        }
    }

    public static void SaveEdificios (List<EdificioData> ListaEdificios){
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/Edificios.sav";
        FileStream stream = new FileStream(path, FileMode.Create);

        ListContainer container = new ListContainer(ListaEdificios);
        string json = JsonUtility.ToJson(container);
        //Debug.Log(json);

        formatter.Serialize(stream, json);
        stream.Close();
    }

    public static List<EdificioData> LoadEdificios (){
        string path = Application.persistentDataPath + "/Edificios.sav";
        if (File.Exists(path)){
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            string json = formatter.Deserialize(stream) as string;
            stream.Close();

            ListContainer container = JsonUtility.FromJson<ListContainer>(json);
            List<EdificioData> ListaEdificios = new List<EdificioData>();

            for (int i = 0; i < container.dataList.Count; i++)
            {
                EdificioData edificioData = new EdificioData(container.dataList[i].pos, container.dataList[i].position, container.dataList[i].UltimoCobro);
                ListaEdificios.Add(edificioData);
                //Debug.Log("pos: " + container.dataList[i].pos + ", position: " + container.dataList[i].position + ", Ultimo Cobro: " + container.dataList[i].UltimoCobro);
            }

            return ListaEdificios;
        } else{
            File.Create(path); // si no existe se genera un save para guardar los edificios
            if (File.Exists(path)){
                Debug.LogError("No se pudo generar el archivo " + path);
            }
            
            return null;
        }
    }

    public static string GetUserFullName(int id){ //retorna en un string nombre y apellidos del usuario por su id
        string nombreCompleto = string.Empty;
        try{
            using (MySqlConnection connection = new MySqlConnection(SystemSave.conexionDB.GetConnection())){
                connection.Open();
                string sqlQuery = "SELECT nombres, apellido_paterno, apellido_materno FROM usuario WHERE id='"+id+"'";
                try{
                    using (MySqlCommand command = new MySqlCommand(sqlQuery, connection)){
                        using (MySqlDataReader reader = command.ExecuteReader()){
                            if (reader.Read()){ 
                                //nombreCompleto = reader["nombres"].ToString() + ' ' + reader["apellido_paterno"].ToString() + ' ' + reader["apellido_materno"].ToString();
                                nombreCompleto = reader["nombres"].ToString();
                            }
                        }
                    }
                        
                }
                catch (MySqlException exception){
                    Debug.Log(exception.Message);
                }
                Debug.Log("MySQL - Closed Connection");
                connection.Close();
            }
        }
        catch (MySqlException exception){
            Debug.Log(exception.Message);
        }
        return nombreCompleto;
    }
}

public struct ListContainer
{
	public List<EdificioData> dataList;

    /// <summary>
	/// Constructor
	/// </summary>
	/// <param name="_dataList">Data list value</param>
	public ListContainer(List<EdificioData> _dataList)
	{
		dataList = _dataList;
	}
}
