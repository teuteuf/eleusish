using System;
using UnityEngine;

namespace Game
{
    public class TimeManager : MonoBehaviour
    {
        public static TimeManager instance { get; private set; } = null;

        public enum TimeState
        {
            RUNNING,
            PAUSE
        }

        public TimeState state { get; private set; } = TimeState.RUNNING;

        private void Awake()
        {
            if (instance != null)
            {
                Destroy(this);
                return;
            }

            instance = this;
        }

        public void TogglePause()
        {
            state = state == TimeState.RUNNING
                ? TimeState.PAUSE
                : TimeState.RUNNING;
        }

        public float GetDeltaTime()
        {
            return state == TimeState.PAUSE
                ? 0.0f
                : Time.deltaTime;
        }
    }
}