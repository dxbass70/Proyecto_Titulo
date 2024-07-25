using UnityEngine;
using System.Collections;
using DG.Tweening;

namespace MadFireOn
{
    public class PipeScript : MonoBehaviour
    {

        [Header("Used to check the piece rotation")]
        [SerializeField]
        private Vector3 currentAngle;    //this the angle the piece is currently in the scene
        [SerializeField]
        private Vector3 originalAngle;    //this the angle the piece need to complete level
        private Vector3 targetAngle;

        public int rotationDirection = -1; // -1 for clockwise & 1 for anti-clockwise
        public int rotationStep = 10; // should be less than 90

        public bool completed = false;     //bool which keep track if the piece is place correctly

        public bool movable = true; //we have 2 types of pipe movable and fixed , movable can rotate 
        //[HideInInspector]
        public bool hintType = false; // this will be used to make pipe hint pipe

        //following variables are used to rotate the pipe
        private Vector3 currentRotation;
        private Vector3 targetRotation;


        void Awake()
        {
            completed = false;
        }

        // Use this for initialization
        void Start()
        {
            //currentAngle = gameObject.transform.eulerAngles;
        }

        // Update is called once per frame
        void Update()
        {
            currentAngle = gameObject.transform.eulerAngles;

                if (currentAngle.z >= (originalAngle.z - 2) && currentAngle.z <= (originalAngle.z + 2))
                {
                    completed = true;
                }
                else
                {
                    completed = false;
                }
        }
        //this is called by cellscript to set the basic settings of piece
        public void PipeSettings(Vector3 currentRot, Vector3 originalRot)
        {
            gameObject.transform.rotation = Quaternion.Euler(currentRot);
            originalAngle = originalRot;
        }

        //this methode rotates the piece when its clicked
        public void RotateObj()
        {
            if (!movable)
            {
                return;
            }

            currentAngle = gameObject.transform.eulerAngles;
            targetAngle.z = currentAngle.z + (90 * rotationDirection);

            transform.DOLocalRotate(targetAngle, 0.2f);

        }


    }


}//namespace MadFireOn