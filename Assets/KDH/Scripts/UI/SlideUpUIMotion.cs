using DG.Tweening;
using UnityEngine;

namespace KDH.Scripts.UI
{
    public class SlideUpUIMotion : MonoBehaviour
    {
        [SerializeField] private RectTransform target;
        [SerializeField] private float minY;
        [SerializeField] private float maxY;
        [SerializeField, Range(0.2f, 3f)] private float duration = 0.5f;

        public void ActiveAnimation()
        {
            target.DOKill();

            if (!target.gameObject.activeSelf)
            {
                target.anchoredPosition = new Vector2(target.anchoredPosition.x, minY);
                target.gameObject.SetActive(true);
                target.DOAnchorPosY(maxY, duration).SetEase(Ease.Linear);
            }
            else
            {
                target.gameObject.SetActive(false);
            }
        }
    }
}