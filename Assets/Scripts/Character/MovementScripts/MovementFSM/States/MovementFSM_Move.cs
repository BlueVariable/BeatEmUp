using _Common.Movement;
using BLUE.FSMSystem;
using UnityEngine;

namespace BLUE.ProjectX.Character.Movement.States
{
    [RequireComponent(typeof(IMovementSpeedProvider))]
    public class MovementFSM_Move : StateBase<EState, MovementFSMTransitionMessage>
    {
        public override EState GetStateType()
        {
            return EState.Move;
        }
    }
}