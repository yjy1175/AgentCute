using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace YJY
{
    public class CostumeSpec : EquipSpec
    {
        #region varialbe
        protected override string typeName
        {
            get { return typeName; }
            set { typeName = value; }
        }
        protected override string equipName
        {
            get { return equipName; }
            set { equipName = value; }
        }
        protected override string equipDesc
        {
            get { return equipDesc; }
            set { equipDesc = value; }
        }
        protected override int equipRank
        {
            get { return equipRank; }
            set { equipRank = value; }
        }
        #endregion
    }
}

