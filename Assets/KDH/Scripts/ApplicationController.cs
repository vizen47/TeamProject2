using KDH.Scripts.Systems;
using UnityEngine;

namespace KDH.Scripts
{
    public class ApplicationController : MonoBehaviour
    {
        [SerializeField] private GameObject[] childGO;
        private IApplicable[] _applicable = new IApplicable[2];
        public IApplicable CurrentApplication { get; private set; }

        void Awake()
        {
            for(int i = 0; i < childGO.Length; i++)
            {
                _applicable[i] = childGO[i].GetComponent<IApplicable>();
            }
        }
        
        public void Init(IApplicable applicable)
        {
            CurrentApplication = applicable;
            ChangeChild();
        }

        
        public void ChangeChild()
        {
            CurrentApplication.TargetApplication.transform.SetAsLastSibling();
        }

        public void DisableApplications()
        {
            foreach (var entity in _applicable)
            {
                entity.TargetApplication.SetActive(true);
                entity.HandleChangeActive();
            }
        }

        public void DisableWorkFrameApps()
        {
            foreach (var entity in _applicable)
                entity.SetUnEnableWorkApp(false);
        }
    }
}
