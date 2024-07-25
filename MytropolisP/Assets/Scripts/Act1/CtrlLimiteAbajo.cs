using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CtrlLimiteAbajo : MonoBehaviour
{
    public GameObject ActivityCtrl;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D other){
        if(other.gameObject.tag == "Basura"){ //Al chocar con un objeto de tag basura, la basura se destruye
            ActivityCtrl.SendMessage("DisminuirVidas");
            Destroy(other.gameObject);
            //Debug.Log("Basura perdida, -1 Vida");
        }
    }
}
