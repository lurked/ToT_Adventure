using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToT_Adventure
{
    public class Node
    {
        public Toolbox.NodeType NodeType { get; set; }
        public int Level { get; set; }
        public bool Active { get; set; }
        public float Speed { get; set; }
    }
}
