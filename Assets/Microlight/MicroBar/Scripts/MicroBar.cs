using System;
using System.Collections.Generic;
using UnityEngine;

namespace Microlight.MicroBar {
    // ****************************************************************************************************
    // Base script for MicroBar, holds base functionality of the health bar
    // Manager for other features added to the health bar
    // ****************************************************************************************************
    public class MicroBar : MonoBehaviour {
        bool isInitalized = false;   // Safety system that gives out warning if health bar has not been initialized

        #region Properties
        float _currentValue = 1f;   // Current value of HP
        public float CurrentValue {
            get => _currentValue;
            private set {
                _currentValue = Mathf.Clamp(value, 0f, MaxValue);
                OnCurrentValueChange?.Invoke(this);
                Debugger.UpdatedCurrentHP(_currentValue);
            }
        }
        float _maxValue = 1f;   // Max value of HP
        public float MaxValue {
            get => _maxValue;
            private set {
                _maxValue = value;
                OnMaxValueChange?.Invoke(this);
                Debugger.UpdatedMaxHP(_maxValue);
            }
        }
        public float HPPercent {
            get => Mathf.Clamp01(CurrentValue / MaxValue);
        }
        static MaxHealthCalculation _maxHealthCalculation = MaxHealthCalculation.FollowIncrease;
        public static MaxHealthCalculation MaxHealthCalculation {
            get => _maxHealthCalculation;
            private set {
                _maxHealthCalculation = value;
            }
        }
        #endregion

        #region Events
        public event Action<MicroBar> OnMaxValueChange;
        public event Action<MicroBar> OnCurrentValueChange;

        internal event Action<bool, UpdateAnim> OnNewMax;
        internal event Action<bool, UpdateAnim> OnBarUpdate;
        internal event Action BarDestroyed;
        internal event Action OnDefaultValuesSnapshot;
        #endregion

        [SerializeField] List<MicroBarAnimation> animations;   // Stores all animations for the bar
        public List<MicroBarAnimation> Animations => animations;

        private void OnDestroy() {
            Debugger.DestroyedBar(this);
            BarDestroyed?.Invoke();
        }

        #region API
        /// <summary>
        /// Initializes health bar with max value and makes it useable
        /// </summary>
        /// <param name="maxValue">Health bar max value</param>
        public void Initialize(float maxValue) {
            isInitalized = true;
            _currentValue = maxValue;
            SetNewMaxHP(maxValue);

            foreach(MicroBarAnimation x in animations) {
                if(!x.Initialize(this)) {
                    isInitalized = false;
                    Debugger.InitializationFailed(this);
                    return;
                }
            }
            Debugger.InitializedBar(this);
        }
        /// <summary>
        /// Changes the way how is max health calculated.
        /// For more info look in MaxHealthCalculation enum
        /// </summary>
        public static void ChangeMaxHealthCalculation(MaxHealthCalculation maxHealthCalculation) {
            MaxHealthCalculation = maxHealthCalculation;
        }
        /// <summary>
        /// Sets new max value for the health bar
        /// </summary>
        /// <param name="skipAnimation">If true, it will skip all animations for changing max health bar value</param>
        public void SetNewMaxHP(float newMaxValue, bool skipAnimation = false) {
            if(!isInitalized) {
                Debugger.NotInitialized();
                return;
            }
            // Don't allow negative max HP
            if(newMaxValue < 1f) {
                newMaxValue = 1f;
            }

            float previousHealth = MaxValue;
            float previousProportion = CurrentValue / MaxValue;
            MaxValue = newMaxValue;
            float change = MaxValue - previousHealth;

            // Calculate new CurrentHealth
            float newHP;
            switch(MaxHealthCalculation) {
                case MaxHealthCalculation.Follow:
                    // Don't allow current HP to go lower than 0
                    newHP = CurrentValue + change;
                    if(newHP < 1) {
                        newHP = 1;
                    }
                    CurrentValue = newHP;
                    break;
                case MaxHealthCalculation.FollowIncrease:
                    if(change > 0) {
                        CurrentValue += change;
                    }
                    break;
                case MaxHealthCalculation.Proportional:
                    // Don't allow current HP to go lower than 0
                    newHP = MaxValue * previousProportion;
                    if(newHP < 1) {
                        newHP = 1;
                    }
                    CurrentValue = newHP;
                    break;
                case MaxHealthCalculation.Keep:
                default:
                    break;
            }

            OnNewMax?.Invoke(skipAnimation, UpdateAnim.MaxHP);
            UpdateBar(CurrentValue, true);
        }
        /// <summary>
        /// Updates the bar's visuals to a new HP value
        /// </summary>
        /// <param name="newHP">New HP value for the bar</param>
        /// <param name="skipAnimation">Will animation be skipped and visual set right to the new value?</param>
        /// <param name="updateType">Type of the animation that will be played (Damage, Heal, etc...)</param>
        public void UpdateBar(float newHP, bool skipAnimation = false, UpdateAnim updateType = UpdateAnim.Damage) {
            if(!isInitalized) {
                Debugger.NotInitialized();
                return;
            }
            CurrentValue = newHP;
            OnBarUpdate?.Invoke(skipAnimation, updateType);
        }
        /// <summary>
        /// Animates the bar's visuals to a new HP value
        /// </summary>
        /// <param name="newHP">New HP value for the bar</param>
        /// <param name="updateType">Type of the animation that will be played (Damage, Heal, etc...)</param>
        public void UpdateBar(float newHP, UpdateAnim updateType) {
            UpdateBar(newHP, false, updateType);
        }
        /// <summary>
        /// Animates the bar's visuals to a new HP value and always uses Damage animation
        /// </summary>
        /// <param name="newHP">New HP value for the bar</param>
        public void UpdateBar(float newHP) {
            UpdateBar(newHP, false, UpdateAnim.Damage);
        }
        /// <summary>
        /// Snapshots current values of the image and rect transform to be used as new default values
        /// </summary>
        public void SnapshotDefaultValues() {
            if(!isInitalized) {
                Debugger.NotInitialized();
                return;
            }
            OnDefaultValuesSnapshot?.Invoke();
            Debugger.DefaultValuesSnapshoted();
        }
        #endregion
    }
}