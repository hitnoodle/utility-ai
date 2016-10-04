using UnityEngine;
using System.Collections.Generic;

namespace AI.Utility
{
    [System.Serializable]
    public class UtilityActions
    {
        [SerializeField] private Action[] _Actions;
        public Action[] Actions
        {
            get { return _Actions; }
        }

        // Cache for performance
        protected Dictionary<string, Action> _ActionsCache;

        public void Initialize()
        {
            _ActionsCache = new Dictionary<string, Action>();
            foreach (Action a in _Actions) _ActionsCache.Add(a.ID, a);
        }

        public void InitializeConsiderations(UtilityProperties localProperties)
        {
            foreach (Action a in _Actions)
            {
                // Initialize all consideration with the appropriate property
                foreach (Consideration c in a.Considerations)
                {
                    if (!c.IsGlobalProperty)
                        c.Initialize(localProperties.GetProperty(c.Property));
                }
            }
        }

        public Action GetAction(string id)
        {
            Action a = null;
            _ActionsCache.TryGetValue(id, out a);

            return a;
        }
    }
}
