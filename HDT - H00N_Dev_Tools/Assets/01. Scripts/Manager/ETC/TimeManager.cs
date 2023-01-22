using System.Collections;
using UnityEngine;
using System;

namespace H00N.Manager
{
    public class TimeManager : MonoBehaviour
    {
        private TimeManager instance = null;
        public TimeManager Instance
        {
            get
            {
                if (instance == null)
                    instance = FindObjectOfType<TimeManager>();

                return instance;
            }
        }

        private bool onTimeDelaying = false;
        public bool OnTimeDelaying => onTimeDelaying;

        public void SetTimeDelay(float timeScale, float duration, Action onFinishDelaying = null)
        {
            if (onTimeDelaying) return;

            onTimeDelaying = true;
            Time.timeScale = timeScale;

            StartCoroutine(TimeResetCoroutine(duration, onFinishDelaying));
        }

        public void BreakTimeDelay()
        {
            StopAllCoroutines();

            Time.timeScale = 1;
            onTimeDelaying = false;
        }

        private IEnumerator TimeResetCoroutine(float duration, Action onFinishDelaying)
        {
            yield return new WaitForSecondsRealtime(duration);

            Time.timeScale = 1;
            onTimeDelaying = false;

            onFinishDelaying?.Invoke();
        }
    }
}
