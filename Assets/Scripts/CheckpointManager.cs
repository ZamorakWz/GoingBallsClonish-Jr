using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointManager : MonoBehaviour
{
    [SerializeField]
    private GameObject currentCheckpoint;
    public Vector3 spawnPoint;
    public bool isPlayerPassCheckpoint = false;

    public List<Transform> checkpointTransforms = new List<Transform>();
    [SerializeField]
    private Transform playerTransform;
    private BallSelect _ballSelect;
    // Start is called before the first frame update
    void Start()
    {
        _ballSelect = FindObjectOfType<BallSelect>();
        playerTransform = _ballSelect.selectedBallTransform;

        Checkpoint[] checkpoints = FindObjectsOfType<Checkpoint>();

        // FinishLine'larý uzaklýðýna göre sýrala
        System.Array.Sort(checkpoints, (a, b) =>
        {
            float distanceA = Vector3.Distance(playerTransform.position, a.transform.position);
            float distanceB = Vector3.Distance(playerTransform.position, b.transform.position);
            return distanceA.CompareTo(distanceB);
        });

        foreach (Checkpoint cp in checkpoints)
        {
            checkpointTransforms.Add(cp.transform);
        }
        if (checkpointTransforms.Count > 0)
        {
            currentCheckpoint = checkpointTransforms[0].gameObject;
            spawnPoint = currentCheckpoint.transform.position;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Checkpoint"))
        {
            isPlayerPassCheckpoint = true;
            if (currentCheckpoint != null && checkpointTransforms.Count > 0)
            {
                currentCheckpoint = checkpointTransforms[0].gameObject;
                spawnPoint = currentCheckpoint.transform.position;
                currentCheckpoint.SetActive(false);
                checkpointTransforms.RemoveAt(0);
            }
        }
    }
}
