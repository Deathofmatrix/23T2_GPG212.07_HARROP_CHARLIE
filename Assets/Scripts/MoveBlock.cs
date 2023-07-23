using System.Collections;
using System.Collections.Generic;
using UnityEditor.Tilemaps;
using UnityEngine;

namespace HarropCharlie.MusicGame
{
    public class MoveBlock : MonoBehaviour
    {
        [SerializeField] private Transform transformStart;
        [SerializeField] private Transform transformToMoveTo;
        [SerializeField] private float speed;
        [SerializeField] private bool isAtStartPosition;

        private void Update()
        {

        }

        private IEnumerator MoveToPositionCoroutine(Vector3 targetPosition)
        {
            while (transform.position != targetPosition)
            {
                float step = speed * Time.deltaTime;
                transform.position = Vector3.MoveTowards(transform.position, targetPosition, step);
                yield return null;
            }
        }

        public void MoveToPosition(bool moveToSecondPosition)
        {
            if (moveToSecondPosition)
            {
                StartCoroutine(MoveToPositionCoroutine(transformToMoveTo.position));
            }
            else
            {
                StartCoroutine(MoveToPositionCoroutine(transformStart.position));
            }
        }

        public void SwitchMove()
        {
            if (isAtStartPosition)
            {
                MoveToPosition(true);
                isAtStartPosition = false;
            }
            else if (!isAtStartPosition)
            {
                MoveToPosition(false);
                isAtStartPosition = true;
            }
        }
    }
}
