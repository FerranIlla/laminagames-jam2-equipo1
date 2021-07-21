using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DanceMoveCard : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerEnterHandler, IPointerExitHandler, IPointerUpHandler
{
    [SerializeField] private DanceMoveTypes type;
    [SerializeField] private string danceName;
    public DanceMove move;
    private RectTransform rectTransform;
    Vector3 startingLocalPosition;
    private CanvasGroup canvasGroup;
    [HideInInspector] public int positionInList;


    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        startingLocalPosition = transform.localPosition;
        canvasGroup = GetComponent<CanvasGroup>();

        //contruct DanceMove class
        move = new DanceMove(danceName, (int)type);
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
        //Debug.Log("OnPointerEnter");
    }
    
    public void OnPointerExit(PointerEventData eventData)
    {
        //stop animation
        //make smaller the card
        //Debug.Log("OnPointerExit");
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        ResetCard();
    }

    public void ResetCard()
    {
        ResetCardPosition();
        canvasGroup.alpha = 1f;
        canvasGroup.blocksRaycasts = true;
    }

    public void ResetCardPosition()
    {
        transform.localPosition = startingLocalPosition;
    }

    public void SetCardVisibility(bool visible)
    {
        GetComponent<Image>().enabled = visible; //card contour image
        transform.GetChild(0).gameObject.SetActive(visible); //dance move img display and debug text

    }
    
}
