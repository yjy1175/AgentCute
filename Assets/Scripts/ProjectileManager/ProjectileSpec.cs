using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace YJY
{
    public class ProjectileSpec : EquipSpec
    {
        #region variable
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

        private float projectileDamage;
        public float ProjectileDamage
        {
            get { return projectileDamage; }
            set { projectileDamage = value; }
        }

        private float speed;
        public float Speed
        {
            get { return speed; }
            set { speed = value; }
        }

        private int count;
        public int Count
        {
            get { return count; }
            set { count = value; }
        }

        private int angle;
        public int Angle
        {
            get { return angle; }
            set { angle = value; }
        }

        private int spawnTime;
        public int SpawnTime
        {
            get { return spawnTime; }
            set { spawnTime = value; }
        }

        private int maxPassCount;
        public int MaxPassCount
        {
            get { return maxPassCount; }
            set { maxPassCount = value; }
        }
        #endregion
    }
}

