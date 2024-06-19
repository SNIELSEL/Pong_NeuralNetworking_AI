using System;
using System.Collections;
using System.Data;
using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Sensors;
using UnityEngine;

public class PongAgent : Agent
{
    [SerializeField] private Transform Target;
    [SerializeField] private Rigidbody2D targetRB;

    [SerializeField] private Collide collide;
    [SerializeField] private Score score;
    [SerializeField] private EnvoirmentsManager envManager;

    [SerializeField] private float moveSpeed;

    [NonSerialized] public float roundTime;

    public float middleDistance;
    public float targetHeight;
    public float targetDistance;

    public bool humanVSAi;

    private float highestReward;

    Rigidbody2D rb;

    private Vector3 startpos;
    private Vector3 lastPos;
    void Start()
    {
        envManager = GameObject.Find("EventSystem").GetComponent<EnvoirmentsManager>();

        roundTime = 60;

        lastPos = transform.position;
        rb = GetComponent<Rigidbody2D>();
        startpos = transform.position;
        collide = transform.parent.transform.GetChild(1).GetComponent<Collide>();

        Target = transform.parent.transform.GetChild(1);
        targetRB = Target.GetComponent<Rigidbody2D>();

        //StartCoroutine(CheckLocation());
    }

    private void Update()
    {
        roundTime -= Time.deltaTime;
        if(roundTime <= 0)
        {
            roundTime = 60;
            AddReward(-20);
        }

/*        targetHeight = Mathf.Max(transform.position.y, Target.position.y) - MathF.Min(Target.position.y, transform.position.y);

        if (targetHeight <= 300)
        {
            AddReward(0.005f);
        }
        else
        {
            AddReward(-0.005f);
        }*/


/*        middleDistance = Vector3.Distance(transform.position, startpos);

        if (middleDistance < 200)
        {
            AddReward(0.00003f);
        }
        else
        {
            AddReward(-0.00003f);
        }*/

        targetDistance = Vector3.Distance(transform.position, Target.position);

    }

    public override void OnEpisodeBegin()
    {
        //transform.position = startpos;

        if (!humanVSAi)
        {
            collide.AIStart();
        }
    }

    public override void CollectObservations(VectorSensor sensor)
    {
        // Target and Agent positions
        sensor.AddObservation((Vector2)Target.localPosition);
        sensor.AddObservation((Vector2)transform.localPosition);

        //velocity
        sensor.AddObservation(targetRB.velocity);
        sensor.AddObservation(rb.velocity);

        sensor.AddObservation(targetHeight);
        sensor.AddObservation(targetDistance);
    }

    public override void OnActionReceived(ActionBuffers actions)
    {
        float moveY = actions.ContinuousActions[0];

        transform.position += new Vector3(0, moveY) * moveSpeed * Time.deltaTime * 1200;
        AddReward(-0.0003f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "BounderyDown")
        {
            AddReward(-5f);
        }

        if (collision.gameObject.name == "BounderyUp")
        {
            AddReward(-5f);
        }

        if (collision.gameObject.name == "Target")
        {
            AddReward(5f);
        }
    }

    public override void Heuristic(in ActionBuffers actionsOut)
    {
        ActionSegment<float> continuousActionsOut = actionsOut.ContinuousActions;
        continuousActionsOut[0] = Input.GetAxis("Vertical");
    }

    public void EndRound()
    {
        roundTime = 60;

        if(envManager != null){
            if (GetCumulativeReward() >= envManager.highestRewardThisTrainingSession)
            {
                highestReward = GetCumulativeReward();

                envManager.SetNewHighes(highestReward);
            }
        }
        transform.position = startpos;

        EndEpisode();

    }

/*    public IEnumerator CheckLocation()
    {
        yield return new WaitForSeconds(1.5f);
        if (lastPos == transform.position)
        {
            AddReward(-0.1f);
        }
        else
        {
            AddReward(0.1f);
        }

        StartCoroutine(CheckLocation());
    }*/
}
