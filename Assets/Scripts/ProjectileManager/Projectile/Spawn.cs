using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace YJY
{
    public class Spawn : Projectile
    {
        #region variable
        public override ProjectileSpec spec
        {
            get { return spec; }
            set { spec = value; }
        }
        public override Vector3 target
        {
            get { return target; }
            set { target = value; }
        }
        #endregion
        #region method
        protected override void setInit()
        {
            // 발사체 데이터 파싱 후 값 대입
            ProjectileSpec newSpec = new ProjectileSpec();
            // spec 데이터 추출
            spec = newSpec;

        }
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
        #endregion
    }
}
