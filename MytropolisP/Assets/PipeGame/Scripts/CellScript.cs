using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace MadFireOn
{
    //[ExecuteInEditMode]
    public class CellScript : MonoBehaviour
    {

        public GameObject pipePiece; //here we assign the peice
        public Vector3 currentRot;   //here we will assign the rotation of piece when level start
        public Vector3 originalRot;  //here we will assign the rotation piece should have to win the level

        string pipeName; //need this to distinguish between pipes

        // Use this for initialization
        void Start()
        {
            GetName();
            GenerateNormalPipe();
            GenerateHintPipe();
        }

        // Update is called once per frame
        void Update()
        {
           
        }
        //this methode gets the name of prefab assigned name
        public void GetName()
        {
            if (pipePiece != null)
            {
                PipeScript script = pipePiece.GetComponent<PipeScript>();
                pipeName = script.gameObject.name;
            }
        }
        //this methode creates the normal pipe
        public void GenerateNormalPipe()
        {
            //the below code to make editor editing possible
           //1st we check is any child is present and his name
            if (transform.Find(pipeName + " " + transform.name))
            {
                DestroyImmediate(GameObject.Find(pipeName + " "+ transform.name));
            }

            if (pipePiece == null)
            {
                return;
            }

            //we create new game object
            GameObject newPipe = (GameObject)Instantiate(pipePiece);
            newPipe.transform.parent = gameObject.transform;//set its parent
            newPipe.name = pipeName + " " + transform.name;//change its name
            newPipe.transform.rotation = Quaternion.Euler(currentRot);//sets its rotation
            newPipe.transform.parent = transform;
            newPipe.transform.localPosition = Vector3.zero;//sets is local position
            PipeScript pipeScript = newPipe.GetComponent<PipeScript>();//get the script componenet
            pipeScript.PipeSettings(currentRot, originalRot);//supply the required setting for the child

        }

        //this is for hint pipe , its only used when the prefabs is movable pipe
        public void GenerateHintPipe()
        {
            if (transform.Find(pipeName + " " + transform.name + "(Hint)"))
            {
                DestroyImmediate(GameObject.Find(pipeName + " " + transform.name + "(Hint)"));
            }

            if (pipePiece == null)
            {
                return;
            }

            PipeScript script = pipePiece.GetComponent<PipeScript>();

            if (script.movable)
            {
                //we make again a new object but this will be for hint pipe
            }
        }


    }
}//namespace MadFireOn