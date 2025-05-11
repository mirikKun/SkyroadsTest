using Cinemachine;
using Code.Gameplay.Input.Service;
using Code.Gameplay.Levels;
using Code.Gameplay.Player.Systems;
using DG.Tweening;
using UnityEngine;
using Zenject;

namespace Code.Gameplay.Player.Behaviours
{
    public class PlayerEffects: MonoBehaviour
    {
        [SerializeField] private Transform _view;

        [Space]
        [SerializeField] private float _boostedFov = 70f;

        [SerializeField] private float _normalFov = 40f;
        [SerializeField] private float _fovChangeDuration = 0.5f;
        [SerializeField] private Ease _fovChangeEase = Ease.OutBack;

        [Space]

        
        [SerializeField] private float _playerStartTiltDuration = 0.5f;

        [SerializeField] private float _playerEndTiltDuration = 0.2f;
        [SerializeField] private float _playerTiltAngle = 16f;
        [SerializeField] private Ease _playerStartTiltEase = Ease.OutBack;
        [SerializeField] private Ease _playerEndTiltEase = Ease.OutBack;

        [Space]
        [SerializeField] private TrailRenderer _trailEffect ;


        private CinemachineVirtualCamera _camera;
        private Tween _fovChangeTween;
        private Tween _rotateTween;
        private IPlayerMoverSystem _playerMoverSystem;

        [Inject]
        private void Construct(IPlayerMoverSystem playerMoverSystem, ILevelDataProvider levelDataProvider, IInputService inputService)
        {
            _playerMoverSystem = playerMoverSystem;
            _camera = levelDataProvider.MainCamera;
      
        }

        private void Start()
        {
            _playerMoverSystem.StartedBoost+= OnPlayerStartedBoost;
            _playerMoverSystem.StoppedBoost+= OnPlayerStoppedBoost;
            _playerMoverSystem.SideMoveStarted+= OnPlayerSideMoveStarted;
            _playerMoverSystem.SideMoveStopped+= OnPlayerSideMoveStopped;
        }
        private void OnDestroy()
        {
            _playerMoverSystem.StartedBoost-= OnPlayerStartedBoost;
            _playerMoverSystem.StoppedBoost-= OnPlayerStoppedBoost;
            _playerMoverSystem.SideMoveStarted-= OnPlayerSideMoveStarted;
            _playerMoverSystem.SideMoveStopped-= OnPlayerSideMoveStopped;
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
            _trailEffect.enabled=false;
        }
        private void OnPlayerStartedBoost()
        {
            ChangeFOV(_camera,_boostedFov, _fovChangeDuration);
            _trailEffect.enabled=true;

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