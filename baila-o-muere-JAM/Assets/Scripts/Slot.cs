using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Slot : MonoBehaviour, IDropHandler
{


    public void OnDrop(PointerEventData eventData)
    {
        //card animation
        //set image by card dragged
        //hide card dragged

        Debug.Log("Dropped in slot");
        eventData.pointerDrag.transform.position = transform.position;
    }

}
