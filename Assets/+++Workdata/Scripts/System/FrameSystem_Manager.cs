using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FinnLenz
{
    /// <summary>
    /// Manages the Game System around the Gameplay Mechanics
    /// </summary>
    public class FrameSystem_Manager : MonoBehaviour
    {
        [SerializeField] internal bool bl_end;                  // bool to control the end of the game
        [SerializeField] internal bool bl_pause;                // bool to control pause function
        [SerializeField] internal bool bl_menu;                 // bool to control main menu function
        [SerializeField] internal bool bl_startscreen;          // bool to control the startscreen functions
        [SerializeField] internal bool bl_titleVisible;         // bool to control the startscreen functions
        [SerializeField] private UI_Manager cl_uim;             // link to UI Manager Instance
        [SerializeField] private DataSave_Manager cl_dsm;       // link to DataSave Manager Instance
        [SerializeField] private Score_Manager cl_sm;           // link to Score Manager Instance
        [SerializeField] internal Vector3 v3_playerPosition;    // variable to save the current Position of Player


        private void Start()
        {
            bl_startscreen = true;
            bl_titleVisible = true;
            bl_menu = false;
            bl_end = false;
            bl_pause = false;
        }

        /// <summary>
        /// Loads Information from Json File to set up a new Highscore Table
        /// </summary>
        internal void CreateStandardTable()
        {
            cl_dsm.sss.hl_highscoreEntryList = new List<HighScoreEntrySave>();

            cl_dsm.ReadJsonFile();



            // Destroys last set up Table
            if (cl_uim.tr_entryContainer.childCount > 1)
            {
                for (int i = 0; i < cl_dsm.sss.tl_highscoreEntries.Count; i++)
                {
                    Destroy(cl_uim.tr_entryContainer.GetChild(i).gameObject);
                }
            }

            cl_dsm.sss.tl_highscoreEntries = new List<Transform>();

            // sets up standard Highscore Table for first time, Highscore gets called
            if (cl_dsm.sss.hl_highscoreEntryList.Count < 1)
            {
                cl_dsm.sss.hl_highscoreEntryList = new List<HighScoreEntrySave>
                {
                    cl_sm.NewHighScoreSave(TimeSpan.FromSeconds(1700).ToString(), "OCT"),
                    cl_sm.NewHighScoreSave(TimeSpan.FromSeconds(1200).ToString(), "NYX"),
                    cl_sm.NewHighScoreSave(TimeSpan.FromSeconds(1300).ToString(), "DOS"),
                    cl_sm.NewHighScoreSave(TimeSpan.FromSeconds(1400).ToString(), "WIN"),
                    cl_sm.NewHighScoreSave(TimeSpan.FromSeconds(1500).ToString(), "ANI"),
                    cl_sm.NewHighScoreSave(TimeSpan.FromSeconds(1600).ToString(), "TUR"),
                    cl_sm.NewHighScoreSave(TimeSpan.FromSeconds(1700).ToString(), "HAI"),
                    cl_sm.NewHighScoreSave(TimeSpan.FromSeconds(1800).ToString(), "Bow"),
                    cl_sm.NewHighScoreSave(TimeSpan.FromSeconds(1900).ToString(), "FIT"),
                    cl_sm.NewHighScoreSave(TimeSpan.FromSeconds(10000).ToString(), "DOL"),

                };
            }

            
        }

        /// <summary>
        /// Sorts Entries in highscoreEntriesSorting from the highest score to the lowest score
        /// <br>translates entries back to strings and adds them into highscoreEntryList</br>
        /// </summary>
        internal void SortScoreList()
        {
            cl_dsm.l_highScoreEntriesSorting.Sort(SortEntryFunction);
            cl_dsm.sss.hl_highscoreEntryList = new List<HighScoreEntrySave>();
            for(int i = 0; i < cl_dsm.l_highScoreEntriesSorting.Count; i++)
            {
                string entryString = cl_dsm.l_highScoreEntriesSorting[i].entryScore.ToString();

                cl_dsm.sss.hl_highscoreEntryList.Add(new HighScoreEntrySave(entryString, cl_dsm.l_highScoreEntriesSorting[i].scoreName));
            }
        }

        private void Update()
        {
            if(bl_end || bl_menu || bl_pause || bl_startscreen)
            {
                Cursor.visible = true;
            }
            else
            {
                Cursor.visible = false;
            }
        }

        /// <summary>
        /// Compares Entryscores of HighscoreEntries
        /// <br>From Tutorial: https://www.youtube.com/watch?v=u4lDyznUd9Q </br>
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        internal int SortEntryFunction(HighScoreEntry a, HighScoreEntry b)
        {
            if(a.entryScore < b.entryScore)
            {
                return -1;
            }
            else if(a.entryScore > b.entryScore)
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }
    }
}
