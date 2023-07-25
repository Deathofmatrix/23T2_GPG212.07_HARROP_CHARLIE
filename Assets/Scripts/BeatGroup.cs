using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace HarropCharlie.MusicGame
{
    public class BeatGroup : MonoBehaviour
    {
        [SerializeField] private UnityEvent _objectsToSwitch;

        public void SwitchObjectState()
        {
            _objectsToSwitch.Invoke();
        }
    }
}