using KDH.Scripts.CoreLib;
using KDH.Scripts.UI;
using UnityEngine;

namespace KDH.Scripts.Systems
{
    public class UIManager : MonoSingleton<UIManager>
    {
        [field: SerializeField] public Popup PopupUI { get; private set; }
        [field: SerializeField] public FadeInOutUI FadeInOutUI { get; private set; }
    }
}
