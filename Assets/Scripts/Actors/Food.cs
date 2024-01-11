using UnityEngine;
using DG.Tweening;
using System.Collections.Generic;
using System.Collections;
using Zenject;


public class Food : Actor, IFood, IPoolObject
{
    [SerializeField]
    private CookingAction[] cookingActions;

    [SerializeField]
    private bool adjustScale;

    [SerializeField]
    private Color splashColor;

    [Inject]
    private SoundManager _soundManager;
    [Inject]
    private SplashParticles.Pool _splashParticlesPool;
    [Inject]
    private DictionaryPool<ActorType, Food, Food.Factory> _pool;

    private CookingAction _currentCookingAction;
    private int _currentCookingIndex = 0;
    private float _currentScore;

    private float _scorePerOneAction;

    private List<UnityEngine.Object> _blades = new List<UnityEngine.Object>();
    private Slicer _slicer;
    private Collider2D _collider;

    private FoodCookingProgressSlider _progressSlider;

    private int _rootInstanceId;
    private Vector2 _size;

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
    public int MaxScore { get => cookingActions.Length; }
    public CookingAction[] CookingActions { get => cookingActions; }
    public FoodCookingProgressSlider ProgressSlider { set => _progressSlider = value; }

    private void Start()
    {
        _collider = GetComponent<Collider2D>();
        _slicer = new Slicer(transform.rotation, gameObject, adjustScale);
        _size = GetComponent<SpriteRenderer>().sprite.rect.size;
    }

#region IPoolObject

    public void OnCreate()
    {
        _currentCookingAction = CookingAction.Empty;
    }

    public void OnSpawn()
    {
        _scorePerOneAction = 1;
        _currentCookingAction = cookingActions[_currentCookingIndex];
        _rootInstanceId = Extensions.GetUniqueId();
    }

    public void OnDespawn()
    {
        _currentCookingIndex = 0;
    }

#endregion

    public void Init(FoodArgs foodArgs)
    {
        actorType = foodArgs.ActorType;
        _progressSlider = foodArgs.FoodCookingProgressSlider;
        cookingActions = foodArgs.CookingActions;
        _currentCookingAction = foodArgs.CurrentCookingAction;
        _currentCookingIndex = foodArgs.CurrentCookingIndex;
        _currentScore = foodArgs.CurrentScore;
        _blades = foodArgs.Blades;
        _scorePerOneAction = foodArgs.ScorePerOneAction;
        _rootInstanceId = foodArgs.RootInstanceId;
        splashColor = foodArgs.SplashColor;
        adjustScale = foodArgs.AdjustScale;
        _soundManager = foodArgs.FruitSoundManager;
        _splashParticlesPool = foodArgs.SplashParticlesPool;
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
        if (_currentCookingAction == CookingAction.Empty)
        {
            CurrentScore = Mathf.Clamp(_currentScore - 0.5f * _scorePerOneAction, 0, _currentScore);
        }

        else if (_currentCookingAction == cookingAction)
        {
            CurrentScore = _currentScore + _scorePerOneAction;
        }

        _currentCookingAction = _currentCookingIndex >= cookingActions.Length - 1
            ? CookingAction.Empty
            : cookingActions[_currentCookingIndex + 1];
        _currentCookingIndex = Mathf.Clamp(_currentCookingIndex + 1, 0, cookingActions.Length - 1);
    }

    public void Slice(Direction direction, UnityEngine.Object blade)
    {
        if (_blades.Contains(blade))
        {
            return;
        }

        var splash = _splashParticlesPool.Spawn(splashColor);
        splash.SetColor(splashColor);
        splash.transform.position = transform.position;

        _soundManager.PlayCutSound();
        ToNextAction(CookingAction.Cut);
        _blades.Add(blade);
        _slicer.Slice(direction, new FoodArgs(actorType, _progressSlider, cookingActions, _currentCookingAction, _currentCookingIndex, _currentScore, _blades, _scorePerOneAction * 0.5f, RootInstanceId, splashColor, _soundManager, _splashParticlesPool));
        _collider.enabled = false;

        if (_scorePerOneAction == 1)
        {
            _pool.Despawn(actorType, this);
        }
        else
        {
            StartCoroutine(WaitToDestroyGameObject());
        }
    }

    private IEnumerator WaitToDestroyGameObject()
    {
        yield return new WaitForSeconds(0.5f);
        Destroy(gameObject);
    }

    public class Factory : PlaceholderFactory<UnityEngine.Object, Food>
    {

    }
}
