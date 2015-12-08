using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Model
{
    public abstract class CharctorInfo
    {
        public int Hp { get; set; }
        public int Damager { get; set; }
        public abstract void TakeDamage(int value);
    }
}