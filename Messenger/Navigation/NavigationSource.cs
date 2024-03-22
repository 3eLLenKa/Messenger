using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Messenger.Navigation
{
    public class NavigationSource
    {
        public static Frame GetNavigation {  get; set; }
        public NavigationSource(Frame frame)
        {
            GetNavigation = frame;
        }
    }
}
