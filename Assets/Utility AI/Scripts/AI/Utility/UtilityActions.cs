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
            for (int i = 0; i < _Actions.Length; i++) _ActionsCache.Add(_Actions[i].ID, _Actions[i]);
        }

        public void InitializeConsiderations(UtilityProperties localProperties, UtilityProperties globalProperties)
        {
            for (int i = 0; i < _Actions.Length; i++)
            {
                // Initialize all consideration with the appropriate property
                Consideration[] considerations = _Actions[i].Considerations;
                for (int j = 0; j < considerations.Length; j++)
                {
                    Consideration c = considerations[j];
                    if (!c.IsGlobalProperty)
                        c.Initialize(localProperties.GetProperty(c.Property));
                    else
                        c.Initialize(globalProperties.GetProperty(c.Property));
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
