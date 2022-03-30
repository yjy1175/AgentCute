using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace YJY
{
    public abstract class EquipSpec
    {
        #region varialbe
        public abstract string type
        {
            get;
            set;
        }
        public abstract string typeName
        {
            get;
            set;
        }
        public abstract string equipName
        {
            get;
            set;
        }
        public abstract string equipDesc
        {
            get;
            set;
        }
        public abstract int equipRank
        {
            get;
            set;
        }
        #endregion
    }
}

