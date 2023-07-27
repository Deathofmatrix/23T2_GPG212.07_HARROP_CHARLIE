using EasyAudioSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HarropCharlie.MusicGame
{
    public class Flag : MonoBehaviour
    {
        public delegate void SetCheckpointAction(int checkpointNumber, Transform flagTransform);
        public static event SetCheckpointAction OnEnterFlag;

        [SerializeField] private int checkpointNumber;
        [SerializeField] private bool hasFlagBeenTriggered;
        [SerializeField] private ParticleSystem myParticleSystem;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Player"))
            {
                OnEnterFlag(checkpointNumber, gameObject.transform);
                if (!hasFlagBeenTriggered)
                {
                    myParticleSystem.Play();
                    FindObjectOfType<AudioManager>().Play("Checkpoint");
                    hasFlagBeenTriggered = true;
                }
            }
        }
    }
}
