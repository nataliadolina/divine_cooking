using UnityEngine;
using DG.Tweening;
using System.Collections.Generic;
using System.Collections;

public enum FoodType
{
    Food1,
    Food2, 
    Food3
}

public class Food : MonoBehaviour
{
    [SerializeField]
    private FoodType foodType;

    [SerializeField]
    private CookingAction[] cookingActions;

    [SerializeField]
    private bool adjustScale;

    [SerializeField]
    private SpawnerType[] availableSpawnerTypes;

    private CookingAction _currentCookingAction;
    private int _currentCookingIndex = 0;
    private float _currentScore;

    private float _scorePerOneAction;

    private List<Object> _blades = new List<Object>();
    private Slicer _slicer;
    private Collider2D _collider;

    private FoodCookingProgressSlider _progressSlider;

    private int _rootInstanceId;
    private Vector2 _size;
    private Rigidbody2D _rigidbody;
    private float CurrentScore
    {
        set
        {
            if (value >= _currentScore)
            {
                _progressSlider.AddScore(value - _currentScore);
            }
            else
            {
                _progressSlider.SubScore(_currentScore - value);
            }

            _currentScore = value;
        }
    }

    public int RootInstanceId { get => _rootInstanceId; }
    public FoodType FoodType { get => foodType; }
    public int MaxScore { get => cookingActions.Length; }
    public CookingAction[] CookingActions { get => cookingActions; }
    public SpawnerType[] AvailableSpawnerTypes { get => availableSpawnerTypes; }
    public FoodCookingProgressSlider ProgressSlider { set => _progressSlider = value; }
    
    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _collider = GetComponent<Collider2D>();
        _slicer = new Slicer(transform.rotation, gameObject, adjustScale);
        _size = GetComponent<SpriteRenderer>().sprite.rect.size;
    }

    public void InitOnCreate(Transform parentTransform)
    {
        transform.parent = parentTransform;
        _scorePerOneAction = 1;
        _currentCookingAction = cookingActions[_currentCookingIndex];
        _rootInstanceId = GetInstanceID();
    }

    public void Init(FoodArgs foodArgs)
    {
        foodType = foodArgs.FoodType;
        _progressSlider = foodArgs.FoodCookingProgressSlider;
        cookingActions = foodArgs.CookingActions;
        _currentCookingAction = foodArgs.CurrentCookingAction;
        _currentCookingIndex = foodArgs.CurrentCookingIndex;
        _currentScore = foodArgs.CurrentScore;
        _blades = foodArgs.Blades;
        _scorePerOneAction = foodArgs.ScorePerOneAction;
        _rootInstanceId = foodArgs.RootInstanceId;
        adjustScale = foodArgs.AdjustScale;
    }

    public void AddForce(Vector3 direction, float force)
    {
        _rigidbody.AddForce(direction * force, ForceMode2D.Impulse);
    }

    public void SetSize(Vector2 size)
    {
        var ratioX = size.x / _size.x;
        var ratioY = size.y / _size.y;
        var scale = transform.localScale;

        transform.localScale = new Vector3(scale.x * ratioX, scale.y * ratioY, scale.z);
    }

    public void MakeStatic()
    {
        _rigidbody.bodyType = RigidbodyType2D.Static;
        _collider.enabled = false;
    }

    public void Rotate(float rotateDegrees)
    {
        if (transform == null)
        {
            return;
        }

        transform.DORotate(new Vector3(0, 0, rotateDegrees), 0.5f);
    }

    private void ToNextAction(CookingAction cookingAction)
    {
        if (_currentCookingAction == CookingAction.None)
        {
            CurrentScore = Mathf.Clamp(_currentScore - 0.5f * _scorePerOneAction, 0, _currentScore);
        }

        else if (_currentCookingAction == cookingAction)
        {
            CurrentScore = _currentScore + _scorePerOneAction;
        }

        _currentCookingAction = _currentCookingIndex >= cookingActions.Length - 1
            ? CookingAction.None
            : cookingActions[_currentCookingIndex + 1];
        _currentCookingIndex = Mathf.Clamp(_currentCookingIndex + 1, 0, cookingActions.Length - 1);
    }

    public void Slice(Direction direction, Object blade)
    {
        if (_blades.Contains(blade))
        {
            return;
        }

        
        ToNextAction(CookingAction.Cut);
        _blades.Add(blade);
        _slicer.Slice(direction, new FoodArgs(foodType, _progressSlider, cookingActions, _currentCookingAction, _currentCookingIndex, _currentScore, _blades, _scorePerOneAction * 0.5f, RootInstanceId, _rigidbody.velocity));
        _collider.enabled = false;
        StartCoroutine(WaitToDestroyGameObject());
    }

    private IEnumerator WaitToDestroyGameObject()
    {
        yield return new WaitForSeconds(0.5f);
        Destroy(gameObject);
    }
}
