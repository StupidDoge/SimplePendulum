using UnityEngine;

namespace Assets.Scripts.Infrastructure.Services
{
    public class InputService : IInputService
    {
        public bool IsClicked => Input.GetMouseButtonDown(0);
    }
}
