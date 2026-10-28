using DG.Tweening;
using UnityEngine;

namespace KDH.Scripts.UI
{
    public class SlideUpUIMotion : MonoBehaviour
    {
        [SerializeField] private GameObject target;
        [SerializeField] private Vector3 minPos;
        [SerializeField] private Vector3 maxPos;
        [SerializeField] [Range(0.2f, 3f)] private float duration;

        private void SetActive()
        {
            target.SetActive(!target.activeSelf);
        }

        public void ActiveAnimation()
        {
            if (!target.activeSelf)
            {
                target.transform.position = new Vector3(target.transform.position.x, minPos.y, target.transform.position.z);
                SetActive();
                target.transform.DOMoveY(maxPos.y, duration).SetEase(Ease.Linear);
            }
            else
            {
                Sequence seq = DOTween.Sequence();

                seq.Append(target.transform.DOMoveY(minPos.y, 0).SetEase(Ease.Linear));
                seq.AppendCallback(SetActive);
            }
        }
    }
}
