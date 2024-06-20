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
    public DateTime datetime_inicio;
    public DateTime datetime_termino;

    public Asigna_reim_alumno(string sesion_id, int usuario_id, int periodo_id, int reim_id, DateTime datetime_inicio, DateTime datetime_termino){
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
    //Inicializados por defecto para agilizar el desarrollo *ACUERDATE DE BORRARLO*
    public static Usuario usuario = new Usuario(); //Guardamos los datos del usuario para futuras consultas
    //usuario.id = 380;
    //usuario.loginame = 'prueba380';
    //usuario.password = '1234';
    //usuario.nombre = 'Benjamin Nelson';

    public static Asigna_reim_alumno asigna_reim_alumno;

    //Datos del Reim
    
    
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
            //TEMPORAL MIENTRAS SE SOLUCIONA EDIFICIOS SUPERPUESTOS
            ListaEdificios = new List<EdificioData>();
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
                //Debug.Log("MySQL - Closed Connection");
                connection.Close();
            }
        }
        catch (MySqlException exception){
            Debug.Log(exception.Message);
        }
        return nombreCompleto;
    }

    //Actualiza la duracion de la sesion
    public static void Updateasigna_reim_alumno(){ //Actualizamos con la fecha actual
        
        DateTime dateFinal = DateTime.Now;
        //Debug.Log("Actualizando duracion de la sesion..." + dateFinal);
        asigna_reim_alumno.datetime_termino = dateFinal; //se actualiza el objeto de la sesion
        try{
            using (MySqlConnection connection = new MySqlConnection(SystemSave.conexionDB.GetConnection())){
                connection.Open();
                string sqlQuery = "UPDATE asigna_reim_alumno SET datetime_termino = @Nuevafecha WHERE sesion_id=@thisesion_id";
                try{
                    using (MySqlCommand command = new MySqlCommand(sqlQuery, connection)){

                        //valores nuevos
                        command.Parameters.AddWithValue("@Nuevafecha", asigna_reim_alumno.datetime_termino);
                        //condicion (id de la sesion)
                        command.Parameters.AddWithValue("@thisesion_id", asigna_reim_alumno.sesion_id);

                        int rowsAffected = command.ExecuteNonQuery();

                        Console.WriteLine($"{rowsAffected} fila(s) afectada(s).");
                    }
                        
                }
                catch (MySqlException exception){
                    Debug.Log(exception.Message);
                }
                //Debug.Log("MySQL - Closed Connection");
                connection.Close();
            }
        }
        catch (MySqlException exception){
            Debug.Log(exception.Message);
        }
    }

    //Actualizar tiempo de la actividad
    public static void SaveTiempoActividad(Tiempoxactividad tiempoactinicial){
        try{
            using (MySqlConnection connection = new MySqlConnection(SystemSave.conexionDB.GetConnection())){
                connection.Open();
                string sqlQuery = "INSERT INTO tiempoxactividad (inicio, final, causa, usuario_id, reim_id, actividad_id) VALUES (@inicio, @final, @causa, @usuario_id, @reim_id, @actividad_id)";
                try{
                    using (MySqlCommand command = new MySqlCommand(sqlQuery, connection)){

                        //valores nuevos
                        command.Parameters.AddWithValue("@inicio", tiempoactinicial.inicio);
                        command.Parameters.AddWithValue("@final", tiempoactinicial.final);
                        command.Parameters.AddWithValue("@causa", tiempoactinicial.causa);
                        command.Parameters.AddWithValue("@usuario_id", tiempoactinicial.usuario_id);
                        command.Parameters.AddWithValue("@reim_id", tiempoactinicial.reim_id);
                        command.Parameters.AddWithValue("@actividad_id", tiempoactinicial.actividad_id);

                        int rowsAffected = command.ExecuteNonQuery();
                        Console.WriteLine($"{rowsAffected} fila(s) afectada(s).");
                        
                    }
                        
                }
                catch (MySqlException exception){
                    Debug.Log(exception.Message);
                }

                //recuperar id del los datos insertados
                string selectQuery = "SELECT id FROM tiempoxactividad WHERE inicio = @inicio AND final = @final AND causa = @causa AND usuario_id = @usuario_id AND reim_id = @reim_id AND actividad_id = @actividad_id";
                try{
                    using (MySqlCommand command = new MySqlCommand(selectQuery, connection)){
                        command.Parameters.AddWithValue("@inicio", tiempoactinicial.inicio);
                        command.Parameters.AddWithValue("@final", tiempoactinicial.final);
                        command.Parameters.AddWithValue("@causa", tiempoactinicial.causa);
                        command.Parameters.AddWithValue("@usuario_id", tiempoactinicial.usuario_id);
                        command.Parameters.AddWithValue("@reim_id", tiempoactinicial.reim_id);
                        command.Parameters.AddWithValue("@actividad_id", tiempoactinicial.actividad_id);
                        using (MySqlDataReader reader = command.ExecuteReader()){
                            if (reader.Read()){
                                // Acceder a los datos de cada fila
                                tiempoactinicial.id_tiempoactividad = reader.GetInt32(0);
                                //Debug.Log("id tiempoactividad: " + tiempoactinicial.id_tiempoactividad);
                            }
                        }
                    }
                }
                catch (MySqlException exception){
                    Debug.Log(exception.Message);
                }
                //Debug.Log("MySQL - Closed Connection");
                connection.Close();
            }
        }
        catch (MySqlException exception){
            Debug.Log(exception.Message);
        }
    }

    public static IEnumerator UpdateTiempoActividad(int id, string dateFinal){ //Actualizamos con el momento actual
        try{
            using (MySqlConnection connection = new MySqlConnection(SystemSave.conexionDB.GetConnection())){
                connection.Open();
                string sqlQuery = "UPDATE tiempoxactividad SET final = @Datetimefinal WHERE id=@id_tiempoactividad";
                try{
                    using (MySqlCommand command = new MySqlCommand(sqlQuery, connection)){
                        //Debug.Log("actualizando fecha final: " + dateFinal);
                        //valores nuevos
                        command.Parameters.AddWithValue("@Datetimefinal", dateFinal);
                        command.Parameters.AddWithValue("@id_tiempoactividad", id);

                        int rowsAffected = command.ExecuteNonQuery();

                        Console.WriteLine($"{rowsAffected} fila(s) afectada(s).");
                    }
                        
                }
                catch (MySqlException exception){
                    Debug.Log(exception.Message);
                }
                //Debug.Log("MySQL - Closed Connection");
                connection.Close();
            }
        }
        catch (MySqlException exception){
            Debug.Log(exception.Message);
        }
        yield return new WaitForSeconds(2f);
    }

    public static IEnumerator UpdateTiempoActividadFinal(int id, string dateFinal, int causa){ //Actualizamos con el momento actual
        try{
            using (MySqlConnection connection = new MySqlConnection(SystemSave.conexionDB.GetConnection())){
                connection.Open();
                string sqlQuery = "UPDATE tiempoxactividad SET final = @Datetimefinal, causa = @causa WHERE id=@id_tiempoactividad";
                try{
                    using (MySqlCommand command = new MySqlCommand(sqlQuery, connection)){
                        //Debug.Log("actualizando fecha final: " + dateFinal);
                        //valores nuevos
                        command.Parameters.AddWithValue("@Datetimefinal", dateFinal);
                        command.Parameters.AddWithValue("@causa", causa);
                        command.Parameters.AddWithValue("@id_tiempoactividad", id);

                        int rowsAffected = command.ExecuteNonQuery();

                        Console.WriteLine($"{rowsAffected} fila(s) afectada(s).");
                    }
                        
                }
                catch (MySqlException exception){
                    Debug.Log(exception.Message);
                }
                //Debug.Log("MySQL - Closed Connection");
                connection.Close();
            }
        }
        catch (MySqlException exception){
            Debug.Log(exception.Message);
        }
        yield return new WaitForSeconds(1f);
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
