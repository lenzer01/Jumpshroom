using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

namespace FinnLenz
{
    /// <summary>
    /// is linked to and manages most of the UI Elements
    /// </summary>
    public class UI_Manager : MonoBehaviour
    {
        [SerializeField] private DataSave_Manager cl_dsm;               // link to Data Save Manger instance
        [SerializeField] private Score_Manager cl_sm;                   // link to Score Manager instance
        [SerializeField] private FrameSystem_Manager cl_fsm;            // link to Frame System Manager instance
        [SerializeField]
        internal Transform tr_entryContainer, 
            tr_highscoreTemplate, 
            tr_highscoreContainer, 
            tr_inputContainer, tr_pauseContainer, 
            tr_levelContainer, tr_menuContainer,
            tr_creditContainer, tr_TitleContainer,
            tr_gameTitle, tr_highscoreLevelTemplate, 
            tr_optionsContainer;                                        // links to UI Containers
        [SerializeField] internal float fl_templateHeight;              // float to control the gap between highscore entries
        [SerializeField] internal TextMeshProUGUI tmpro_timer;          // link to TMPro Object "Time value"
        [SerializeField] internal TMP_InputField if_name;               // link to Input Field Object "name"
        [SerializeField] private GameObject go_levelEndContainer;       // link to Level End Container Object
        [SerializeField] internal Color col_standard, col_mouseOver;    // colours to highlight buttons
        [SerializeField] internal Animator anim_title, anim_click;      // links to animators of starttitle elements
        [SerializeField] internal Slider sl_Music, sl_Sound;            // links to Volume Sliders

        /// <summary>
        /// ensures that every object that shall not be active in the beginning isn't active
        /// </summary>
        private void Start()
        {
            tr_inputContainer.gameObject.SetActive(false);
            tr_highscoreContainer.gameObject.SetActive(false);
            tr_creditContainer.gameObject.SetActive(false);
            tr_levelContainer.gameObject.SetActive(false);
            tr_optionsContainer.gameObject.SetActive(false);
            StartCoroutine(StartAnim());
        }

        /// <summary>
        /// controls pause screen
        /// </summary>
        private void Update()
        {
            tr_menuContainer.gameObject.SetActive(cl_fsm.bl_menu);
            tr_pauseContainer.gameObject.SetActive(cl_fsm.bl_pause);
            tr_TitleContainer.gameObject.SetActive(cl_fsm.bl_titleVisible);
        }


        /// <summary>
        /// Sets and activates the High Score Table and saves it to json
        /// </summary>
        public void ShowHighScore()
        {
            cl_fsm.CreateStandardTable();

            //activates and deactivates UI Elements depending on whether this an in Game Level or the Main Menu
            if (!cl_fsm.bl_menu)
            {
                cl_dsm.sss.hl_highscoreEntryList.Add(cl_sm.NewHighScoreSave(cl_sm.SetScore(), if_name.text.ToUpper()));
                tmpro_timer.gameObject.SetActive(false);
                go_levelEndContainer.SetActive(true);
                tr_entryContainer = GameObject.Find("Entry Container Level").transform;
            }
            else
            {
                tr_highscoreContainer.gameObject.SetActive(true);
                tr_entryContainer = GameObject.Find("Entry Container Menu").transform;
            }

            cl_dsm.l_highScoreEntriesSorting = new List<HighScoreEntry>();

            // fills SortHighscoreEntriesSorting with the Content of HighscoreEntrySave
            foreach (HighScoreEntrySave savedEntry in cl_dsm.sss.hl_highscoreEntryList)
            {
                TimeSpan entryTime = TimeSpan.Parse(savedEntry.entryScore);
                cl_dsm.l_highScoreEntriesSorting.Add(cl_sm.NewHighScore(entryTime, savedEntry.scoreName));
            }

            cl_fsm.SortScoreList();

            //limits the High Score List to 6
            if (cl_dsm.sss.hl_highscoreEntryList.Count > 6)
            {
                for (int i = 6; i <= cl_dsm.sss.hl_highscoreEntryList.Count; i++)
                {
                    i--;
                    cl_dsm.sss.hl_highscoreEntryList.RemoveAt(i);
                }
            }

            cl_dsm.sss.tl_highscoreEntries = new List<Transform>();

            if(!cl_fsm.bl_menu)
            {
                //feeds highScoreEntryList and highScoreEntries and calls CreateHighscoreTable
                foreach (HighScoreEntrySave entry in cl_dsm.sss.hl_highscoreEntryList)
                {
                    CreateHighscoreTableLevel(entry, cl_dsm.sss.tl_highscoreEntries);
                }
            }
            else
            {
                //feeds highScoreEntryList and highScoreEntries and calls CreateHighscoreTable
                foreach (HighScoreEntrySave entry in cl_dsm.sss.hl_highscoreEntryList)
                {
                    CreateHighscoreTableMenu(entry, cl_dsm.sss.tl_highscoreEntries);
                }
            }
            

            cl_dsm.WriteJsonFile();
        }

        /// <summary>
        /// Creates new highScoreTemplates with given information
        /// </summary>
        /// <param name="entry">HighScoreEntry Variable holding Information, linked to highScoreEntryList in ScoreSave</param>
        /// <param name="transformList">Variable linked to highScoreEntries in ScoreSave</param>
        public void CreateHighscoreTableMenu(HighScoreEntrySave entry, List<Transform> transformList)
        {
            //instatiating new highscoreTemplate, and positioning it
            Transform newTransform = Instantiate(tr_highscoreTemplate, tr_entryContainer);
            RectTransform newRectTransform = newTransform.GetComponent<RectTransform>();
            RectTransform oldRectTransform = tr_highscoreTemplate.GetComponent<RectTransform>();
            newRectTransform.anchoredPosition = new Vector2(0, oldRectTransform.anchoredPosition.y - fl_templateHeight * (transformList.Count + 1));
            newTransform.gameObject.SetActive(true);

            //sets the Information for every entry in newTransform
            newTransform.Find("Name").GetComponent<TextMeshProUGUI>().text = entry.scoreName.ToUpper();
            newTransform.Find("Time").GetComponent<TextMeshProUGUI>().text = entry.entryScore;

            //Adds newTransform to transformList
            transformList.Add(newTransform);
        }

        public void CreateHighscoreTableLevel(HighScoreEntrySave entry, List<Transform> transformList)
        {
            //instatiating new highscoreTemplate, and positioning it
            Transform newTransform = Instantiate(tr_highscoreLevelTemplate, tr_entryContainer);
            RectTransform newRectTransform = newTransform.GetComponent<RectTransform>();
            RectTransform oldRectTransform = tr_highscoreLevelTemplate.GetComponent<RectTransform>();
            newRectTransform.anchoredPosition = new Vector2(0, oldRectTransform.anchoredPosition.y - fl_templateHeight * (transformList.Count + 1));
            newTransform.gameObject.SetActive(true);

            //sets the Information for every entry in newTransform
            newTransform.Find("Name").GetComponent<TextMeshProUGUI>().text = entry.scoreName.ToUpper();
            newTransform.Find("Time").GetComponent<TextMeshProUGUI>().text = entry.entryScore;

            //Adds newTransform to transformList
            transformList.Add(newTransform);
        }

        /// <summary>
        /// Holds every function to start the level
        /// </summary>
        public void StartLevel()
        {
            tr_menuContainer.gameObject.SetActive(false);
            tr_levelContainer.gameObject.SetActive(true);
            Cursor.visible = false;
        }

        /// <summary>
        /// starts the animation of the title Object after half a second
        /// </summary>
        /// <returns></returns>
        private IEnumerator StartAnim()
        {
            yield return new WaitForSeconds(0.5f);
            anim_title.SetInteger("Intro", 1);
        }
    } 
}