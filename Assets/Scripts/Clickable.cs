using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class Clickable : MonoBehaviour
{
    [SerializeField]
    private SpriteRenderer spriteRenderer;

    [SerializeField]
    private PresetChangeColor presetColor;

    [Space(10)]
    [SerializeField]
    private UnityEvent OnMouseDowned;

    private WaitForSeconds wait;

    private void Start()
    {
        wait = new WaitForSeconds(presetColor.duration);
    }

    private void OnMouseDown()
    {
        if (CheckClick.IsCan == false)
            return;

        Click();

        OnMouseDowned?.Invoke();

        CheckClick.IsCan = false;
    }

    public void ChangeCheckClick(bool value) => CheckClick.IsCan = value;

    public void Click() => StartCoroutine(OnDown());

    private IEnumerator OnDown()
    {
        spriteRenderer.color = presetColor.pressed;

        yield return wait;

        spriteRenderer.color = presetColor.normal;
    }
}