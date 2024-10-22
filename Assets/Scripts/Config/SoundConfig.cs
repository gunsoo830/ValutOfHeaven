using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace config
{
    public class SoundConfig
    {
        private static string[] bgmSourcePath = null;
        
        public static void _initBgmSourcePath()
        {
            bgmSourcePath = new string[(int)BGM_LIST.COUNT];

            bgmSourcePath[(int)BGM_LIST.LOBBY] = "Sound/Bgm/Bgm_Lobby";
            bgmSourcePath[(int)BGM_LIST.BATTLE_STAGE] = "Sound/Bgm/Bgm_Stage_Battle";
            bgmSourcePath[(int)BGM_LIST.FREET] = "Sound/Bgm/Bgm_Freet";
        }

        public static string getSoundPath(int index)
        {
            if (bgmSourcePath != null && bgmSourcePath.Length >= index)
            {
                return bgmSourcePath[index];
            }
            else
            {
                return "FindPathFailed";
            }
        }
        public enum SoundType
        {
            Bgm = 0,
            Effect,
            Count,
            Max
        }
    
        public enum BGM_LIST
        {
            INTRO = -1,
            LOBBY,
            BATTLE_STAGE,
            FREET,
            COUNT,
        }
    } 
}

