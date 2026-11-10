using KDH.Scripts.Systems;
using UnityEngine;

namespace KDH.Scripts
{
    public class ApplicationController : MonoBehaviour
    {
        [SerializeField] private GameObject[] childGO;
        public IApplicable[] Applicable { get; private set; } = new IApplicable[2];
        public IApplicable CurrentApplication { get; private set; }

        void Awake()
        {
            for(int i = 0; i < childGO.Length; i++)
            {
                Applicable[i] = childGO[i].GetComponent<IApplicable>();
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
            foreach (var entity in Applicable)
            {
                entity.TargetApplication.SetActive(false);
                entity.TargetApplication.transform.localScale = Vector3.zero;
                entity.WorkApp.SetActive(false);
            }
        }

        public void DisableWorkFrameApps()
        {
            foreach (var entity in Applicable)
                entity.SetUnEnableWorkApp(false);
        }
    }
}
