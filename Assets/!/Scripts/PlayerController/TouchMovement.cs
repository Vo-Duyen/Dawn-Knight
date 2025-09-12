using System;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;
using UnityEngine.InputSystem.EnhancedTouch;
using UnityEngine.UI;
using Touch = UnityEngine.InputSystem.EnhancedTouch.Touch;

namespace DanlangA
{
    public class TouchMovement : SerializedMonoBehaviour
    {
        private Transform Tf => transform;
        
        [BoxGroup("Core"), OdinSerialize, ReadOnly] private RectTransform _rect;
        [BoxGroup("Core"), OdinSerialize, ReadOnly] private Vector2 _joystickSize;
        [BoxGroup("Core"), OdinSerialize, ReadOnly] private Image _image;
        [BoxGroup("Core"), OdinSerialize, ReadOnly] private RectTransform _knobRect;
        [BoxGroup("Core"), OdinSerialize, ReadOnly, InfoBox("Default: None", InfoMessageType.Warning)] private Finger _moveFinger;
        [BoxGroup("Core"), OdinSerialize, ReadOnly] private float _maxMove;
        [BoxGroup("Core"), OdinSerialize, Required] private Player _player;
        [BoxGroup("Core"), OdinSerialize, ReadOnly] private Vector2 _moveAmount;
        
        [Button("Setup")]
        private void Setup()
        {
            _rect = GetComponent<RectTransform>();
            _joystickSize = _rect.sizeDelta;
            _image = GetComponent<Image>();

            _knobRect = Tf.GetChild(0).GetComponent<RectTransform>();
            _maxMove = _joystickSize.x / 2;
        }
        
        private void OnEnable()
        {
            EnhancedTouchSupport.Enable();
            Touch.onFingerDown += HandleFingerDown;
            Touch.onFingerMove += HandleFingerMove;
            Touch.onFingerUp += HandleFingerUp;
        }

        private void OnDisable()
        {
            Touch.onFingerDown -= HandleFingerDown;
            Touch.onFingerMove -= HandleFingerMove;
            Touch.onFingerUp -= HandleFingerUp;
            EnhancedTouchSupport.Disable();
        }

        private void HandleFingerDown(Finger finger)
        {
            if (_moveFinger != null || !(finger.screenPosition.x < Screen.width / 2f) || !(finger.screenPosition.x >= 0) || !(finger.screenPosition.y >= 0) || !(finger.screenPosition.y <= Screen.height)) return;
            _moveFinger = finger;
            _image.enabled = true;
            _rect.anchoredPosition = GetStartPosition(finger.screenPosition);
            _knobRect.gameObject.SetActive(true);
        }

        private void HandleFingerMove(Finger finger)
        {
            if (finger != _moveFinger) return;
            var curTouch = finger.currentTouch;
            
            if (Vector2.Distance(_rect.anchoredPosition, curTouch.screenPosition) > _maxMove)
            {
                _knobRect.anchoredPosition = (curTouch.screenPosition - _rect.anchoredPosition).normalized * _maxMove;
            }
            else
            {
                _knobRect.anchoredPosition = curTouch.screenPosition - _rect.anchoredPosition;
            }

            _moveAmount = _knobRect.anchoredPosition / _maxMove;
            
            // TODO: Player Move
            _player.moveAmount = _moveAmount;
        }

        private void HandleFingerUp(Finger finger)
        {
            if (finger != _moveFinger) return;
            _moveFinger = null;
            _image.enabled = false;
            _knobRect.gameObject.SetActive(false);
            
            _moveAmount = Vector2.zero;
            
            // TODO: Player Move
            _player.moveAmount = _moveAmount;
        }

        private Vector2 GetStartPosition(Vector2 pos)
        {
            if (pos.x < _joystickSize.x / 2)
            {
                pos.x = _joystickSize.x / 2;
            }

            if (pos.y < _joystickSize.y / 2)
            {
                pos.y = _joystickSize.y / 2;
            }
            else if (pos.y > Screen.height - _joystickSize.y / 2)
            {
                pos.y = Screen.height - _joystickSize.y / 2;
            }
            
            return pos;
        }
    }
}