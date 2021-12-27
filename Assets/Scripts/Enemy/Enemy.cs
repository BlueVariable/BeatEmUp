using UnityEngine;

namespace BLUE.ProjectX.Enemy
{
    public class Enemy : MonoBehaviour
    {
        private void Awake()
        {
            RegisterSelfToEnemyManager();
        }

        private void OnDestroy()
        {
            UnregisterSelfFromEnemyManager();
        }

        private void RegisterSelfToEnemyManager()
        {
            EnemyManager.Instance.RegisterEnemy(this);
        }

        private void UnregisterSelfFromEnemyManager()
        {
            if (EnemyManager.Instance == null)
            {
                return;
            }
            
            EnemyManager.Instance.UnregisterEnemy(this);
        }
    }
}