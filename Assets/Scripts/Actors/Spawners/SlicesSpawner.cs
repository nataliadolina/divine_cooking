using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlicesSpawner
{
    private SlicesPool _sliceSpawner;
    private Dictionary<Direction, SlicePart[]> _directionSlicePartMap = new Dictionary<Direction, SlicePart[]>();
    public SlicesSpawner(SlicesPool sliceSpawner)
    {
        _sliceSpawner = sliceSpawner;
        _directionSlicePartMap.Add(Direction.Up, new SlicePart[2] {SlicePart.Down, SlicePart.Up });
        _directionSlicePartMap.Add(Direction.Right, new SlicePart[2] {SlicePart.Left, SlicePart.Right });
    }

    public void Spawn(Direction direction, Vector3 position, Vector2 size, FoodArgs args)
    {
        foreach (var part in _directionSlicePartMap[direction])
        {
            Food foodPart = _sliceSpawner.Create(part);
            foodPart.Init(args);
            foodPart.transform.position = position + Slicer.GetLocalPositionOffset(size * 0.5f, part);
            foodPart.transform.parent = null;
            foodPart.gameObject.SetActive(true);
        }
    }
}
