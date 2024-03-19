using UnityEngine;

public class BombSpawner : IActorPoolSpawner
{
    public ActorGroupType ActorGroupType => ActorGroupType.Bomb;
    private Bomb.Pool _bombPool;
    public BombSpawner(Bomb.Pool bombPool)
    {
        _bombPool = bombPool;
    }

    public void FillPool(ActorType actor, int num)
    {

    }

    public GameObject Spawn(ActorType actor, Vector3 position)
    {
        Bomb bomb = _bombPool.Spawn();
        bomb.transform.position = position;
        return bomb.gameObject;
    }
}
