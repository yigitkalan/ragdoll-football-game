using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{

    public static PlayerManager Instance;
    public PhotonView PV;
    public GameObject player;
    public int myTeam;

    private void Awake()
    {
        PV = GetComponent<PhotonView>();
        Instance = this;
    }

    void Start()
    {
        if (PV.IsMine)
        {
            Debug.Log("PV:IsMine çalýþtý");
            PV.RPC(nameof(AssignTeam), RpcTarget.All);
            if(myTeam != 0)
            {
                CreatePlayer();
            }
            

            //Torso, Stomach, Hips, Right Shoulder, Left Shoulder, Right Thigh, Left Thigh
        }
        Debug.Log(myTeam);
    }

    void CreatePlayer()
    {
        Transform spawnpoint = RoomManager.Instance.GetSpawnpoint(myTeam);

        if (myTeam == 2 || myTeam == 4)
        {
            player = PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "RagdollBlue"), spawnpoint.position, Quaternion.identity, 0, new object[] { PV.ViewID });
        }
        else
        {
            player = PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "Ragdoll"), spawnpoint.position, Quaternion.identity, 0, new object[] { PV.ViewID });
        }
    }

    public void Respawn()
    {
        PhotonNetwork.Destroy(player);
        CreatePlayer();
    }

    [PunRPC]
    void GetTeam()
    {
        myTeam = RoomManager.Instance.nextPlayerTeam;
        RoomManager.Instance.UpdateTeam();
        PV.RPC(nameof(SentTeam), RpcTarget.OthersBuffered, myTeam);
    }

    [PunRPC]
    void SentTeam(int team)
    {
        myTeam = team;
    }

    [PunRPC]
    void setTeamColor()
    {
        if (PV.IsMine)
        {  
            GameObject[] body = GameObject.FindGameObjectsWithTag("RagdollBody");
        foreach(GameObject i in body)
        {
            i.GetComponent<Renderer>().material.color = Color.blue;
        }

        } 
    }

    [PunRPC]
    private void AssignTeam()
    {
        foreach (Transform t in PhotonLauncher.Instance.team2PlayerListContent)
        {
            if (t.GetComponent<PlayerListItem>().player.NickName.Equals(PhotonNetwork.LocalPlayer.NickName))
            {
                myTeam = 2;
                Debug.Log("çalýþtý + " + t.GetComponent<PlayerListItem>().player.NickName + " ve takýmým : " + myTeam);
                PV.RPC(nameof(SentTeam), RpcTarget.OthersBuffered, myTeam);
            }
        }
        foreach (Transform t in PhotonLauncher.Instance.team1PlayerListContent)
        {
            
            if (t.GetComponent<PlayerListItem>().player.NickName.Equals(PhotonNetwork.LocalPlayer.NickName))
            {
                myTeam = t.GetComponent<PlayerListItem>().team;
                Debug.Log("çalýþtý + " + t.GetComponent<PlayerListItem>().player.NickName + " ve takýmým : "  + myTeam);
                PV.RPC(nameof(SentTeam), RpcTarget.OthersBuffered, myTeam);
            }
        }
        Destroy(GameObject.Find("Canvases"));
    }
    private void Update()
    {
        //if (PV.IsMine)
            //Debug.Log(myTeam + ".takým");
    }
}
