using Unity.VisualScripting;
using UnityEngine;
using TMPro;
using System;
using System.Collections.Generic;

[Serializable] public class PlayerData
{
    [SerializeField] public int ID;
    [SerializeField] public GameObject playerDisconnectedObj;
    [SerializeField] public GameObject playerObj;
    [SerializeField] public GameObject playerChar;
    [SerializeField] public GameObject playerSpawn;
    [SerializeField] public Camera playerCam;
    [SerializeField] public bool isActive = false;
    internal int currentPlayerOrder;
}

[Serializable] public class UIFliper
{
    [SerializeField] public bool isFlipped = false;
    [SerializeField] public List<Transform> flipObj;
}

public class MultiplayerScreenManager : MonoBehaviour
{
    [Header("Referances")]
    [SerializeField] private ControllerManager controllerManager;

    [Header("UI Elements")]
    [SerializeField] private Camera miniMap;
    [SerializeField] private GameObject movingObject;
    [SerializeField] private TextMeshProUGUI playerCountText;
    [SerializeField] private String[] playerCountString = {"Players: 0", "Players: 1", "Players: 2", "Players: 3", "Players: 4"};

    [Header("UI Settings")]
    [SerializeField] private List<UIFliper> flipUiX;
    [SerializeField] internal List<UIFliper> flipUiY;

    [Header("Player Data")]
    [SerializeField] public List<PlayerData> playerData;
    internal int playerAmount = 0;

    void Start()
    {
        for (int i = 0; i < playerData.Count; i++)
        {
            playerData[i].currentPlayerOrder = -1;
            playerData[i].playerObj.SetActive(false);
            if (playerData[i].playerDisconnectedObj != null)
                playerData[i].playerDisconnectedObj.SetActive(true);
            if (playerData[i].playerChar != null && playerData[i].playerSpawn != null)
                playerData[i].playerChar.transform.position = playerData[i].playerSpawn.transform.position;
        }
        playerAmount = controllerManager.activePlayerAmount;
        if (playerCountText != null)
            playerCountText.text = playerCountString[playerAmount];
        UpdatePlayerAmount();
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.U))
        {
            UpdatePlayerAmount();
            Debug.Log("Key U pressed in MultiplayerScreenManager"); //Temp test log
        }
    }

    public void UpdatePlayerAmount()
    {
        TestForActivePlayers();
        playerAmount = controllerManager.activePlayerAmount;
        if (playerAmount == 0)
            PlayerAmount_0();
        if (playerAmount == 1)
            PlayerAmount_1();
        if (playerAmount == 2)
            PlayerAmount_2();
        if (playerAmount == 3)
            PlayerAmount_3();
        if (playerAmount == 4)
            PlayerAmount_4();
    }

    private void TestForActivePlayers()
    {
        int playerOrder = 0;
        if (miniMap != null)
            miniMap.gameObject.SetActive(true);
        
        for (int i = 0; i < playerData.Count; i++)
        {
            if (playerData[i].playerObj == null) return;
            
            if (playerData[i].isActive)
            {
                //Activate Player Object
                playerData[i].playerObj.SetActive(true);
                if (playerData[i].playerDisconnectedObj != null)
                    playerData[i].playerDisconnectedObj.SetActive(false);
                
                playerData[i].currentPlayerOrder = playerOrder;
                playerOrder++;

                //Flip UI X
                if (flipUiX[playerData[i].ID].isFlipped)
                    FlipUI(true, playerData[i].ID);
                //Flip UI Y
                if (flipUiY[playerData[i].ID].isFlipped)
                    FlipUI(false, playerData[i].ID);
                
            } else
            {
                //Deactivate Player Object
                playerData[i].currentPlayerOrder = -1;
                playerData[i].playerObj.SetActive(false);
                if (playerData[i].playerDisconnectedObj != null)
                    playerData[i].playerDisconnectedObj.SetActive(true);
                if (playerData[i].playerChar != null && playerData[i].playerSpawn != null)
                    playerData[i].playerChar.transform.position = playerData[i].playerSpawn.transform.position;
            }
        }
    }

    void PlayerAmount_0()
    {
        if (miniMap != null)
        {
            miniMap.rect = new Rect( (1 - 0.8f * Screen.height / Screen.width)/2 , (1f - 0.8f) / 2, 0.8f * Screen.height / Screen.width, 0.8f);
        }

        //Player Visibility
        for (int i = 0; i < playerData.Count; i++)
        {
            if (playerData[i].playerObj == null || playerData[i].playerCam == null) return;
            playerData[i].isActive = false;
            playerData[i].playerObj.SetActive(false);
            if (playerData[i].playerDisconnectedObj != null)
                playerData[i].playerDisconnectedObj.SetActive(true);
            playerData[i].playerChar.transform.position = playerData[i].playerSpawn.transform.position;
        }
        
        //Update Text
        if (playerCountText != null)
        {
            movingObject.transform.localPosition = new Vector2(-625,450);
            movingObject.transform.localScale = new Vector2(2,2);
            playerCountText.text = playerCountString[0];
        }
        //Debug.Log("0 Players active");
    }

    void PlayerAmount_1()
    {
        for (int i = 0; i < playerData.Count; i++)
        {
            
            if (playerData[i].playerObj == null || playerData[i].playerCam == null) return;
            
            if (playerData[i].isActive)
            {
                
                //Change Camera
                if (playerData[i].currentPlayerOrder == 0)
                    playerData[i].playerCam.rect = new Rect(0.0f, 0.0f, 1.0f, 1.0f);
                else Debug.LogError("Invalid player order in PlayerAmount_1");
            }
                
        }

        //Update MiniMap
        if (miniMap != null)
        {
            miniMap.rect = new Rect(0.814f, 0.67f, 0.3f * Screen.height / Screen.width, 0.3f);
        }

        //Update Text
        if (playerCountText != null)
        {
            movingObject.transform.localPosition = new Vector2(800, 140);
            movingObject.transform.localScale = new Vector2(1.25f, 1.25f);
            playerCountText.text = playerCountString[1];
        }
        //Debug.Log("1 Player active");
    }

    void PlayerAmount_2()
    {
        for (int i = 0; i < playerData.Count; i++)
        {
            
            if (playerData[i].playerObj == null || playerData[i].playerCam == null) return;
            if (playerData[i].isActive)
            {
                //Change Camera
                if (playerData[i].currentPlayerOrder == 0)
                    playerData[i].playerCam.rect = new Rect(0.0f, 0.20f, 0.5f, 0.5f);
                else if (playerData[i].currentPlayerOrder == 1)
                    playerData[i].playerCam.rect = new Rect(0.5f, 0.20f, 0.5f, 0.5f);
                else Debug.LogError("Invalid player order in PlayerAmount_2");

                //Flip UI
                if (playerData[i].currentPlayerOrder == 1)
                    FlipUI(true, playerData[i].ID);
            }
                
        }

        //Update MiniMap
        if (miniMap != null)
        {
            miniMap.rect = new Rect(0.5f - (0.3f * Screen.height / Screen.width)/2, 0.69f, 0.3f * Screen.height / Screen.width, 0.3f);
        }

        //Update Text
        if (playerCountText != null)
        {
            movingObject.transform.localPosition = new Vector2(-500, 480);
            movingObject.transform.localScale = new Vector2(1.25f, 1.25f);
            playerCountText.text = playerCountString[2];
        }
        //Debug.Log("2 Players active");
    }

    void PlayerAmount_3()
    {
        for (int i = 0; i < playerData.Count; i++)
        {
            
            if (playerData[i].playerObj == null || playerData[i].playerCam == null) return;
            if (playerData[i].isActive)
            {
                //Change Camera
                if (playerData[i].currentPlayerOrder == 0)
                    playerData[i].playerCam.rect = new Rect(0.0f, 0.5f, 0.5f, 0.5f);
                else if (playerData[i].currentPlayerOrder == 1)
                    playerData[i].playerCam.rect = new Rect(0.5f, 0.5f, 0.5f, 0.5f);
                else if (playerData[i].currentPlayerOrder == 2)
                    playerData[i].playerCam.rect = new Rect(0.25f, 0.0f, 0.5f, 0.5f);
                else Debug.LogError("Invalid player order in PlayerAmount_3");

                //Flip UI
                if (playerData[i].currentPlayerOrder == 1)
                    FlipUI(true, playerData[i].ID);
                if (playerData[i].currentPlayerOrder == 0 || playerData[i].currentPlayerOrder == 1)
                    FlipUI(false, playerData[i].ID);
            }
                
        }

        //Update MiniMap
        if (miniMap != null)
        {
            miniMap.rect = new Rect(0.7925f, 0.125f, 0.3f * Screen.height / Screen.width, 0.3f);
        }
        
        //Update Text
        if (playerCountText != null)
        {
            movingObject.transform.localPosition = new Vector2(-700, -60);
            movingObject.transform.localScale = new Vector2(1.5f, 1.5f);
            playerCountText.text = playerCountString[3];
        }
        //Debug.Log("3 Players active");
    }

    void PlayerAmount_4()
    {
        for (int i = 0; i < playerData.Count; i++)
        {
            if (playerData[i].playerObj == null || playerData[i].playerCam == null) return;
            if (playerData[i].isActive)
            {
                //Change Camera
                if (playerData[i].currentPlayerOrder == 0)
                    playerData[i].playerCam.rect = new Rect(0.0f, 0.5f, 0.5f, 0.5f);
                else if (playerData[i].currentPlayerOrder == 1)
                    playerData[i].playerCam.rect = new Rect(0.5f, 0.5f, 0.5f, 0.5f);
                else if (playerData[i].currentPlayerOrder == 2)
                    playerData[i].playerCam.rect = new Rect(0.0f, 0.0f, 0.5f, 0.5f);
                else if (playerData[i].currentPlayerOrder == 3)
                    playerData[i].playerCam.rect = new Rect(0.5f, 0.0f, 0.5f, 0.5f);
                else Debug.LogError("Invalid player order in PlayerAmount_4");

                //Flip UI
                if (playerData[i].currentPlayerOrder == 3 || playerData[i].currentPlayerOrder == 1)
                    FlipUI(true, playerData[i].ID);
                if (playerData[i].currentPlayerOrder == 0 || playerData[i].currentPlayerOrder == 1)
                    FlipUI(false, playerData[i].ID);
            }
                
        }

        //Update MiniMap
        if (miniMap != null)
        {
            miniMap.rect = new Rect(0.5f - (0.3f * Screen.height / Screen.width)/2, 0.35f, 0.3f * Screen.height / Screen.width, 0.3f);
        }

        //Update Text
        if (playerCountText != null)
        {
            movingObject.transform.localPosition = new Vector2(0f, -200f);
            movingObject.transform.localScale = new Vector2(1, 1);
            playerCountText.text = playerCountString[4];
        }
        //Debug.Log("4 Players active");
    }

    private void FlipUI(bool flipdirectionX, int playerID)
    {
        if (flipdirectionX)
        {
            if (!flipUiX[playerID].isFlipped)
                flipUiX[playerID].isFlipped = true;
            else
                flipUiX[playerID].isFlipped = false;

            if (flipUiX[playerID].flipObj != null)
            {
                for (int o = 0; o < flipUiX[playerID].flipObj.Count; o++)
                {
                    if (flipUiX[playerID].flipObj[o] != null)
                    {
                        Vector2 flipObject = flipUiX[playerID].flipObj[o].localScale;
                        flipObject.x *= -1;
                        flipUiX[playerID].flipObj[o].localScale = flipObject;
                    }
                }
            }
        } 
        else 
        {
            if (!flipUiY[playerID].isFlipped)
                flipUiY[playerID].isFlipped = true;
            else
                flipUiY[playerID].isFlipped = false;
            

            if (flipUiY[playerID].flipObj != null)
            {
                for (int o = 0; o < flipUiY[playerID].flipObj.Count; o++)
                {
                    if (flipUiY[playerID].flipObj[o] != null)
                    {
                        Vector2 flipObject = flipUiY[playerID].flipObj[o].localPosition;
                        flipObject.y *= -1;
                        flipUiY[playerID].flipObj[o].localPosition = flipObject;
                        /*/
                        Vector2 flipObject = flipUiY[playerID].flipObj[o].localScale;
                        flipObject.y *= -1;
                        flipUiY[playerID].flipObj[o].localScale = flipObject;
                        /*/
                    }
                }
            }
        }
    }
}