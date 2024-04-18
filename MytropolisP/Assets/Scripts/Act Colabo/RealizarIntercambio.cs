using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using MySql.Data;
using MySql.Data.MySqlClient;

public class RealizarIntercambio : MonoBehaviour
{
    public GameObject VentanaTablon;
    [Header("Datos Usuario")]

    public Anuncio usuarioData = new Anuncio(); //se almacenan los recursos que ofrece el usuario
    public Text nombreUser;
    public InputField agua1;
    public InputField energia1;
    public InputField monedas1;
    private string aguatext1;
    private string energiatext1;
    private string monedastext1;
    [Header("Datos Anunciante")]
    public Anuncio anuncio;
    public Text NombreAnunciante;
    public Text agua2;
    public Text energia2;
    public Text monedas2;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ConfirmarIntercambio(){
        gameObject.SetActive(false);
        //enviar datos a la bbdd
        if (usuarioData.monedas > 0)   //confirmamos que la oferta no este vacia
        {
            RealizarOferta(SystemSave.Ulearcoin.id, usuarioData.monedas, anuncio.monedas);
        }
        if (usuarioData.agua > 0)   //confirmamos que la oferta no este vacia
        {
            RealizarOferta(SystemSave.Agua.id, usuarioData.agua, anuncio.agua);
        }
        if (usuarioData.elect > 0)   //confirmamos que la oferta no este vacia
        {
            RealizarOferta(SystemSave.Electricidad.id, usuarioData.elect, anuncio.elect);
        }
        usuarioData = new Anuncio();
        VentanaTablon.SetActive(true);
        print("Intercambio Confirmado");
        
    }

    public void CancelarIntercambio(){
        gameObject.SetActive(false);
        usuarioData = new Anuncio();    //Se limpia el objeto para hacer una nueva oferta a futuro
        VentanaTablon.SetActive(true);
        print("Intercambio Cancelado");
    }

    public void SetDatos(Anuncio anun){ //Asignamos los datos del anuncio
        anuncio = anun;
        NombreAnunciante.text = anuncio.nombre;
        agua2.text = anuncio.agua.ToString();
        energia2.text = anuncio.elect.ToString();
        monedas2.text = anuncio.monedas.ToString();
    }

    public void GuardaMonedas(Text textfield){
        //Confirmar si tiene las monedas necesarias
        usuarioData.monedas =  Convert.ToInt32(textfield.text);
        print(usuarioData.monedas);
    }

    public void GuardAgua(Text textfield){
        //Confirmar si tiene las monedas necesarias
        usuarioData.agua =  Convert.ToInt32(textfield.text);
        print(usuarioData.agua);
    }

    public void GuardaElect(Text textfield){
        //Confirmar si tiene las monedas necesarias
        usuarioData.elect =  Convert.ToInt32(textfield.text);
        print(usuarioData.elect);
    }

    private void RealizarOferta(int elemento_id, int cantidadOfertada, int cantidadPedida){
        try{
            using (MySqlConnection connection = new MySqlConnection(SystemSave.conexionDB.GetConnection())){
                connection.Open();
                /*string sqlQuery = @"UPDATE transaccion_reim
                                    SET usuarioenvia_id = '382',
                                        catalogo_id = '50' ,
                                        estado = '1'
                                    WHERE usuarioenvia_id = '380' AND usuariorecibe_id = '380' AND elemento_id = '3016' AND cantidad = '100' AND datetime_transac = '2024-04-17 19:07:23' AND estado = '0' ;";*/
                string sqlQuery = "UPDATE transaccion_reim SET usuarioenvia_id = @NuevoValor1, catalogo_id = @NuevoValor2, estado = @NuevoValor3 WHERE usuarioenvia_id = @Condicion1 AND usuariorecibe_id = @Condicion2 AND elemento_id = @Condicion3 AND cantidad = @Condicion4 AND datetime_transac = @Condicion5 AND estado = @Condicion6";
                try{
                    using (MySqlCommand command = new MySqlCommand(sqlQuery, connection)){

                        //valores nuevos
                        command.Parameters.AddWithValue("@NuevoValor1", SystemSave.usuario.id);
                        command.Parameters.AddWithValue("@NuevoValor2", cantidadOfertada);
                        command.Parameters.AddWithValue("@NuevoValor3", 1);

                        //condiciones
                        command.Parameters.AddWithValue("@Condicion1", anuncio.id);
                        command.Parameters.AddWithValue("@Condicion2", anuncio.id);
                        command.Parameters.AddWithValue("@Condicion3", elemento_id);
                        command.Parameters.AddWithValue("@Condicion4", cantidadPedida);
                        command.Parameters.AddWithValue("@Condicion5", anuncio.date);
                        command.Parameters.AddWithValue("@Condicion6", 0);  //el anuncio no debe tener oferta

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
