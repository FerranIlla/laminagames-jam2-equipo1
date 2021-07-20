using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Slot : MonoBehaviour, IDropHandler
{
    DanceMoveTypes? moveSelected;

    void Awake()
    {
        ResetCardDisplay();
    }

    public void OnDrop(PointerEventData eventData)
    {
        DanceMove danceMoveDropped = eventData.pointerDrag.GetComponent<DanceMoveCard>().move;

        //card animation
        //set move by card dragged
        moveSelected = danceMoveDropped.type;
        DebugCardSelected(danceMoveDropped.DebugNumber);

        //hide card dragged

        Debug.Log("Dropped in slot");
        eventData.pointerDrag.transform.position = transform.position;
        eventData.pointerDrag.GetComponent<CanvasGroup>().alpha = 0f;
    }


    void DebugCardSelected(int numberToShow)
    {
        transform.GetChild(0).GetComponent<Text>().text = numberToShow.ToString();
    }

    void ResetCardDisplay()
    {
        //Debug with normal text
        if (moveSelected == null) transform.GetChild(0).GetComponent<Text>().text = "";
        else
        {
            transform.GetChild(0).GetComponent<Text>().text = (((int)moveSelected)+1).ToString();
        }

        //Game
    }
}
