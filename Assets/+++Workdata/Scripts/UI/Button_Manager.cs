using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

namespace FinnLenz
{
    /// <summary>
    /// Contains every custom Method for buttons in the Game
    /// </summary>
    public class Button_Manager : MonoBehaviour
    {
        [SerializeField] private DataSave_Manager cl_dsm;       // link to Data Save Manager Instance
        [SerializeField] private UI_Manager cl_uim;             // link to UI Manager Instance
        [SerializeField] private FrameSystem_Manager cl_fsm;    // link to Framesystem Manger Instance
        [SerializeField] private Music_Manager cl_mm;           // link to Music Manager Instance

        public void BUTTON_Start()
        {
            cl_fsm.bl_menu = false;
            cl_uim.StartLevel();
            cl_mm.arr_gameMusic[1].Stop();
            cl_mm.arr_gameMusic[2].Play();
            cl_mm.arr_gameMusic[3].Play();

        }

        public void BUTTON_Highscore()
        {
            cl_uim.ShowHighScore();
        }

        public void BUTTON_Exit()
        {
            Application.Quit();
        }

        public void BUTTON_LevelExit()
        {
            SceneManager.LoadScene(0);
        }

    }
}