using UnityEngine;

public class ProjectileTrajectory : MonoBehaviour
{
    [SerializeField] private int _pointsCount;
    [SerializeField] private SpriteRenderer _pointPrefab;
    [SerializeField] private float _timeSpacing;
    [SerializeField] private AnimationCurve _pointsWidth;

    private Transform[] _points;

    private void Start()
    {
        _points = new Transform[_pointsCount];

        float scaleStep = 1f / _pointsCount;

        for (int i = 0; i < _pointsCount; i++)
        {
            _points[i] = Instantiate(_pointPrefab, transform).transform;
            _points[i].localScale = Vector3.one * _pointsWidth.Evaluate(scaleStep * i);
        }

        Hide();
    }

    public void Draw(Vector2 origin, Vector2 velocity)
    {   
        for (int pointIndex = 0; pointIndex < _points.Length; pointIndex++) 
        {
            float pointTime = (pointIndex + 1) * _timeSpacing;
            _points[pointIndex].position = origin + velocity * pointTime + Physics2D.gravity * Mathf.Pow(pointTime, 2) / 2;
        }
    }

    public void Show()
    {
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }
}