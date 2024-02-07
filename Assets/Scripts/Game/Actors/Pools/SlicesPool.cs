using System.Collections.Generic;
using UnityEngine;


public class SlicesPool : DictionaryPool<SlicePart, Food, Food.Factory>
{
    private SpriteRenderer _spriteRenderer;
    private Slicer _slicer = null;

    public SlicesPool(SpriteRenderer spriteRenderer, Dictionary<SlicePart, UnityEngine.Object> prefabMap, Food.Factory factory) : base(prefabMap, factory)
    {
        _spriteRenderer = spriteRenderer;
    }

    private void CreateSlicer()
    {
        _slicer = new Slicer(_spriteRenderer.sprite);
    }

    public override Food Create(SlicePart key)
    {
        if (_slicer == null)
        {
            CreateSlicer();
        }

        Food obj = _factory.Create(_prefabMap[key]);
        obj.GetComponent<CircleCollider2D>().radius *= 0.5f;
        obj.GetComponent<SpriteRenderer>().sprite = _slicer.CreateSprite(key);
        obj.gameObject.SetActive(false);
        obj.OnCreate();
        _freeObjects.AddNewValue(key, obj);
        return obj;
    }
}
