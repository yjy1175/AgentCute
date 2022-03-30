using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace YJY
{
    public class CostumeSpec : EquipSpec
    {
        #region varialbe
        public override string type
        {
            get { return type; }
            set { type = value; }
        }
        public override string typeName
        {
            get { return typeName; }
            set { typeName = value; }
        }
        public override string equipName
        {
            get { return equipName; }
            set { equipName = value; }
        }
        public override string equipDesc
        {
            get { return equipDesc; }
            set { equipDesc = value; }
        }
        public override int equipRank
        {
            get { return equipRank; }
            set { equipRank = value; }
        }
        #endregion
    }
}