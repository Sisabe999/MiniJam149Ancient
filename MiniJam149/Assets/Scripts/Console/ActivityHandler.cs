using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ActivityHandler : MonoBehaviour
{
    [SerializeField] private Transform[] keyPositions;
    [SerializeField] private SymbolBehavior[] symbolsPrefabs;

    private HashSet<SymbolBehavior> spawnedSymbols = new HashSet<SymbolBehavior>();

    private float velocity;

    private HashSet<SymbolBehavior> checkList = new HashSet<SymbolBehavior>();

    private Coroutine gameCycleCoroutine;

    private bool spawnFinishedFlag;

    private void Update()
    {
        CheckButtonPressed();
    }

    public void StartGame(float speed)
    {
        spawnFinishedFlag = false;
        
        velocity = speed;

        gameCycleCoroutine = StartCoroutine(GameCycle());
    }

    private void GenerateKey()
    {
        int randomSymbol = Random.Range(0, 4);

        SymbolBehavior symbol = Instantiate(symbolsPrefabs[randomSymbol], keyPositions[randomSymbol].position, Quaternion.identity);
        symbol.Initialize(velocity, randomSymbol);

        spawnedSymbols.Add(symbol);
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

        spawnFinishedFlag = true;

    }

    private void CheckButtonPressed()
    {
        if (Input.anyKeyDown)
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                var searchFilter = checkList.Where(x => x.ID.Equals(0));

                if (searchFilter.Count() > 0)
                {
                    SymbolBehavior item = searchFilter.First();
                    AudioManager.instance.Play("Symbol 1");
                    checkList.Remove(item);
                    spawnedSymbols.Remove(item);

                    if (spawnFinishedFlag && spawnedSymbols.Count == 0)
                    {
                        Invoke(nameof(ActivityFinished), 2f);
                        AudioManager.instance.Play("Win");
                    }

                    Destroy(item.gameObject);
                }
                else
                {
                    ActivityLose(); 
                }
            }

            if (Input.GetKeyDown(KeyCode.W))
            {
                var searchFilter = checkList.Where(x => x.ID.Equals(1));

                if (searchFilter.Count() > 0)
                {
                    SymbolBehavior item = searchFilter.First();
                    AudioManager.instance.Play("Symbol 2");
                    checkList.Remove(item);
                    spawnedSymbols.Remove(item);

                    if (spawnFinishedFlag && spawnedSymbols.Count == 0)
                    {
                        Invoke(nameof(ActivityFinished), 2f);
                        AudioManager.instance.Play("Win");
                    }

                    Destroy(item.gameObject);
                }
                else
                {
                    ActivityLose();
                }
            }

            if (Input.GetKeyDown(KeyCode.E))
            {
                var searchFilter = checkList.Where(x => x.ID.Equals(2));

                if (searchFilter.Count() > 0)
                {
                    SymbolBehavior item = searchFilter.First();
                    AudioManager.instance.Play("Symbol 3");
                    checkList.Remove(item);
                    spawnedSymbols.Remove(item);

                    if(spawnFinishedFlag && spawnedSymbols.Count == 0)
                    {
                        Invoke(nameof(ActivityFinished), 2f);
                        AudioManager.instance.Play("Win");
                    }

                    Destroy(item.gameObject);
                }
                else
                {
                    ActivityLose();                    
                }
            }

            if (Input.GetKeyDown(KeyCode.R))
            {
                var searchFilter = checkList.Where(x => x.ID.Equals(3));

                if (searchFilter.Count() > 0)
                {
                    SymbolBehavior item = searchFilter.First();
                    AudioManager.instance.Play("Symbol 4");
                    checkList.Remove(item);
                    spawnedSymbols.Remove(item);

                    if (spawnFinishedFlag && spawnedSymbols.Count == 0)
                    {
                        Invoke(nameof(ActivityFinished), 2f);
                        AudioManager.instance.Play("Win");

                    }

                    Destroy(item.gameObject);
                }
                else
                {
                    ActivityLose();
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

    public void ActivityLose()
    {
        StopCoroutine(gameCycleCoroutine);

        gameCycleCoroutine = null;

        AudioManager.instance.Play("Lose");

        foreach (SymbolBehavior s in spawnedSymbols)
        {
            s.StopSymbol();

            s.transform.DOScale(Vector3.zero, 1.5f).onComplete += () =>
            {
                Destroy(s.gameObject);
            };
        }

        spawnedSymbols.Clear();

        GameManager.Instance.SendActivityState(false);
    }

    private void ActivityFinished()
    {
        GameManager.Instance.SendActivityState(true);
    }
}
