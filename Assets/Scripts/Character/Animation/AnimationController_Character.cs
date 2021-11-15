using BLUE.AnimationSystem;

namespace BLUE.ProjectX.Character
{
    public class AnimationController_Character : AnimationController<ECharacterAnimation>
    {
        #region Constants

        public const int MOVEMENT_LAYER = 1;

        public const int ATTACK_LAYER = 2;

        #endregion
        
        public void UpdateParameter(string paramName, float value)
        {
            _animator.SetFloat(paramName, value);   
        }

        public void SetLayerWeight(int layerIndex, float val)
        {
            _animator.SetLayerWeight(layerIndex, val);
        }
    }
}