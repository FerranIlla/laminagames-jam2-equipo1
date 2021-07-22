using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour
{
    static public AudioSource menuMusicSource;

    // Start is called before the first frame update
    void Start()
    {
        menuMusicSource = AudioManager.instance.PlaySoundGetReference("MenuMusic");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static void Escena (string Nombredelaescena){
        menuMusicSource.Stop();
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
