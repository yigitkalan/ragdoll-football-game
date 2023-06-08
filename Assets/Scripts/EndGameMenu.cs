using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class EndGameMenu : MonoBehaviour
{
    public static int team1;
    public static int team2;
    public TextMeshProUGUI score;

    // public static Score score;

    public void Start()
    {
        Debug.Log(team1 + "  " + team2);
        score.text = "red " + team1 + "-" + team2 + " blue";
    }
    public void GoMenu()
    {
        SceneManager.LoadScene(0);
    }
}