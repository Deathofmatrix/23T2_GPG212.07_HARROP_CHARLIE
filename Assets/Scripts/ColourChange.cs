using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

namespace HarropCharlie.MusicGame
{
    public class ColourChange : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI m_TextMeshProUGUI;

        private void Awake()
        {
            m_TextMeshProUGUI = GetComponent<TextMeshProUGUI>();
        }

        public void ChangeColour()
        {
            Color randColour = new Color(Random.Range(0,255) / 255f, Random.Range(0, 255) / 255f, Random.Range(0, 255) / 255f, 1f);
            m_TextMeshProUGUI.color = randColour;
            Debug.Log(randColour.ToString());
        }
    }
}
