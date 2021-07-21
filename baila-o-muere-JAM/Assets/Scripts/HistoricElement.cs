using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HistoricElement : MonoBehaviour
{
    public int numberOfCorrectAnswers;
    TMP_Text resultDisplay;
    public List<DanceMoveTypes> combination = new List<DanceMoveTypes>();

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public HistoricElement(int correctAnswers, List<DanceMoveTypes> comb)
    {
        numberOfCorrectAnswers = correctAnswers;
        combination = comb;
        
    }

    private void SetResultText()
    {
        resultDisplay.text = numberOfCorrectAnswers.ToString() + " / 5";
    }

    
}
