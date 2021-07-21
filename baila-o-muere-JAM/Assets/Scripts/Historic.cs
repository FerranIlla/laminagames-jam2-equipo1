using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Historic : MonoBehaviour
{
    List<HistoricElement> elements = new List<HistoricElement>();
    [SerializeField] private GameObject elementPrefab;
    [SerializeField] private Transform elementsParent;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CreateAndAddHistoricElement(int correctAnswers, List<DanceMoveTypes> comb)
    {
        GameObject elementObj = Instantiate(elementPrefab, elementsParent);
        //set element position (RectTransform maybe)

        HistoricElement element = new HistoricElement(correctAnswers, comb);

    }
}
