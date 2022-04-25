using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public string GameStartScene = "BossScene";

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            LoadScene();
        }
        else if (Input.GetMouseButtonDown(1))
        {
            LoadSettings();
        }
    }

    private void LoadScene()
    {
        SceneManager.LoadScene(GameStartScene);
    }

    private void LoadSettings()
    {

    }
}
