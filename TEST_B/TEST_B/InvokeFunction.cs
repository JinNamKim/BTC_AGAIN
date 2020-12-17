using System;
using System.Collections.Generic;
using System.Drawing;
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

        public static void DataGridView_Rows_Add(DataGridView dgv)
        {
            if(dgv.InvokeRequired)
            {
                dgv.Invoke(new MethodInvoker(delegate
                {
                    dgv.Rows.Add();
                }));
            }
            else
            {
                dgv.Rows.Add();
            }
        }

        public static void DataGridView_Rows_SetText(DataGridView dgv, int col, int row, string text)
        {
            if (dgv.InvokeRequired)
            {
                dgv.Invoke(new MethodInvoker(delegate
                {
                    //dgv[col, row].Style.BackColor = Color.Red;
                    dgv[col, row].Value = text;
                    //dgv.Rows[row].SetValues(text);

                }));
            }
            else
            {
                //dgv[col, row].Style.BackColor = Color.Red;
                //dgv.Rows[row].SetValues(text);
                dgv[col, row].Value = text;
            }
        }


        public static void DataGridView_Rows_SetBackColor(DataGridView dgv, int col, int row, Color color)
        {
            if (dgv.InvokeRequired)
            {
                dgv.Invoke(new MethodInvoker(delegate
                {
                    //dgv[col, row].Style.BackColor = Color.Red;
                    dgv[col, row].Style.BackColor = color;
                    //dgv.Rows[row].SetValues(text);

                }));
            }
            else
            {
                //dgv[col, row].Style.BackColor = Color.Red;
                //dgv.Rows[row].SetValues(text);
                dgv[col, row].Style.BackColor = color;
            }
        }
        public static void DataGridView_Rows_SetFontColor(DataGridView dgv, int col, int row, Color color)
        {
            if (dgv.InvokeRequired)
            {
                dgv.Invoke(new MethodInvoker(delegate
                {
                    //dgv[col, row].Style.BackColor = Color.Red;
                    dgv[col, row].Style.ForeColor = color;
                    //dgv.Rows[row].SetValues(text);

                }));
            }
            else
            {
                //dgv[col, row].Style.BackColor = Color.Red;
                //dgv.Rows[row].SetValues(text);
                dgv[col, row].Style.ForeColor = color;
            }
        }


    }
}
