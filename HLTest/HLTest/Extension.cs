using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Linq;
using System.IO.Ports;
using System.Data;

namespace HLTest
{
    public static class Extension
    {
        #region Label

        public static void SetText(this Label lbl, string text)
        {
            lbl.Invoke(new MethodInvoker(() => { lbl.Text = text; }));
        }

        public static void SetText(this Label lbl, string text, Color backColor)
        {
            lbl.Invoke(new MethodInvoker(() => { lbl.Text = text; lbl.BackColor = backColor; }));
        }



        #endregion

        #region TextBox

        public static void SetText(this TextBox tb, string text)
        {
            tb.Invoke(new MethodInvoker(() => { tb.Text = text; }));
        }

        public static void AppendText(this TextBox tb, string text)
        {
            tb.Invoke(new MethodInvoker(() => { tb.Text += text; }));
        }
        public static void SetFocus(this TextBox tb)
        {
            tb.Invoke(new MethodInvoker(() => { tb.Focus (); }));
        }
       

        #endregion

        #region DataGridView

        public static void AddRow(this DataGridView grid, params object[] row)
        {
            grid.Invoke(new MethodInvoker(() => { grid.Rows.Add(row); }));
        }

        public static void InsertRow(this DataGridView grid, int index, params object[] row)
        {
            grid.Invoke(new MethodInvoker(() => { grid.Rows.Insert(index, row); }));
        }

        public static void ClearRows(this DataGridView grid)
        {
            grid.Invoke(new MethodInvoker(() => { grid.Rows.Clear(); }));
        }
        public static void SetDataSource(this DataGridView grid, DataTable dt)
        {
            grid.Invoke(new MethodInvoker(() => { grid.DataSource=dt; }));
        }
        public static void RemoveRow(this DataGridView grid,int index)
        {
            grid.Invoke(new MethodInvoker(() => { grid.Rows.RemoveAt(index) ; }));
        }

        #endregion

        #region FlowLayoutPanel

        public static void Enable(this FlowLayoutPanel panel)
        {
            panel.Invoke(new MethodInvoker(() => { panel.Enabled = true; }));
        }

        public static void Disable(this FlowLayoutPanel panel)
        {
            panel.Invoke(new MethodInvoker(() => { panel.Enabled = false; }));
        }

        #endregion

        #region ComboBox

        public static void SetText(this ComboBox  cb, string text)
        {
            cb.Invoke(new MethodInvoker(() => { cb.Text = text; }));
        }

        public static void AddItem(this ComboBox cb, string text)
        {
            cb.Invoke(new MethodInvoker(() => { cb.AddItem(text); }));
        }

        #endregion

        #region PictureBox

        public static void SetImage(this PictureBox pb, Image image)
        {
            pb.Invoke(new MethodInvoker(() => { pb.Image = image; }));
        }
        public static void SetImage(this PictureBox pb, string path)
        {
            pb.Invoke(new MethodInvoker(() => { pb.Image = Image.FromFile(path); }));
        }
        public static void SaveImage(this PictureBox pb, string  path)
        {
            pb.Invoke(new MethodInvoker(() => { pb.Image.Save(path); }));
        }
        public static void _Refresh(this PictureBox pb)
        {
            pb.Invoke(new MethodInvoker(() => { pb.Refresh(); }));
        }




        #endregion

        #region RichTextBox
        public static void AppendStr(this RichTextBox rtb, string text)
        {
            rtb.Invoke(new MethodInvoker(() => { rtb.AppendText(text); }));
        }

        #endregion

        #region RichTextBox
        //public static void AppendStr(this SerialPort serialPort, string text)
        //{
        //    serialPort.Invoke(new MethodInvoker(() => { rtb.AppendText(text); }));
        //}

        #endregion
    }
}
