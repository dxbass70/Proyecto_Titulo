using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MySql.Data.MySqlClient;


public class DBConnection : MonoBehaviour
{
	private static MySqlConnection conn;
	private string StrConection;


	void Start()
	{
		MySqlConnection conn = new MySqlConnection();
		string StrConection = "Server=localhost;Database=ulearnet_reim_pilotaje;Uid=root;Pwd=1234;CharSet=utf8;port=3306";
		conn.ConnectionString = StrConection;
		Debug.Log("intentando Coneccion");
		try
		{
			Debug.Log("Coneccion apunto");
			conn.Open();
			Debug.Log("Coneccion lista");
		}
		catch (MySqlException error)
		{
			Debug.Log("Fallo la conexión. " + error);
		}
	}

	void Update()
	{

	}

}
