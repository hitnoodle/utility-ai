using UnityEngine;
using System.Collections;

namespace AI.Utility
{
    [System.Serializable]
    public class PropertyFloat : Property
    {
        [SerializeField] private float _MinValue = 0;
        public float MinValue
        {
            get { return _MinValue; }
        }

        [SerializeField] private float _MaxValue = 100f;
        public float MaxValue
        {
            get { return _MaxValue; }
            set
            {
                _MaxValue = value;
            }
        }

        [SerializeField] private float _Value = 100f;
        public float Value
        {
            get { return _Value; }
            set
            {
                _Value = Mathf.Clamp(value, _MinValue, _MaxValue);
            }
        }

        public PropertyFloat()
        {
            _Type = PropertyType.Float;
        }

        public PropertyFloat(float minValue, float maxValue, float currentValue)
        {
            _Type = PropertyType.Float;
            _MinValue = minValue;
            _MaxValue = maxValue;

            Value = currentValue;
        }

        public override float NormalizedValue
        {
            get
            {
                return _Value / _MaxValue;
            }
        }
    }
}
