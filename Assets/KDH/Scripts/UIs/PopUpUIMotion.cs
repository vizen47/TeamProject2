using DG.Tweening;
using UnityEngine;

namespace KDH.Scripts.UIs
{
    public class PopUpUIMotion : MonoBehaviour
    {
        [SerializeField] private GameObject target;
        [SerializeField] private Vector3 minSize;
        [SerializeField] private Vector3 maxSize;
        [SerializeField] [Range(0.1f, 3f)] private float duration;

        private void SetActive()
        {
            target.SetActive(!target.activeSelf);
        }

        public void ActiveAnimation()
        {   
            if (!target.activeSelf)
            {
                SetActive();
                target.transform.DOScale(maxSize, duration).SetEase(Ease.Linear);
            }
            else
            {
                Sequence seq = DOTween.Sequence();

                seq.Append(target.transform.DOScale(minSize, 0).SetEase(Ease.Linear));
                seq.AppendCallback(SetActive);
            }
        }

        public void ActiveTrueAnimation()
        {
            SetActive();
            target.transform.DOScale(maxSize, duration).SetEase(Ease.Linear);
        }

        public void ActiveFalseAnimation()
        {
            Sequence seq = DOTween.Sequence();

            seq.Append(target.transform.DOScale(minSize, 0).SetEase(Ease.Linear));
            seq.AppendCallback(SetActive);
        }

        public void NormalizeAnimation()
        {
            Sequence seq = DOTween.Sequence();

            seq.Append(target.transform.DOScale(minSize, duration).SetEase(Ease.Linear));
            seq.AppendCallback(SetActive);
        }
    }
}
