using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FeedbackText : MonoBehaviour
{
    TMP_Text text;

    // Start is called before the first frame update
    void Start()
    {
        text = transform.GetChild(1).GetComponent<TMP_Text>();
        HideFeedbackText();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ShowFeedbackText()
    {
        transform.GetChild(0).gameObject.SetActive(true); //panel
        transform.GetChild(1).gameObject.SetActive(true); //text
    }

    public void HideFeedbackText()
    {
        transform.GetChild(0).gameObject.SetActive(false); //panel
        transform.GetChild(1).gameObject.SetActive(false); //text
    }

    public void SetQueenSentence(int numberOfCorrectAnswers)
    {
        string generateSentence = "";
        if (numberOfCorrectAnswers < 5) //Wrong combination
        {
            int randomSentence = Random.Range(0, 4);
            switch (randomSentence)
            {
                case 0:
                    generateSentence = "I only liked * of those 5 moves.\n\nBad luck... NEXT!";
                    break;
                case 1:
                    generateSentence = "Too bad! Only * moves of your performance satisfied me.\n\nGoodbye...";
                    break;
                case 2:
                    generateSentence = "I liked * out of 5 of your moves. And that's not enough!";
                    break;
                case 3:
                    generateSentence = "Not even close! * moves out of 5.\n\nTry a little harder...";
                    break;
            }

            generateSentence = generateSentence.Replace('*', System.Convert.ToChar(numberOfCorrectAnswers.ToString()));
        }
        else //Right combination
        {
            generateSentence = "Yes! This is what I call entertainment.";
        }

        text.text = generateSentence;
    }
}
