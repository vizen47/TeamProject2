using DG.Tweening;
using System;
using TMPro;
using UnityEngine;

namespace KDH.Scripts.UIs
{
    public class NextDayFadeInOutUI : FadeInOutUI
    {
        [SerializeField] private TextMeshProUGUI _message;

        public override void Fade(float fadeInTime, float fadeOutTime, float relateTime)
        {
            
        }
        
        public void Fade(float fadeInTime, float fadeOutTime, float relateTime, string message, Action action)
        {
            Sequence seq = DOTween.Sequence();
            
            _targetRect.gameObject.SetActive(true);
            _message.gameObject.SetActive(true);
            
            _message.SetText(message);
            
            seq.Append(_targetRect.DOFade(1, fadeInTime).SetEase(Ease.Linear));
            seq.Append(_message.DOFade(1, fadeInTime).SetEase(Ease.Linear));
            
            seq.AppendInterval(relateTime);
            seq.AppendCallback(() => action());
            
            seq.Append(_message.DOFade(0, fadeOutTime).SetEase(Ease.Linear));
            seq.Append(_targetRect.DOFade(0, fadeOutTime).SetEase(Ease.Linear));
            seq.AppendCallback(() => _targetRect.gameObject.SetActive(false));
            seq.AppendCallback(() => _message.gameObject.SetActive(false));
        }
    }
}