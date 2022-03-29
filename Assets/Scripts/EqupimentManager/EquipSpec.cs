using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace YJY
{
    public abstract class EquipSpec
    {
        #region varialbe
        protected abstract string typeName
        {
            get;
            set;
        }
        protected abstract string equipName
        {
            get;
            set;
        }
        protected abstract string equipDesc
        {
            get;
            set;
        }
        protected abstract int equipRank
        {
            get;
            set;
        }
        #endregion
    }
}

