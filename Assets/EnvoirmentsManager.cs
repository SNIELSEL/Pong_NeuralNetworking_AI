using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvoirmentsManager : MonoBehaviour
{
    public float highestRewardThisTrainingSession;

    public void SetNewHighes(float newHighest)
    {
        highestRewardThisTrainingSession = newHighest;

        Debug.Log(highestRewardThisTrainingSession);
    }
}
