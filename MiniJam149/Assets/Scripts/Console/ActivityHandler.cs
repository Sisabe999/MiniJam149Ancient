using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivityHandler : MonoBehaviour
{
    [SerializeField] private Transform[] keyPositions;
    [SerializeField] private SymbolBehavior[] symbolsPrefabs;

    private List<GameObject> spawnedSymbols = new List<GameObject>();

    [SerializeField] private float velocity;

    public void StartGame(float speed)
    {
        velocity = speed;

        StartCoroutine(GameCycle());

    }

    private void GenerateKey()
    {
        int randomSymbol = Random.Range(0, 4);

        SymbolBehavior symbol = Instantiate(symbolsPrefabs[randomSymbol], keyPositions[randomSymbol].position, Quaternion.identity);
        symbol.Initialize(velocity, randomSymbol);

        spawnedSymbols.Add(symbol.gameObject);
    }

    private IEnumerator GameCycle()
    {
        int TOTALTIMER = 6;

        float spawnTimer = (TOTALTIMER - 1)/Mathf.Pow(2, velocity);

        float count = 0;

        while (count < TOTALTIMER)
        {
            count += spawnTimer;

            GenerateKey();

            yield return new WaitForSeconds(spawnTimer);
        }
    }
}
