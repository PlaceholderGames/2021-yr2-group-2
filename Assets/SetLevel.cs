using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class SetLevel : MonoBehaviour
{
    public int FirstLevel;
    public void StartGame()
    {
        SceneManager.LoadScene(FirstLevel);
    }

    
}
