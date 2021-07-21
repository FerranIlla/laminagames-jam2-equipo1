using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HistoricElement : MonoBehaviour
{
    [HideInInspector] public int numberOfCorrectAnswers;
    TMP_Text resultDisplay;
    [HideInInspector] public List<DanceMoveTypes> combination = new List<DanceMoveTypes>();
    List<Image> combinationDisplay = new List<Image>();

    

    void Awake()
    {
        //text reference
        resultDisplay = transform.GetChild(0).GetChild(0).GetComponent<TMP_Text>();
        
        //combination images reference
        foreach(Transform t in transform.GetChild(1))
        {
            combinationDisplay.Add(t.GetComponent<Image>());
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Initialize(int correctAnswers, List<DanceMoveTypes> comb)
    {
        numberOfCorrectAnswers = correctAnswers;
        combination = comb;
    }

    private void SetResultText()
    {
        resultDisplay.text = numberOfCorrectAnswers.ToString() + " / 5";
    }

    private void SetCombinationImages()
    {
        List<Sprite> sprites = transform.parent.parent.GetComponent<Historic>().cardsImages;
        for(int i = 0; i < combinationDisplay.Count; i++)
        {
            combinationDisplay[i].sprite = sprites[(int)combination[i]];
        }
    }

    public void DisplayInfo()
    {
        SetResultText();
        SetCombinationImages();
    }

    
}
