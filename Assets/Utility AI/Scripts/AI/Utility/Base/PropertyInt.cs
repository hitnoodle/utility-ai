using UnityEngine;
using System.Collections;

namespace AI.Utility
{
    [System.Serializable]
    public class PropertyInt : Property
    {
        [SerializeField] private int _MinValue = 0;
        public int MinValue
        {
            get { return _MinValue; }
        }

        [SerializeField] private int _MaxValue = 100;
        public int MaxValue
        {
            get { return _MaxValue; }
            set
            {
                _MaxValue = value;
            }
        }

        [SerializeField] private int _Value = 100;
        public int Value
        {
            get { return _Value; }
            set
            {
                _Value = Mathf.Clamp(value, _MinValue, _MaxValue);
            }
        }

        public PropertyInt()
        {
            _Type = PropertyType.Int;
        }

        public PropertyInt(int minValue, int maxValue, int currentValue)
        {
            _Type = PropertyType.Int;
            _MinValue = minValue;
            _MaxValue = maxValue;

            Value = currentValue;
        }

        public override float NormalizedValue
        {
            get
            {
                return (float)_Value / (float)_MaxValue;
            }
        }
    }
}
