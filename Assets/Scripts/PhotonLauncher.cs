using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using System;
using Photon.Realtime;
using TMPro;
using System.Linq;

public class PhotonLauncher : MonoBehaviourPunCallbacks {
  public static PhotonLauncher Instance;

   PhotonView PV;
  [SerializeField]
  TMP_InputField roomNameInput;
  [SerializeField]
  TMP_Text roomName;
  [SerializeField]
  Transform roomListContent;
  [SerializeField]
  GameObject RoomListItemPrefab;
  [SerializeField]
  public Transform team1PlayerListContent;
  [SerializeField]
  public Transform team2PlayerListContent;
  [SerializeField]
  GameObject PlayerListItemPrefab;
  [SerializeField]
  GameObject startButton;

  private void Awake() {
        
        if (Instance)
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject); 
        Instance = this; }

  void Start() {
    PhotonNetwork.GameVersion = "0.0.1";
    PhotonNetwork.ConnectUsingSettings();
  }

  public override void OnConnectedToMaster() {
    Debug.Log("Connected to Master");
    PhotonNetwork.JoinLobby();
    PhotonNetwork.AutomaticallySyncScene = true;
  }

  public override void OnJoinedLobby() {
    Debug.Log("Joined Lobby");
    PhotonNetwork.NickName =
        "Player " + UnityEngine.Random.Range(0, 1000).ToString("0000");
  }

  public void CreateRoom() {
    if (!PhotonNetwork.IsConnected)
      return;

    if (string.IsNullOrEmpty(roomNameInput.text)) {
      return;
    }
    PhotonNetwork.CreateRoom(roomNameInput.text);
  }

  public override void OnJoinedRoom() {
    roomName.text = PhotonNetwork.CurrentRoom.Name;
    MenuManager.Instance.OpenMenu(MenuManager.Instance.menus[3]);

    Player[] players = PhotonNetwork.PlayerList;
    int playerCount = players.Length;
    foreach(Transform t in team1PlayerListContent)
        {
            Destroy(t.gameObject);
        }
    for(int i = 0; i < playerCount; i++)
        {
            if(playerCount == 1)
            {
                Instantiate(PlayerListItemPrefab, team1PlayerListContent)
            .GetComponent<PlayerListItem>()
            .SetUp(players[i], 1);
            }
            else if(playerCount == 2)
            {
                Instantiate(PlayerListItemPrefab, team1PlayerListContent)
            .GetComponent<PlayerListItem>()
            .SetUp(players[i], 2);
            }
            if (playerCount == 3)
            {
                Instantiate(PlayerListItemPrefab, team1PlayerListContent)
            .GetComponent<PlayerListItem>()
            .SetUp(players[i], 3);
            }
            if (playerCount == 4)
            {
                Instantiate(PlayerListItemPrefab, team1PlayerListContent)
            .GetComponent<PlayerListItem>()
            .SetUp(players[i], 4);
            }
            else { }
        }


        /*
    foreach (Transform t in team1PlayerListContent) {
      Destroy(t.gameObject);
    }
    foreach (Transform t in team2PlayerListContent) {
      Destroy(t.gameObject);
    }
    for (int i = 0; i < players.Count(); i++) {
      if (team1PlayerListContent.childCount < 2) {
        Instantiate(PlayerListItemPrefab, team1PlayerListContent)
            .GetComponent<PlayerListItem>()
            .SetUp(players[i], 1);
      } else  if (team2PlayerListContent.childCount < 2){
        Instantiate(PlayerListItemPrefab, team2PlayerListContent)
            .GetComponent<PlayerListItem>()
            .SetUp(players[i], 2);
      }
    }
        */
    startButton.SetActive(PhotonNetwork.IsMasterClient);
  }
    /*
  public void SwithToOne() {
    if (team1PlayerListContent.childCount < 2) {
      foreach (Transform t in team2PlayerListContent) {
        if (t.GetComponent<PlayerListItem>().player.NickName.Equals(PhotonNetwork.LocalPlayer.NickName)) {
                    Instantiate(PlayerListItemPrefab, team1PlayerListContent)
              .GetComponent<PlayerListItem>()
              .SetUp(t.GetComponent<PlayerListItem>().player, 1);
        }
      }
    }
  }
  public void SwithToTwo() {
    if (team2PlayerListContent.childCount < 2) {
      foreach (Transform t in team1PlayerListContent) {
        print("AKSJDLAJSLKDJAKSJDKA");
        if (t.GetComponent<PlayerListItem>().player.NickName.Equals(PhotonNetwork.LocalPlayer.NickName)) {
                    Instantiate(PlayerListItemPrefab, team2PlayerListContent)
            .GetComponent<PlayerListItem>()
            .SetUp(t.GetComponent<PlayerListItem>().player, 2);
        }
      }
    }
  }
    */
  public override void OnMasterClientSwitched(Player newMasterClient) {
    startButton.SetActive(PhotonNetwork.IsMasterClient);
  }

  public override void OnCreateRoomFailed(short returnCode, string message) {
    Debug.Log("Create room failed");
  }

  public void LeaveRoom() {
        PhotonNetwork.LeaveRoom();
        MenuManager.Instance.OpenMenu(MenuManager.Instance.menus[0]);
  }

  public void JoinRoom(RoomInfo info) { PhotonNetwork.JoinRoom(info.Name); }

  public override void OnLeftRoom() {}

  public override void OnRoomListUpdate(List<RoomInfo> roomList) {
    foreach (Transform t in roomListContent) {
      Destroy(t.gameObject);
    }
    for (int i = 0; i < roomList.Count; i++) {
      if (roomList[i].RemovedFromList)
        continue;
      Instantiate(RoomListItemPrefab, roomListContent)
        .GetComponent<RoomListItem>()
        .SetUp(roomList[i]);
    }
  }

  public void OnClickBack() { PhotonNetwork.LoadLevel(0); }

  
  public override void OnPlayerEnteredRoom(Player newPlayer) {
    Instantiate(PlayerListItemPrefab, team1PlayerListContent)
      .GetComponent<PlayerListItem>()
      .SetUp(newPlayer, 2);
  }
  

  public void StartGame() {
        foreach (Transform t in roomListContent)
        {
            Destroy(t.gameObject);
        }
        PhotonNetwork.LoadLevel(2); }
}
