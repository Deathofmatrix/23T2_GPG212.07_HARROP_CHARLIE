using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HarropCharlie.MusicGame
{
    [CreateAssetMenu(fileName = "LevelMenu", menuName = "ScriptableObjects/New Level", order = 1)]
    public class LevelSO : ScriptableObject
    {
        public int level;
        public string description;
        public string difficulty;
    }
}
