using UnityEngine;
using System.Collections;

namespace AI.Utility
{
    [System.Serializable]
    public class PropertyBool : Property
    {
        [SerializeField] private bool _Value = false;
        public bool Value
        {
            get { return _Value;  }
            set { _Value = value; }
        }

        public PropertyBool()
        {
            _Type = PropertyType.Bool;
        }

        public PropertyBool(bool currentValue)
        {
            _Type = PropertyType.Bool;
            _Value = currentValue;
        }

        public override float NormalizedValue
        {
            get { return (_Value) ? 1f : 0f; }
        }
    }
}
