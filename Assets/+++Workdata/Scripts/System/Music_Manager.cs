using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FinnLenz
{
    /// <summary>
    /// Holds all Music for the Game
    /// </summary>
    public class Music_Manager : MonoBehaviour
    {
        [SerializeField] internal AudioSource[] arr_gameMusic;  // Array that holds the Music
        [SerializeField] internal UI_Manager cl_uim;            // link to UI Manager

        public void VolumeChangeMusic()
        {
            float fl_vol = cl_uim.sl_Music.value;

            for (int i = 0; i < arr_gameMusic.Length; i++)
            {
                arr_gameMusic[i].volume = fl_vol;
            }
        }
    }
}