using System;
using System.Collections;
using System.Collections.Generic;
using DesignPattern.Observer;
using TMPro;
using UnityEngine;

namespace DesignPattern.NumberCounter
{
    [RequireComponent(typeof(TextMeshProUGUI))]
    public class NumberCounter : MonoBehaviour
    {
        public  TextMeshProUGUI text;
        public  int             counterFPS   = 60;
        public  float           duration     = 1.5f;
        public  string          numberFormat = "N0";
        private int             _value;

        private Action<object> _setValue;

        private void OnEnable()
        {
            _setValue = param => SetValue((int) param);
            
            // ObserverManager<EventID>.Instance.RegisterEvent(EventID.SetScore, _setValue);
        }

        private void OnDisable()
        {
            // ObserverManager<EventID>.Instance.RemoveEvent(EventID.SetScore, _setValue);
        }

        public int Value
        {
            get => _value;
            set
            {
                UpdateText(value);
                _value = value;
            }
        }

        private Coroutine _countingCoroutine;

        private void Awake()
        {
            text                  = GetComponent<TextMeshProUGUI>();
            text.enableAutoSizing = true;
            text.alignment        = TextAlignmentOptions.CaplineRight;
        }

        private void UpdateText(int newValue)
        {
            if (_countingCoroutine != null)
            {
                StopCoroutine(_countingCoroutine);
            }

            _countingCoroutine = StartCoroutine(CountText(newValue));
        }

        private IEnumerator CountText(int newValue)
        {
            WaitForSeconds wait          = new WaitForSeconds(1f / counterFPS);
            int            previousValue = _value;
            int stepAmount = newValue > previousValue
                                 ? Mathf.CeilToInt((newValue  - previousValue) / (counterFPS * duration))
                                 : Mathf.FloorToInt((newValue - previousValue) / (counterFPS * duration));

            if (newValue > previousValue)
            {
                while (newValue > previousValue)
                {
                    previousValue += stepAmount;
                    if (newValue < previousValue)
                    {
                        previousValue = newValue;
                    }

                    text.SetText(previousValue.ToString(numberFormat));
                    yield return wait;
                }
            }
            else
            {
                while (newValue < previousValue)
                {
                    previousValue += stepAmount;
                    if (newValue > previousValue)
                    {
                        previousValue = newValue;
                    }

                    text.SetText(previousValue.ToString(numberFormat));
                    yield return wait;
                }
            }
        }
        // Call method
        private void SetValue(int newValue)
        {
            Value = newValue;
        }
    }
}