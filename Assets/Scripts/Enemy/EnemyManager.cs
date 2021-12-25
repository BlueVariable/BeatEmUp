using System;
using System.Collections.Generic;
using UnityEngine;

namespace BLUE.ProjectX.Enemy
{
    public class EnemyManager : MonoBehaviour
    {
        #region Singleton

        private static EnemyManager _instance;
        public static EnemyManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = FindObjectOfType<EnemyManager>();
                }

                return _instance;
            }
        }

        #endregion

        private List<Enemy> _enemies = new List<Enemy>();

        #region Events

        public Action OnNoEnemiesLeft { get; set; }

        #endregion

        public void RegisterEnemy(Enemy enemy)
        {
            if (_enemies.Contains(enemy))
            {
                return;
            }

            _enemies.Add(enemy);
        }

        public void UnregisterEnemy(Enemy enemy)
        {
            _enemies.Remove(enemy);

            TryFireNoEnemiesLeft();
        }

        private bool TryFireNoEnemiesLeft()
        {
            if (_enemies.Count == 0)
            {
                OnNoEnemiesLeft?.Invoke();

                return true;
            }

            return false;
        }
    }
}