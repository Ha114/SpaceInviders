using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SaveSystem : MonoBehaviour
{
    #region singleton
    public static SaveSystem instance;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
    #endregion

    public List<int> list = new List< int>();
    public string s_list = "";


    void Start()
    {
        //set result of current play
        if (SceneManager.GetActiveScene().buildIndex == 2 || SceneManager.GetActiveScene().buildIndex == 3)
        {
            Text text = GameObject.Find("Points").GetComponent<Text>();
            GetListOfPoints();
            text.text = "Points: " + list.LastOrDefault().ToString();
        }
        GetListOfPoints();
    }


    //save data
    public void SetString(string KeyName, string Value)
    {
        PlayerPrefs.SetString(KeyName, Value);
    }
    //get data
    public string GetString(string KeyName)
    {
        return PlayerPrefs.GetString(KeyName);
    }
    //get in list form
    void GetListOfPoints()
    {
        list.Clear();
        string[] wordScore = GetString("Results").Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
        for (int i = 0; i < wordScore.Length; i++)
        {
            list.Add(Int32.Parse(wordScore[i], CultureInfo.InvariantCulture));
        }
    }
    public void SaveLastOne(int score)
    {
        SetString("Results", GetString("Results") + score + ",");
        GetListOfPoints();
    }

    //Show and update statistics
    public void ShowList(Text text)
    {
        list.Sort();
        list.Reverse();
        list.RemoveRange(10, list.Count - 10);

        string s = "";
        for (int i = 0, j = 1; i < 10; i++)
        {
            s += "<b>" + j++ + ".</b> " + list[i] + "\n";
        }
        text.text = s;
        SaveNewList();
    }
    //create string
    void SaveNewList()
    {
        s_list = "";
        for (int i = 0; i < 10; i++)
        {
            s_list += list[i] + ",";
        }
        SetString("Results", s_list);
    }
}
