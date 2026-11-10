using KDH.Scripts.UIs;
using TMPro;
using UnityEngine;

namespace KDH.Scripts.Systems
{
    public class UIManager : MonoBehaviour
    {
        [field: SerializeField] public Popup PopupUI { get; private set; }
        [field: SerializeField] public FadeInOutUI FadeInOutUI { get; private set; }
        [field: SerializeField] public NextDayFadeInOutUI NextDayFadeInOutUI { get; private set; }
        [field: SerializeField] public UpdateCreditText CreditText { get; private set; }
        [field: SerializeField] public HealthText HealthText { get; private set; }
    }
}
