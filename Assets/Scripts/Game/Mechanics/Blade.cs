using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Blade : MonoBehaviour
{
    [SerializeField]
    private Direction sliceDirection;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Food food;
        if (collision.TryGetComponent<Food>(out food))
        {
            food.Slice(sliceDirection, this);
        }
    }
}
