using System;
using UnityEngine;

public class PreviewSystem : MonoBehaviour
{
    [SerializeField] 
    private float previewOffset = 0.06f;

    [SerializeField] 
    private GameObject cellIndicator, displayLocation;
    private GameObject previewObject, displayObject;

    [SerializeField] 
    private Material previewMaterialsPrefab;
    private Material previewMaterialInstance;

    private Renderer cellIndicatorRenderer;

    private void Start()
    {
        previewMaterialInstance = new Material(previewMaterialsPrefab);
        cellIndicator.SetActive(false);
        cellIndicatorRenderer = cellIndicator.GetComponentInChildren<Renderer>();
    }

    public void StartShowingPlacementPreview(GameObject prefab, Vector2Int size)
    {
        previewObject = Instantiate(prefab);
        displayObject = Instantiate(prefab, displayLocation.transform);
        PrepareCursor(size);
        PreparePreview(previewObject);
        PrepareDisplay(displayObject, size);
        
        cellIndicator.SetActive(true);
    }

    private void PrepareDisplay(GameObject displayObject, Vector2Int size)
    {
        var function = displayObject.transform.Find("Function");
        if (function != null)
            function.gameObject.SetActive(false);
        displayObject.layer = displayLocation.gameObject.layer;
        foreach (Transform child in displayObject.GetComponentsInChildren<Transform>())
        {
            child.gameObject.layer = displayLocation.gameObject.layer;
        }

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
        Destroy(previewObject);
        Destroy(displayObject);
    }

    public void UpdatePosition(Vector3 position, bool validity)
    {
        MovePreview(position);
        MoveCursor(position);
        ApplyFeedback(validity);
    }

    private void ApplyFeedback(bool validity)
    {
        Color c = validity ? Color.white : Color.red;
        cellIndicatorRenderer.material.color = c;
        c.a = 0.5f;
        previewMaterialInstance.color = c;
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
