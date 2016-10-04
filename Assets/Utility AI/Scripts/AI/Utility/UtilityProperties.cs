using UnityEngine;
using System.Collections.Generic;

namespace AI.Utility
{
    [System.Serializable]
    public class UtilityProperties
    {
        [Tooltip("For decimal properties")]
        [SerializeField] private PropertyFloat[] _FloatProperties;

        [Tooltip("For properties with True/False value")]
        [SerializeField] private PropertyBool[] _BooleanProperties;

        [Tooltip("For simple numeric properties")]
        [SerializeField] private PropertyInt[] _IntegerProperties;

        // Cache for performance
        private Dictionary<string, Property> _PropertiesCache;

        // Use this for initialization
        public void Initialize()
        {
            // Just add to cache
            _PropertiesCache = new Dictionary<string, Property>();
            foreach (Property p in _FloatProperties) _PropertiesCache.Add(p.ID, p);
            foreach (Property p in _BooleanProperties) _PropertiesCache.Add(p.ID, p);
            foreach (Property p in _IntegerProperties) _PropertiesCache.Add(p.ID, p);
        }

        #region Properties

        public Property GetProperty(string id)
        {
            Property p = null;
            _PropertiesCache.TryGetValue(id, out p);

            return p;
        }

        public float GetFloat(string propertyID)
        {
            Property p = GetProperty(propertyID);
            if (p != null && p.Type == Property.PropertyType.Float)
                return ((PropertyFloat)p).Value;

            return -9999f;
        }

        public bool SetFloat(string propertyID, float value)
        {
            Property p = GetProperty(propertyID);
            if (p != null && p.Type == Property.PropertyType.Float)
                ((PropertyFloat)p).Value = value;

            return p != null;
        }

        public bool GetBool(string propertyID)
        {
            Property p = GetProperty(propertyID);
            if (p != null && p.Type == Property.PropertyType.Bool)
                return ((PropertyBool)p).Value;

            return false;
        }

        public bool SetBool(string propertyID, bool value)
        {
            Property p = GetProperty(propertyID);
            if (p != null && p.Type == Property.PropertyType.Bool)
                ((PropertyBool)p).Value = value;

            return p != null;
        }

        public int GetInt(string propertyID)
        {
            Property p = GetProperty(propertyID);
            if (p != null && p.Type == Property.PropertyType.Int)
                return ((PropertyInt)p).Value;

            return -9999;
        }

        public bool SetInt(string propertyID, int value)
        {
            Property p = GetProperty(propertyID);
            if (p != null && p.Type == Property.PropertyType.Int)
                ((PropertyInt)p).Value = value;

            return p != null;
        }

        #endregion
    }
}