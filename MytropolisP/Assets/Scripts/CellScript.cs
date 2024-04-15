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
        if(wallR.activeSelf == false){
            if(wallU.activeSelf == false){
                if(wallD.activeSelf == false){
                    //poner sprite de esquina
                    GetComponent<SpriteRenderer>().sprite = sprites[12];
                    //GetComponent<Renderer>().material.SetColor("_Color", Color.red);
                }
                else if(wallL.activeSelf == false){
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
            else if(wallD.activeSelf == false){
                if(wallL.activeSelf == false){
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
            else if(wallL.activeSelf == true){
                GetComponent<SpriteRenderer>().sprite = sprites[7];
            }
        
        }

        //Si no no hay muro izq
        else if(wallL.activeSelf == false){
            if(wallU.activeSelf == false){
                if(wallD.activeSelf == false){
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
            else if(wallD.activeSelf == false){
                //poner sprite de esquina
                GetComponent<SpriteRenderer>().sprite = sprites[2];
                //GetComponent<Renderer>().material.SetColor("_Color", Color.red);
            }
            else{
                GetComponent<SpriteRenderer>().sprite = sprites[5];
            }
        
        }

        else if(wallU.activeSelf == false && wallD.activeSelf == false){
            //poner sprite de vertical
            GetComponent<SpriteRenderer>().sprite = sprites[4];
            //GetComponent<Renderer>().material.SetColor("_Color", Color.blue);            
        }
        else if(wallU.activeSelf == false){
            GetComponent<SpriteRenderer>().sprite = sprites[6];
        }

        else if(wallD.activeSelf == false){
            GetComponent<SpriteRenderer>().sprite = sprites[8];
        }

    }
}
