using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SliceTrigger : MonoBehaviour
{
    [SerializeField]
    private SliceDirection sliceDirection;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Food food;
        if (collision.TryGetComponent(out food))
        {
            food.Slice(sliceDirection);
        }
    }
}
