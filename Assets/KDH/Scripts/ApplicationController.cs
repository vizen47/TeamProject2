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

        public void ChangeChild()
        {
            _currentApplication.GameObject.transform.SetAsLastSibling();
        }

        public void DisableApplications()
        {
            IApplicable[] childGO = GetComponentsInChildren<IApplicable>();

            foreach (var entity in childGO)
                entity.HandleChangeActive();
        }

        public void DisableWorkFrameApps()
        {
            IApplicable[] childGO = GetComponentsInChildren<IApplicable>(true);

            foreach (var entity in childGO)
                entity.SetUnEnableWorkApp(false);
        }
    }
}
