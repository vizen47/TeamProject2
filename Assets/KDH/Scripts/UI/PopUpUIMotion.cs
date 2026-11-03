using DG.Tweening;
using UnityEngine;

namespace KDH.Scripts.UI
{
    public class PopUpUIMotion : MonoBehaviour
    {
        [SerializeField] private GameObject target;
        [SerializeField] private Vector3 minSize;
        [SerializeField] private Vector3 maxSize;
        [SerializeField] [Range(0.2f, 3f)] private float duration;

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
    }
}
