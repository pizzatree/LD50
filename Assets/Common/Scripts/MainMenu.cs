using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    void Start()
    {
        PlayerInputs inputs = new PlayerInputs();
        inputs.UI.Enable();
    }
    public void PlayGame()
    {
        SceneManager.LoadScene("MainLevel");
    }
}
