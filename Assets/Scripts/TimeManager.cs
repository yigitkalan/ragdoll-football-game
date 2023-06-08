using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Photon.Pun;
using UnityEngine.SceneManagement;
using Photon.Realtime;


public class TimeManager : MonoBehaviourPunCallbacks
{
    public float startingTime;
    float timerIncrementValue;
    float currentTime;
    //float timeAfterGoal = 1f; 
    //float centerTime = 3f;
    //string onsetTeam;


    ExitGames.Client.Photon.Hashtable CustomeValue = new ExitGames.Client.Photon.Hashtable();
    [SerializeField] TextMeshProUGUI TimeText;
    void Start()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            currentTime = (float)PhotonNetwork.Time;
            CustomeValue.Add("StartTime", currentTime);
            PhotonNetwork.CurrentRoom.SetCustomProperties(CustomeValue);
        }
        else
        {
            currentTime = float.Parse(PhotonNetwork.CurrentRoom.CustomProperties["StartTime"].ToString());
        }
    }

    void Update()
    {
        if (!PhotonNetwork.IsMasterClient)
        {
            currentTime = float.Parse(PhotonNetwork.CurrentRoom.CustomProperties["StartTime"].ToString());
        }

        timerIncrementValue = startingTime - ((float)PhotonNetwork.Time - currentTime);
        string minutes = Mathf.Floor(timerIncrementValue / 60).ToString("00");
        string seconds = Mathf.Floor(timerIncrementValue % 60).ToString("00");
        TimeText.text = " " + minutes + ":" + seconds;

        if (timerIncrementValue <= 0 && PhotonNetwork.CurrentRoom.IsOpen)
        {
            PhotonNetwork.CurrentRoom.IsOpen = false;
            PhotonNetwork.Disconnect();
            Destroy(GameObject.Find("RoomManager"));
            PhotonNetwork.LoadLevel(4);
            Cursor.lockState = CursorLockMode.None;
        }
    }
}
