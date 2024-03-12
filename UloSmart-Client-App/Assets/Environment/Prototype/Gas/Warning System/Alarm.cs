using UnityEngine;

using DG.Tweening;

namespace GasWarningSystem
{
    [AddComponentMenu("Gas Warning System/" + nameof(Alarm))]
    public partial class Alarm : MonoBehaviour
    {
        private void Start()
        {
            audioSource = GetComponent<AudioSource>();
        }

        public void StartAlarm()
        {
            if (isPlaying) return;

            killFadeTweener();

            if (audioSource.isPlaying) audioSource.Stop();

            audioSource.Play();
            fadeTweener = audioSource.DOFade(1.0f, fadeDuration).OnComplete( () => killFadeTweener() );
            isPlaying = true;
        }

        public void Stop()
        {
            if (!isPlaying) return;

            killFadeTweener();
            fadeTweener = audioSource.DOFade(0.0f, fadeDuration)
            .OnComplete(
                () => 
                {
                    killFadeTweener();
                    audioSource.Stop();
                }
            );

            isPlaying = false;
        }

        private void killFadeTweener()
        {
            if (!fadeTweener.IsActive()) return;

            fadeTweener.Kill();
            fadeTweener = null;
        }

        public bool IsPlaying => isPlaying;



        private bool isPlaying = false;
        [SerializeField]
        private float fadeDuration = 5.0f;
        private Tweener fadeTweener = null;

        private AudioSource audioSource = null;
    }

#if UNITY_EDITOR
    public partial class Alarm : MonoBehaviour
    {
        [ContextMenu("Start Alarm")]
        private void StartAlarmContextMenu() => StartAlarm();

        [ContextMenu("Stop Alarm")]
        private void StopAlarmContextMenu() => Stop();
    }
#endif
}
