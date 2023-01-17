using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace FinnLenz
{
    /// <summary>
    /// Manages the Time Score
    /// </summary>
    public class Score_Manager : MonoBehaviour
    {
        internal TimeSpan ts_timeValue;                         // TimeSpan Instance to translate fl_timeScore into
        internal float fl_timeScore;                            // float to measure the time
        [SerializeField] private Key_Manager cl_km;             // link to Key Manager Instance
        [SerializeField] private UI_Manager cl_uim;             // link to UI Manager Instance
        [SerializeField] private FrameSystem_Manager cl_fsm;    // link to FrameSystem_Manager Instance


        void Start()
        {
            fl_timeScore = 0;
        }

        /// <summary>
        /// Adds time to timeScore float. 
        /// translates timeScore to timeValue TimeSpan and displays it in UI
        /// </summary>
        void Update()
        {
            if (!cl_fsm.bl_startscreen && !cl_fsm.bl_menu && !cl_fsm.bl_end && !cl_fsm.bl_pause)
            {
                fl_timeScore += Time.deltaTime;

                ts_timeValue = TimeSpan.FromSeconds(fl_timeScore);

                string text = ts_timeValue.ToString("mm':'ss':'ff");

                cl_uim.tmpro_timer.text = text;
            }

        }

        
        /// <summary>
        /// returns the current time score as integar
        /// </summary>
        /// <returns></returns>
        public string SetScore()
        {
            string newScore = ts_timeValue.ToString("mm':'ss':'ff");

            Debug.Log(newScore);


            return newScore;
        }
        
        


        /// <summary>
        /// returns new HighScoreEntry with fed score and string
        /// </summary>
        /// <param name="score">the current score value</param>
        /// <param name="text">the name for the current score</param>
        /// <returns></returns>
        public HighScoreEntry NewHighScore(TimeSpan score, string text)
        {
            HighScoreEntry newScoreEntry = new HighScoreEntry
            {
                entryScore = score,
                scoreName = text
            };

            return newScoreEntry;
        }

        /// <summary>
        /// returns new HighScoreEntry with fed score and string
        /// </summary>
        /// <param name="score">the current score value</param>
        /// <param name="text">the name for the current score</param>
        /// <returns></returns>
        public HighScoreEntrySave NewHighScoreSave(string score, string text)
        {
            HighScoreEntrySave newScoreEntry = new HighScoreEntrySave
            {
                entryScore = score,
                scoreName = text
            };

            return newScoreEntry;
        }
    }
    /// <summary>
    /// Class used as template for Entries in the High Score Table
    /// </summary>
    [System.Serializable]
    public struct HighScoreEntry
    {
        public TimeSpan entryScore;
        public string scoreName;

        public HighScoreEntry(TimeSpan entryScore, string scoreName)
        {
            this.entryScore = entryScore;
            this.scoreName = scoreName;
        }

    }

    [System.Serializable]
    public struct HighScoreEntrySave
    {
        public string entryScore;
        public string scoreName;

        public HighScoreEntrySave(string entryScore, string scoreName)
        {
            this.entryScore = entryScore;
            this.scoreName = scoreName;
        }
    }
}