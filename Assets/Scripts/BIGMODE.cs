using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HarropCharlie.MusicGame
{
    public class BIGMODE : MonoBehaviour
    {
        [SerializeField] private CinemachineVirtualCamera m_Camera;
        [SerializeField] private bool isBigMode;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Player"))
            {
                if (isBigMode)
                {
                    m_Camera.m_Lens.OrthographicSize = 12f;
                }
                else
                {
                    m_Camera.m_Lens.OrthographicSize = 5f;
                }
                
            }
        }
    }
}
