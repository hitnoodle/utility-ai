using UnityEngine;
using UnityEngine.Events;
using System.Linq;

namespace AI.Utility
{
    public class UtilityAgent : MonoBehaviour
    {
        [System.Serializable]
        public enum EvaluateMethod
        {
            TopScore,
            WeightedRandom,
            Debug,
        };

        [Header("Data")]

        [SerializeField] private UtilityProperties _UtilityProperties;
        [SerializeField] private UtilityActions _UtilityActions;

        [Header("Calculation")]

        [SerializeField] private EvaluateMethod _Method;
        [SerializeField] private string _DebugAction;

        [Header("Runtime Attributes")]

        [SerializeField] private string _CurrentActionID;
        private Action _CurrentAction;

        // Delegates

        public delegate void OnActionSelected(string actionID);
        private OnActionSelected _OnActionSelected;

        // Use this for initialization
        void Awake()
        {
            _UtilityProperties.Initialize();

            _UtilityActions.Initialize();
            _UtilityActions.InitializeConsiderations(_UtilityProperties);
        }

        #region Delegates

        public void SetGlobalActionDelegate(OnActionSelected onActionSelected)
        {
            _OnActionSelected = onActionSelected;
        }

        public void SetActionDelegate(string actionID, UnityAction onActionExecuted)
        {
            Action action = _UtilityActions.GetAction(actionID);
            if (action != null)
                action.OnExecuted.AddListener(onActionExecuted);
        }

        #endregion

        #region Action-related functions

        private void ExecuteCurrentAction()
        {
            //Debug.Log("[Utility] Agent " + gameObject.name + " is now doing action " + _CurrentAction.Name);

            if (_CurrentAction != null)
            {
                _CurrentActionID = _CurrentAction.ID;

                if (_OnActionSelected != null)
                    _OnActionSelected(_CurrentActionID);

                if (_CurrentAction.OnExecuted != null)
                    _CurrentAction.OnExecuted.Invoke();
            }
            else
            {
                _CurrentActionID = "NO_ACTION_SELECTED";
                //Debug.LogWarning();
            }
        }

        #endregion

        #region Evaluation

        public void Evaluate()
        {
            if (_Method == EvaluateMethod.TopScore)
                _CurrentAction = EvaluateUsingTopScore();
            else if (_Method == EvaluateMethod.WeightedRandom)
                _CurrentAction = EvaluateUsingWeightedRandom();
            else if (_Method == EvaluateMethod.Debug)
                _CurrentAction = EvaluateUsingDebug();

            ExecuteCurrentAction();
        }

        private Action EvaluateUsingTopScore()
        {
            Action topAction = null;
            float topActionScore = 0f;

            for (int i = 0; i < _UtilityActions.Actions.Length; i++)
            {
                float currentScore = _UtilityActions.Actions[i].EvaluateAction();
                //Debug.Log("[Utility] Action " + Actions[i].Name + " score is " + currentScore);

                if (currentScore > topActionScore)
                {
                    topAction = _UtilityActions.Actions[i];
                    topActionScore = currentScore;
                }
            }

            //Debug.Log("[Utility] Top score is " + topActionScore);
            return topAction;
        }

        private Action EvaluateUsingWeightedRandom()
        {
            Action selectedAction = null;
            float[] scores = new float[_UtilityActions.Actions.Length];
            float selectedScore, totalScore = 0f;

            for (int i = 0; i < _UtilityActions.Actions.Length; i++)
            {
                scores[i] = _UtilityActions.Actions[i].EvaluateAction();
                //Debug.Log("[Utility] Action " + Actions[i].Name + " score is " + scores[i]);
                
                // We don't need to challenge perfect score
                // SCRATCH THAT WE ACTUALY ARE
                //if (scores[i] == 1) return Actions[i];

                totalScore += scores[i];
            }

            selectedScore = Random.Range(0, totalScore);
            //Debug.Log("[Utility] Selected score is " + selectedScore);

            for (int i = 0; i < scores.Length; i++)
            {
                if (selectedScore <= scores[i])
                {
                    selectedAction = _UtilityActions.Actions[i];
                    break;
                }

                selectedScore -= scores[i];
            }

            return selectedAction;
        }

        private Action EvaluateUsingDebug()
        {
            Action topAction = _UtilityActions.Actions.Where(x => x.ID.Equals(_DebugAction)).FirstOrDefault();
            return topAction;
        }

        #endregion
    }
}
