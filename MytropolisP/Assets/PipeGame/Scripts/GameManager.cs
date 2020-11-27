using UnityEngine;
using System.Collections;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace MadFireOn
{

    public class GameManager : MonoBehaviour
    {

        public static GameManager instance;

        private GameData data;

        [SerializeField]
        private int totalLevels = 5;

        //variables which are not saved on device
        [HideInInspector]
        public bool levelComplete;
        [HideInInspector]
        public int currentLevel = 0; //this is used to unlock levels because the array start from zero

        //this is for change from one level to another when player completes level , it start from level 1
        [HideInInspector]
        public int currentLevelNumber = 1;

        //variables which are saved on the device
        [HideInInspector]
        public bool isGameStartedFirstTime;
        [HideInInspector]
        public int hints;
        [HideInInspector]
        public bool canShowAds;
        [HideInInspector]
        public bool isMusicOn;
        [HideInInspector]
        public bool[] levels; // to keep track on levels
        [HideInInspector]
        public bool[] levelCompleted; //this is to keep track which level is already completed
        [HideInInspector]
        public int[] bestMoves;
        [HideInInspector]
        public bool fbBtnClicked, twitterBtnClicked;
        [HideInInspector]

        void Awake()
        {
            MakeSingleton();
            InitializeGameVariables();
        }

        // Use this for initialization
        void Start()
        {
            
        }

        void MakeSingleton()
        {
            //this state that if the gameobject to which this script is attached , if it is present in scene then destroy the new one , and if its not present
            //then create new 
            if (instance != null)
            {
                Destroy(gameObject);
            }
            else
            {
                instance = this;
                DontDestroyOnLoad(gameObject);
            }
        }

        void InitializeGameVariables()
        {
            Load();
            if (data != null)
            {
                isGameStartedFirstTime = data.getIsGameStartedFirstTime();
            }
            else
            {
                isGameStartedFirstTime = true;
            }

            if (isGameStartedFirstTime)
            {
                isGameStartedFirstTime = false;
                isMusicOn = true;
                canShowAds = true;
                hints = 3;
                levels = new bool[totalLevels];
                fbBtnClicked = twitterBtnClicked = false;
                levels[0] = true;
                for (int i = 1; i < levels.Length; i++)
                {
                    levels[i] = false;
                }

                levelCompleted = new bool[levels.Length];

                for (int i = 0; i < levels.Length; i++)
                {
                    levelCompleted[i] = false;
                }

                bestMoves = new int[levels.Length];

                data = new GameData();

                data.setIsGameStartedFirstTime(isGameStartedFirstTime);
                data.setLevels(levels);
                data.setLevelsCompleted(levelCompleted);
                data.setMusicOn(isMusicOn);
                data.setCanShowAds(canShowAds);
                data.setHints(hints);
                data.setBestMoves(bestMoves);
                data.setFbClick(fbBtnClicked);
                data.setTwitterClick(twitterBtnClicked);
                Save();

                Load();
            }
            else
            {
                isGameStartedFirstTime = data.getIsGameStartedFirstTime();
                isMusicOn = data.getMusicOn();
                levels = data.getLevels();
                levelCompleted = data.getLevelsCompleted();
                hints = data.getHints();
                canShowAds = data.getCanShowAds();
                bestMoves = data.getBestMoves();
                fbBtnClicked = data.getFbClick();
                twitterBtnClicked = data.getTwitterClick();
            }
        }


        //                              .........this function take care of all saving data like score , current player , current weapon , etc
        public void Save()
        {
            FileStream file = null;
            //whicle working with input and output we use try and catch
            try
            {
                BinaryFormatter bf = new BinaryFormatter();

                file = File.Create(Application.persistentDataPath + "/GameData.dat");

                if (data != null)
                {
                    data.setIsGameStartedFirstTime(isGameStartedFirstTime);
                    data.setLevels(levels);
                    data.setLevelsCompleted(levelCompleted);
                    data.setMusicOn(isMusicOn);
                    data.setCanShowAds(canShowAds);
                    data.setHints(hints);
                    data.setBestMoves(bestMoves);
                    data.setFbClick(fbBtnClicked);
                    data.setTwitterClick(twitterBtnClicked);
                    bf.Serialize(file, data);
                }
            }
            catch (Exception e)
            {
            }
            finally
            {
                if (file != null)
                {
                    file.Close();
                }
            }


        }
        //                            .............here we get data from save
        public void Load()
        {
            FileStream file = null;
            try
            {
                BinaryFormatter bf = new BinaryFormatter();
                file = File.Open(Application.persistentDataPath + "/GameData.dat", FileMode.Open);
                data = (GameData)bf.Deserialize(file);

            }
            catch (Exception e)
            {
            }
            finally
            {
                if (file != null)
                {
                    file.Close();
                }
            }
        }

        //for resetting the gameManager

        public void ResetGameManager()
        {
            isGameStartedFirstTime = false;
            isMusicOn = true;
            canShowAds = true;
            hints = 3;
            levels = new bool[totalLevels];
            fbBtnClicked = twitterBtnClicked = false;
            levels[0] = true;
            for (int i = 1; i < levels.Length; i++)
            {
                levels[i] = false;
            }

            levelCompleted = new bool[levels.Length];

            for (int i = 0; i < levels.Length; i++)
            {
                levelCompleted[i] = false;
            }

            bestMoves = new int[levels.Length];

            data = new GameData();

            data.setIsGameStartedFirstTime(isGameStartedFirstTime);
            data.setLevels(levels);
            data.setLevelsCompleted(levelCompleted);
            data.setMusicOn(isMusicOn);
            data.setCanShowAds(canShowAds);
            data.setHints(hints);
            data.setBestMoves(bestMoves);
            data.setFbClick(fbBtnClicked);
            data.setTwitterClick(twitterBtnClicked);
            Save();
            Load();

            Debug.Log("GameManager Reset");
        }


    }

    [Serializable]
    class GameData
    {
        private bool isGameStartedFirstTime;

        private int hints;

        private bool isMusicOn;

        private bool canShowAds;

        private bool[] levels; //this keep track of which level is locked and which is not

        private bool[] levelCompleted; //this keep track whether the level is complete or not

        private int[] bestMoves;

        private bool fbBtnClicked, twitterBtnClicked;

        public void setHints(int hints)
        {
            this.hints = hints;
        }

        public int getHints()
        {
            return hints;
        }

        public void setCanShowAds(bool canShowAds)
        {
            this.canShowAds = canShowAds;
        }

        public bool getCanShowAds()
        {
            return this.canShowAds;
        }

        public void setIsGameStartedFirstTime(bool isGameStartedFirstTime)
        {
            this.isGameStartedFirstTime = isGameStartedFirstTime;

        }

        public bool getIsGameStartedFirstTime()
        {
            return this.isGameStartedFirstTime;

        }
        //                                                                    ...............music
        public void setMusicOn(bool isMusicOn)
        {
            this.isMusicOn = isMusicOn;

        }

        public bool getMusicOn()
        {
            return this.isMusicOn;

        }
        //                                                                      .......music

        //                                                                       ..................Level locked/unlocked
        public void setLevels(bool[] levels)
        {
            this.levels = levels;

        }

        public bool[] getLevels()
        {
            return this.levels;

        }
        //                                                                       ..................Level locked/unlocked

        //                                                                       ..................Level complete/not complete
        public void setLevelsCompleted(bool[] levelCompleted)
        {
            this.levelCompleted = levelCompleted;

        }

        public bool[] getLevelsCompleted()
        {
            return this.levelCompleted;

        }
        //                                                                       ..................Level complete/not complete

        public void setBestMoves(int[] bestMoves)
        {
            this.bestMoves = bestMoves;
        }

        public int[] getBestMoves()
        {
            return this.bestMoves;
        }
        //....................................................for fb btn
        public void setFbClick(bool fbBtnClicked)
        {
            this.fbBtnClicked = fbBtnClicked;

        }

        public bool getFbClick()
        {
            return this.fbBtnClicked;

        }

        //....................................................for twitter btn
        public void setTwitterClick(bool twitterBtnClicked)
        {
            this.twitterBtnClicked = twitterBtnClicked;

        }

        public bool getTwitterClick()
        {
            return this.twitterBtnClicked;

        }

    }
}//namespace MadFireOn
