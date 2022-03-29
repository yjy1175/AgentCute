using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace YJY
{
    public abstract class Projectile : MonoBehaviour
    {
        #region variable
        protected abstract float moveSpeed
        {
            get;
            set;
        }
        protected abstract float damage
        {
            get;
            set;
        }
        protected abstract Vector3 target
        {
            get;
            set;
        }
        protected abstract int count
        {
            get;
            set;
        }
        #endregion

        #region method
        protected abstract void destroySelf();
        protected abstract void launchProjectile();
        #endregion
    }
}

