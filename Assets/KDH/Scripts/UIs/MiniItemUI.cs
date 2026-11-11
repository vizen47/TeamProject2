using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MiniItemUI : MonoBehaviour
{
    [field: SerializeField] public Image Icon { get; private set; }
    [field: SerializeField] public TextMeshProUGUI Count { get; private set; }
}
