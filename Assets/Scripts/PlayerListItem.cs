using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using TMPro;

public class PlayerListItem : MonoBehaviourPunCallbacks
{
    [SerializeField] TMP_Text text;
    public Player player;
    public int team;

   public void SetUp(Player _player, int _team)
    {
        player = _player;
        text.text = _player.NickName;
        team = _team;
    }
    /*
    public void SetUp(Player _player)
    {
        player = _player;
        text.text = _player.NickName;
        team = 0;
    }
    */

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        if(player != otherPlayer)
        {
            Destroy(gameObject);
        }
    }

    public override void OnLeftRoom()
    {
        Destroy(gameObject);
    }
}
