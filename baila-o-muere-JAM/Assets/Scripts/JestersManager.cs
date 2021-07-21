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
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Jump"))
        {
            KillCandidate();
        }
    }

    private void FillJestersList()
    {
        foreach (Transform child in jestersParent)
        {
            jestersList.Add(child);
        }
        jesterCandidate = jestersList[0];
    }

    public void KillCandidate()
    {
        //throw candidate animation
        StartCoroutine(FallAnimation(jesterCandidate));

        //remove from list
        jestersList.Remove(jesterCandidate);

        //set next candidate
        if(jestersList.Count>0)
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
        }
    }


}
