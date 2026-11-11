using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class ClickHotspot : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private UnityEvent onClick = new UnityEvent();

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            onClick.Invoke();
        }
    }
}