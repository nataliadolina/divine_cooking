using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISpringy
{
    public void Ricochet(Vector3 ricochetDirection);

    public void MoveToAim(Vector3 aimPosition, float speed);
}
