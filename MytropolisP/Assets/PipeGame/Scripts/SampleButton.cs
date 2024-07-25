using UnityEngine;
using System.Collections;
using UnityEngine.UI;
#if UNITY_5_3 || UNITY_5_3_OR_NEWER
using UnityEngine.SceneManagement;
#endif

namespace MadFireOn
{
    public class SampleButton : MonoBehaviour
    {

        public static SampleButton instance;

        //ref to the component of button
        public Image levelImage;
        public Text levelNum;
        public GameObject levelLocked;
        [HideInInspector]
        public int levelIndex;    //this is assigned by scroll list which creates the level buttons
        [HideInInspector]
        public string levelName;  //this is assigned by scroll list which creates the level buttons
        public Button button;

        void Awake()
        {
            if (instance == null)
                instance = this;
        }

        // Use this for initialization
        void Start()
        {
            button.GetComponent<Button>().onClick.AddListener(() => { ButtonPressed(); });
        }

        // Update is called once per frame
        void Update()
        {
            
        }

        //methode called when we press the button
        public void ButtonPressed()
        {
            //we tell game manager which level is selected
            GameManager.instance.currentLevel = levelIndex;
            GameManager.instance.currentLevelNumber = levelIndex + 1;
#if UNITY_5_3 || UNITY_5_3_OR_NEWER
            SceneManager.LoadScene(levelName);
#else
        Application.LoadLevel(levelName);
#endif
        }

    }

}//namespace MadFireOn