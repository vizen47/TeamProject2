using System;
using KDH.Scripts.CoreLib;

namespace KDH.Scripts
{
    public class ResourceManager : MonoSingleton<ResourceManager>
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
