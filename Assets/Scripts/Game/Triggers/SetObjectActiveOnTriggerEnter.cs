using System.Collections;
using System.Linq;
using UnityEngine;
using Zenject;

public class SetObjectActiveOnTriggerEnter : MonoBehaviour
{
    [SerializeField]
    private GameObject gameObj;
    [SerializeField]
    private SliceZone sliceZone;

    [Inject]
    private void Construct()
    {
        sliceZone.onSlice += SetGameObjInactive;
        gameObj.SetActive(false);
    }

    private void OnDestroy()
    {
        Destroy(gameObj);
        sliceZone.onSlice -= SetGameObjInactive;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Food food;
        if (collision.TryGetComponent<Food>(out food))
        {
            gameObj.SetActive(true);
        }
    }

    private void SetGameObjInactive()
    {
        gameObj.SetActive(false);
        Destroy(gameObject);
    }
}
