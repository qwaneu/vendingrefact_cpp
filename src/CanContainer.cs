using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Vender
{
    public class CanContainer
    {
        private Can type;
        public Can Type
        {
            get
            {
                return type;
            }
            set
            {
                type = value;
            }
        }
        public int Price;
        public int Amount;
    }
}
