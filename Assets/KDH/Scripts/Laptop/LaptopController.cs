using UnityEngine;
using UnityEngine.InputSystem;

namespace KDH.Scripts.Laptop
{
    public class LaptopController : MonoBehaviour
    {
        [SerializeField] private ApplicationController _application;
        
        private void Awake()
        {
            Application.wantsToQuit += ControlAlfF4;
        }

        private void Update()
        {
            if (Keyboard.current.altKey.wasPressedThisFrame && Keyboard.current.f4Key.wasPressedThisFrame)
                _application.CurrentApplication.HandleChangeActive();
        }
        
        private void OnDestroy()
        {
            Application.wantsToQuit -= ControlAlfF4;
        }

        private bool ControlAlfF4()
        {
            Debug.Log("Alt F4 막기");
            return false;
        }
    }
}
