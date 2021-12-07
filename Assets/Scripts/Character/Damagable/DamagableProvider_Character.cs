using BLUE.DamageSystem;

namespace BLUE.ProjectX.Character
{
    public class DamagableProvider_Character : DamagableProviderBase
    {
        private Damagable _damagable;

        public override Damagable Damagable
        {
            get
            {
                if (_damagable == null)
                {
                    _damagable = Character.Instance.GetComponentInChildren<Damagable>();
                }

                return _damagable;
            }
        }
    }
}