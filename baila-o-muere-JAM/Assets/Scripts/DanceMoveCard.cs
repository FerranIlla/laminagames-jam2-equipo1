using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DanceMoveCard : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerEnterHandler, IPointerExitHandler, IPointerUpHandler
{
    public DanceMove move;
    private bool isInteracting = false;
    private RectTransform rectTransform;
    Vector3 startingLocalPosition;
    private CanvasGroup canvasGroup;


    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        startingLocalPosition = transform.localPosition;
        canvasGroup = GetComponent<CanvasGroup>();
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position += new Vector3(eventData.delta.x, eventData.delta.y, 0);
        //rectTransform.anchoredPosition += eventData.delta;
    }



    public void OnPointerDown(PointerEventData eventData)
    {
        //stop animation
        //make smaller the card

        //make !interactable
        canvasGroup.alpha = .6f;
        canvasGroup.blocksRaycasts = false;
        
    }
    
    public void OnPointerEnter(PointerEventData eventData)
    {
        //show animation
        //make bigger the card
    }
    
    public void OnPointerExit(PointerEventData eventData)
    {
        //stop animation
        //make smaller the card
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        Debug.Log("OnPointerUp from Card");
        transform.localPosition = startingLocalPosition;
        canvasGroup.alpha = 1f;
        canvasGroup.blocksRaycasts = true;
    }
}
