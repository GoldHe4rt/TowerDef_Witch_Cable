using Unity.VisualScripting;
using UnityEngine;
using TMPro;
using System;
using System.Collections.Generic;

[Serializable] public class PlayerData
{
    [SerializeField] public int ID;
    [SerializeField] public GameObject playerObj;
    [SerializeField] public GameObject playerChar;
    [SerializeField] public GameObject playerSpawn;
    [SerializeField] public Camera playerCam;
    [SerializeField] public bool isActive = false;
    public int currentPlayerOrder;
}

[Serializable] public class UIFliper
{
    [SerializeField] public int playerID;
    [SerializeField] public List<Transform> flipObj;
}

public class MultiplayerScreenManager : MonoBehaviour
{
    [Header("Referances")]
    [SerializeField] private ControllerManager controllerManager;

    [Header("UI Elements")]
    [SerializeField] private Camera miniMap;
    [SerializeField] private GameObject playerCountObj;
    [SerializeField] private TextMeshProUGUI playerCountText;
    [SerializeField] private String[] playerCountString = {"Players: 0", "Players: 1", "Players: 2", "Players: 3", "Players: 4"};

    [Header("UI Settings")]
    [SerializeField] private List<UIFliper> flipUI;

    [Header("Player Data")]
    [SerializeField] public List<PlayerData> playerData;

    void Start()
    {
        if (playerCountText != null)
            playerCountText.text = playerCountString[controllerManager.activePlayerAmount];
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
        if (controllerManager.activePlayerAmount == 0)
            PlayerAmount_0();
        if (controllerManager.activePlayerAmount == 1)
            PlayerAmount_1();
        if (controllerManager.activePlayerAmount == 2)
            PlayerAmount_2();
        if (controllerManager.activePlayerAmount == 3)
            PlayerAmount_3();
        if (controllerManager.activePlayerAmount == 4)
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
                playerData[i].currentPlayerOrder = playerOrder;
                playerOrder++;

                //Flip UI
                for (int f = 0; f < flipUI.Count; f++)
                {
                    if (playerData[flipUI[f].playerID].isActive)
                        if (flipUI[f].flipObj != null)
                            for (int o = 0; o < flipUI[f].flipObj.Count; o++)
                            {
                                if (flipUI[f].flipObj[o].localScale.x < 0)
                                {
                                    Vector2 flipObject = flipUI[f].flipObj[o].localScale;
                                    flipObject.x *= -1;
                                    flipUI[f].flipObj[o].localScale = flipObject;
                                }
                            }
                }
            } else
            {
                //Deactivate Player Object
                playerData[i].currentPlayerOrder = -1;
                playerData[i].playerObj.SetActive(false);
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
            if (playerData[i].playerObj == null) return;
            playerData[i].isActive = false;
            playerData[i].playerObj.SetActive(false);
            playerData[i].playerChar.transform.position = playerData[i].playerSpawn.transform.position;
        }
        
        //Update Text
        if (playerCountText != null)
        {
            playerCountObj.transform.localPosition = new Vector2(0,0);
            playerCountObj.transform.localScale = new Vector2(5,5);
            playerCountText.text = playerCountString[0];
        }
        //Debug.Log("0 Players active");
    }

    void PlayerAmount_1()
    {
        for (int i = 0; i < playerData.Count; i++)
        {
            
            if (playerData[i].playerObj == null) return;
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
            playerCountObj.transform.localPosition = new Vector2(660, 440);
            playerCountObj.transform.localScale = new Vector2(3, 3);
            playerCountText.text = playerCountString[1];
        }
        //Debug.Log("1 Player active");
    }

    void PlayerAmount_2()
    {
        for (int i = 0; i < playerData.Count; i++)
        {
            
            if (playerData[i].playerObj == null) return;
            if (playerData[i].isActive)
            {
                //Change Camera
                if (playerData[i].currentPlayerOrder == 0)
                    playerData[i].playerCam.rect = new Rect(0.0f, 0.25f, 0.5f, 0.5f);
                else if (playerData[i].currentPlayerOrder == 1)
                    playerData[i].playerCam.rect = new Rect(0.5f, 0.25f, 0.5f, 0.5f);
                else Debug.LogError("Invalid player order in PlayerAmount_2");

                //Flip UI
                if (playerData[i].currentPlayerOrder == 1)
                    for (int f = 0; f < flipUI.Count; f++)
                    {
                        if (flipUI[f].flipObj != null && playerData[i].ID == flipUI[f].playerID)
                        {
                            for (int o = 0; o < flipUI[f].flipObj.Count; o++)
                            {
                                if (flipUI[f].flipObj[o] != null)
                                {
                                    Vector3 flipObject = flipUI[f].flipObj[o].localScale;
                                    flipObject.x *= -1;
                                    flipUI[f].flipObj[o].localScale = flipObject;
                                }
                            }
                        }
                    }
            }
                
        }

        //Update MiniMap
        if (miniMap != null)
        {
            miniMap.rect = new Rect(0.5f - (0.3f * Screen.height / Screen.width)/2, 0.01f, 0.3f * Screen.height / Screen.width, 0.3f);
        }

        //Update Text
        if (playerCountText != null)
        {
            playerCountObj.transform.localPosition = new Vector2(0, 450);
            playerCountObj.transform.localScale = new Vector2(3, 3);
            playerCountText.text = playerCountString[2];
        }
        //Debug.Log("2 Players active");
    }

    void PlayerAmount_3()
    {
        for (int i = 0; i < playerData.Count; i++)
        {
            
            if (playerData[i].playerObj == null) return;
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
                    for (int f = 0; f < flipUI.Count; f++)
                    {
                        if (flipUI[f].flipObj != null && playerData[i].ID == flipUI[f].playerID)
                        {
                            for (int o = 0; o < flipUI[f].flipObj.Count; o++)
                            {
                                if (flipUI[f].flipObj[o] != null)
                                {
                                    Vector2 flipObject = flipUI[f].flipObj[o].localScale;
                                    flipObject.x *= -1;
                                    flipUI[f].flipObj[o].localScale = flipObject;
                                }
                            }
                        }
                    }
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
            playerCountObj.transform.localPosition = new Vector2(-725, -60);
            playerCountObj.transform.localScale = new Vector2(2, 2);
            playerCountText.text = playerCountString[3];
        }
        //Debug.Log("3 Players active");
    }

    void PlayerAmount_4()
    {
        for (int i = 0; i < playerData.Count; i++)
        {
            if (playerData[i].playerObj == null) return;
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
                if (playerData[i].currentPlayerOrder == 1)
                    for (int f = 0; f < flipUI.Count; f++)
                    {
                        if (flipUI[f].flipObj != null && playerData[i].ID == flipUI[f].playerID)
                        {
                            for (int o = 0; o < flipUI[f].flipObj.Count; o++)
                            {
                                if (flipUI[f].flipObj[o] != null)
                                {
                                    Vector2 flipObject = flipUI[f].flipObj[o].localScale;
                                    flipObject.x *= -1;
                                    flipUI[f].flipObj[o].localScale = flipObject;
                                }
                            }
                        }
                    }
                if (playerData[i].currentPlayerOrder == 3)
                    for (int f = 0; f < flipUI.Count; f++)
                    {
                        if (flipUI[f].flipObj != null && playerData[i].ID == flipUI[f].playerID)
                        {
                            for (int o = 0; o < flipUI[f].flipObj.Count; o++)
                            {
                                if (flipUI[f].flipObj[o] != null)
                                {
                                    Vector2 flipObject = flipUI[f].flipObj[o].localScale;
                                    flipObject.x *= -1;
                                    flipUI[f].flipObj[o].localScale = flipObject;
                                }
                            }
                        }
                    }
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
            playerCountObj.transform.localPosition = new Vector2(-150f, 500f);
            playerCountObj.transform.localScale = new Vector2(1.5f, 1.5f);
            playerCountText.text = playerCountString[4];
        }
        //Debug.Log("4 Players active");
    }
}