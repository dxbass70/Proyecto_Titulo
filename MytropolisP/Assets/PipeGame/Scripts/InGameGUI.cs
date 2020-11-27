using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace MadFireOn
{
    public class InGameGUI : MonoBehaviour
    {

        public static InGameGUI instance;

        [SerializeField]
        private string fbPageLink, twitterLink;

        //variable important in game
        [SerializeField]
        private Button homeBtn, nextBtn, retryBtn, fbBtn, twitterBtn, adsBtn, closeAdsBtn;
        [SerializeField]
        private Button pauseBtn, resumeBtn, restartBtn, pausehomeBtn;
        [Tooltip("Here set the least number of moves required to complete level")]
        [SerializeField]
        private int targetMoves;  //you need to change this in inspector for each level
        [SerializeField]
        private Text movesText, targetMovesText, bestMovesText, totalHints;
        [SerializeField]
        private GameObject levelCompletePanel, getHints, pausePanel;
        [SerializeField]
        private string mainMenu;
        private int currentLevelInd; //this is for the level name number

        [SerializeField]
        private Image[] stars; // the stars from left to right , element 0 holds left star 
        [SerializeField]
        private Sprite[] starSprites; // 0 for not achieved and 1 for achieved
        [HideInInspector]
        public bool showHint = false;
        public AudioClip[] soundClips; //0 for button click and 1 for victory
        private AudioSource sound;
        int i = 0; //just to play victory sound onces

        void Awake()
        {
            if (instance == null)
                instance = this;
        }

        // Use this for initialization
        void Start()
        {
            //get the audiosource component attached to the game object
            sound = GetComponent<AudioSource>();

            //sound button
            if (GameManager.instance.isMusicOn == true)
                AudioListener.volume = 1;
            else
                AudioListener.volume = 0;

            //in game scene want banner at top
            //AdsManager.instance.RequestBanner("AdPosition.Top");

            //we want levelComplete panel deative is by mistake it is active at start
            levelCompletePanel.SetActive(false);
            //we store the current level number in currentLevelInd variable
            currentLevelInd = GameManager.instance.currentLevelNumber;
            //here we assign the methodes respective button must call when clicked
            homeBtn.GetComponent<Button>().onClick.AddListener(() => { HomeBtn(); });    //home
            retryBtn.GetComponent<Button>().onClick.AddListener(() => { RetryBtn(); });    //retry
            nextBtn.GetComponent<Button>().onClick.AddListener(() => { NextBtn(); });    //next
            //hint panel btns
            fbBtn.GetComponent<Button>().onClick.AddListener(() => { FbBtn(); }); //fb 
            twitterBtn.GetComponent<Button>().onClick.AddListener(() => { TwitterBtn(); }); //twitter
            adsBtn.GetComponent<Button>().onClick.AddListener(() => { AdsBtn(); }); //ads
            closeAdsBtn.GetComponent<Button>().onClick.AddListener(() => { CloseGetHintPanel(); });

            //pause panel btns
            pauseBtn.GetComponent<Button>().onClick.AddListener(() => { PauseBtn(); }); //pause 
            resumeBtn.GetComponent<Button>().onClick.AddListener(() => { ResumeBtn(); }); //resume
            restartBtn.GetComponent<Button>().onClick.AddListener(() => { RetryBtn(); }); //restart
            pausehomeBtn.GetComponent<Button>().onClick.AddListener(() => { HomeBtn(); });


            //this shows how many hinst are remaining
            totalHints.text = "" + GameManager.instance.hints;

        }

        // Update is called once per frame
        void Update()
        {
            //when level is comlete
            if (GameManager.instance.levelComplete)
            {
                //we assign the stars
                StarTracker();
                // change the following details
                movesText.text = "Total " + InputHandler.instance.moves;
                targetMovesText.text = "Target " + targetMoves.ToString();
                bestMovesText.text = "WENA WOMMMMMM ";
                //and make the levelComplete panel active
                levelCompletePanel.SetActive(true);
                if (i == 0)
                {
                    sound.PlayOneShot(soundClips[1]);
                    i = 1;
                }
            }

            //fb and twitter buttons will not be interactable once they are clicked
            if (GameManager.instance.fbBtnClicked)
            {
                fbBtn.interactable = false;
            }

            if (GameManager.instance.twitterBtnClicked)
            {
                twitterBtn.interactable = false;
            }


        }
        //method called when "home" button is clicked
        void HomeBtn()
        {
            sound.PlayOneShot(soundClips[0]);
            GameManager.instance.levelComplete = false;
            SceneManager.LoadScene(mainMenu);
            //if player puase the game and click on hime button the pause must be over
            Time.timeScale = 1;
        }

        //method called when "next" button is clicked
        void NextBtn()
        {
            sound.PlayOneShot(soundClips[0]);
            //this is to prevent game to change scene after the last level
            if (GameManager.instance.currentLevel < GameManager.instance.levels.Length - 1)
            {
                GameManager.instance.currentLevel++;
                GameManager.instance.currentLevelNumber = currentLevelInd + 1;
                GameManager.instance.levelComplete = false;
            }
        }


        //method called when "retry" button is clicked
        void RetryBtn()
        {
            sound.PlayOneShot(soundClips[0]);
            //when we click we wand levelComplete to be false
            GameManager.instance.levelComplete = false;
            //code to reload the current level
            Scene scene = SceneManager.GetActiveScene();
            SceneManager.LoadScene(scene.name);
            Time.timeScale = 1;
        }

        //this method keep track of star depending on the total moves taken by player to complete level
        void StarTracker()
        {
            int totalMoves = InputHandler.instance.moves;
            //targetMoves , this vaarialbe has to be set in the inspector depeding on the 
            //level;s least required moves to complete 
            if (totalMoves == targetMoves)
            {
                stars[0].sprite = starSprites[1];
                stars[1].sprite = starSprites[1];
                stars[2].sprite = starSprites[1];
            }
            else if (totalMoves > targetMoves && totalMoves <= targetMoves + 2)
            {
                stars[0].sprite = starSprites[1];
                stars[1].sprite = starSprites[1];
                stars[2].sprite = starSprites[0];
            }
            else if (totalMoves > targetMoves + 2 && totalMoves <= targetMoves + 4)
            {
                stars[0].sprite = starSprites[1];
                stars[1].sprite = starSprites[0];
                stars[2].sprite = starSprites[0];
            }
            else
            {
                stars[0].sprite = starSprites[0];
                stars[1].sprite = starSprites[0];
                stars[2].sprite = starSprites[0];
            }

        }
        //methode called when fb btn is pressed
        void FbBtn()
        {
            sound.PlayOneShot(soundClips[0]);
            //1st the url is opened
            Application.OpenURL(fbPageLink);
            //the hint panel is deactivated
            getHints.SetActive(false);
            GameManager.instance.fbBtnClicked = true;
            //hint is increased by 5
            GameManager.instance.hints += 5;
            //its then save
            GameManager.instance.Save();
            InputHandler.instance.hintPanelOn = false;
        }

        //methode called when twitter btn is pressed
        void TwitterBtn()
        {
            sound.PlayOneShot(soundClips[0]);
            //1st the url is opened
            Application.OpenURL(twitterLink);
            //the hint panel is deactivated
            getHints.SetActive(false);
            GameManager.instance.twitterBtnClicked = true;
            //hint is increased by 5
            GameManager.instance.hints += 5;
            //its then save
            GameManager.instance.Save();
            InputHandler.instance.hintPanelOn = false;
        }

        //methode called when ads btn is pressed
        void AdsBtn()
        {
            sound.PlayOneShot(soundClips[0]);
            //here we ask to play the reward ads
            UnityAdsManager.instance.ShowRewardedAd(); //for unity
            //AdsManager.instance.ShowRewardBasedVideo();//for admob
            //the hint panel is deactivated
            getHints.SetActive(false);
            //hint is increased by 1
            GameManager.instance.hints ++;
            //its then save
            GameManager.instance.Save();
            InputHandler.instance.hintPanelOn = false;
        }

        void CloseGetHintPanel()
        {
            sound.PlayOneShot(soundClips[0]);
            getHints.SetActive(false);
            InputHandler.instance.hintPanelOn = false;
        }

        void PauseBtn()
        {
            sound.PlayOneShot(soundClips[0]);
            pausePanel.SetActive(true);
            Time.timeScale = 0;
        }

        void ResumeBtn()
        {
            sound.PlayOneShot(soundClips[0]);
            Time.timeScale = 1;
            pausePanel.SetActive(false);         
        }

    }
}//namespace MadFireOn