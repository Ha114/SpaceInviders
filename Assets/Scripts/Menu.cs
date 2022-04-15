using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{


    Scene sceneLoaded;
    public string res;
    public int p;

    private void Start()
    {
        sceneLoaded = SceneManager.GetActiveScene();
    }

    public void ShowStatistic(Text text)
    {
        SaveSystem.instance.ShowList(text);
    }

    public void UpdateBulletButton()
    {
        GameManager.instance.UpdateBullet();
    }

    public void OpenScene(int sceneANumber)
    {
        SceneManager.LoadScene(sceneANumber);
    }

    public void ChangeState(GameObject g)
    {
        g.SetActive(!g.activeSelf);
    }
    public void ExitGame()
    {
        Application.Quit();
    }
}
