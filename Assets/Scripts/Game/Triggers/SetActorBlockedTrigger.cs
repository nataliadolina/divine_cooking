using UnityEngine;

public class SetActorBlockedTrigger : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        IActor actor;
        if (other.TryGetComponent(out actor))
        {
            actor.IsBlocked = true;
            other.gameObject.layer = 7;
        }
    }
}
