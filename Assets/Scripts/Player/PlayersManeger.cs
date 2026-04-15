using Unity.VisualScripting;
using UnityEngine;
using TMPro;


public class PlayerManegers : MonoBehaviour
{
    [Header("Referances")]
    [SerializeField] private TMP_Dropdown dropdown;

    [Header("Players")]
    [SerializeField] private GameObject playerObj1;
    [SerializeField] private GameObject playerObj2;
    [SerializeField] private GameObject playerObj3;
    [SerializeField] private GameObject playerObj4;

    [Header("Characters")]
    [SerializeField] private GameObject playerChar1;
    [SerializeField] private GameObject playerChar2;
    [SerializeField] private GameObject playerChar3;
    [SerializeField] private GameObject playerChar4;

    [Header("SpawnPoint")]
    [SerializeField] private GameObject playerSpawn1;
    [SerializeField] private GameObject playerSpawn2;
    [SerializeField] private GameObject playerSpawn3;
    [SerializeField] private GameObject playerSpawn4;

    [Header("Cameras")]
    [SerializeField] private Camera playerCam1;
    [SerializeField] private Camera playerCam2;
    [SerializeField] private Camera playerCam3;
    [SerializeField] private Camera playerCam4;

    private bool player1IsActive = true;
    private bool player2IsActive = true;
    private bool player3IsActive = true;
    private bool player4IsActive = true;

    void Start()
    {
        if (playerObj1 == null)
            player1IsActive = false;
        if (playerObj2 == null)
            player2IsActive = false;
        if (playerObj3 == null)
            player3IsActive = false;
        if (playerObj4 == null)
            player4IsActive = false;
        
        PlayerAmount_0();
        UpdatePlayerAmount();
    }

    public void UpdatePlayerAmount()
    {
        if (dropdown.value == 0)
            PlayerAmount_1();
        if (dropdown.value == 1)
            PlayerAmount_2();
        if (dropdown.value == 2)
            PlayerAmount_3();
        if (dropdown.value == 3)
            PlayerAmount_4();
    }

    void PlayerAmount_0()
    {
        //Player Visibility
        
        if (player1IsActive)
        {
            if (playerObj1 == null) return;
            player1IsActive = false;
            playerObj1.SetActive(false);
            playerChar1.transform.position = playerSpawn1.transform.position;
        }
        
        if (player2IsActive)
        {
            if (playerObj2 == null) return;
            player2IsActive = false;
            playerObj2.SetActive(false);
            playerChar2.transform.position = playerSpawn2.transform.position;
        }
        
        if (player3IsActive)
        {
            if (playerObj3 == null) return;
            player3IsActive = false;
            playerObj3.SetActive(false);
            playerChar3.transform.position = playerSpawn3.transform.position;
        }
        
        if (player4IsActive)
        {
            if (playerObj4 == null) return;
            player4IsActive = false;
            playerObj4.SetActive(false);
            playerChar4.transform.position = playerSpawn4.transform.position;
        }
    }

    void PlayerAmount_1()
    {
        if (playerObj1 == null) return;
        //Change Camera
        playerCam1.rect = new Rect(0.0f, 0.0f, 1.0f, 1.0f);

        //Player Visibility
        if (!player1IsActive)
        {
            player1IsActive = true;
            playerObj1.SetActive(true);
            playerChar1.transform.position = playerSpawn1.transform.position;
        }
        if (player2IsActive)
        {
            player2IsActive = false;
            playerObj2.SetActive(false);
        }
        if (player3IsActive)
        {
            player3IsActive = false;
            playerObj3.SetActive(false);
        }
        if (player4IsActive)
        {
            player4IsActive = false;
            playerObj4.SetActive(false);
        }

        Debug.Log("1 Player active");
    }

    void PlayerAmount_2()
    {
        if (playerObj2 == null) return;
        //Change Camera
        playerCam1.rect = new Rect(0.0f, 0.25f, 0.5f, 0.5f);
        playerCam2.rect = new Rect(0.5f, 0.25f, 0.5f, 0.5f);

        //Player Visibility
        if (!player1IsActive)
        {
            player1IsActive = true;
            playerObj1.SetActive(true);
            playerChar1.transform.position = playerSpawn1.transform.position;
        }
        if (!player2IsActive)
        {
            player2IsActive = true;
            playerObj2.SetActive(true);
            playerChar2.transform.position = playerSpawn2.transform.position;
        }
        if (player3IsActive)
        {
            player3IsActive = false;
            playerObj3.SetActive(false);
        }
        if (player4IsActive)
        {
            player4IsActive = false;
            playerObj4.SetActive(false);
        }

        Debug.Log("2 Players active");
    }

    void PlayerAmount_3()
    {
        if (playerObj3 == null) return;
        //Change Camera
        playerCam1.rect = new Rect(0.0f, 0.5f, 0.5f, 0.5f);
        playerCam2.rect = new Rect(0.5f, 0.5f, 0.5f, 0.5f);
        playerCam3.rect = new Rect(0.25f, 0.0f, 0.5f, 0.5f);

        //Player Visibility
        if (!player1IsActive)
        {
            player1IsActive = true;
            playerObj1.SetActive(true);
            playerChar1.transform.position = playerSpawn1.transform.position;
        }
        if (!player2IsActive)
        {
            player2IsActive = true;
            playerObj2.SetActive(true);
            playerChar2.transform.position = playerSpawn2.transform.position;
        }
        if (!player3IsActive)
        {
            player3IsActive = true;
            playerObj3.SetActive(true);
            playerChar3.transform.position = playerSpawn3.transform.position;
        }
        if (player4IsActive)
        {
            player4IsActive = false;
            playerObj4.SetActive(false);
        }

        Debug.Log("3 Players active");
    }

    void PlayerAmount_4()
    {
        if (playerObj4 == null) return;
        //Change Camera
        playerCam1.rect = new Rect(0.0f, 0.5f, 0.5f, 0.5f);
        playerCam2.rect = new Rect(0.5f, 0.5f, 0.5f, 0.5f);
        playerCam3.rect = new Rect(0.0f, 0.0f, 0.5f, 0.5f);
        playerCam4.rect = new Rect(0.5f, 0.0f, 0.5f, 0.5f);

        //Player Visibility
        if (!player1IsActive)
        {
            player1IsActive = true;
            playerObj1.SetActive(true);
            playerChar1.transform.position = playerSpawn1.transform.position;
        }
        if (!player2IsActive)
        {
            player2IsActive = true;
            playerObj2.SetActive(true);
            playerChar2.transform.position = playerSpawn2.transform.position;
        }
        if (!player3IsActive)
        {
            player3IsActive = true;
            playerObj3.SetActive(true);
            playerChar3.transform.position = playerSpawn3.transform.position;
        }
        if (!player4IsActive)
        {
            player4IsActive = true;
            playerObj4.SetActive(true);
            playerChar4.transform.position = playerSpawn4.transform.position;
        }

        Debug.Log("4 Players active");
    }
}