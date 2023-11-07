using UnityEngine;

public class Archer : MonoBehaviour
{
    [SerializeField] private ArcherAnimator _archerAnimation;
    [SerializeField] private Bow _bow;
    [SerializeField] private ArcherMouseInput _input;

    private void Start()
    {
        _archerAnimation.PlayIdle();
    }

    private void OnEnable()
    {
        _archerAnimation.Shoot += _bow.Shoot;
        _input.InputStart += OnInputStart;
        _input.InputAngle += OnInputAngle;
        _input.InputEnd += OnInputEnd;
    }

    private void OnDisable()
    {
        _archerAnimation.Shoot -= _bow.Shoot;
        _input.InputStart -= OnInputStart;
        _input.InputAngle -= OnInputAngle;
        _input.InputEnd -= OnInputEnd;
    }

    private void OnInputAngle()
    {
        _archerAnimation.SetAngle(_input.Angle);

        if (_archerAnimation.State == AnimationID.Aim)
            _bow.Pull(_input.Speed);
    }

    private void OnInputEnd()
    {
        if (_archerAnimation.State == AnimationID.Aim)
            Shoot();
        else
            CancelAttack();
    }

    private void OnInputStart()
    {
        _archerAnimation.PlayAim();
    }

    private void CancelAttack()
    {
        _bow.Pull(0);
        _archerAnimation.PlayIdle();
    }

    private void Shoot()
    {
        _archerAnimation.PlayAttack();
    }
}