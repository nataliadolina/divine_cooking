using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KinematicTrigger : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Food food;
        if (collision.TryGetComponent<Food>(out food))
        {
            food.MakeStatic();
        }
    }
}
