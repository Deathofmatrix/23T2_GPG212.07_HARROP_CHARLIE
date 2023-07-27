using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

namespace HarropCharlie.MusicGame
{
    public class RotateChomper : MonoBehaviour
    {
        [SerializeField] private float speedOfRotation;
        private void FixedUpdate()
        {
            transform.Rotate(0,0,6*speedOfRotation * Time.deltaTime);
        }
    }
}
