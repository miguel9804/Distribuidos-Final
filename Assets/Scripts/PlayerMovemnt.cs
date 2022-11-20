using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Photon.Pun;

public class PlayerMovemnt : MonoBehaviour
{
    CharacterController chController;
    public float movementSpeed;
    float ActualSpeed;
    //public GameObject pers;
    private float horiInput, vertInput;
    private Vector3 forwardMovement, rightMovement, Move;
    //[SerializeField] private Mesh[] characterMeshes;
    //private MeshFilter _meshFilter;
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI pointsText;
    private int points = 100;
    public PhotonView view;
    //private int mySkin;

    private void Start()
    {
        //mySkin = PlayerPrefs.GetInt("skin");
        view = GetComponent<PhotonView>();
        //_meshFilter = GetComponent<MeshFilter>();
        chController = GetComponent<CharacterController>();
        //_meshFilter.mesh = characterMeshes[PlayerPrefs.GetInt("skin")];
        //CustomPlayer("Juanito", 0);
        /*if (view.IsMine)
        {
            view.RPC("PickCharacterRPC", RpcTarget.AllBuffered, mySkin);
        }*/
    }

    /*[PunRPC]
    void PickCharacterRPC(int thisSkin) // string name, int skin
    {
        //nameText.text = PlayerPrefs.GetString("user");
        _meshFilter.mesh = characterMeshes[thisSkin];
    }*/

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Coin"))
        {
            Destroy(other.gameObject);
            points += 100;
            pointsText.text = points.ToString();
        }
    }

    private void Update()
    {
        if (view.IsMine)
        {
            PlayerMovement();
            //rotateCharacter();

            if(Input.GetKeyDown(KeyCode.LeftShift))
            {
                movementSpeed = movementSpeed * 2;
            }

            if(Input.GetKeyUp(KeyCode.LeftShift))
            {
                movementSpeed = movementSpeed / 2;

            }
        }
    }

    void PlayerMovement()
    {
        horiInput = Input.GetAxisRaw("Horizontal") * movementSpeed * Time.deltaTime;
        vertInput = Input.GetAxisRaw("Vertical") * movementSpeed * Time.deltaTime;
        Move = new Vector3(-vertInput,0,horiInput);
        chController.Move(Move);
    }
    

}
