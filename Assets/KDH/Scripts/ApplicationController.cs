using KDH.Scripts.CoreLib;
using KDH.Scripts.Systems;

namespace KDH.Scripts
{
    public class ApplicationController : MonoSingleton<ApplicationController>
    {
        public void Init(IApplicable applicable)
        {
            _currentApplication = applicable;
            ChangeChild();
        }

        private IApplicable _currentApplication;
        private int _countChildren;

        void Start()
        {
            _countChildren = transform.childCount;
        }

        void ChangeChild()
        {
            _currentApplication.GameObject.transform.SetAsLastSibling();
        }
    }
}
