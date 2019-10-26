using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToT_Adventure
{
    public class UIAction
    {
        public Toolbox.UIAction Action { get; set; }
        public string ActionParam { get; set; }

        public UIAction(Toolbox.UIAction action = Toolbox.UIAction.Nothing, string actionParam = "")
        {
            Action = action;
            ActionParam = actionParam;
        }
    }
}
