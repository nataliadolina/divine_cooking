using UnityEngine;
using DG.Tweening;
using System.Collections.Generic;
using System.Collections;
using Zenject;
using System;
using System.Linq;

public class Food : Actor, IFood, IPoolObject
{
    private Color _splashColor;
    private CookingAction[] _cookingActions;

    [Inject]
    private SoundManager _soundManager;
    [Inject]
    private SplashParticles.Pool _splashParticlesPool;
    [Inject]
    private DictionaryPool<ActorType, Food, Food.Factory> _pool;
    [Inject]
    private SlicesSpawner _slicesSpawner;

    private CookingAction _currentCookingAction;
    private int _currentCookingIndex = 0;
    private float _currentScore;
    private float _currentTotalScore;

    private List<UnityEngine.Object> _blades = new List<UnityEngine.Object>();

    private FoodCookingProgressSlider _progressSlider;

    private Vector2 _size;

    public float CurrentScore
    {
        get
        {
            
            return _currentScore;
        }

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

    public float CurrentTotalScore
    {
        get
        {
            return _currentTotalScore * _partOfOne;
        }
        private set
        {
            _currentTotalScore = value;
        }
    }
    
    public int MaxScore { get => _cookingActions.Length; }
    public CookingAction[] CookingActions { get => _cookingActions; }
    public FoodCookingProgressSlider ProgressSlider { set => _progressSlider = value; }

    [Inject]
    private void Construct(FoodSettings[] foodsSettings)
    {
        var foodSettings = foodsSettings.Where(x => x.ActorType == actorType).FirstOrDefault();
        _splashColor = foodSettings.SplashColor;
        _cookingActions = foodSettings.CookingActions;
        _size = GetComponent<SpriteRenderer>().sprite.bounds.size;
    }

#region IPoolObject

    public void OnCreate()
    {
        _currentCookingAction = CookingAction.Empty;
    }

    public void OnSpawn()
    {
        _partOfOne = 1;
        _currentCookingAction = _cookingActions[_currentCookingIndex];
        _rootInstanceId = Extensions.GetUniqueId();
        _currentScore = 0;
        CurrentTotalScore = 0;
    }

    public void OnDespawn()
    {
        _currentCookingIndex = 0;
        _collider.enabled = true;
        _blades.Clear();
        _ricochets.Clear();
        Dispose();
        ChangePhysics(_initialPhysicsType);
    }

#endregion

    public void Init(FoodArgs foodArgs)
    {
        _progressSlider = foodArgs.FoodCookingProgressSlider;
        _currentCookingAction = foodArgs.CurrentCookingAction;
        _currentCookingIndex = foodArgs.CurrentCookingIndex;
        _currentScore = foodArgs.CurrentScore;

        _blades = foodArgs.Blades.Copy();
        _ricochets = foodArgs.Ricochets.Copy();
        _partOfOne = foodArgs.PartOfOne;
        _rootInstanceId = foodArgs.RootInstanceId;
        _isSlice = true;
        _rigidbody.velocity = foodArgs.Velocity * _partOfOne;
        Direction = foodArgs.Direction;
        Speed = foodArgs.Speed;
        CurrentTotalScore = foodArgs.CurrentTotalScore;
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
            CurrentScore = Mathf.Clamp(_currentScore - 0.5f * _partOfOne, 0, _currentScore);
            CurrentTotalScore = Mathf.Clamp(_currentTotalScore - 0.5f, 0, CurrentScore);
        }

        else if (_currentCookingAction == cookingAction)
        {
            CurrentScore = _currentScore + _partOfOne;
            CurrentTotalScore = _currentTotalScore + 1;
        }

        _currentCookingAction = _currentCookingIndex >= _cookingActions.Length - 1
            ? CookingAction.Empty
            : _cookingActions[_currentCookingIndex + 1];
        _currentCookingIndex = Mathf.Clamp(_currentCookingIndex + 1, 0, _cookingActions.Length - 1);
    }

    public void Slice(Direction direction, UnityEngine.Object blade)
    {
        if (_blades.Contains(blade))
        {
            return;
        }

        var splash = _splashParticlesPool.Spawn(_splashColor);
        splash.SetColor(_splashColor);
        splash.transform.position = transform.position;

        _soundManager.PlayCutSound();
        ToNextAction(CookingAction.Cut);
        _blades.Add(blade);
        _collider.enabled = false;

        _slicesSpawner.Spawn(direction, transform.position, transform.rotation, _size * transform.localScale, new FoodArgs(_progressSlider, _currentCookingAction, _currentCookingIndex, _currentScore, _blades, _partOfOne * 0.5f, _rigidbody.velocity, _rootInstanceId, Direction, Speed, _currentTotalScore, Ricochets));

        if (_partOfOne == 1)
        {
            _pool.Despawn(actorType, this);
        }
        else
        {
            Destroy(gameObject);
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

    [Serializable]
    public struct FoodSettings
    {
        public ActorType ActorType;
        public CookingAction[] CookingActions;
        public Color SplashColor;
    }
}
