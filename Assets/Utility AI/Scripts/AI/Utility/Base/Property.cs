using UnityEngine;
using System.Collections;

namespace AI.Utility
{
    [System.Serializable]
    public abstract class Property
    {
        [Tooltip("Must be unique")]
        [SerializeField] private string _ID;
        public string ID
        {
            get { return _ID; }
        }
        
        // Which type is it
        public enum PropertyType
        {
            None, Float, Bool, Int,
        };

        protected PropertyType _Type;
        public PropertyType Type
        {
            get { return _Type; }
        }

        // Normalized value to be used on calculation
        public abstract float NormalizedValue { get; }
    }
}
