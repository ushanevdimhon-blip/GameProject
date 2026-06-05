using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameProject.Components
{
    public class KeysComponent
    {
        int keysToCollect;
        public int KeysCollected { get; private set; }

        public Action OnAllKeysCollected;

        public KeysComponent(int keysToCollect)
        {
            this.keysToCollect = keysToCollect;
            this.KeysCollected = 0;
        }

        public void Update()
        {
            if (KeysCollected >= keysToCollect)
            {
                OnAllKeysCollected?.Invoke();
                KeysCollected = 0;
            }
        }

        public void CollectKey()
        {
            KeysCollected++;
        }
    }
}
