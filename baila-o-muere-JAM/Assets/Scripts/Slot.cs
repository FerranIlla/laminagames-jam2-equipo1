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
        DanceMove danceMoveDropped = eventData.pointerDrag.GetComponent<DanceMoveCard>().move;

        //card animation
        //set move by card dragged
        cardSelectedIndex = eventData.pointerDrag.GetComponent<DanceMoveCard>().positionInList;
        DebugCardSelected(danceMoveDropped.DebugNumber);

        //hide card dragged
        eventData.pointerDrag.GetComponent<DanceMoveCard>().SetCardVisibility(false);
        eventData.pointerDrag.GetComponent<DanceMoveCard>().ResetCardPosition();

        slotsManager.CheckIfAllSlotsFilled();
    }


    void DebugCardSelected(int numberToShow)
    {
        transform.GetChild(0).GetComponent<Text>().text = numberToShow.ToString();
    }

    void ResetCardDisplay()
    {
        //Debug with normal text
        if (cardSelectedIndex == -1) transform.GetChild(0).GetComponent<Text>().text = "";
        else
        {
            transform.GetChild(0).GetComponent<Text>().text = cardsManager.cardsList[cardSelectedIndex].move.DebugNumber.ToString();
        }

        //Game
    }

    public void OnPointerDown(PointerEventData eventData)
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
