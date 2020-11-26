using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TEST_B
{
    public static class InvokeFunction
    {
        public static void ListBoxClear(ListBox lb)
        {
            if (lb.InvokeRequired)
            {
                lb.Invoke(new MethodInvoker(delegate
                {
                    lb.Items.Clear();
                }));
            }
            else
            {
                lb.Items.Clear();
            }
        }

        public static void ListBoxAdd(ListBox lb, string txt)
        {
            if (lb.InvokeRequired)
            {
                lb.Invoke(new MethodInvoker(delegate
                {
                    lb.Items.Add(txt);
                }));
            }
            else
            {
                lb.Items.Add(txt);
            }
        }

    }
}
