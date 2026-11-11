using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace KDH.Scripts.UIs
{
    public class FadeInOutUI : MonoBehaviour
    {
        [SerializeField] protected Image _targetRect;

        public virtual void Fade(float fadeInTime, float fadeOutTime, float relateTime)
        {
            Sequence seq = DOTween.Sequence();
            _targetRect.gameObject.SetActive(true);
            seq.Append(_targetRect.DOFade(1, fadeInTime).SetEase(Ease.Linear));
            seq.AppendInterval(relateTime);
            seq.Append(_targetRect.DOFade(0, fadeOutTime).SetEase(Ease.Linear));
            seq.AppendCallback(() => _targetRect.gameObject.SetActive(false));
        }
    }
}
