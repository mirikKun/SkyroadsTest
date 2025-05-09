using System;
using System.Collections;
using Cinemachine;
using Code.Gameplay.Input.Service;
using DG.Tweening;
using UnityEngine;
using Zenject;

namespace Project.Code.Gameplay.Player.Behaviours
{
    public class PlayerEffects: MonoBehaviour
    {

        [SerializeField] private CinemachineVirtualCamera _camera;
        [SerializeField] private Transform _view;
        [SerializeField] private PlayerMover _playerMover;
        
        [SerializeField] private float _boostedFov = 70f;
        [SerializeField] private float _normalFov = 40f;
        [SerializeField] private float _fovChangeDuration = 0.5f;
        [SerializeField] private Ease _fovChangeEase = Ease.OutBack;

        
        [SerializeField] private float _playerStartTiltDuration = 0.5f;
        [SerializeField] private float _playerEndTiltDuration = 0.2f;
        [SerializeField] private float _playerTiltAngle = 16f;
        [SerializeField] private Ease _playerStartTiltEase = Ease.OutBack;
        [SerializeField] private Ease _playerEndTiltEase = Ease.OutBack;
        
        private Tween _fovChangeTween;
        private Tween _rotateTween;

        private void Start()
        {
            _playerMover.StartedBoost+= OnPlayerStartedBoost;
            _playerMover.StoppedBoost+= OnPlayerStoppedBoost;
            _playerMover.SideMoveStarted+= OnPlayerSideMoveStarted;
            _playerMover.SideMoveStopped+= OnPlayerSideMoveStopped;
        }

        private void OnPlayerSideMoveStopped()
        {
            RotatePlayerView(0, _playerEndTiltDuration, _playerEndTiltEase);

        }

        private void OnPlayerSideMoveStarted(int direction)
        {
            RotatePlayerView(direction* _playerTiltAngle, _playerStartTiltDuration, _playerStartTiltEase);
        }


        private void OnPlayerStoppedBoost()
        {
            ChangeFOV(_camera,_normalFov, _fovChangeDuration);
        }

        private void OnPlayerStartedBoost()
        {
            ChangeFOV(_camera,_boostedFov, _fovChangeDuration);

        }
        private void RotatePlayerView(float angle, float duration,Ease ease)
        {
            if (_rotateTween != null && _rotateTween.IsActive())
                _rotateTween.Kill();
            _rotateTween = _view.DORotate(new Vector3(0, 0, angle), duration).SetEase(ease);
        }

        private void ChangeFOV(CinemachineVirtualCamera vcam, float targetFov, float duration)
        {
            if (_fovChangeTween != null && _fovChangeTween.IsActive())
                _fovChangeTween.Kill();
            _fovChangeTween = DOTween.To(
                    () => vcam.m_Lens.FieldOfView,
                    (value) => vcam.m_Lens.FieldOfView = value,
                    targetFov,
                    duration
                )
                .SetEase(_fovChangeEase)
                .OnComplete(() => _fovChangeTween = null);
        }
     
    }
}