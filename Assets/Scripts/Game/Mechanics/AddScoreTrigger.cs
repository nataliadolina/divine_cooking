using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class AddScoreTrigger : MonoBehaviour
{
    [Inject]
    private TotalScoreSlider _totalScoreSlider;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
    }
}
