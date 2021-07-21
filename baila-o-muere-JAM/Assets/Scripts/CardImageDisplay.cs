using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardImageDisplay : MonoBehaviour
{
    Text debugText;
    DanceMoveCard card;

    // Start is called before the first frame update
    void Start()
    {
        debugText = transform.GetChild(0).GetComponent<Text>();
        card = transform.parent.GetComponent<DanceMoveCard>();
        debugText.text = card.move.DebugNumber.ToString();
    }
}
