using UnityEngine;
using UnityEngine.Events;

namespace AI.Utility
{
    [System.Serializable]
    public class Action
    {
        [SerializeField] private string _ID;
        public string ID
        {
            get { return _ID; }
        }

        [SerializeField] private Consideration[] _Considerations;
        public Consideration[] Considerations
        {
            get { return _Considerations; }
        }

        [SerializeField] private UnityEvent _OnExecuted;
        public UnityEvent OnExecuted
        {
            get { return _OnExecuted; }
        }

        private float _ActionScore;
        public float ActionScore
        {
            get { return _ActionScore;  }
            set { _ActionScore = value; }
        }

        // TODO: score calculation could be better according to GW2 methods
        public float EvaluateAction()
        {
            // 0
            _ActionScore = 0f;
            
            // Add by each consideration score and it's weight
            for (int i = 0; i < Considerations.Length; i++)
            {
                float considerationScore = Considerations[i].UtilityScore * Considerations[i].Weight;
                //Debug.Log(Considerations[i].Property + " " + considerationScore);

                if (considerationScore > 0)
                    _ActionScore += considerationScore;
                else 
                    return 0; // We ignore even if there's one 0 score
            }

            // Average
            _ActionScore = _ActionScore / Considerations.Length;

            // Also return the current action score
            return _ActionScore;
        }
    }
}
