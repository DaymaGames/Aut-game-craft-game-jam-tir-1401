using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class SpawnManager : MonoBehaviour
{
    public List<Round<Virus>> rounds;

    public bool autoRound = true;

    public float roundDelay = 5;

    [HideInInspector] public int currentRound = 0;
    
    [HideInInspector] public bool inRound = false;

    private void Start()
    {
        if (autoRound)
            StartRound(currentRound);
    }

    public bool StartRound(int roundIndex)
    {
        if (roundIndex < rounds.Count && roundIndex >= 0)
        {
            if (roundIndex == 0)
                StartCoroutine(SpawnRound(rounds[roundIndex]));
            else
                StartCoroutine(SpawnRound(rounds[roundIndex], roundDelay));
            return true;
        }
        return false;
    }

    IEnumerator SpawnRound(Round<Virus> round, float startDelay = 0)
    {
        inRound = true;

        if (startDelay > 0)
            yield return new WaitForSeconds(startDelay);

        Vector2 spawnPos = round.spawnPointManager.GetRandomPosition();

        int spawnedViruses = 0;

        for (int i = 0; i < round.toSpawn.Count; i++)
        {
            Vector2 pos = new Vector2();

            if (round.isFixedPos)
            {
                pos = round.fixedSpawnPos[i].position;
            }
            else
            {
                pos = spawnPos;
            }

            Virus virus = Spawn(round.toSpawn[i], pos);

            spawnedViruses++;

            virus.OnDieAction += () => { spawnedViruses--; };
            
            yield return new WaitForSeconds(round.delayBetweenSpawns);
        }

        while (spawnedViruses > 0)
        {
            yield return null;
        }

        currentRound++;

        if (autoRound)
        {
            if (StartRound(currentRound))
            {
                yield break;
            }
        }

        inRound = false;
    }

    public Virus Spawn(Virus prefab, Vector2 position)
    {
        return Instantiate(prefab, position, Quaternion.identity);
    }
}
[System.Serializable]
public class Round<T> where T : Object
{
    public List<T> toSpawn = new List<T>();
    [HideIf(nameof(isFixedPos))]
    public SpawnPointManager spawnPointManager;
    public float delayBetweenSpawns = 3;
    [HideInInspector] public Vector2 spawnPos = Vector2.zero;
    public void SetPos(Vector2 point)
    {
        spawnPos = point;
    }

    public bool isFixedPos = false;
    [ShowIf(nameof(isFixedPos))]
    public List<Transform> fixedSpawnPos = new List<Transform>();
}