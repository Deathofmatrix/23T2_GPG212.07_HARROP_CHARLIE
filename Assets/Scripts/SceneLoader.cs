using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace HarropCharlie.MusicGame
{
    public class SceneLoader : MonoBehaviour
    {
        public void LoadLevel(int sceneNumber)
        {
            SceneManager.LoadScene(sceneNumber);
        }
    }
}
