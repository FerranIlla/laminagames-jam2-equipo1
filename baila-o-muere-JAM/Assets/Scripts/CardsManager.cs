using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardsManager : MonoBehaviour
{
    [SerializeField] private Transform cardsParent;
    [HideInInspector] public List<DanceMoveCard> cardsList = new List<DanceMoveCard>();

    [HideInInspector] private SlotsManager slotsManager;

    void Awake()
    {
        GetCardsReference();
        slotsManager = GetComponent<SlotsManager>();
    }

    private void GetCardsReference()
    {
        int iterator = 0;
        foreach (Transform child in cardsParent)
        {
            DanceMoveCard card = child.GetComponent<DanceMoveCard>();
            cardsList.Add(card);
            card.positionInList = iterator;
            iterator++;
        }
    }
}
