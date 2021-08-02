using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JestersManager : MonoBehaviour
{
    List<Transform> jestersList = new List<Transform>();
    [SerializeField] private Transform jestersParent;
    private Transform jesterCandidate;
    [SerializeField] private Transform jesterSpotlight;
    [SerializeField] private Animator trapAnimator;

    // Start is called before the first frame update
    void Awake()
    {
        FillJestersList();
        GetComponent<GameManager>().tries = jestersList.Count;
        MakeEveryJesterHaveDifferentAnimationOffsets(); 

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FillJestersList()
    {
        foreach (Transform child in jestersParent)
        {
            jestersList.Add(child);
        }
        jesterCandidate = jestersList[0];
    }


    void MakeEveryJesterHaveDifferentAnimationOffsets()
    {
        if (jestersList.Count <= 0) return;

        //set different cycle offsets for every jester
        foreach (Transform jester in jestersList)
        {
            jester.GetComponent<Animator>().SetFloat("Offset", Random.Range(0f, 1f));
        }
    }

    public void KillCandidate()
    {
        //throw candidate animation
        StartCoroutine(FallAnimation(jesterCandidate));

        //remove from list
        jestersList.Remove(jesterCandidate);
        GetComponent<GameManager>().tries = jestersList.Count;

        //set next candidate
        if (jestersList.Count>0)
        {
            jesterCandidate = jestersList[0]; //maybe set random
        }
        else
        {
            jesterCandidate = null;
        }
    }

    IEnumerator FallAnimation(Transform jester)
    {
        if (jester != null)
        {
            trapAnimator.SetTrigger("OpenTrap");
            float fallSpeed = 20f;
            float animationDurationInSeconds = 1.5f;

            for (float f = 0; f < animationDurationInSeconds; f += Time.deltaTime)
            {
                jester.transform.position = new Vector3(jester.transform.position.x, jester.transform.position.y - Time.deltaTime * fallSpeed, jester.transform.position.z);
                yield return null;
            }

            StartCoroutine(GetNextJesterOnStage(jesterCandidate));
        }
    }

    IEnumerator GetNextJesterOnStage(Transform jester)
    {
        if (jester != null)
        {
            jester.GetComponent<Animator>().SetTrigger("Walk");
            jester.LookAt(jesterSpotlight.position);
            Vector3 initialPos = jester.position;
            float t = 0f;
            float animationDurationInSeconds = 1f;

            for (float f = 0; f < animationDurationInSeconds; f += Time.deltaTime)
            {
                t = f / animationDurationInSeconds; //current linear iteration
                t = Mathf.SmoothStep(0, 1, t); //smooth the current iteration
                jester.position = Vector3.Lerp(initialPos, jesterSpotlight.position, t);
                yield return null;
            }
            //Debug.Log("Next jester is on spotlight");
            jester.LookAt(jester.position + Vector3.forward);
            jester.GetComponent<Animator>().SetTrigger("Idle");
        }
    }

    public void MakeJesterDance(List<DanceMoveTypes> combination)
    {
        StartCoroutine(TriggerNextDanceMoveAnimation(combination));
    }

    private void TriggerDanceMoveAnimation(Transform jester, DanceMoveTypes type)
    {
        jester.GetComponent<Animator>().SetTrigger(DanceMoveTypeToAnimationTrigger(type));
    }

    private string DanceMoveTypeToAnimationTrigger(DanceMoveTypes type)
    {
        string triggerName;
        switch (type)
        {
            case DanceMoveTypes._1:
                triggerName = "Squat";
                break;
            case DanceMoveTypes._2:
                triggerName = "Saturday";
                break;
            case DanceMoveTypes._3:
                triggerName = "Feet";
                break;
            case DanceMoveTypes._4:
                triggerName = "Backflip";
                break;
            case DanceMoveTypes._5:
                triggerName = "Arms";
                break;
            default:
                triggerName = "Idle";
                break;
        }
        return triggerName;
    }

    IEnumerator TriggerNextDanceMoveAnimation(List<DanceMoveTypes> combination)
    {
        if (jesterCandidate != null)
        {
            for (int i = 0; i < combination.Count; i++)
            {
                TriggerDanceMoveAnimation(jesterCandidate, combination[i]);
                yield return new WaitForSeconds(2f);
            }
            jesterCandidate.GetComponent<Animator>().SetTrigger("Idle");
        }
        transform.GetComponent<GameManager>().ShowFeedbackText();
    }
}
