using UnityEngine;
using System.Collections;

namespace AI.Utility
{
    [System.Serializable]
    public class Consideration
    {
        // ID of the property
        [SerializeField] private string _Property;
        public string Property
        {
            get { return _Property; }
        }

        // Whether we're using property from local entity or from global entity
        [SerializeField] private bool _IsGlobalProperty = false;
        public bool IsGlobalProperty
        {
            get { return _IsGlobalProperty; }
        }

        // Curve we're using to calculate score
        [SerializeField] private AnimationCurve _Curve;

        // Weight of the score
        [SerializeField] private float _Weight = 1f;
        public float Weight
        {
            get { return _Weight; }
        }

        // Pointer to property class that have the data
        private Property _PropertyCache;

        // For runtime initializing cache data from the agent
        public void Initialize(Property p)
        {
            _PropertyCache = p;
        }

        // Normalized value of current property on this consideration
        public float PropertyScore
        {
            get { return (_PropertyCache != null) ? _PropertyCache.NormalizedValue : 0; }
        }

        // Calculated Y score from X where X = normalized property value using the consideration curve
        public float UtilityScore
        {
            get { return (_PropertyCache != null) ? _Curve.Evaluate(PropertyScore) : 0; }
        }
    }
}