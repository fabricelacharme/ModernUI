using System;
using System.Collections.Generic;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace ModernUI.Forms
{
    public partial class frmPlayer : Form, IMessageFilter
    {
        #region Move form 

        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;
        public const int WM_LBUTTONDOWN = 0x0201;

        [DllImportAttribute("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [DllImportAttribute("user32.dll")]
        public static extern bool ReleaseCapture();

        private readonly HashSet<Control> controlsToMove = new HashSet<Control>();

        private Point Mouselocation;

        #endregion Move form


        public frmPlayer()
        {
            InitializeComponent();

            #region Move form without title bar

            Application.AddMessageFilter(this);
            controlsToMove.Add(this);

            controlsToMove.Add(this.gradientPanel1);

            #endregion Move form without title bar

        }

        private void frmPlayer_Load(object sender, EventArgs e)
        {
            LoadTheme();
        }

        private void LoadTheme()
        {
            foreach (Control btns in Controls)
            {
                if (btns.GetType() == typeof(Button))
                {
                    Button btn = (Button)btns;
                    btn.BackColor = ThemeColors.PrimaryColor;
                    btn.ForeColor = Color.White;
                    btn.FlatAppearance.BorderColor = ThemeColors.SecondaryColor;
                }
            }
            label4.ForeColor = ThemeColors.PrimaryColor;
            label5.ForeColor = ThemeColors.PrimaryColor;

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }


        #region Move Window
        /// <summary>
        /// Move form without title bar
        /// UserControls of the form manage themselves this move
        /// by sending the message to their parent form (this.ParentForm.Handle)
        /// </summary>
        /// <param name="m"></param>
        /// <returns></returns>
        public bool PreFilterMessage(ref Message m)
        {
            if (m.Msg == WM_LBUTTONDOWN &&
                 controlsToMove.Contains(Control.FromHandle(m.HWnd)))
            {
                ReleaseCapture();
                SendMessage(this.Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
                return true;
            }
            return false;
        }

        #endregion Move Window
    }
}
