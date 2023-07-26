using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HarropCharlie.MusicGame
{
    public class BlockVIsibility : MonoBehaviour
    {
        [SerializeField] private GameObject[] childToToggle;
        [SerializeField] private int numberofBeatsPassed;

        [SerializeField] private bool isBlockVisible;

        public void SwitchVisibility()
        {
            if (isBlockVisible)
            {
                for (int i = 0; i < childToToggle.Length; i++)
                {
                    childToToggle[i].SetActive(false);
                    isBlockVisible = false;
                }
            }
            else if (!isBlockVisible)
            {
                for (int i = 0;i < childToToggle.Length; i++)
                {
                    childToToggle[i].SetActive(true);
                    isBlockVisible = true;
                }
            }
        }

        public void ToggleAtOffset(int offset)
        {
            numberofBeatsPassed++;
            if (numberofBeatsPassed == offset)
            {
                SwitchVisibility();
            }
            if (numberofBeatsPassed == 4)
            {
                numberofBeatsPassed = 0;
            }

        }
    }
}
