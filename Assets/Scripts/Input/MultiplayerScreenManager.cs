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

public class MultiplayerScreenManager : MonoBehaviour
{
    [Header("Referances")]
    [SerializeField] private ControllerManager controllerManager;

    [Header("UI Elements")]
    [SerializeField] private GameObject playerCountObj;
    [SerializeField] private TextMeshProUGUI playerCountText;

    [Header("Player Data")]
    [SerializeField] public List<PlayerData> playerData;

    void Start()
    {
        if (playerCountText != null)
            playerCountText.text = $"Players: {controllerManager.activePlayerAmount}";
        UpdatePlayerAmount();
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
        for (int i = 0; i < playerData.Count; i++)
        {
            if (playerData[i].playerObj == null) return;
            
            if (playerData[i].isActive)
            {
                //Activate Player Object
                playerData[i].playerObj.SetActive(true);
                playerData[i].currentPlayerOrder = playerOrder;
                playerOrder++;
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
            playerCountText.text = $"Players: {controllerManager.activePlayerAmount}";
        }
            
        Debug.Log("0 Players active");
    }

    void PlayerAmount_1()
    {
        for (int i = 0; i < playerData.Count; i++)
        {
            //Change Camera
            if (playerData[i].playerObj == null) return;
            if (playerData[i].isActive) 
                if (playerData[i].currentPlayerOrder == 0)
                    playerData[i].playerCam.rect = new Rect(0.0f, 0.0f, 1.0f, 1.0f);
                else Debug.LogError("Invalid player order in PlayerAmount_1");
        }

        //Update Text
        if (playerCountText != null)
        {
            playerCountObj.transform.localPosition = new Vector2(660, 440);
            playerCountObj.transform.localScale = new Vector2(3, 3);
            playerCountText.text = $"Players: {controllerManager.activePlayerAmount}";
        }
        Debug.Log("1 Player active");
    }

    void PlayerAmount_2()
    {
        for (int i = 0; i < playerData.Count; i++)
        {
            //Change Camera
            if (playerData[i].playerObj == null) return;
            if (playerData[i].isActive) 
                if (playerData[i].currentPlayerOrder == 0)
                    playerData[i].playerCam.rect = new Rect(0.0f, 0.25f, 0.5f, 0.5f);
                else if (playerData[i].currentPlayerOrder == 1)
                    playerData[i].playerCam.rect = new Rect(0.5f, 0.25f, 0.5f, 0.5f);
                else Debug.LogError("Invalid player order in PlayerAmount_2");
        }

        //Update Text
        if (playerCountText != null)
        {
            playerCountObj.transform.localPosition = new Vector2(0, 450);
            playerCountObj.transform.localScale = new Vector2(3, 3);
            playerCountText.text = $"Players: {controllerManager.activePlayerAmount}";
        }
        Debug.Log("2 Players active");
    }

    void PlayerAmount_3()
    {
        for (int i = 0; i < playerData.Count; i++)
        {
            //Change Camera
            if (playerData[i].playerObj == null) return;
            if (playerData[i].isActive)
                if (playerData[i].currentPlayerOrder == 0)
                    playerData[i].playerCam.rect = new Rect(0.0f, 0.5f, 0.5f, 0.5f);
                else if (playerData[i].currentPlayerOrder == 1)
                    playerData[i].playerCam.rect = new Rect(0.5f, 0.5f, 0.5f, 0.5f);
                else if (playerData[i].currentPlayerOrder == 2)
                    playerData[i].playerCam.rect = new Rect(0.25f, 0.0f, 0.5f, 0.5f);
                else Debug.LogError("Invalid player order in PlayerAmount_3");
        }
        
        //Update Text
        if (playerCountText != null)
        {
            playerCountObj.transform.localPosition = new Vector2(-725, -60);
            playerCountObj.transform.localScale = new Vector2(2, 2);
            playerCountText.text = $"Players: {controllerManager.activePlayerAmount}";
        }
        Debug.Log("3 Players active");
    }

    void PlayerAmount_4()
    {
        for (int i = 0; i < playerData.Count; i++)
        {
            //Change Camera
            if (playerData[i].playerObj == null) return;
            if (playerData[i].isActive)
                if (playerData[i].currentPlayerOrder == 0)
                    playerData[i].playerCam.rect = new Rect(0.0f, 0.5f, 0.5f, 0.5f);
                else if (playerData[i].currentPlayerOrder == 1)
                    playerData[i].playerCam.rect = new Rect(0.5f, 0.5f, 0.5f, 0.5f);
                else if (playerData[i].currentPlayerOrder == 2)
                    playerData[i].playerCam.rect = new Rect(0.0f, 0.0f, 0.5f, 0.5f);
                else if (playerData[i].currentPlayerOrder == 3)
                    playerData[i].playerCam.rect = new Rect(0.5f, 0.0f, 0.5f, 0.5f);
                else Debug.LogError("Invalid player order in PlayerAmount_4");
        }

        //Update Text
        if (playerCountText != null)
        {
            playerCountObj.transform.localPosition = new Vector2(-150f, 500f);
            playerCountObj.transform.localScale = new Vector2(1.5f, 1.5f);
            playerCountText.text = $"Players: {controllerManager.activePlayerAmount}";
        }
        Debug.Log("4 Players active");
    }
}