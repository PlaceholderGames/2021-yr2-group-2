using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextLevel : MonoBehaviour
{
    [SerializeField]
    [Tooltip("Next Level to trigger when level over")]
    public int nextLevel;
    public void GoToNextLevel()
    {
        SceneManager.LoadScene(nextLevel);
    }

}
