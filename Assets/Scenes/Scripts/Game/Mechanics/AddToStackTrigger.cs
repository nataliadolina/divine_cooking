using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddToStackTrigger : MonoBehaviour
{
    [SerializeField]
    private Transform stackStartPoint;
    [SerializeField]
    private Vector2 blockInStackSizePixels;

    [SerializeField]
    private int numBlocksPerX;

    private Vector3 _blockInStackStartPosition;
    private int _blocksCount;
    private float _blockSizeUnitsX;
    private float _blockSizeUnitsY;

    private void Start()
    {
        _blockInStackStartPosition = stackStartPoint.localPosition;
        _blockSizeUnitsX = blockInStackSizePixels.x / 100;
        _blockSizeUnitsY = blockInStackSizePixels.y / 100;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Food food;
        if (other.TryGetComponent(out food))
        {
            AddFoodPartToStack(food);
        }
    }

    private void AddFoodPartToStack(Food food)
    {
        Transform foodTransform = food.transform;

        foodTransform.parent = transform;
        foodTransform.localRotation = Quaternion.identity;
        Vector3 position = GetPositionInStack();
        foodTransform.localPosition = position;

        food.SetSize(blockInStackSizePixels);
        food. MakeStatic();

        _blocksCount++;
    }

    private Vector2 GetPositionInStack()
    {
        float x = _blockInStackStartPosition.x + _blocksCount % numBlocksPerX * _blockSizeUnitsX * 2;
        float y = _blockInStackStartPosition.y + _blocksCount / numBlocksPerX * _blockSizeUnitsY * 2;
        return new Vector2(x, y);
    }
}
