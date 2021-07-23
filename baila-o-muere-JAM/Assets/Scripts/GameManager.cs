using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    enum GameState { Waiting, ChoosingPassword, Dancing, Feedback, EndMenu}
    GameState state = GameState.ChoosingPassword;
    [HideInInspector] public int tries;

    List<DanceMoveTypes> password = new List<DanceMoveTypes>();

    [HideInInspector] private SlotsManager slotsManager;
    [HideInInspector] private CardsManager cardsManager;
    [HideInInspector] private JestersManager jestersManager;

    [SerializeField] private FeedbackText queenFeedbackScript;
    private CameraAnimation cameraAnim;
    [SerializeField] private GameObject winPanel;
    [SerializeField] private GameObject losePanel;
    [SerializeField] private Historic historic;

    [SerializeField] private Image fadePanel;



    // Start is called before the first frame update
    void Awake()
    {
        slotsManager = GetComponent<SlotsManager>();
        cardsManager = GetComponent<CardsManager>();
        jestersManager = GetComponent<JestersManager>();
        cameraAnim = Camera.main.GetComponent<CameraAnimation>();

        GenerateRandomPasswordWithoutRepeating(5);
        Debug.Log(PrintPassword());

        //start song
        AudioManager.instance.PlayGameplayMusic("GameplayMusic");
        AudioManager.instance.gameplayMusicSource.volume = AudioManager.instance.musicMinVolume;
    }

    // Update is called once per frame
    void Update()
    {
        switch (state)
        {
            case GameState.Dancing:
                //wait for the animations to end, then change to state=feedback
                break;
            case GameState.Feedback:
                //wait for the queen to say feedback
                break;

        }
    }

    private void GenerateRandomPasswordWithoutRepeating(int passwordLength)
    {
        List<DanceMoveTypes> typesList = new List<DanceMoveTypes>();

        for (int i = 0; i < System.Enum.GetNames(typeof(DanceMoveTypes)).Length; i++)
        {
            typesList.Add((DanceMoveTypes)i);
        }

        if(passwordLength > typesList.Count)
        {
            Debug.LogWarning("Could not generate a password becase there are not enough types");
            return;
        }

        //randomize
        for (int i = 0; i < passwordLength; i++)
        {
            int nextIndex = Random.Range(0, typesList.Count);
            password.Add(typesList[nextIndex]);
            typesList.RemoveAt(nextIndex);
        }

    }

    private void GenerateRandomPassword(int passwordLength)
    {
        for(int i= 0; i < passwordLength; i++)
        {
            DanceMoveTypes random = (DanceMoveTypes)Random.Range(0, System.Enum.GetNames(typeof(DanceMoveTypes)).Length);
            password.Add(random);
        }
    }

    private string PrintPassword()
    {
        string passwordLog = "";
        passwordLog += "Password: ";
        foreach (DanceMoveTypes t in password)
        {
            passwordLog += t.ToString() + ", ";
        }

        return passwordLog;
    }

    //to call from button
    public void SubmitAnswer()
    {
        state = GameState.Dancing;
        //UI animation (move answers away or below)
        slotsManager.danceButton.gameObject.SetActive(false);
        //zoom in camera
        cameraAnim.ZoomIn();


        //StartCoroutine(SimulateDanceAnimations());
        jestersManager.MakeJesterDance(GetCurrentCombination());
        AudioManager.instance.ZoomIn();
    }

    IEnumerator SimulateDanceAnimations()
    {
        Debug.Log("Jester is dancing!");
        yield return new WaitForSeconds(5);
        ShowFeedbackText();
    }

    private List<DanceMoveTypes> GetCurrentCombination()
    {
        List<DanceMoveTypes> currentCombination = new List<DanceMoveTypes>();
        for (int i = 0; i < slotsManager.slotsList.Count; ++i)
        {
            DanceMoveTypes slotResult = cardsManager.cardsList[slotsManager.slotsList[i].cardSelectedIndex].move.type;
            currentCombination.Add(slotResult);
        }

        return currentCombination;
    }

    private int TestPassword(out List<DanceMoveTypes> combination)
    {
        int correctAnswers = 0;
        combination = new List<DanceMoveTypes>();
        for(int i = 0; i < slotsManager.slotsList.Count; ++i)
        {
            DanceMoveTypes slotResult = cardsManager.cardsList[slotsManager.slotsList[i].cardSelectedIndex].move.type;
            combination.Add(slotResult);
            DanceMoveTypes passwordResult = password[i];
            if (slotResult == passwordResult) correctAnswers++;
        }
        return correctAnswers;
    }

    //call after dance animation ends
    public void ShowFeedbackText()
    {
        AudioManager.instance.ZoomOut();
        cameraAnim.ZoomOut(); //zoom out camera to default

        List<DanceMoveTypes> currentCombination;
        int correctAnswers = TestPassword(out currentCombination);
        queenFeedbackScript.SetQueenSentence(correctAnswers);
        queenFeedbackScript.ShowFeedbackText();
        //add to historic
        historic.CreateAndAddHistoricElement(correctAnswers, currentCombination);

        if (correctAnswers == password.Count)
        {
            //Debug.Log("Correct");
            //winPanel.SetActive(true);

            AudioManager.instance.PlaySound("RightCombination");
            StartCoroutine(FadeOutAndChangeScene("UI_Win"));
        }
        else
        {
            
            slotsManager.ResetAllSlots();
            jestersManager.KillCandidate(); //tries -1


            if(tries <= 0)
            {
                //Debug.Log("Wrong");
                //losePanel.SetActive(true);
                AudioManager.instance.PlaySound("WrongCombination");
                StartCoroutine(FadeOutAndChangeScene("UI_Lost"));
            }
        }
        

    }

    IEnumerator FadeOutAndChangeScene(string sceneName)
    {
        yield return new WaitForSeconds(2f);
        float t = 0;
        while (t < 1f)
        {
            t += Time.deltaTime;
            fadePanel.color = new Color(fadePanel.color.r, fadePanel.color.g, fadePanel.color.b, t);
            yield return null;
        }
        SceneManager.LoadScene(sceneName);
    }

    public void CloseGame()
    {
        Application.Quit();
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
