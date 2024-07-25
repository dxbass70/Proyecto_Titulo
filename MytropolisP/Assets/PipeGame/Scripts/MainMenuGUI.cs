using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace MadFireOn
{
    public class MainMenuGUI : MonoBehaviour
    {
        [Header("Put the respective link")]
        public string ANDROID_URL, iOS_URL, moreGames = "";
        public string levelSelectScene;
        public Button playBtn, rateBtn, moreGamesBtn, soundBtn, noAds;
        public Sprite[] soundSprite;
        public Image soundImage;
        private AudioSource sound;

        // Use this for initialization
        void Start()
        {
            //get the audiosource component attached to the game object
            sound = GetComponent<AudioSource>();

            //in game scene want banner at bottom
            //AdsManager.instance.RequestBanner("AdPosition.Bottom");

            //sound button
            if (GameManager.instance.isMusicOn == true)
            {
                AudioListener.volume = 1;
                soundImage.sprite = soundSprite[1];
            }
            else
            {
                AudioListener.volume = 0;
                soundImage.sprite = soundSprite[0];
            }

            playBtn.GetComponent<Button>().onClick.AddListener(() => { PlayBtn(); });    //play
            rateBtn.GetComponent<Button>().onClick.AddListener(() => { RateBtn(); });    //rate
            moreGamesBtn.GetComponent<Button>().onClick.AddListener(() => { MoreGameBtn(); });    //more games
            soundBtn.GetComponent<Button>().onClick.AddListener(() => { SoundBtn(); });    //sound
            noAds.GetComponent<Button>().onClick.AddListener(() => { NoAds(); });    //noAds
        }

        // Update is called once per frame
        void Update()
        {

        }

        void PlayBtn()
        {
            sound.Play();
            SceneManager.LoadScene(levelSelectScene);
        }

        void RateBtn()
        {
            sound.Play();
#if UNITY_IPHONE
		Application.OpenURL(iOS_URL);
#endif

#if UNITY_ANDROID
            Application.OpenURL(ANDROID_URL);
#endif
        }

        void MoreGameBtn()
        {
            sound.Play();
            Application.OpenURL(moreGames);
        }

        void SoundBtn()
        {
            sound.Play();
            if (GameManager.instance.isMusicOn == true)
            {
                GameManager.instance.isMusicOn = false;
                AudioListener.volume = 0;
                soundImage.sprite = soundSprite[0];
                GameManager.instance.Save();
            }
            else
            {
                GameManager.instance.isMusicOn = true;
                AudioListener.volume = 1;
                soundImage.sprite = soundSprite[1];
                GameManager.instance.Save();
            }
        }

        void NoAds()
        {
            sound.Play();
            //Purchaser.instance.BuyNoAds();
        }



    }


}//namespace MadFireOn

