using UnityEngine;
using System.Collections.Generic;

namespace AI.Utility
{
    [System.Serializable]
    public class UtilityProperties
    {
        [Tooltip("For decimal properties")]
        [SerializeField] private List<PropertyFloat> _FloatProperties;

        [Tooltip("For properties with True/False value")]
        [SerializeField] private List<PropertyBool> _BooleanProperties;

        [Tooltip("For simple numeric properties")]
        [SerializeField] private List<PropertyInt> _IntegerProperties;

        // Cache for performance
        private Dictionary<string, Property> _PropertiesCache;

        // Use this for initialization
        public void Initialize()
        {
            // Just add to cache
            _PropertiesCache = new Dictionary<string, Property>();
            for (int i = 0; i < _FloatProperties.Count; i++) _PropertiesCache.Add(_FloatProperties[i].ID, _FloatProperties[i]);
            for (int i = 0; i < _BooleanProperties.Count; i++) _PropertiesCache.Add(_BooleanProperties[i].ID, _BooleanProperties[i]);
            for (int i = 0; i < _IntegerProperties.Count; i++) _PropertiesCache.Add(_IntegerProperties[i].ID, _IntegerProperties[i]);
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

        #region Dynamic Properties

        public void AddFloatProperty(string propertyID, float minValue, float maxValue, float currentValue)
        {
            // Create
            PropertyFloat pFloat = new PropertyFloat(minValue, maxValue, currentValue);
            pFloat.ID = propertyID;

            // Add
            _FloatProperties.Add(pFloat);
            _PropertiesCache.Add(pFloat.ID, pFloat);
        }

        public void AddBoolProperty(string propertyID, bool currentValue)
        {
            // Create
            PropertyBool pBool = new PropertyBool(currentValue);
            pBool.ID = propertyID;

            // Add
            _BooleanProperties.Add(pBool);
            _PropertiesCache.Add(pBool.ID, pBool);
        }

        public void AddIntProperty(string propertyID, int minValue, int maxValue, int currentValue)
        {
            // Create
            PropertyInt pInt = new PropertyInt(minValue, maxValue, currentValue);
            pInt.ID = propertyID;

            // Add
            _IntegerProperties.Add(pInt);
            _PropertiesCache.Add(pInt.ID, pInt);
        }

        public void RemoveProperty(string propertyID)
        {
            // Do we have this property?
            Property p;
            _PropertiesCache.TryGetValue(propertyID, out p);

            if (p != null)
            {
                // Delete from list
                if (p.Type == Property.PropertyType.Float)
                    _FloatProperties.Remove((PropertyFloat)p);
                else if (p.Type == Property.PropertyType.Bool)
                    _BooleanProperties.Remove((PropertyBool)p);
                else if (p.Type == Property.PropertyType.Int)
                    _IntegerProperties.Remove((PropertyInt)p);

                // and from Dictionary
                _PropertiesCache.Remove(propertyID);
            }
        }

        #endregion
    }
}