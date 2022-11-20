using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using TMPro;

public class RoomsManager : MonoBehaviourPunCallbacks
{
    public TMP_InputField createInput;
    public TMP_InputField joinInput;
    public TMP_InputField username;
    public int skin;

    public void CreateRoom()
    {
        if (username.text == "")
            return;
        PhotonNetwork.CreateRoom(createInput.text);
    }

    public void JoinRoom()
    {
        if (username.text == "")
            return;
        PhotonNetwork.JoinRoom(joinInput.text);
    }

    public override void OnJoinedRoom()
    {
        PlayerPrefs.SetInt("skin", skin);
        PlayerPrefs.SetString("user",username.text);

        PhotonNetwork.LoadLevel("Game");
    }

    public void skinSelector(int skinNum)
    {
        skin = skinNum;
    }
}
