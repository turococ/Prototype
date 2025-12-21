using UnityEngine;
using UnityEngine.EventSystems;

public class DragWindow : MonoBehaviour, IPointerDownHandler, IDragHandler
{
    private RectTransform window;
    private RectTransform canvas;
    private Vector2 offset;

    void Awake()
    {
        window = transform.parent.GetComponent<RectTransform>();
        canvas = window.GetComponentInParent<Canvas>().GetComponent<RectTransform>();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        Vector2 canvasPos;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            canvas,
            eventData.position,
            eventData.pressEventCamera,
            out canvasPos
        );

        offset = canvasPos - window.anchoredPosition;
    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector2 canvasPos;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            canvas,
            eventData.position,
            eventData.pressEventCamera,
            out canvasPos
        );

        window.anchoredPosition = canvasPos - offset;
    }
}
