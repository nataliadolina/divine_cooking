using UnityEngine;
using System.Collections.Generic;

public enum Direction
{
    Up,
    Right
}

public enum SlicePart
{
    Up,
    Down,
    Right,
    Left
}

public class Slicer
{
    private Sprite _sprite;

    public Slicer(Sprite sprite)
    {
        _sprite = sprite;
    }

    public Sprite[] CreateSliceSprites(Direction direction)
    {
        Rect rect = _sprite.rect;
        Rect rect1 = direction == Direction.Up
            ? CreateDownRect(rect)
            : CreateLeftRect(rect);
        Rect rect2 = direction == Direction.Up
            ? CreateUpRect(rect)
            : CreateRightRect(rect);

        var sprite1 = Sprite.Create(_sprite.texture, rect1, Vector2.one * 0.5f);
        var sprite2 = Sprite.Create(_sprite.texture, rect2, Vector2.one * 0.5f);

        return new Sprite[] {sprite1, sprite2 };
    }

    public Sprite CreateSprite(SlicePart slicePart)
    {
        Rect rect = _sprite.rect;
        Rect newRect = rect;
        switch (slicePart)
        {
            case SlicePart.Left:
                newRect = CreateLeftRect(rect);
                break;
            case SlicePart.Right:
                newRect = CreateRightRect(rect);
                break;
            case SlicePart.Down:
                newRect = CreateDownRect(rect);
                break;
            case SlicePart.Up:
                newRect = CreateUpRect(rect);
                break;
        }

        return Sprite.Create(_sprite.texture, newRect, Vector2.one * 0.5f);
    }

    public static Vector3 GetLocalPositionOffset(Vector2 size, SlicePart slicePart)
    {
        switch (slicePart)
        {
            case SlicePart.Up:
                return new Vector3(0, size.y * 0.5f, 0);
            case SlicePart.Down:
                return new Vector3(0, -size.y * 0.5f, 0);
            case SlicePart.Left:
                return new Vector3(-size.x * 0.5f, 0, 0);
            case SlicePart.Right:
                return new Vector3(size.x * 0.5f, 0, 0);
        }
        return Vector3.zero;
    }

    private Rect CreateDownRect(Rect rect)
    {
        return new Rect(new Vector2(rect.xMin, rect.yMin), new Vector2(rect.width, rect.height / 2));
    }

    private Rect CreateUpRect(Rect rect)
    {
        return new Rect(new Vector2(rect.xMin, rect.yMin + rect.height / 2), new Vector2(rect.width, rect.height / 2));
    }

    private Rect CreateLeftRect(Rect rect)
    {
        return new Rect(new Vector2(rect.xMin, rect.yMin), new Vector2(rect.width / 2, rect.height));
    }

    private Rect CreateRightRect(Rect rect)
    {
        return new Rect(new Vector2(rect.xMin + rect.width / 2, rect.yMin), new Vector2(rect.width / 2, rect.height));
    }
}
