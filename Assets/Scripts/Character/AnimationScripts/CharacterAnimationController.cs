using BLUE.AnimationSystem;

namespace Character.Animation
{
    public class CharacterAnimationController : AnimationController<ECharacterAnimation>
    {
        public void UpdateParameter(string paramName, float value)
        {
            _animator.SetFloat(paramName, value);   
        }
    }
}