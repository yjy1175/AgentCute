using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace YJY
{
    public abstract class Projectile : MonoBehaviour
    {
        #region variable
        public abstract ProjectileSpec Spec
        {
            get;
            set;
        }
        public abstract Vector3 Target
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

