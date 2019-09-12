using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointManagerScript : MonoBehaviour
{

    public GameObject[] checkpointsArr;
    int activeCheckpointID = 0;

    // Start is called before the first frame update

    public void SetActiveCheckpoint(int newID)
    {
        activeCheckpointID = newID;
    }

    public GameObject GetActiveCheckpoint()
    {
        return checkpointsArr[activeCheckpointID];
    }
}
