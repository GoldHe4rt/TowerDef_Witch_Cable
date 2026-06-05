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
            var priceDisplay = selectionIndicators[i].transform.Find("Price");
            
            disabledDisplay.gameObject.SetActive(i != id);
            priceDisplay.gameObject.SetActive(i == id);
            
            
            // wrap around distance calculation for circular list
            int rawDistance = i - id;
            int distance = rawDistance;
            int count = selectionIndicators.Count;
            if (Mathf.Abs(rawDistance) > count / 2)
            {
                distance = rawDistance > 0 ? rawDistance - count : rawDistance + count;
            }

            // Set scale based on distance from id
            if (i == id)
            {
                GameObject indicator = selectionIndicators[i];
                indicator.transform.SetSiblingIndex(2);
                RectTransform rectTransform = indicator.GetComponent<RectTransform>();
                rectTransform.sizeDelta = new Vector2(6.25f, 6); 
                float scale = 15f;
                indicator.transform.localScale = new Vector3(scale, scale, scale);
            }
            else if (Mathf.Abs(distance) == 1)
            {
                GameObject indicator = selectionIndicators[i];
                // Reorder sibling to ensure correct layering based on position relative to id
                if (distance < 0)
                {
                    indicator.transform.SetSiblingIndex(1);
                }
                else
                {
                    indicator.transform.SetSiblingIndex(3);
                }

                SpriteRenderer spriteRenderer = disabledDisplay.GetComponent<SpriteRenderer>();
                Color c = spriteRenderer.color;
                c.a = 0.7f;
                spriteRenderer.color = c; 

                RectTransform rectTransform = indicator.GetComponent<RectTransform>();
                rectTransform.sizeDelta = new Vector2(6.15f, 6); 

                float scale = 8f;
                indicator.transform.localScale = new Vector3(scale, scale, scale);
                
            }
            else if (Mathf.Abs(distance) == 2)
            {
                GameObject indicator = selectionIndicators[i];
                // Reorder sibling to ensure correct layering based on position relative to id
                if (distance < 0)
                {
                    indicator.transform.SetSiblingIndex(0);
                }
                else
                {
                    indicator.transform.SetSiblingIndex(4);
                }
                
                SpriteRenderer spriteRenderer = disabledDisplay.GetComponent<SpriteRenderer>();
                Color c = spriteRenderer.color;
                c.a = 0.7f;
                spriteRenderer.color = c; 

                RectTransform rectTransform = indicator.GetComponent<RectTransform>();
                rectTransform.sizeDelta = new Vector2(6.1f, 6); 

                float scale = 3f;
                indicator.transform.localScale = new Vector3(scale, scale, scale);
            }
            else
            {
                GameObject indicator = selectionIndicators[i];
                float scale = 0f;
                indicator.transform.localScale = new Vector3(scale, scale, scale);
            }

            
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
                nameText.text = structureName;
            }
        }
        var costDisplay = selectionIndicator.transform.Find("Price/Price Amount");
        if (costDisplay != null)
        {
            TextMeshProUGUI costText = costDisplay.GetComponent<TextMeshProUGUI>();
            if (costText != null)
            {
                if (cost <= 0)
                {
                    costText.text = "";
                    var costTextDisplay = selectionIndicator.transform.Find("Price/Price Text");
                    if (costTextDisplay != null)
                    {
                        costTextDisplay.gameObject.SetActive(false);
                    }
                } else
                {
                    costText.text = cost.ToString();
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
        float scale = 80f;
        Vector2 offset = Vector2.zero;
        if (size.x > size.y)
        {
            scale = scale / size.x;
            offset = new Vector2(0, size.y - scale / 40);
        }
        else if (size.y > size.x)
        {
            scale = scale / size.y;
            offset = new Vector2(size.x - scale / 40, 0);
        }
        else
        {
            scale = scale / size.x;
            offset = Vector2.zero;
        }
        if (id == -1)
        {
            scale = 120f;
            offset = new Vector2(-15, -25);
            displayObject.transform.localPosition = new Vector3(
            displayObject.transform.localPosition.x + offset.x, 
            displayObject.transform.localPosition.y + offset.y, 
            displayObject.transform.localPosition.z);
        } else
        {
            displayObject.transform.position = new Vector3(
            displayObject.transform.position.x + offset.x, 
            displayObject.transform.position.y + offset.y, 
            displayObject.transform.position.z);
        }
        displayObject.transform.localScale = new Vector3(scale, scale, scale);
        
        
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
