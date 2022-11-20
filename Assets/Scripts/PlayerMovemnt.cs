using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Photon.Pun;
using UnityEngine.SceneManagement;

public class PlayerMovemnt : MonoBehaviourPunCallbacks
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
    public GameObject coins;
    public GameObject win;
    public GameObject lose;
    private int points = 0;
    public PhotonView view;
    private float time = 5f;
    private bool terminado = false;
    //private int mySkin;

    private void Start()
    {
        //mySkin = PlayerPrefs.GetInt("skin");
        view = GetComponent<PhotonView>();
        //_meshFilter = GetComponent<MeshFilter>();
        chController = GetComponent<CharacterController>();
        if(view.IsMine)
        {
            view.RPC("SetCoin", RpcTarget.All);
        }

    }

    /*[PunRPC]
    void PickCharacterRPC(int thisSkin) // string name, int skin
    {
        //nameText.text = PlayerPrefs.GetString("user");
        _meshFilter.mesh = characterMeshes[thisSkin];
    }*/

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Coin") && view.IsMine)
        {
            Destroy(other.gameObject);
            points += 100;
            pointsText.text = points.ToString();
        }
    }
    [PunRPC]
    void SetCoin()
    {
        coins.SetActive(true);
    }

    [PunRPC]
    void Win()
    {
        terminado = true;
        movementSpeed=0f;

        //Time.timeScale = 0f;
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
            if (points >= 600)
            {
                view.RPC("Win", RpcTarget.All); 
            }
        }
        if (terminado == true)
        {
            if(view.IsMine)
            {
                if (points >= 600)
                {
                    win.SetActive(true);
                    time -= 1 * Time.deltaTime;
                    if (time <= 0)
                    {
                        PhotonNetwork.JoinLobby();
                        SceneManager.LoadScene("Lobby");
                    }
                }
                else
                {
                    lose.SetActive(true);
                    time -= 1 * Time.deltaTime;
                    if (time <= 0)
                    {
                        PhotonNetwork.JoinLobby();
                        SceneManager.LoadScene("Lobby");
                    }
                }
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
