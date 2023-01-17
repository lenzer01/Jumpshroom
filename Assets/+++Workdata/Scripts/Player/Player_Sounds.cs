using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FinnLenz
{
    /// <summary>
    /// Holds all SFX Sounds for the Player
    /// </summary>
    public class Player_Sounds : MonoBehaviour
    {
        [SerializeField] internal UI_Manager cl_uim;
        /// <summary>
        /// 0: Instant Jump, 1: Charge, 2: Release Jump, 3: Hit Ground
        /// </summary>
        public AudioSource[] arr_audios;

        public void PlayAudio(int id)
        {
            arr_audios[id].Play();
        }

        public void VolumeChangeSound()
        {
            float fl_vol = cl_uim.sl_Sound.value;

            for (int i = 0; i < arr_audios.Length; i++)
            {
                arr_audios[i].volume = fl_vol;
            }
        }
    }
}