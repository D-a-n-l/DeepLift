using UnityEngine;
using UnityEngine.EventSystems;

public class LongPress : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] 
    private PlayerMovement johnWick;

    private bool isDown;

    public void OnPointerDown(PointerEventData eventData)
    {
        this.isDown = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        this.isDown = false;
    }

    private void Update()
    {
        if (!this.isDown) return;
        //johnWick.Shoot();
    }
}