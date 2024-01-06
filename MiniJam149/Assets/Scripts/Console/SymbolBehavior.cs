using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SymbolBehavior : MonoBehaviour
{

    private float velocity;
    public int ID;

    public void Initialize(float speed, int id)
    {
        velocity = speed;
        ID = id;
    }


    private void Update()
    {
         transform.Translate(Vector2.down * velocity * Time.deltaTime);
    }

    public void StopSymbol()
    {
        velocity = 0;
    }
}
