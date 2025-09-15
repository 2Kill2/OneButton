using UnityEngine;

public class TallyManager : MonoBehaviour
{
    [Header("Tally Settings")]
    public GameObject TallyPrefab;
    public Transform TallyContainer;
    public Vector3 SpawnOffset = new Vector3(0.5f, 0f, 0f);

    private int TallyCount = 0;


    public void AddTally()
    {
        // adds 1 to tally count and spawns a new tally mark

        TallyCount++;
        SpawnTally();
    }

    private void SpawnTally()
    {
        // spawns a new tally mark at the next position
        //Vector3 SpawnPos = 
    }


}
