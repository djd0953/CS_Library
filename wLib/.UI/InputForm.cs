using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace wLib.UI
{
    public partial class InputForm : Form
    {
        public InputForm()
        {
            InitializeComponent();

            this.Load += InputForm_Load;
            this.Shown += InputForm_Shown;
            this.FormClosing += InputForm_FormClosing;
            this.FormClosed += InputForm_FormClosed;
        }

        private void InputForm_Load(object sender, EventArgs e)
        {
            // 키박스
            textBox.KeyUp += TextBox_KeyUp;

            // 1열
            key_1.Click += Key_Click;
            key_2.Click += Key_Click;
            key_3.Click += Key_Click;
            key_4.Click += Key_Click;
            key_5.Click += Key_Click;
            key_6.Click += Key_Click;
            key_7.Click += Key_Click;
            key_8.Click += Key_Click;
            key_9.Click += Key_Click;
            key_0.Click += Key_Click;
            key_minus.Click += Key_Click;
            key_plus.Click += Key_Click;

            // 2열
            key_q.Click += Key_Click;
            key_w.Click += Key_Click;
            key_e.Click += Key_Click;
            key_r.Click += Key_Click;
            key_t.Click += Key_Click;
            key_y.Click += Key_Click;
            key_u.Click += Key_Click;
            key_i.Click += Key_Click;
            key_o.Click += Key_Click;
            key_p.Click += Key_Click;
            key_backslash.Click += Key_Click;
            key_backspace.Click += Key_backspace_Click;

            // 3열
            key_a.Click += Key_Click;
            key_s.Click += Key_Click;
            key_d.Click += Key_Click;
            key_f.Click += Key_Click;
            key_g.Click += Key_Click;
            key_h.Click += Key_Click;
            key_j.Click += Key_Click;
            key_k.Click += Key_Click;
            key_l.Click += Key_Click;
            key_quot.Click += Key_Click;
            key_enter.Click += BtnOK_Click;

            // 4열
            key_z.Click += Key_Click;
            key_x.Click += Key_Click;
            key_c.Click += Key_Click;
            key_v.Click += Key_Click;
            key_b.Click += Key_Click;
            key_n.Click += Key_Click;
            key_m.Click += Key_Click;
            key_comma.Click += Key_Click;
            key_dot.Click += Key_Click;
            key_question.Click += Key_Click;

            // 5열
            btnOK.Click += BtnOK_Click;
            btnCancel.Click += BtnCancel_Click;
        }

        private void InputForm_Shown(object sender, EventArgs e)
        {
            textBox.Focus();
        }

        private void InputForm_FormClosing(object sender, FormClosingEventArgs e)
        {

        }

        private void InputForm_FormClosed(object sender, FormClosedEventArgs e)
        {

        }

        private void Key_Click(object sender, EventArgs e)
        {
            Button button = sender as Button;

            textBox.AppendText(button.Tag.ToString());
            textBox.Focus();
        }

        private void Key_backspace_Click(object sender, EventArgs e)
        {
            if (textBox.Text.Length <= 0)
                return;

            textBox.Text = textBox.Text.Remove(textBox.Text.Length - 1);
            textBox.SelectionStart = textBox.Text.Length;
            textBox.Focus();
        }

        private void BtnOK_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            textBox.Text = textBox.Text.Trim().Replace("\r", string.Empty).Replace("\n", string.Empty);
        }

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        private void TextBox_KeyUp(object sender, KeyEventArgs e)
        {
            switch (e.KeyData)
            {
                case Keys.Enter:
                    btnOK.PerformClick();
                    break;
                case Keys.Escape:
                    btnCancel.PerformClick();
                    break;
            }
        }

        private void InputForm_Load_1(object sender, EventArgs e)
        {

        }
    }
}
