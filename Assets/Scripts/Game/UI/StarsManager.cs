using System.Collections;
using UnityEngine;
using Zenject;
using UnityEngine.UI;
using System;

public class StarsManager : MonoBehaviour
{
    [SerializeField]
    private RectTransform startStarPositionAnimation;
    [SerializeField]
    private float oneStarAnimationTime;
    [SerializeField]
    private float timeBetweenStars;

    [SerializeField]
    private Image[] aimStarsImages;

    [Inject]
    private StarUI.Pool _starUIPool;

    [Inject]
    private TotalScoreSlider _totalScoreSlider;

    private Vector3 _startPos;

    private Sprite _uiStarSprite;
    private int _currentLevel;

    [Inject]
    private void Construct(Settings settings, LevelSettingsInstaller.Settings levelSettings)
    {
        _startPos = startStarPositionAnimation.position;
        _uiStarSprite = settings.UIStarSprite;
        _currentLevel = levelSettings.NumLevel;
    }

    private void Start()
    {
        StartCoroutine(AnimateStars());
    }

    private void AnimateCurrentStar(int currentStarCount)
    {
        Image image = aimStarsImages[currentStarCount];
        RectTransform rectTransform = image.rectTransform;
        Vector2 rectPosition = rectTransform.position;
        Vector2 size = rectTransform.rect.size;
        StarUIArgs starUIArgs = new StarUIArgs(size, rectTransform.rotation, new Vector3(rectPosition.x, _startPos.y, 0), rectPosition, () => image.sprite = _uiStarSprite);

        StarUI starUI = _starUIPool.Spawn();
        starUI.Init(starUIArgs);
        starUI.Animate(oneStarAnimationTime);
    }

    private IEnumerator AnimateStars()
    {
        float currentTime = 0;
        int currentStarCount = 0;
        while (currentStarCount < _totalScoreSlider.NumStars)
        {
            currentTime += Time.deltaTime;
            if (currentTime >= timeBetweenStars)
            {
                AnimateCurrentStar(currentStarCount);
                currentStarCount++;
                currentTime = 0;
            }
            yield return new WaitForEndOfFrame();
        }
        yield return null;
    }

    [Serializable]
    public struct Settings
    {
        public Sprite UIStarSprite;
    }
}
