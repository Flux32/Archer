using Spine;
using Spine.Unity;
using System;
using UnityEngine;
using System.Collections.Generic;

using Event = Spine.Event;
using AnimationState = Spine.AnimationState;

[RequireComponent(typeof(SkeletonAnimation))]
public class ArcherAnimator : MonoBehaviour
{
    [Header("Animations")]
    [SerializeField, SpineAnimation] private string _idleAnimationName;
    [SerializeField, SpineAnimation] private string _attackStartAnimationName;
    [SerializeField, SpineAnimation] private string _attackAimAnimationName;
    [SerializeField, SpineAnimation] private string _attackEndAnimationName;

    [Header("Bones")]
    [SerializeField, SpineBone(dataField: "skeletonAnimation")] private string _gunBoneName;

    [Header("Events")]
    [SerializeField, SpineEvent(dataField: "skeletonAnimation")] private string _shootEventName;

    public AnimationID State { get; private set; } = AnimationID.None;

    private Bone _cursorBone;
    private AnimationState _animationState;
    private Dictionary<string, AnimationID> _animationIDs;

    public event Action Shoot;

    private void Awake()
    {
        SkeletonAnimation skeletonAnimation = GetComponent<SkeletonAnimation>();
        _animationState = skeletonAnimation.AnimationState;
        _cursorBone = skeletonAnimation.Skeleton.FindBone(_gunBoneName);

        _animationIDs = new Dictionary<string, AnimationID>
        {
            [_idleAnimationName] = AnimationID.Idle,
            [_attackStartAnimationName] = AnimationID.AttackStart,
            [_attackAimAnimationName] = AnimationID.Aim,
            [_attackEndAnimationName] = AnimationID.AttackEnd,
        };
    }

    private void OnEnable()
    {
        _animationState.Event += OnAnimationEvent;
        _animationState.Start += OnAnimationStart;
    }

    private void OnDisable()
    {
        _animationState.Event -= OnAnimationEvent;
        _animationState.Start -= OnAnimationStart;
    }

    private void OnAnimationStart(TrackEntry trackEntry)
    {
        State = _animationIDs[trackEntry.Animation.Name];
    }

    private void OnAnimationEvent(TrackEntry trackEntry, Event animationEvent)
    {
        if (animationEvent.Data.Name == _shootEventName)
            Shoot?.Invoke();
    }

    public void PlayIdle()
    {
        _animationState.SetAnimation(0, _idleAnimationName, true);
    }

    public void PlayAttack()
    {
        _animationState.SetAnimation(0, _attackEndAnimationName, false);
        _animationState.AddAnimation(0, _idleAnimationName, true, 0f);
    }

    public void PlayAim()
    {
        _animationState.SetAnimation(0, _attackStartAnimationName, false);
        _animationState.AddAnimation(0, _attackAimAnimationName, false, 0f);
    }

    public void SetAngle(float angle)
    {
        _cursorBone.Rotation = angle;
    }
}