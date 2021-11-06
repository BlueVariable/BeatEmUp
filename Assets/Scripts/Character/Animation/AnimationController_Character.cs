using BLUE.AnimationSystem;

namespace BLUE.ProjectX.Character
{
    public class AnimationController_Character : AnimationController<ECharacterAnimation>
    {
        public void UpdateParameter(string paramName, float value)
        {
            _animator.SetFloat(paramName, value);   
        }
    }
}