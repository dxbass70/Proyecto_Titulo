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
    public CtrlRecursos ctrlRecursos;
    [Header("Datos Usuario")]

    public Anuncio usuarioData = new Anuncio(); //se almacenan los recursos que ofrece el usuario (Valores a la izquierda)
    public Text nombreUser;
    public InputField agua1;
    public InputField energia1;
    public InputField monedas1;
    private string aguatext1;
    private string energiatext1;
    private string monedastext1;
    [Header("Datos Anunciante")]
    public Anuncio anuncio; //datos de la peticion u oferta segun corresponde (Valores a la derecha)
    public Text NombreAnunciante;
    public Text agua2;
    public Text energia2;
    public Text monedas2;
    
    void OnEnable()
    {
        //comprobar si es una oferta o una peticion(estado = 1; estado = 0)
        if (anuncio.estado == 1)    //El usuario debe confirmar la oferta
        {
            print(anuncio.estado);
            print("El usuario debe confirmar");
        }else if(anuncio.estado == 0){  //el usuario debe ofertar
            print(anuncio.estado);
            print("el usuario debe oferta");
        }
    }

    public void ConfirmarIntercambio(){
        gameObject.SetActive(false);
        //enviar datos a la bbdd
        if (anuncio.estado == 0){  //el usuario debe ofertar
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
        }else if(anuncio.estado == 1){  //el usuario debe confirmar
            //se cierra el intercambio en caso de que entreguen todos los recursos pedidos(estado = 2 no aparece en el tablon)
            //se restan los recursos entregados en caso de contrario y se vuelve a listar(estado = 0 vuelve al tablon)
            //se entregan los recursos ofertados
            //se envian los recursos al otro usuario en caso de ser necesario
            
        }
        
        
    }

    public void CancelarIntercambio(){
        gameObject.SetActive(false);
        usuarioData = new Anuncio();    //Se limpia el objeto para hacer una nueva oferta a futuro
        VentanaTablon.SetActive(true);
        print("Intercambio Cancelado");
    }

    public void SetDatos(Anuncio anun){ //Asignamos los datos del anuncio
        anuncio = anun;
        if(SystemSave.usuario.nombre != null){
            nombreUser.text = SystemSave.usuario.nombre;
        }else{
            nombreUser.text = "Tu";
        }
        NombreAnunciante.text = anuncio.nombre;
        agua2.text = anuncio.agua.ToString();
        energia2.text = anuncio.elect.ToString();
        monedas2.text = anuncio.monedas.ToString();
    }

    public void GuardaMonedas(Text textfield){
        int countaux = Convert.ToInt32(textfield.text);
        if(countaux <= ctrlRecursos.CountMonedas){   //Comprobamos que el usuario tenga la cantidad de monedas que ofrece
            usuarioData.monedas = countaux;
            print(usuarioData.monedas);
            return;
        }else{  //si no tiene suficientes monedas 
            print("monedas insuficientes");
        }
        

        
    }

    public void GuardAgua(Text textfield){
        int countaux = Convert.ToInt32(textfield.text);
        if(countaux <= ctrlRecursos.CountAgua){   //Comprobamos que el usuario tenga la cantidad de monedas que ofrece
            usuarioData.agua = countaux;
            print(usuarioData.agua);
            return;
        }else{  //si no tiene suficientes monedas 
            print("Agua insuficiente");
        }
    }

    public void GuardaElect(Text textfield){
        int countaux = Convert.ToInt32(textfield.text);
        if(countaux <= ctrlRecursos.CountElect){   //Comprobamos que el usuario tenga la cantidad de monedas que ofrece
            usuarioData.elect = countaux;
            print(usuarioData.elect);
            return;
        }else{  //si no tiene suficientes monedas 
            print("Electricidad insuficiente");
        }
    }

    //Se envia la oferta a la base de datos para listarla en los anuncios del usuario
    private void RealizarOferta(int elemento_id, int cantidadOfertada, int cantidadPedida){
        try{
            using (MySqlConnection connection = new MySqlConnection(SystemSave.conexionDB.GetConnection())){
                connection.Open();
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

    //Se confirma la oferta para entregar los recursos y cambiar el estado a 0 o 2 segun corresponda
    private void ConfirmarOferta(int elemento_id, int cantidadOfertada, int cantidadPedida){
        try{
            using (MySqlConnection connection = new MySqlConnection(SystemSave.conexionDB.GetConnection())){
                connection.Open();
                string sqlQuery = "UPDATE transaccion_reim SET usuarioenvia_id = @NuevoValor1, catalogo_id = @NuevoValor2, estado = @NuevoValor3 WHERE usuarioenvia_id = @Condicion1 AND usuariorecibe_id = @Condicion2 AND elemento_id = @Condicion3 AND cantidad = @Condicion4 AND datetime_transac = @Condicion5 AND estado = @Condicion6";
                try{
                    using (MySqlCommand command = new MySqlCommand(sqlQuery, connection)){

                        //valores nuevos
                        command.Parameters.AddWithValue("@NuevoValor1", SystemSave.usuario.id);
                        command.Parameters.AddWithValue("@NuevoValor2", cantidadOfertada);
                        if (cantidadOfertada >= cantidadPedida){
                            command.Parameters.AddWithValue("@NuevoValor3", 2); //Si se entrega la cantidad pedida se cierra la oferta
                        }else if(cantidadOfertada < cantidadPedida){
                            command.Parameters.AddWithValue("@NuevoValor3", 0); //si se entrega menos se vuelve a listar en el tablon 
                        }
                        //cantidad ofertada registrar en la base de datos como un objeto en catalogo_reim

                        //condiciones
                        command.Parameters.AddWithValue("@Condicion1", anuncio.id);
                        command.Parameters.AddWithValue("@Condicion2", anuncio.id);
                        command.Parameters.AddWithValue("@Condicion3", elemento_id);
                        command.Parameters.AddWithValue("@Condicion4", cantidadPedida);
                        command.Parameters.AddWithValue("@Condicion5", anuncio.date);
                        command.Parameters.AddWithValue("@Condicion6", 1);  //el anuncio no debe tener oferta

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
