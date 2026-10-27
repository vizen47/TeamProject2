using System;
using System.Threading.Tasks;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace KDH.Scripts.UI
{
    public class FadeInOutUI : MonoBehaviour
    {
        [SerializeField] private Image targetRect;
        
        public void Fade(float fadeInTime, float fadeOutTime, float relateTime)
        {
            Sequence seq = DOTween.Sequence();
            targetRect.gameObject.SetActive(true);
            seq.Append(targetRect.DOFade(1, fadeInTime).SetEase(Ease.Linear));
            seq.AppendInterval(relateTime);
            seq.Append(targetRect.DOFade(0, fadeOutTime).SetEase(Ease.Linear));
            seq.AppendCallback(() => targetRect.gameObject.SetActive(false));
        }
    }
}
