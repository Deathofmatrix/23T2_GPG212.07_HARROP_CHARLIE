using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HarropCharlie.MusicGame
{
    public class parentToPlatform : MonoBehaviour
    {
        private void OnTriggerStay2D(Collider2D collision)
        {
            if (collision.CompareTag("Player"))
            {
                collision.transform.parent = transform;
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.CompareTag("Player"))
            {
                collision.transform.parent = PlayerCharacterController.playerParent;
            }
        }
    }
}
