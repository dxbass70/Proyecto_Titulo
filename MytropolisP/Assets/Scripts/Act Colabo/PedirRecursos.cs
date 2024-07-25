using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using MySql.Data;
using MySql.Data.MySqlClient;

public class PedirRecursos : MonoBehaviour
{
    public GameObject VentanaTablon;
    public Anuncio usuarioData = new Anuncio(); 
    public InputField agua;
    public InputField energia;
    public InputField monedas;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ConfirmarPeticion(){
        gameObject.SetActive(false);
        usuarioData.date = DateTime.Now;
        print("Usuario_id: " + SystemSave.usuario.id);
        if (usuarioData.monedas > 0)   //confirmamos que la peticion no este vacia
        {
            RegistrarPeticion(SystemSave.Ulearcoin.id, usuarioData.monedas);
        }
        if (usuarioData.agua > 0)   //confirmamos que la peticion no este vacia
        {
            RegistrarPeticion(SystemSave.Agua.id, usuarioData.agua);
        }
        if (usuarioData.elect > 0)   //confirmamos que la peticion no este vacia
        {
            RegistrarPeticion(SystemSave.Electricidad.id, usuarioData.elect);
        }        
        usuarioData = new Anuncio();
        VentanaTablon.SetActive(true);
        print("Peticion Confirmada");
        //enviar datos a la bbdd
    }
    public void CancelarPeticion(){
        gameObject.SetActive(false);
        usuarioData = new Anuncio();    //Se limpia el objeto para hacer una nueva peticion
        VentanaTablon.SetActive(true);
        print("Peticion Cancelada");
    }

    //Estas funciones actualizan el valor almacenado en los textfield y lo guardan en un objeto de clase anuncio
    public void GuardaMonedas(Text textfield){  
        usuarioData.monedas =  Convert.ToInt32(textfield.text);
        print(usuarioData.monedas);
    }

    public void GuardAgua(Text textfield){
        usuarioData.agua =  Convert.ToInt32(textfield.text);
        print(usuarioData.agua);
    }

    public void GuardaElect(Text textfield){
        usuarioData.elect =  Convert.ToInt32(textfield.text);
        print(usuarioData.elect);
    }

    private void RegistrarPeticion(int elemento_id, int cantidad){ //realiza el insert del anuncio a la base de datos (uno por cada recurso)
        try{
            using (MySqlConnection connection = new MySqlConnection(SystemSave.conexionDB.GetConnection())){
                connection.Open();
                string sqlQuery = "INSERT INTO transaccion_reim (usuarioenvia_id, usuariorecibe_id, elemento_id, cantidad, datetime_transac, estado) VALUES (@Valor1, @Valor2, @Valor3, @Valor4, @Valor5, @Valor6)";
                try{
                    using (MySqlCommand command = new MySqlCommand(sqlQuery, connection)){
                        command.Parameters.AddWithValue("@Valor1", SystemSave.usuario.id);  //usuario que envia (por defecto es el mismo que pide)
                        command.Parameters.AddWithValue("@Valor2", SystemSave.usuario.id);  //usuario que pide
                        command.Parameters.AddWithValue("@Valor3", elemento_id);    //recurso que pide
                        command.Parameters.AddWithValue("@Valor4", cantidad);   //cantidad que solicita
                        command.Parameters.AddWithValue("@Valor5", usuarioData.date);   //momento en que hace la solicitud
                        command.Parameters.AddWithValue("@Valor6", 0);  //estado por defecto 0 (no tiene oferta)

                        int rowsAffected = command.ExecuteNonQuery();

                        Console.WriteLine($"{rowsAffected} fila(s) afectada(s).");
                    }
                        
                }
                catch (MySqlException exception){
                    print(exception.Message);
                }
                print("MySQL - Closed Connection");
                connection.Close();
            }
        }
        catch (MySqlException exception){
            print(exception.Message);
        }
    }
}
