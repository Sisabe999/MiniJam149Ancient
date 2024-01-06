using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathZone : MonoBehaviour
{

    [SerializeField] private ActivityHandler activityHandler;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Destroy(collision.gameObject);

        activityHandler.ActivityFinished();
    }
}
