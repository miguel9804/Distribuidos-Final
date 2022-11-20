using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Barrier : MonoBehaviourPun
{

    public GameObject barPrefab;
    public Transform spawnBar;

    private void Update()
    {
        if(photonView.IsMine && Input.GetKeyDown(KeyCode.Space))
        {
            photonView.RPC("SetBarrier", RpcTarget.AllBuffered);
        }
    }

    [PunRPC]
    void SetBarrier()
    {
        Instantiate(barPrefab, spawnBar);
    }
    
}
