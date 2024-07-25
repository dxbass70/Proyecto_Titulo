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
            agua1.DeactivateInputField();
            energia1.DeactivateInputField();
            monedas1.DeactivateInputField();
            print(anuncio.estado);
            print("El usuario debe confirmar");
        }else if(anuncio.estado == 0){  //el usuario debe ofertar
            agua1.ActivateInputField();
            energia1.ActivateInputField();
            monedas1.ActivateInputField();
            print(anuncio.estado);
            print("el usuario debe oferta");
        }
    }

    public void ConfirmarIntercambio(){
        gameObject.SetActive(false);
        //enviar datos a la bbdd
        if (anuncio.estado == 0){  //el usuario debe ofertar
            if (usuarioData.monedas > 0 && usuarioData.monedas <= SystemSave.ciudad_data.recursos.Monedas)   //confirmamos que la oferta no este vacia y tenga recursos
            {
                RealizarOferta(SystemSave.Ulearcoin.id, usuarioData.monedas, anuncio.monedas);
                if (usuarioData.monedas < anuncio.monedas) // si la cantidad que da es menor que la pedida se crea un anuncio con los recursos faltantes
                {
                    int newCantidad = anuncio.monedas - usuarioData.monedas;
                    RegistrarPeticion(SystemSave.Ulearcoin.id, newCantidad);
                }
                ctrlRecursos.RestaMonedas(usuarioData.monedas); //se restan los recursos al usuario
            }
            if (usuarioData.agua > 0 && usuarioData.agua <= SystemSave.ciudad_data.recursos.Agua)   //confirmamos que la oferta no este vacia y tenga recursos
            {
                RealizarOferta(SystemSave.Agua.id, usuarioData.agua, anuncio.agua);
                if (usuarioData.agua < anuncio.agua) // si la cantidad que da es menor que la pedida se crea un anuncio con los recursos faltantes
                {
                    int newCantidad = anuncio.agua - usuarioData.agua;
                    RegistrarPeticion(SystemSave.Agua.id, newCantidad);
                }
                ctrlRecursos.RestaAgua(usuarioData.agua); //se restan los recursos al usuario
            }
            if (usuarioData.elect > 0 && usuarioData.elect <= SystemSave.ciudad_data.recursos.Elect)   //confirmamos que la oferta no este vacia y tenga recursos
            {
                RealizarOferta(SystemSave.Electricidad.id, usuarioData.elect, anuncio.elect);
                if (usuarioData.elect < anuncio.elect) // si la cantidad que da es menor que la pedida se crea un anuncio con los recursos faltantes
                {
                    int newCantidad = anuncio.elect - usuarioData.elect;
                    RegistrarPeticion(SystemSave.Electricidad.id, newCantidad);
                }
                ctrlRecursos.RestaElect(usuarioData.elect); //se restan los recursos al usuario
            }
            //guardar nuevo monto de recursos en bbdd
            ctrlRecursos.SavePlayer(); //Guarda los datos
            usuarioData = new Anuncio();
            VentanaTablon.SetActive(true);
            print("Intercambio Confirmado");  
        }else if(anuncio.estado == 1){  //el usuario debe confirmar
            //se cierra el intercambio en caso de que entreguen todos los recursos pedidos(estado = 2 no aparece en el tablon)
            if (anuncio.monedas != 0)   //si se entregaron monedas
            {
                ConfirmarOferta(SystemSave.Ulearcoin.id, anuncio.monedas);
                ctrlRecursos.SumarMonedas(anuncio.monedas); //se agregan los recursos
            }
            if (anuncio.agua != 0)   //si se entregaron monedas
            {
                ConfirmarOferta(SystemSave.Agua.id, anuncio.agua);
                ctrlRecursos.SumarAgua(anuncio.agua); //se agregan los recursos
            }
            if (anuncio.elect != 0)   //si se entregaron monedas
            {
                ConfirmarOferta(SystemSave.Electricidad.id, anuncio.elect);
                ctrlRecursos.SumarElect(anuncio.elect); //se agregan los recursos
            }
            ctrlRecursos.SavePlayer();  //se guardan los datos en la bbdd
            
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
                string sqlQuery = "UPDATE transaccion_reim SET usuarioenvia_id = @NuevoValor1, cantidad = @NuevoValor2, estado = @NuevoValor3 WHERE usuarioenvia_id = @Condicion1 AND usuariorecibe_id = @Condicion2 AND elemento_id = @Condicion3 AND cantidad = @Condicion4 AND datetime_transac = @Condicion5 AND estado = @Condicion6";
                try{
                    using (MySqlCommand command = new MySqlCommand(sqlQuery, connection)){

                        //valores nuevos
                        command.Parameters.AddWithValue("@NuevoValor1", SystemSave.usuario.id);
                        command.Parameters.AddWithValue("@NuevoValor2", cantidadOfertada);  //se cambia la cantidad pedida por la ofertada
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

    //Se confirma la oferta para entregar los recursos y cambiar el estado a 2
    private void ConfirmarOferta(int elemento_id, int cantidadPedida){
        try{
            using (MySqlConnection connection = new MySqlConnection(SystemSave.conexionDB.GetConnection())){
                connection.Open();
                string sqlQuery = "UPDATE transaccion_reim SET estado = @NuevoValor WHERE usuarioenvia_id = @Condicion1 AND usuariorecibe_id = @Condicion2 AND elemento_id = @Condicion3 AND cantidad = @Condicion4 AND datetime_transac = @Condicion5 AND estado = @Condicion6";
                try{
                    using (MySqlCommand command = new MySqlCommand(sqlQuery, connection)){

                        //valores nuevos
                        command.Parameters.AddWithValue("@NuevoValor", 2); //Si se entrega la cantidad pedida se cierra la oferta

                        //condiciones
                        command.Parameters.AddWithValue("@Condicion1", anuncio.id); //id del que envia
                        command.Parameters.AddWithValue("@Condicion2", SystemSave.usuario.id); //id del que pide (el usuario)
                        command.Parameters.AddWithValue("@Condicion3", elemento_id);    //id del recurso
                        command.Parameters.AddWithValue("@Condicion4", cantidadPedida); //cantidad que se da
                        command.Parameters.AddWithValue("@Condicion5", anuncio.date);   //fecha del anuncio
                        command.Parameters.AddWithValue("@Condicion6", 1);  //el anuncio debe tener oferta

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

    private void RegistrarPeticion(int elemento_id, int cantidad){ //realiza el insert del anuncio a la base de datos (uno por cada recurso)
        try{
            using (MySqlConnection connection = new MySqlConnection(SystemSave.conexionDB.GetConnection())){
                connection.Open();
                string sqlQuery = "INSERT INTO transaccion_reim (usuarioenvia_id, usuariorecibe_id, elemento_id, cantidad, datetime_transac, estado) VALUES (@Valor1, @Valor2, @Valor3, @Valor4, @Valor5, @Valor6)";
                try{
                    using (MySqlCommand command = new MySqlCommand(sqlQuery, connection)){
                        command.Parameters.AddWithValue("@Valor1", anuncio.id);  //usuario que envia (por defecto es el mismo que pide)
                        command.Parameters.AddWithValue("@Valor2", anuncio.id);  //usuario que pide
                        command.Parameters.AddWithValue("@Valor3", elemento_id);    //recurso que pide
                        command.Parameters.AddWithValue("@Valor4", cantidad);   //cantidad que solicita
                        command.Parameters.AddWithValue("@Valor5", DateTime.Now);   //momento en que hace la solicitud original
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
