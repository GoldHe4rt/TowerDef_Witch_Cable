using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using TMPro;

public class PreviewSystem : MonoBehaviour
{
    [SerializeField] PlayerUI playerUI;
    [SerializeField] private float previewOffset = 0.06f;

    [SerializeField] private GameObject cellIndicator, displayLocation, dismantleIndicator;
    private GameObject previewObject, displayObject;

    [SerializeField] private ObjectDatabaseSO databaseSO;
    [SerializeField] private GameObject selectionIndicatorPrefab;
    private List<GameObject> selectionIndicators = new List<GameObject>();

    [SerializeField] private Material previewMaterialPrefab;
    private Material previewMaterialInstance;

    private Renderer cellIndicatorRenderer;

    private void Start()
    {
        selectionIndicatorPrefab.SetActive(false);
        previewMaterialInstance = new Material(previewMaterialPrefab);
        cellIndicator.SetActive(false);
        cellIndicatorRenderer = cellIndicator.GetComponentInChildren<Renderer>();

        float rectSize = 60f;
        NewPrepareDisplay(dismantleIndicator, Vector2Int.one, "Dismantle", -1, -1);
        foreach (var data in databaseSO.objectData)
        {
            NewPrepareDisplay(data.Prefab, data.Size, data.Name, data.Cost, data.ID);
            rectSize += 60f;
        }
        
        RectTransform rectTransform = displayLocation.GetComponent<RectTransform>();
        // Set both width and height directly
        rectTransform.sizeDelta = new Vector2(rectSize, 75f); 
        UpdateSelectionDisplay(3);
    }

    public void StartShowingPlacementPreview(GameObject prefab, Vector2Int size, int id)
    {
        previewObject = Instantiate(prefab);
        
        PrepareCursor(size);
        PreparePreview(previewObject);

        UpdateSelectionDisplay(id + 1);

        cellIndicator.SetActive(true);
    }

    internal void StartShowingRemovePreview()
    {

        cellIndicator.SetActive(true);
        PrepareCursor(Vector2Int.one);
        ApplyFeedbackToCursor(false);
        UpdateSelectionDisplay(0);
    }

    internal void UpdateSelectionDisplay(int id)
    {
        //Change size of selection indicator based on distance from id
        for (int i = 0; i < selectionIndicators.Count; i++)
        {
            var disabledDisplay = selectionIndicators[i].transform.Find("Front");
            disabledDisplay.gameObject.SetActive(i != id);

            GameObject indicator = selectionIndicators[i];
            float distance = Mathf.Abs(i - id);
            float scale = 10f - (distance * 1f);
            indicator.transform.localScale = new Vector3(scale, scale, scale);
            Debug.Log("Distance: " + distance + " Scale: " + scale + " ID: " + id + " Indicator ID: " + i);
        }
    }

    private void NewPrepareDisplay(GameObject displayPrefab, Vector2Int size, string structureName, float cost, int id)
    {
        GameObject selectionIndicator = Instantiate(selectionIndicatorPrefab, displayLocation.transform);
        selectionIndicator.SetActive(true);
        selectionIndicators.Add(selectionIndicator);
        Transform prefabDisplayLocation = selectionIndicator.transform.Find("DisplayLocation");
        GameObject displayObject = Instantiate(displayPrefab, prefabDisplayLocation);

        var nameDisplay = selectionIndicator.transform.Find("Name");
        if (nameDisplay != null)
        {
            TextMeshProUGUI nameText = nameDisplay.GetComponent<TextMeshProUGUI>();
            if (nameText != null)
            {
                nameText.text = "Name: " + structureName;
            }
        }
        var costDisplay = selectionIndicator.transform.Find("Price");
        if (costDisplay != null)
        {
            TextMeshProUGUI costText = costDisplay.GetComponent<TextMeshProUGUI>();
            if (costText != null)
            {
                if (cost <= 0)
                {
                    costText.text = "";
                } else
                {
                    costText.text = "Price: " + cost.ToString();
                }
                
            }
        }

        // Disable function component if it exists
        var function = displayObject.transform.Find("Function");
        if (function != null)
            function.gameObject.SetActive(false);
        displayObject.layer = prefabDisplayLocation.gameObject.layer;
        foreach (Transform child in displayObject.GetComponentsInChildren<Transform>())
        {
            child.gameObject.layer = prefabDisplayLocation.gameObject.layer;
        }

        // Adjust scale and position based on size
        float scale = 1f;
        Vector2 offset = Vector2.zero;
        if (size.x > size.y)
        {
            scale = 100 / size.x;
            offset = new Vector2(0, size.y - scale / 100);
        }
        else if (size.y > size.x)
        {
            scale = 100 / size.y;
            offset = new Vector2(size.x - scale / 100, 0);
        }
        else
        {
            scale = 100 / size.x;
            offset = Vector2.zero;
        }
        displayObject.transform.localScale = new Vector3(scale, scale, scale);
        displayObject.transform.position = new Vector3(
            displayObject.transform.position.x + offset.x, 
            displayObject.transform.position.y + offset.y, 
            displayObject.transform.position.z);
    }

    private void PrepareDisplay(GameObject displayObject, Vector2Int size)
    {
        // Disable function component if it exists
        var function = displayObject.transform.Find("Function");
        if (function != null)
            function.gameObject.SetActive(false);
        displayObject.layer = displayLocation.gameObject.layer;
        foreach (Transform child in displayObject.GetComponentsInChildren<Transform>())
        {
            child.gameObject.layer = displayLocation.gameObject.layer;
        }

        // Adjust scale and position based on size
        float scale = 1f;
        Vector2 offset = Vector2.zero;
        if (size.x > size.y)
        {
            scale = 100 / size.x;
            offset = new Vector2(0, size.y - scale / 100);
        }
        else if (size.y > size.x)
        {
            scale = 100 / size.y;
            offset = new Vector2(size.x - scale / 100, 0);
        }
        else
        {
            scale = 100 / size.x;
            offset = Vector2.zero;
        }
        displayObject.transform.localScale = new Vector3(scale, scale, scale);
        displayObject.transform.position = new Vector3(
            displayObject.transform.position.x + offset.x, 
            displayObject.transform.position.y + offset.y, 
            displayObject.transform.position.z);
    }

    private void PrepareCursor(Vector2Int size)
    {
        if(size.x > 0 || size.y > 0)
        {
            cellIndicator.transform.localScale = new Vector3(size.x, size.y, 1);
            cellIndicatorRenderer.material.mainTextureScale = size;
        }
    }

    private void PreparePreview(GameObject previewObject)
    {
        Renderer[] renderers = previewObject.GetComponentsInChildren<Renderer>();

        foreach(Renderer renderer in renderers)
        {
            Material[] materials = renderer.materials;
            for(int i = 0; i < materials.Length; i++)
            {
                materials[i] = previewMaterialInstance;
            }
            renderer.materials = materials;
        }
        
        var child = previewObject.transform.Find("Function");
        if (child != null)
            child.gameObject.SetActive(false);
    }

    public void StopShowingPreview()
    {
        cellIndicator.SetActive(false);
        if(previewObject!= null)
            Destroy(previewObject);
        if(displayObject!= null)
            Destroy(displayObject);
    }

    public void UpdatePosition(Vector3 position, bool validity)
    {
        if(previewObject != null)
        {
            MovePreview(position);
            ApplyFeedbackToPreview(validity);
        }
        
        MoveCursor(position);
        ApplyFeedbackToCursor(validity);
    }

    private void ApplyFeedbackToPreview(bool validity)
    {
        Color c = validity ? Color.white : Color.red;
        
        c.a = 0.5f;
        previewMaterialInstance.color = c;
    }

    private void ApplyFeedbackToCursor(bool validity)
    {
        Color c = validity ? Color.white : Color.red;

        c.a = 0.5f;
        cellIndicatorRenderer.material.color = c;
    }

    private void MoveCursor(Vector3 position)
    {
        cellIndicator.transform.position = position;
    }

    private void MovePreview(Vector3 position)
    {
        previewObject.transform.position = new Vector3(
            position.x,
            position.y + previewOffset,
            position.z
        );
    }
}
