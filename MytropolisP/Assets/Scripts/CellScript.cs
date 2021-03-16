using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellScript : MonoBehaviour {

    public GameObject wallL;
    public GameObject wallR;
    public GameObject wallU;
    public GameObject wallD;
    public Sprite[] sprites;

    void Update(){
        checkwalls();
    }

    void checkwalls(){  //Comprueba si no hay muros
        ////Si no no hay muro der
        if(wallR.active == false){
            if(wallU.active == false){
                if(wallD.active == false){
                    //poner sprite de esquina
                    GetComponent<SpriteRenderer>().sprite = sprites[12];
                    //GetComponent<Renderer>().material.SetColor("_Color", Color.red);
                }
                else if(wallL.active == false){
                    //poner sprite de esquina
                    GetComponent<SpriteRenderer>().sprite = sprites[11];
                    //GetComponent<Renderer>().material.SetColor("_Color", Color.red);
                }
                else{
                    //poner sprite de esquina
                    GetComponent<SpriteRenderer>().sprite = sprites[0];
                    //GetComponent<Renderer>().material.SetColor("_Color", Color.red);
                }  
            }
            else if(wallD.active == false){
                if(wallL.active == false){
                    //poner sprite de esquina
                    GetComponent<SpriteRenderer>().sprite = sprites[9];
                    //GetComponent<Renderer>().material.SetColor("_Color", Color.red);
                }
                else{
                    //poner sprite de esquina
                    GetComponent<SpriteRenderer>().sprite = sprites[3];
                    //GetComponent<Renderer>().material.SetColor("_Color", Color.red);
                } 
            }
            else if(wallL.active == true){
                GetComponent<SpriteRenderer>().sprite = sprites[7];
            }
        
        }

        //Si no no hay muro izq
        else if(wallL.active == false){
            if(wallU.active == false){
                if(wallD.active == false){
                    //poner sprite de esquina
                    GetComponent<SpriteRenderer>().sprite = sprites[10];
                    //GetComponent<Renderer>().material.SetColor("_Color", Color.red);
                }
                else{
                    //poner sprite de esquina
                    GetComponent<SpriteRenderer>().sprite = sprites[1];
                    //GetComponent<Renderer>().material.SetColor("_Color", Color.red);
                }  
            }
            else if(wallD.active == false){
                //poner sprite de esquina
                GetComponent<SpriteRenderer>().sprite = sprites[2];
                //GetComponent<Renderer>().material.SetColor("_Color", Color.red);
            }
            else{
                GetComponent<SpriteRenderer>().sprite = sprites[5];
            }
        
        }

        else if(wallU.active == false && wallD.active == false){
            //poner sprite de vertical
            GetComponent<SpriteRenderer>().sprite = sprites[4];
            //GetComponent<Renderer>().material.SetColor("_Color", Color.blue);            
        }
        else if(wallU.active == false){
            GetComponent<SpriteRenderer>().sprite = sprites[6];
        }

        else if(wallD.active == false){
            GetComponent<SpriteRenderer>().sprite = sprites[8];
        }

    }
}
