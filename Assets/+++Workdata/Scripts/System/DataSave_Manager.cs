using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;


namespace FinnLenz
{
    /// <summary>
    /// Manages Data Saving and Loading Methods
    /// </summary>
    public class DataSave_Manager : MonoBehaviour
    {
        internal ScoreSaveState sss;                        // instance of Score Save State Class
        private string localDestination, path, filename;    // strings to set name and filepath of JSON file
        internal string jsonString;                         // string that contains the data loaded from JSON file
        internal List<HighScoreEntry> l_highScoreEntriesSorting = new List<HighScoreEntry>();   // List used for sorting Highscores
                                                                                                // It seems that you neither can save TimeSpans to a Json File,
                                                                                                // nor can you sort a String List by Value

        /// <summary>
        /// Declares all important classes and Elements
        /// </summary>
        void Start()
        {
            localDestination = Application.persistentDataPath;
            filename = "HighscoreData.json";
            path = localDestination + "/" + filename;
            sss = new ScoreSaveState
            {
                hl_highscoreEntryList = new List<HighScoreEntrySave>(),

                tl_highscoreEntries = new List<Transform>(),

            };
        }

        /// <summary>
        /// Saves Data to Json File
        /// </summary>
        internal void WriteJsonFile()
        {
            jsonString = JsonUtility.ToJson(sss);
            File.WriteAllText(path, jsonString);
        }

        /// <summary>
        /// Loads Data from Json File
        /// </summary>
        internal void ReadJsonFile()
        {
            if(File.Exists(path))
            {
                jsonString = File.ReadAllText(path);
                sss = JsonUtility.FromJson<ScoreSaveState>(jsonString);
            }
        }
    }

    /// <summary>
    /// class that holds every important Data to be saved or loaded
    /// </summary>
    public class ScoreSaveState
    {
        public List<Transform> tl_highscoreEntries;
        public List<HighScoreEntrySave> hl_highscoreEntryList;
    }
}