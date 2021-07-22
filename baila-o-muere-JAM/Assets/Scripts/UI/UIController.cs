using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static void Escena (string Nombredelaescena){
        Application.LoadLevel(Nombredelaescena);

    }

    public static void PanelA(GameObject Activacion){
        Activacion.SetActive(true);
    }
        public static void PanelD(GameObject Desactivacion){
        Desactivacion.SetActive(false);
    }

    public static void Salir(){
         Application.Quit();
    } 
}
