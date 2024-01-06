using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ActivityHandler : MonoBehaviour
{
    [SerializeField] private Transform[] keyPositions;
    [SerializeField] private SymbolBehavior[] symbolsPrefabs;

    private HashSet<GameObject> spawnedSymbols = new HashSet<GameObject>();

    private float velocity;

    private HashSet<SymbolBehavior> checkList = new HashSet<SymbolBehavior>();

    private void Update()
    {
        CheckButtonPressed();
    }

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

    private void CheckButtonPressed()
    {
        if (Input.anyKeyDown)
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                SymbolBehavior item = checkList.Where(x => x.ID.Equals(0)).First();

                if(item != null)
                {
                    AudioManager.instance.Play("Symbol 1");
                    checkList.Remove(item);
                    Destroy(item.gameObject);
                }
                else
                {
                    ActivityFinished(); 
                }
            }

            if (Input.GetKeyDown(KeyCode.W))
            {
                SymbolBehavior item = checkList.Where(x => x.ID.Equals(1)).First();

                if (item != null)
                {
                    AudioManager.instance.Play("Symbol 2");
                    checkList.Remove(item);
                    Destroy(item.gameObject);
                }
                else
                {
                    ActivityFinished();
                }
            }

            if (Input.GetKeyDown(KeyCode.E))
            {
                SymbolBehavior item = checkList.Where(x => x.ID.Equals(2)).First();

                if (item != null)
                {
                    AudioManager.instance.Play("Symbol 3");
                    checkList.Remove(item);
                    Destroy(item.gameObject);
                }
                else
                {
                    ActivityFinished();                    
                }
            }

            if (Input.GetKeyDown(KeyCode.R))
            {
                SymbolBehavior item = checkList.Where(x => x.ID.Equals(3)).First();

                if (item != null)
                {
                    AudioManager.instance.Play("Symbol 4");
                    checkList.Remove(item);
                    Destroy(item.gameObject);
                }
                else
                {
                    ActivityFinished();
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        SymbolBehavior symbol = collision.GetComponent<SymbolBehavior>();

        checkList.Add(symbol);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        SymbolBehavior symbol = collision.GetComponent<SymbolBehavior>();

        checkList.Remove(symbol);
    }

    public void ActivityFinished()
    {
        foreach (GameObject s in spawnedSymbols)
        {
            s.transform.DOScale(Vector3.zero, 1.5f).onComplete += () =>
            {
                Destroy(s);
            };
        }

        spawnedSymbols.Clear();

        GameManager.Instance.SendActivityState(false);
    }
}
