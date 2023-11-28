using System.Collections;
using UnityEngine;

public class SliceZone : MonoBehaviour
{
    private Blade _blade;

    private void Start()
    {
        _blade = GetComponentInChildren<Blade>();
    }

    private void OnMouseDown()
    {
        _blade.Cut();
    }
}
