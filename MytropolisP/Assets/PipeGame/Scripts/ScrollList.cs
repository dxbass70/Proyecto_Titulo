using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

namespace MadFireOn
{
    //this script creates the scrolling level buttons in level menu

    [System.Serializable]
    public class Items
    {
        public string levelName; // scene name which to load
        public int levelIndex; //number on the level icon
        public string levelNum; //level number
        public Sprite[] levelImages; //0 for incomplete and 1 for complete
        [HideInInspector]
        public bool unLock , completed;
        public Button.ButtonClickedEvent thingToDo;
    }

    public class ScrollList : MonoBehaviour
    {

        public static ScrollList instance;


        public Button homeBtn;                    //ref to home button
        public string homeScene;                  //ref to home scene name

        public Items[] levels;                    //levels array

        public GameObject refButton;              //ref to sample button prefab

        public Transform scrollPanel;             //ref to scroll panel where all the buttons will be Instantiated

        void Awake()
        {
            MakeInstance();
        }

        void MakeInstance()
        {
            if (instance == null)
            {
                instance = this;
            }
        }

        // Use this for initialization
        void Start()
        {
            //to hide banner ads in the level select scene
            //bannerView.Hide()

            //the methode called when we click home button
            homeBtn.GetComponent<Button>().onClick.AddListener(() => { HomeButton(); });

            //we check how many levels are unlocked before loading all the buttons
            for (int i = 0; i < GameManager.instance.levels.Length; i++)
            {
                levels[i].unLock = GameManager.instance.levels[i];
                levels[i].completed = GameManager.instance.levelCompleted[i];
            }

            //handle the requirement for each level as per the list
            foreach (Items i in levels)
            {

                GameObject btn = Instantiate(refButton);//here we get ref to the instanciated button

                SampleButton samBtn = btn.GetComponent<SampleButton>();//ref to the script of button

                samBtn.levelIndex = i.levelIndex; //here we set index to keep track whihc level is on
                samBtn.levelNum.text = i.levelNum;//here we set the number of level button
                samBtn.levelName = i.levelName; //here we set the name os scene which to be loaded

                //f level is unlocked we do following
                if (i.unLock == true)
                {
                    samBtn.levelLocked.gameObject.SetActive(false);
                    //if our level is just unlocked we show incomplete level image on button
                    samBtn.levelImage.sprite = i.levelImages[0];
                    samBtn.levelNum.enabled = true;

                    samBtn.button.interactable = true;
                }
                //if not we do following
                else
                {
                    samBtn.levelLocked.gameObject.SetActive(true);
                    samBtn.levelNum.enabled = false;

                    samBtn.button.interactable = false;
                }

                if (i.unLock && i.completed)
                {
                    samBtn.levelLocked.gameObject.SetActive(false);
                    //if our level is just unlocked & complete we show complete level image on button
                    samBtn.levelImage.sprite = i.levelImages[1];
                    samBtn.levelNum.enabled = true;

                    samBtn.button.interactable = true;
                }


                samBtn.button.onClick = i.thingToDo;

                btn.transform.SetParent(scrollPanel);
                //we set the scale of button fo by default they dot become too large or too small
                btn.transform.localScale = new Vector3(1f, 1f, 1f);

            }

        }

        // Update is called once per frame
        void Update()
        {

        }

        void HomeButton()
        {
            SceneManager.LoadScene(homeScene);
        }

    }
}//namespace MadFireOn