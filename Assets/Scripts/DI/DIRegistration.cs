using System;

namespace Assets.Scripts.DI
{
    public class DIRegistration
    {
        public Func<DIContainer, object> Factory { get; set; }
        public bool IsSingleton { get; set; }
        public object Instance { get; set; }
    }
}