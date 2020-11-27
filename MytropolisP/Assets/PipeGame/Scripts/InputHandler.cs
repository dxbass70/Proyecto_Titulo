using UnityEngine;
using System.Collections;

namespace MadFireOn
{
    public class InputHandler : MonoBehaviour
    {

        public static InputHandler instance;

        [SerializeField]
        private LayerMask pipeLayer;//layers which ray can hit

        private bool rotating = false;//bool which help in input control
        [HideInInspector]
        public int moves = 0; //this variable store number f moves took to complete the level

        [HideInInspector]
        public bool hintPanelOn = false;

        private AudioSource sound;

        void Awake()
        {
            if (instance == null)
                instance = this;
        }

        // Use this for initialization
        void Start()
        {
            sound = GetComponent<AudioSource>();
        }

        // Update is called once per frame
        void Update()
        {
            //we check if "tapped" and piece is not already rotating and hint panel is not on
            if (Input.GetMouseButtonDown(0) && !rotating && !hintPanelOn)
            {                
                rotating = true;
                DetectPipe();
                StartCoroutine(RotatingTrue());
            }
        }
        //this methode detects the pipe
        void DetectPipe()
        {
            //ray is created at the point of touch
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            //physics is added to the raycast
            RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction, Mathf.Infinity, pipeLayer);
            //check for the collider
            if (hit.collider != null)
            {
                sound.Play();
                //Debug.Log(hit.collider.name);
                moves++; //when we hit the puzzle piece we increase the moves by 1
                PipeScript pipeScript = hit.collider.gameObject.GetComponent<PipeScript>();
                pipeScript.RotateObj();

            }
        }
        //for limiting the number of taps per second
        IEnumerator RotatingTrue()
        {
            yield return new WaitForSeconds(0.25f);
            rotating = false;
        }
    }
}//namespace MadFireOn