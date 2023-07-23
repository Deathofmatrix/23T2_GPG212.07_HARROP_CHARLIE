using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Tilemaps;
using UnityEngine;

namespace HarropCharlie.MusicGame
{
    public class MoveBlock : MonoBehaviour
    {
        [SerializeField] private Transform childToMove;
        [SerializeField] private Transform transformStart;
        [SerializeField] private Transform transformToMoveTo;
        [SerializeField] private float speed;
        [SerializeField] private bool isAtStartPosition;

        [SerializeField] private int numberofBeatsPassed;

        private void Start()
        {
            isAtStartPosition = true;
        }

        private IEnumerator MoveToPositionCoroutine(Vector3 targetPosition)
        {
            while (childToMove.position != targetPosition)
            {
                float step = speed * Time.deltaTime;
                childToMove.position = Vector3.MoveTowards(childToMove.position, targetPosition, step);
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

        public void MoveAtOffset(int offset)
        {
            numberofBeatsPassed++;
            if (numberofBeatsPassed == offset)
            {
                SwitchMove();
            }
            if (numberofBeatsPassed == 4)
            {
                numberofBeatsPassed = 0;
            }
            
        }
    }
}
