using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace YJY
{
    public class Chase : Projectile
{
        #region variable

        protected override float moveSpeed 
        { 
            get { return moveSpeed; }
            set { moveSpeed = value; }
        }
        protected override float damage 
        {
            get { return damage; }
            set { damage = value; }
        }
        protected override Vector3 target 
        {
            get { return target; }
            set { target = value; }
        }
        protected override int count 
        {
            get { return count; }
            set { count = value; }
        }
        #endregion
        protected override void destroySelf()
        {
            /*생성후 파괴되는 매서드*/
        }

        protected override void launchProjectile()
        {
            /*추적 매서드*/
        }
        // Start is called before the first frame update
        void Start()
        {
            destroySelf();
        }
        // Update is called once per frame
        void Update()
        {
            launchProjectile();
        }
    }
}

