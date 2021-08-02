using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Slot : MonoBehaviour, IDropHandler, IPointerDownHandler
{
    [HideInInspector] public int cardSelectedIndex = -1;
    GameObject managers;
    SlotsManager slotsManager;
    CardsManager cardsManager;
    [HideInInspector] public int positionInList;

    void Awake()
    {
        ResetCardDisplay();
        managers = GameObject.FindWithTag("ListsManager");
        slotsManager = managers.GetComponent<SlotsManager>();
        cardsManager = managers.GetComponent<CardsManager>();
    }

    public void OnDrop(PointerEventData eventData)
    {
        if (cardSelectedIndex == -1)
        {
            DanceMove danceMoveDropped = eventData.pointerDrag.GetComponent<DanceMoveCard>().move;

            //card animation
            //set move by card dragged
            cardSelectedIndex = eventData.pointerDrag.GetComponent<DanceMoveCard>().positionInList;
            ShowCardSelectedInSlot(danceMoveDropped.DebugNumber);

            //hide card dragged
            eventData.pointerDrag.GetComponent<DanceMoveCard>().SetCardVisibility(false);
            eventData.pointerDrag.GetComponent<DanceMoveCard>().ResetCardPosition();

            slotsManager.CheckIfAllSlotsFilled();
        }
    }


    void ShowCardSelectedInSlot(int numberToShow)
    {
        //show number - debug
        transform.GetChild(2).GetChild(1).GetComponent<Text>().text = numberToShow.ToString();

        //set and show card
        Image cardInSlot = transform.GetChild(2).GetChild(0).GetComponent<Image>();
        cardInSlot.enabled = true;
        cardInSlot.sprite = slotsManager.cardSprites[numberToShow - 1];

        //hide empty slot
        transform.GetChild(0).gameObject.SetActive(false);
        transform.GetChild(1).gameObject.SetActive(false);

    }

    void ResetCardDisplay()
    {
        //Debug with normal text
        if (cardSelectedIndex == -1)
        {
            //text
            transform.GetChild(2).GetChild(1).GetComponent<Text>().text = "";

            //hide card in slot
            transform.GetChild(2).GetChild(0).GetComponent<Image>().enabled = false;

            //show slot
            transform.GetChild(0).gameObject.SetActive(true);
            transform.GetChild(1).gameObject.SetActive(true);
        }
        else
        {
            //should be never here
            transform.GetChild(2).GetChild(1).GetComponent<Text>().text = cardsManager.cardsList[cardSelectedIndex].move.DebugNumber.ToString();
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        ResetSlot();
    }

    public void ResetSlot()
    {
        if (cardSelectedIndex != -1)
        {
            //Debug.Log("OnPointerDown in Slot");


            //show card
            cardsManager.cardsList[cardSelectedIndex].SetCardVisibility(true);
            cardsManager.cardsList[cardSelectedIndex].ResetCard(); //maybe not needed

            //reset slot
            cardSelectedIndex = -1;
            ResetCardDisplay();
            slotsManager.CheckIfAllSlotsFilled();

        }
    }
}
