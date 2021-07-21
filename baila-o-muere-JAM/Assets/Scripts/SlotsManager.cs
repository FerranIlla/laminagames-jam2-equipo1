using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SlotsManager : MonoBehaviour
{

    [SerializeField] private Transform slotsParent;
    [HideInInspector] public List<Slot> slotsList = new List<Slot>();

    [HideInInspector] private CardsManager cardsManager;

    public Button danceButton;

    void Awake()
    {
        GetSlotsReference();
        cardsManager = GetComponent<CardsManager>();
        CheckIfAllSlotsFilled();
    }

    private void GetSlotsReference()
    {
        int iterator = 0;
        foreach (Transform child in slotsParent)
        {
            Slot slot = child.GetComponent<Slot>();
            slotsList.Add(slot);
            slot.positionInList = iterator;
            iterator++;
        }
    }

    public void CheckIfAllSlotsFilled()
    {
        bool allSlotsFilled = true;
        foreach(Slot s in slotsList)
        {
            if (s.cardSelectedIndex != -1) continue;
            else
            {
                allSlotsFilled = false;
                break;
            }
        }
        danceButton.gameObject.SetActive(allSlotsFilled);
    }
}
