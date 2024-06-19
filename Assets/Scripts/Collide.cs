using System.Collections;
using TMPro;
using UnityEngine;

public class Collide : MonoBehaviour
{
    public float ballSpeedx;
    public float ballSpeedy;

    public float xRange1 = 600;
    public float xRange2 = 1300;

    public float yRange1 = 550;
    public float yRange2 = 850;

    public float xMaxRange;
    public float xMinRange;
    public float yMaxRange;
    public float yMinRange;

    public Vector3 ballStart;
    public Vector2 ballVelocity;

    public Transform[] lockTransforms;

    public Score score;
    public TextMeshProUGUI timer;
    public TextMeshProUGUI spaceToContinue;
    public GameObject middleLine;

    public AudioSource scoreSound;
    public AudioSource wallAudio;
    public AudioSource paddeAudio;

    public PongAgent pongAgentL;
    public PongAgent pongAgentR;
    public TextMeshProUGUI rewardText;

    private bool gameStarted;
    private bool addedSpeed;
    public bool notTraining;

    private int aiGoals;

    public Vector3 posR;
    private Vector3 startPos;

    public void Start()
    {
        if (middleLine == null)
        {
            middleLine = GameObject.Find("Middel lijn");
        }

        ballStart = transform.localPosition;

        pongAgentL = transform.parent.transform.GetChild(2).GetComponent<PongAgent>();
        pongAgentR = transform.parent.transform.GetChild(3).GetComponent<PongAgent>();

        lockTransforms[0] = transform.parent.transform.GetChild(8);
        lockTransforms[1] = transform.parent.transform.GetChild(9);
        lockTransforms[2] = transform.parent.transform.GetChild(6);
        lockTransforms[3] = transform.parent.transform.GetChild(7);

        rewardText = transform.parent.transform.GetChild(12).transform.GetChild(0).GetComponent<TextMeshProUGUI>();
    }

    public void AIStart()
    {
        if (middleLine == null)
        {
            middleLine = GameObject.Find("Middel lijn");
        }

        startPos = transform.position;
        if (middleLine != null && spaceToContinue != null)
        {
            middleLine.SetActive(true);
            spaceToContinue.gameObject.SetActive(false);
        }
        gameStarted = true;
        ballSpeedx = Random.Range(xRange1, xRange2);
        ballSpeedy = Random.Range(yRange1, yRange2);
        if (!addedSpeed)
        {
            addedSpeed = true;
            ballVelocity.x += ballSpeedx;
            ballVelocity.y += ballSpeedy;
        }
    }
    public void Update()
    {
        if (!gameStarted && Input.GetKeyDown(KeyCode.Space))
        {
            ballStart = transform.localPosition;

            startPos = transform.position;

            if (middleLine != null && spaceToContinue != null)
            {
                middleLine.SetActive(true);
                spaceToContinue.gameObject.SetActive(false);
            }

            gameStarted = true;
            ballSpeedx = Random.Range(xRange1, xRange2);
            ballSpeedy = Random.Range(yRange1, yRange2);

            ballVelocity.x += ballSpeedx;
            ballVelocity.y += ballSpeedy;
        }
        if (pongAgentR != null && !notTraining)
        {
            rewardText.text = ((pongAgentR.GetCumulativeReward() + pongAgentL.GetCumulativeReward()) / 2).ToString();
            gameObject.GetComponent<Rigidbody2D>().velocity = ballVelocity * Time.deltaTime * 20;
        }
        else
        {
            gameObject.GetComponent<Rigidbody2D>().velocity = ballVelocity;
        }

        if (pongAgentR != null && notTraining)
        {
            gameObject.GetComponent<Rigidbody2D>().velocity = ballVelocity * Time.deltaTime * 200;
        }

        posR.y = Mathf.Clamp(transform.position.y, lockTransforms[0].position.y, lockTransforms[1].position.y);
        posR.x = Mathf.Clamp(transform.position.x, lockTransforms[2].position.x, lockTransforms[3].position.x);
        transform.position = posR;
    }

    public void DirectionSwitchX()
    {
        if (xRange1 > 0)
        {
            xRange1 = -xMinRange;
            xRange2 = -xMaxRange;
        }
        else
        {
            xRange1 = xMinRange;
            xRange2 = xMaxRange;
        }

        ballSpeedx = Random.Range(xRange1, xRange2);
    }

    public void DirectionSwitchY()
    {
        if (yRange1 > 0)
        {
            yRange1 = -yMinRange;
            yRange2 = -yMaxRange;
        }
        else
        {
            yRange1 = yMaxRange;
            yRange2 = yMaxRange;
        }

        ballSpeedy = Random.Range(yRange1, yRange2);
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "GoalL")
        {
            if (score != null)
            {
                score.scoreL++;
            }

            ballVelocity.x = 0;
            ballVelocity.y = 0;

            if (scoreSound != null)
            {
                scoreSound.Play();
            }

            if (pongAgentL != null)
            {
                aiGoals++;

                pongAgentL.roundTime = 60;
                pongAgentR.roundTime = 60;

                if (aiGoals >= 3)
                {
                    pongAgentL.EndRound();
                    pongAgentR.EndRound();

                    aiGoals = 0;
                }
            }

            StartCoroutine(WaitAfterGoalL());
        }

        if (collision.gameObject.tag == "GoalR")
        {
            if (score != null)
            {
                score.scoreR++;
            }

            ballVelocity.x = 0;
            ballVelocity.y = 0;

            if (scoreSound != null)
            {
                scoreSound.Play();
            }

            aiGoals++;

            if (pongAgentL != null)
            {
                pongAgentL.roundTime = 60;
                pongAgentR.roundTime = 60;

                if (aiGoals >= 3)
                {
                    pongAgentL.EndRound();
                    pongAgentR.EndRound();

                    aiGoals = 0;
                }
            }

            StartCoroutine(WaitAfterGoalR());
        }

        if (collision.gameObject.tag == "BoundaryUp")
        {
            if (wallAudio != null)
            {
                wallAudio.Play();
            }

            DirectionSwitchY();
            ballVelocity.y = ballSpeedy;
        }

        if (collision.gameObject.tag == "BoundaryDown")
        {
            if (wallAudio != null)
            {
                wallAudio.Play();
            }

            DirectionSwitchY();
            ballVelocity.y = ballSpeedy;
        }

        if (collision.gameObject.tag == "BouncerR")
        {
            if (paddeAudio != null)
            {
                paddeAudio.Play();
            }

            DirectionSwitchX();
            ballVelocity.x = ballSpeedx;
        }

        if (collision.gameObject.tag == "AgentL")
        {
            if (paddeAudio != null)
            {
                paddeAudio.Play();
            }

            DirectionSwitchX();
            ballVelocity.x = ballSpeedx;
        }

        if (collision.gameObject.tag == "AgentR")
        {
            if (paddeAudio != null)
            {
                paddeAudio.Play();
            }

            DirectionSwitchX();
            ballVelocity.x = ballSpeedx;
        }

        if (collision.gameObject.tag == "BouncerL")
        {
            if (paddeAudio != null)
            {
                paddeAudio.Play();
            }

            DirectionSwitchX();
            ballVelocity.x = ballSpeedx;
        }

        IEnumerator WaitAfterGoalL()
        {
            xRange1 = xMinRange / 2f;
            xRange2 = xMaxRange / 2f;
            ballSpeedx = Random.Range(xRange1, xRange2);

            if (middleLine != null && timer != null)
            {
                middleLine.SetActive(false);
                timer.gameObject.SetActive(true);
            }

            if (pongAgentL != null)
            {
                if (middleLine != null && timer != null)
                {
                    middleLine.SetActive(true);
                    timer.gameObject.SetActive(false);
                }

                ballVelocity.x += ballSpeedx;
                ballVelocity.y += ballSpeedy;

                transform.localPosition = ballStart;
            }
            else
            {
                timer.text = 3.ToString();
                yield return new WaitForSeconds(1f);
                timer.text = 2.ToString();
                yield return new WaitForSeconds(1f);
                timer.text = 1.ToString();
                yield return new WaitForSeconds(1f);
                timer.text = 0.ToString();

                if (middleLine != null && timer != null)
                {
                    middleLine.SetActive(true);
                    timer.gameObject.SetActive(false);
                }

                ballVelocity.x += ballSpeedx;
                ballVelocity.y += ballSpeedy;

                transform.localPosition = ballStart;
            }
        }

        IEnumerator WaitAfterGoalR()
        {
            xRange1 = -xMinRange / 2;
            xRange2 = -xMaxRange / 2f;
            ballSpeedx = Random.Range(xRange1, xRange2);

            if (middleLine != null && timer != null)
            {
                middleLine.SetActive(false);
                timer.gameObject.SetActive(true);
            }

            if (pongAgentL != null)
            {
                if (middleLine != null && timer != null)
                {
                    middleLine.SetActive(true);
                    timer.gameObject.SetActive(false);
                }

                ballVelocity.x += ballSpeedx;
                ballVelocity.y += ballSpeedy;

                transform.localPosition = ballStart;
            }
            else
            {
                timer.text = 3.ToString();
                yield return new WaitForSeconds(1f);
                timer.text = 2.ToString();
                yield return new WaitForSeconds(1f);
                timer.text = 1.ToString();
                yield return new WaitForSeconds(1f);
                timer.text = 0.ToString();

                if (middleLine != null && timer != null)
                {
                    middleLine.SetActive(true);
                    timer.gameObject.SetActive(false);
                }

                ballVelocity.x += ballSpeedx;
                ballVelocity.y += ballSpeedy;

                transform.localPosition = ballStart;
            }
        }
    }
}
