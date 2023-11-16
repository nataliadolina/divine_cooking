using UnityEngine;

public class RotateFoodTrigger : MonoBehaviour
{
    [SerializeField]
    private float targetRotation;

    private void OnTriggerEnter2D(Collider2D other)
    {
        Food foodComponent;
        if (other.GetComponent<Collider2D>().TryGetComponent<Food>(out foodComponent))
        {
            foodComponent.Rotate(targetRotation);   
        }
    }
}
