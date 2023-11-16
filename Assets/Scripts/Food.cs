using UnityEngine;
using DG.Tweening;
using System.Collections.Generic;
using System.Linq;
using EzySlice;

public class Food : MonoBehaviour
{
    private GameObject[] _lastSlices;
    private int counter = 0;

    private void Start()
    {
        _lastSlices = new GameObject[1] { gameObject };
    }

    public void Rotate(float rotateDegrees)
    {
        transform.DORotate(new Vector3(0, 0, rotateDegrees), 0.5f);
    }

    public void Slice(Vector3 planeWorldDirection)
    {
        GameObject[] newSlices = new GameObject[_lastSlices.Length * 2];
        for (int i = 0; i < _lastSlices.Length; i++)
        {
            _lastSlices[i].GetComponent<SpriteRenderer>().enabled = false;
            GameObject[] slices = transform.gameObject.SliceInstantiate(transform.position, planeWorldDirection);
            for (int j = 0; j < slices.Length; j++)
            {
                var slice = slices[j];
                slice.transform.SetParent(transform);
                slice.AddComponent<Rigidbody2D>().isKinematic = true;
                slice.AddComponent<PolygonCollider2D>().isTrigger = true;
                newSlices[counter] = slice;
                counter++;
            }
        }
        _lastSlices = newSlices;
    }
}
