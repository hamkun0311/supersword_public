using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class VirtualJoystick : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
{

    private Image imageBackground;
    private Image imageController;
    private Vector2 touchPosition;

    private void Awake()
    {
        imageBackground = GetComponent<Image>();
        imageController = transform.GetChild(0).GetComponent<Image>();
    }
    public void OnPointerDown(PointerEventData eventData)
    {

    }

    public void OnDrag(PointerEventData eventData)
    {

        touchPosition = Vector2.up;

        if(RectTransformUtility.ScreenPointToLocalPointInRectangle(imageBackground.rectTransform, eventData.position, eventData.pressEventCamera, out touchPosition))
        {

            touchPosition.x = (touchPosition.x / imageBackground.rectTransform.sizeDelta.x);
            touchPosition.y = (touchPosition.y / imageBackground.rectTransform.sizeDelta.y);

            touchPosition = new Vector2(touchPosition.x * 2 - 1, touchPosition.y * 2 - 1);

            touchPosition = (touchPosition.magnitude > 1) ? touchPosition.normalized : touchPosition;

            imageController.rectTransform.anchoredPosition = new Vector2 (
                touchPosition.x * imageBackground.rectTransform.sizeDelta.x/2, touchPosition.y * imageBackground.rectTransform.sizeDelta.y/2
            );

        }

    }

    public void OnPointerUp(PointerEventData eventData)
    {
        imageController.rectTransform.anchoredPosition = Vector2.zero;
        touchPosition = Vector2.zero;
        
    }

    public float Horizontal()
    {
        return touchPosition.x;
    }

    public float Vertical()
    {
        return touchPosition.y;
    }

}
