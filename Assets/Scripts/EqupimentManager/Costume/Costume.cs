using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace YJY
{
    public class Costume : MonoBehaviour
    {
        // Start is called before the first frame update
        #region varialbe
        private CostumeSpec spec = new CostumeSpec();
        public CostumeSpec Spec
        {
            get { return spec; }
            set { spec = value; }
        }
        private bool isLocked;
        public bool IsLocked
        {
            get { return isLocked; }
            set { isLocked = value; }
        }
        #endregion
        #region method
        public void setInit()
        {
            // 코스튬 데이터 추출
        }
        #endregion
    }
}

