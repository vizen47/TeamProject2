using UnityEngine;

namespace KDH.Scripts
{
    public class ResourceManager : MonoBehaviour
    {
        private int _credit;

        public int Credit
        {
            get => _credit;
            set
            {
                if (value > 0)
                    _credit = value;
            }
        }
    }
}
