using UnityEngine;
using System.Collections;

namespace AI.Utility
{
    public class UtilityBlackboard : MonoBehaviour
    {
        #region Singleton Handling

        private static UtilityBlackboard _Instance;

        public static UtilityBlackboard Instance
        {
            get
            {
                if (_Instance == null)
                {
                    _Instance = GameObject.FindObjectOfType<UtilityBlackboard>();
                    _Instance._GlobalProperties.Initialize();
                }

                return _Instance;
            }
        }

        #endregion

        [Header("Data")]

        [SerializeField] protected UtilityProperties _GlobalProperties;
        public UtilityProperties GlobalProperties
        {
            get { return _GlobalProperties; }
        }
    }
}
