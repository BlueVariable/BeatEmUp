using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace BLUE.ProjectX.Character
{
    // TODO :: need enable disable mechanism
    public class CharacterStatusController : MonoBehaviour
    {
        [SerializeField] private CharacterStatusBase[] _statuses = null;

        public List<ECharacterStatus> AppearedStatuses { get; private set; } = new List<ECharacterStatus>();

        private void LateUpdate()
        {
            CheckCharacterStatuses();
        }

        private void CheckCharacterStatuses()
        {
            List<CharacterStatusBase> currentStatuses = new List<CharacterStatusBase>();

            foreach (CharacterStatusBase status in _statuses)
            {
                bool statusResult = status.CheckStatus();

                if (statusResult)
                {
                    currentStatuses.Add(status);

                    if (status.BreakIfStatusAppear)
                    {
                        break;
                    }
                }
            }
            
            UpdateAppearedStatuses(currentStatuses);
        }

        private void UpdateAppearedStatuses(List<CharacterStatusBase> currentStatuses)
        {
            int index = 0;
            int prevDetected = 0;
            
            for (int i = 0; i < currentStatuses.Count; i++)
            {
                if ((index = AppearedStatuses.IndexOf(currentStatuses[i].GetCharacterStatus())) > 0)
                {
                    for (int deleteIndex = prevDetected; deleteIndex < index; deleteIndex++)
                    {
                        CharacterStatusBase disappearedStatus = _statuses.SingleOrDefault(i => i.GetCharacterStatus() == AppearedStatuses[deleteIndex]);
                        
                        disappearedStatus.DisappearStatus();
                        
                        AppearedStatuses.RemoveAt(deleteIndex);
                    }

                    prevDetected = index;
                }
            }
        }
    }
}