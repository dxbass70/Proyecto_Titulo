using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CtrlGeneradorBasura : MonoBehaviour
{
    private Rigidbody2D rb2d;
    private string direccion = "izquierda";
    public float velocity = 2f;
    public GameObject BasuraPrefab;
    public float timerGenerador = 1.75f;
    public float positionx = 0;
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("CreateBasura", 1.0f, timerGenerador);
    }

    // Update is called once per frame
    void Update()
    {
        positionx=Random.Range(-7.09f,7.56f);      
    }
    
    void CreateBasura(){
        Instantiate(BasuraPrefab, new Vector3(positionx, 6.45f, -3.5703f), Quaternion.identity);
    }


}
