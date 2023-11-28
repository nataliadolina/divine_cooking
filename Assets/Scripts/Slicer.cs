using UnityEngine;
using System.Collections.Generic;

public enum SliceDirection
{
    Up,
    Right
}

public class Slicer
{
    private float _rectRatio;
    private Quaternion _rootRotation;
    private GameObject[] _lastSlices;
    private int _counter = 0;

    public Slicer(Quaternion rootRotation, GameObject rootGameObject, bool adjustScale = true)
    {
        _rootRotation = rootRotation;
        _rectRatio = adjustScale == true ? rootGameObject.transform.localScale.x / 2.5f : rootGameObject.transform.localScale.x;
        _lastSlices = new GameObject[] { rootGameObject };
    }

    private GameObject CreateSlice(string name, SliceDirection direction, Transform parentTransform, float offset, Sprite sprite)
    {
        GameObject slice = new GameObject(name);
        slice.transform.parent = parentTransform;
        slice.AddComponent<SpriteRenderer>().sprite = sprite;
        Vector3 localPosition = direction == SliceDirection.Up
            ? new Vector3(0, offset, 0)
            : new Vector3(offset, 0, 0);
        slice.transform.localPosition = localPosition;
        slice.transform.localRotation = _rootRotation;
        return slice;
    }

    private GameObject[] Slice(SliceDirection direction, GameObject gm)
    {
        SpriteRenderer spriteRenderer = gm.GetComponent<SpriteRenderer>();
        Sprite sprite = spriteRenderer.sprite;
        Rect rect = sprite.rect;

        Rect rect1 = direction == SliceDirection.Up
            ? CreateDownRect(rect)
            : CreateLeftRect(rect);
        Rect rect2 = direction == SliceDirection.Up
            ? CreateUpRect(rect)
            : CreateRightRect(rect);

        var sprite1 = Sprite.Create(sprite.texture, rect1, Vector2.one * 0.5f);
        var sprite2 = Sprite.Create(sprite.texture, rect2, Vector2.one * 0.5f);

        float offset = direction == SliceDirection.Up
            ? sprite.bounds.size.y * 0.25f
            : sprite.bounds.size.x * 0.25f;

        
        GameObject hull1 = CreateSlice(direction == SliceDirection.Up ? "DownHull" : "LeftHull", direction, gm.transform, -offset, sprite1);
        GameObject hull2 = CreateSlice(direction == SliceDirection.Up ? "UpHull" : "RightHull", direction, gm.transform, offset, sprite2);

        return new GameObject[] { hull1, hull2 };
    }
    public void Slice(SliceDirection direction, List<Blade> blades)
    {
        GameObject[] newSlices = new GameObject[_lastSlices.Length * 2];
        for (int i = 0; i < _lastSlices.Length; i++)
        {
            var lastSlice = _lastSlices[i];
            lastSlice.GetComponent<SpriteRenderer>().enabled = false;
            GameObject[] slices = Slice(direction, lastSlice);
            for (int j = 0; j < slices.Length; j++)
            {
                var slice = slices[j];
                slice.AddComponent<Rigidbody2D>();
                slice.transform.localScale *= _rectRatio;
                slice.AddComponent<PolygonCollider2D>();
                Food food = slice.AddComponent<Food>();

                List<Blade> bladesCopy = new List<Blade>();
                foreach (var b in blades)
                {
                    bladesCopy.Add(b);
                }

                food.Blades = bladesCopy;
                food.AdjustScale = false;
                slice.transform.parent = null;
                newSlices[_counter] = slice;
                _counter++;
            }
        }

        _lastSlices = newSlices;
        _counter = 0;
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
