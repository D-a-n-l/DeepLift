using UnityEngine;
using System.Collections;

public class ChangeOrderLayerByY : MonoBehaviour
{
    [SerializeField]
    private Transform root;

    [SerializeField]
    private bool isUpdate = false;

    [SerializeField]
    private PresetSpriteRenderer[] sprites;

    private const int multiply = 100;

    private void OnEnable()
    {
        InvokeRepeating(nameof(Set), 0.5f, 5f);
    }

    private void Update()
    {
        if (isUpdate == false)
            return;

        Set();
    }

    private void Set()
    {
        float orderLayer = Mathf.Abs(root.position.y) * multiply;

        foreach (PresetSpriteRenderer sprite in sprites)
        {
            sprite.spriteRenderer.sortingOrder = (int) orderLayer + sprite.offset;
        }
    }
}

[System.Serializable]
public struct PresetSpriteRenderer
{
    public SpriteRenderer spriteRenderer;

    public int offset;
}