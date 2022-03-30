using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace YJY
{
    public abstract class Projectile : MonoBehaviour
    {
        #region variable
        public abstract ProjectileSpec spec
        {
            get;
            set;
        }
        public abstract Vector3 target
        {
            get;
            set;
        }
        #endregion

        #region method
        protected abstract void setInit();
        protected abstract void destroySelf();
        protected abstract void launchProjectile();
        #endregion
    }
}

