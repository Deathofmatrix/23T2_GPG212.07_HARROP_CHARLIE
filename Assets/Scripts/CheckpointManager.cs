using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace HarropCharlie.MusicGame
{
    public class CheckpointManager : MonoBehaviour
    {
        [SerializeField] private GameObject playerCharacterGO;
        [SerializeField] private Transform startTransform;

        public int currentCheckpoint;
        private Transform currentCheckpointTransform;

        private void Start()
        {
            currentCheckpointTransform = startTransform;
        }
        private void OnEnable()
        {
            Flag.OnEnterFlag += SetCurrentCheckpoint;
            PlayerCharacterController.OnPlayerInvisible += RespawnPlayer;
        }

        private void OnDisable()
        {
            Flag.OnEnterFlag -= SetCurrentCheckpoint;
            PlayerCharacterController.OnPlayerInvisible -= RespawnPlayer;
        }

        private void SetCurrentCheckpoint(int checkpointNumber, Transform flagTransform)
        {
            if (currentCheckpoint < checkpointNumber)
            {
                currentCheckpoint = checkpointNumber;
                currentCheckpointTransform = flagTransform;
            }

            if (checkpointNumber == 7)
            {
                SceneManager.LoadScene(2);
            }
        }

        private void RespawnPlayer()
        {
            playerCharacterGO.transform.position = currentCheckpointTransform.position;
            playerCharacterGO.gameObject.GetComponent<SpriteRenderer>().enabled = true;
        }
    }
}
