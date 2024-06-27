
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
using MySqlX.XDevAPI;
using System.Linq;



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
    public int id_dibujo_reim;
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

[Serializable]
public class Ciudad_data{   //datos asociados a la ciudad
    public PlayerData recursos; //datos con los recursos del jugador
    public List<EdificioData> edificiosCiudad;  //Lista con los datos de los edificios de la ciudad del jugador
}
public static class SystemSave{
    //Datos conexion a la bbdd
    public static DBConnection conexionDB = new DBConnection("66.97.47.164", "ulearnet_web", "Uchile2020.", "ulearnet_reim_pilotaje");

    //Datos de la sesion
    public static Usuario usuario = new Usuario(); //Guardamos los datos del usuario para futuras consultas
    public static Asigna_reim_alumno asigna_reim_alumno;    //Datos de duracion de la sesion
    public static Ciudad_data ciudad_data = new Ciudad_data();  //Datos asociados a la ciudad

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

    //Consultas a BBDD
    public static void SavePlayer (CtrlRecursos recursos){  //actualiza los recursos del jugador en la base de datos
        PlayerData data = new PlayerData(recursos);
        ciudad_data.recursos = data;    //se guardan los recursos en ciudad data
        //Guardar en la base de datos
        UpdateEdificios(ciudad_data.edificiosCiudad);
    }

    public static PlayerData LoadPlayer (){ //carga los recursos del jugador de la base de datos
        //Cargar de la base de datos
        LoadEdificios();
        return ciudad_data.recursos;   //Retornamos los recursos
    }

    public static void SaveEdificios (List<EdificioData> ListaEdificios){   //guarda la ciudad en un nuevo valor en la bbdd
        ciudad_data.edificiosCiudad = ListaEdificios;   //actualizamos el objeto ciudad_data con los nuevos datos de edificios

        BinaryFormatter formatter = new BinaryFormatter();
        MemoryStream memoryStream = new MemoryStream(); //datos binarios del objetos ciudad_data
        try
        {
            formatter.Serialize(memoryStream, ciudad_data);
            using (MySqlConnection connection = new MySqlConnection(SystemSave.conexionDB.GetConnection())){
                connection.Open();
                string sqlQuery = "INSERT INTO dibujo_reim (sesion_id, usuario_id, reim_id, actividad_id, imagen) VALUES (@sesion_id, @usuario_id, @reim_id, @actividad_id, @imagen)";
                try{
                    using (MySqlCommand command = new MySqlCommand(sqlQuery, connection)){
                        command.Parameters.AddWithValue("@sesion_id", asigna_reim_alumno.sesion_id);  //id de la sesion
                        command.Parameters.AddWithValue("@usuario_id", usuario.id);    //id del usuario
                        command.Parameters.AddWithValue("@reim_id", reim.id);   //id del reim
                        command.Parameters.AddWithValue("@actividad_id", Ciudad.id);   //id de la actividad asociada
                        command.Parameters.AddWithValue("@imagen", memoryStream.ToArray());  //datos binarios de la ciudad

                        int rowsAffected = command.ExecuteNonQuery();

                        Console.WriteLine($"{rowsAffected} fila(s) afectada(s).");
                    }        
                }
                catch (MySqlException exception){
                    Debug.Log(exception.Message);
                }
                Debug.Log("MySQL - Closed Connection");
                connection.Close();
            }
        }
        catch (Exception e)
        {
            Debug.LogError("error al guardar edificios en la base de datos" + e.Message);
        }
    }

    public static void UpdateEdificios (List<EdificioData> ListaEdificios){ //actualiza la ciudad
        ciudad_data.edificiosCiudad = ListaEdificios;   //actualizamos el objeto ciudad_data con los nuevos datos de edificios

        BinaryFormatter formatter = new BinaryFormatter();
        MemoryStream memoryStream = new MemoryStream(); //datos binarios del objetos ciudad_data
        try
        {
            formatter.Serialize(memoryStream, ciudad_data);
            using (MySqlConnection connection = new MySqlConnection(SystemSave.conexionDB.GetConnection())){
                connection.Open();
                string sqlQuery = "UPDATE dibujo_reim SET imagen = @imagen WHERE usuario_id = @usuario_id AND reim_id = @reim_id AND actividad_id = @actividad_id";
                try{
                    using (MySqlCommand command = new MySqlCommand(sqlQuery, connection)){
                        //command.Parameters.AddWithValue("@sesion_id", asigna_reim_alumno.sesion_id);  //id de la sesion
                        command.Parameters.AddWithValue("@usuario_id", usuario.id);    //id del usuario
                        command.Parameters.AddWithValue("@reim_id", reim.id);   //id del reim
                        command.Parameters.AddWithValue("@actividad_id", Ciudad.id);   //id de la actividad asociada
                        command.Parameters.AddWithValue("@imagen", memoryStream.ToArray());  //datos binarios de la ciudad

                        int rowsAffected = command.ExecuteNonQuery();

                        Console.WriteLine($"{rowsAffected} fila(s) afectada(s).");
                    }        
                }
                catch (MySqlException exception){
                    Debug.Log(exception.Message);
                }
                Debug.Log("MySQL - Closed Connection");
                connection.Close();
            }
        }
        catch (Exception e)
        {
            Debug.LogError("error al guardar edificios en la base de datos" + e.Message);
        }
    }

    public static List<EdificioData> LoadEdificios (){  //descarga los datos de la ciudad, edificios y recursos; retorna una lista con los datos de los edificios
        try{
            using (MySqlConnection connection = new MySqlConnection(SystemSave.conexionDB.GetConnection())){
                connection.Open();
                string selectQuery = "SELECT imagen FROM dibujo_reim WHERE usuario_id = @usuario_id AND reim_id = @reim_id AND actividad_id = @actividad_id ";
                
                try{
                    using (MySqlCommand command = new MySqlCommand(selectQuery, connection)){
                        command.Parameters.AddWithValue("@usuario_id", usuario.id);
                        command.Parameters.AddWithValue("@reim_id", reim.id);
                        command.Parameters.AddWithValue("@actividad_id", Ciudad.id);
                        using (MySqlDataReader reader = command.ExecuteReader()){
                            if (reader.Read()){
                                Ciudad_data data_ciudad = new Ciudad_data();    //datos nuevos de la ciudad
                                // Acceder a los datos de cada fila
                                byte[] data = (byte[])reader["imagen"];
                                data_ciudad = DeserializarCiudadData(data); //Datos en objeto nuevo
                                ciudad_data = data_ciudad;  //remplazamos los datos antiguos por los descargados
                            }else{  //si no encuentra datos guardados
                                //Generamos un guardado en la base de datos
                                List<EdificioData> emptyCity = new List<EdificioData>();  //nueva ciudad con lista de edificios vacia
                                SaveEdificios(emptyCity);   //guardamos el registro en la bbdd para asociarlo al usuario (primer inicio de la aplicacion)
                                Debug.Log("No hay datos de guardado, creando nuevo archivo de guardado");
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
        return ciudad_data.edificiosCiudad; //retorna los datos con los edificios de la ciudad
    }

    private static Ciudad_data DeserializarCiudadData(byte[] data)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        MemoryStream memoryStream = new MemoryStream(data);
        return (Ciudad_data)formatter.Deserialize(memoryStream);
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

    //Carga y descarga de imagenes
    public static List<dibujo_reim> ImportDibujo(){ // se importan todos los dibujos del usuario
        List<dibujo_reim> listDibujos = new List<dibujo_reim>();    //lista para guardar los dibujos (en caso de que se quiera hacer algo mas)
        
        try{
            using (MySqlConnection connection = new MySqlConnection(SystemSave.conexionDB.GetConnection())){
                connection.Open();
                string selectQuery = "SELECT id_dibujo_reim, imagen FROM dibujo_reim WHERE usuario_id = @usuario_id AND reim_id = @reim_id AND actividad_id = @actividad_id ";
                
                try{
                    using (MySqlCommand command = new MySqlCommand(selectQuery, connection)){
                        command.Parameters.AddWithValue("@usuario_id", usuario.id);
                        command.Parameters.AddWithValue("@reim_id", reim.id);
                        command.Parameters.AddWithValue("@actividad_id", actividad4.id);
                        using (MySqlDataReader reader = command.ExecuteReader()){
                            while (reader.Read()){
                                dibujo_reim dibujo = new dibujo_reim();
                                // Acceder a los datos de cada fila
                                dibujo.id_dibujo_reim = Convert.ToInt32(reader["id_dibujo_reim"]);
                                dibujo.sesion_id = asigna_reim_alumno.sesion_id;
                                dibujo.usuario_id = usuario.id;
                                dibujo.reim_id = reim.id;
                                dibujo.actividad_id = actividad4.id;
                                dibujo.imagen = (byte[])reader["imagen"];
                                listDibujos.Add(dibujo);
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

        return listDibujos;
    }

    public static void ExportDibujo(dibujo_reim dibujo){  //Exporta el dibujo a la base de datos
        try{
            using (MySqlConnection connection = new MySqlConnection(SystemSave.conexionDB.GetConnection())){
                connection.Open();
                string sqlQuery = "INSERT INTO dibujo_reim (id_dibujo_reim, sesion_id, usuario_id, reim_id, actividad_id, imagen) VALUES (@id_dibujo_reim, @sesion_id, @usuario_id, @reim_id, @actividad_id, @imagen)";
                try{
                    using (MySqlCommand command = new MySqlCommand(sqlQuery, connection)){
                        command.Parameters.AddWithValue("@id_dibujo_reim", dibujo.id_dibujo_reim);  //id del dibujo
                        command.Parameters.AddWithValue("@sesion_id", dibujo.sesion_id);  //id de la sesion
                        command.Parameters.AddWithValue("@usuario_id", dibujo.usuario_id);    //id del usuario
                        command.Parameters.AddWithValue("@reim_id", dibujo.reim_id);   //id del reim
                        command.Parameters.AddWithValue("@actividad_id", dibujo.actividad_id);   //id de la actividad asociada
                        command.Parameters.AddWithValue("@imagen", dibujo.imagen);  //datos binarios del dibujo

                        int rowsAffected = command.ExecuteNonQuery();

                        Console.WriteLine($"{rowsAffected} fila(s) afectada(s).");
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
