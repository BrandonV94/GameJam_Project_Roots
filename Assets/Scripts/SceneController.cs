using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    public void LoadInstructionsScene()
    {
        SceneManager.LoadScene("Instructions Scene");
    }
    public void LoadGamePlayScene()
    {
        SceneManager.LoadScene("Gameplay");
    }
    public void LoadMenuScene()
    {
        SceneManager.LoadScene("Menu Scene");
    }
    public void LoadSandbox()
    {
        SceneManager.LoadScene("Sandbox");
    }
    public void LoadLevelScene()
    {
        SceneManager.LoadScene("Level Scene");
    }
}
