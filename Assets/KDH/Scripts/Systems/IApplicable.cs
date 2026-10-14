using System;
using UnityEngine;

namespace KDH.Scripts.Systems
{
    public interface IApplicable
    {        
        public GameObject TargetApplication { get; set; }
        public GameObject WorkApp { get; }
        public GameObject GameObject { get; }
        public event Action OnChangeActive;

        void SetUnEnableWorkApp(bool isActive);
        void HandleChangeActive();
    }
}