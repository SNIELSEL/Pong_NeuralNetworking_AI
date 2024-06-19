using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Score : MonoBehaviour
{
    public int scoreL;
    public int scoreR;

    public float winR;
    public float winL;

    public bool awardedPoint;
    public bool UsingAI;
    public bool oneVOne;

    public TextMeshProUGUI scoreTextLeft;
    public TextMeshProUGUI scoreTextRight;

    public PongAgent pongAgentR;
    public PongAgent pongAgentL;

    public void Update()
    {
        scoreTextLeft.text = scoreL.ToString();
        scoreTextRight.text = scoreR.ToString();

        if(scoreL > 2)
        {
            if (!awardedPoint && !UsingAI)
            {
                StartCoroutine(SetScoreL());
            }

            if(!awardedPoint && UsingAI)
            {
                StartCoroutine(SetScoreLAI());
            }
        }

        if (scoreR > 2)
        {
            if (!awardedPoint && !UsingAI)
            {
                StartCoroutine(SetScoreR());
            }

            if(!awardedPoint && UsingAI)
            {
                StartCoroutine(SetScoreRAI());
            }
        }

        IEnumerator SetScoreR()
        {
            if (!UsingAI)
            {
                winL = PlayerPrefs.GetFloat("WinsR");
                winL += 1;
                awardedPoint = true;
                yield return new WaitForSeconds(1f);
                PlayerPrefs.SetFloat("WinsR", winL);
                yield return new WaitForSeconds(2f);
                awardedPoint = false;
            }

            if(pongAgentL != null)
            {
                pongAgentR.EndRound();
                pongAgentL.EndRound();
            }

            SceneManager.LoadScene("Start Screen");
        }

        IEnumerator SetScoreL()
        {
            if (!UsingAI)
            {
                winR = PlayerPrefs.GetFloat("WinsL");
                winR += 1;
                awardedPoint = true;
                yield return new WaitForSeconds(1f);
                PlayerPrefs.SetFloat("WinsL", winR);
                yield return new WaitForSeconds(2f);
                awardedPoint = false;
            }

            if(pongAgentL != null)
            {
                pongAgentL.EndRound();
                pongAgentR.EndRound();
            }
            SceneManager.LoadScene("Start Screen");
        }


        IEnumerator SetScoreRAI()
        {
            if (oneVOne)
            {
                winL = PlayerPrefs.GetFloat("AiGamesPlayed");
                winL += 1;
                awardedPoint = true;
                PlayerPrefs.SetFloat("AiGamesPlayed", winL);
                yield return new WaitForSeconds(1f);

                winL = PlayerPrefs.GetFloat("AIWinsR");
                winL += 1;
                awardedPoint = true;
                yield return new WaitForSeconds(1f);
                PlayerPrefs.SetFloat("AIWinsR", winL);
                yield return new WaitForSeconds(2f);
                awardedPoint = false;
                pongAgentR.EndRound();
                SceneManager.LoadScene("Start Screen");
            }
            else
            {
                yield return new WaitForSeconds(1f);
                SceneManager.LoadScene("Start Screen");
            }
        }

        IEnumerator SetScoreLAI()
        {
            if (oneVOne)
            {
                winL = PlayerPrefs.GetFloat("AiGamesPlayed");
                winL += 1;
                awardedPoint = true;
                PlayerPrefs.SetFloat("AiGamesPlayed", winL);
                yield return new WaitForSeconds(1f);

                winL = PlayerPrefs.GetFloat("AIWinsL");
                winL += 1;
                yield return new WaitForSeconds(1f);
                PlayerPrefs.SetFloat("AIWinsL", winL);
                yield return new WaitForSeconds(2f);
                awardedPoint = false;
                pongAgentR.EndRound();
                SceneManager.LoadScene("Start Screen");
            }
            else
            {
                SceneManager.LoadScene("Start Screen");
            }
        }
    }
}
