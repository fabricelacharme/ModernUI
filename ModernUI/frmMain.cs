using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ModernUI
{
    public partial class frmMain : Form
    {

        // Fields
        private Button currentButton;
        private Random random;
        private int tempIndex;
        private Form activeForm;

        PictureBox imgArrowPlaylists;
        PictureBox imgArrowEdit;
        PictureBox imgArrowMusician;
        PictureBox imgArrowTools;


        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();
        
        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr hWnd, int wMsg, int wParam, int lParam);


        // Constructor
        public frmMain()
        {
            InitializeComponent();
            random = new Random();
            
            CustomizeDesign();
        }

        /// <summary>
        /// Load a second image for all buttons with submenu arrows
        /// Ensure that the image will not be hidden behinf the vertical scrollbar when it appears
        /// </summary>
        /// <param name="e"></param>
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            // Add a second image to the btnPlaylists button on the right side
            imgArrowPlaylists = new PictureBox()
            {
                Image = Properties.Resources.arrowright_white9,
                Size = new Size(9, 9),
                SizeMode = PictureBoxSizeMode.CenterImage,
                Location = new Point(btnPlaylists.Width - 30, (btnPlaylists.Height - 9) / 2),
            };
            btnPlaylists.Controls.Add(imgArrowPlaylists);

            imgArrowEdit = new PictureBox()
            {
                Image = Properties.Resources.arrowright_white9,
                Size = new Size(9, 9),
                SizeMode = PictureBoxSizeMode.CenterImage,
                Location = new Point(btnEdit.Width - 30, (btnEdit.Height - 9) / 2),
            };
            btnEdit.Controls.Add(imgArrowEdit);
            
            imgArrowMusician = new PictureBox()
            {
                Image = Properties.Resources.arrowright_white9,
                Size = new Size(9, 9),
                SizeMode = PictureBoxSizeMode.CenterImage,
                Location = new Point(btnMusician.Width - 30, (btnMusician.Height - 9) / 2),
            };
            btnMusician.Controls.Add(imgArrowMusician);
            
            imgArrowTools = new PictureBox()
            {
                Image = Properties.Resources.arrowright_white9,
                Size = new Size(9, 9),
                SizeMode = PictureBoxSizeMode.CenterImage,
                Location = new Point(btnTools.Width - 30, (btnTools.Height - 9) / 2),
            };
            btnTools.Controls.Add(imgArrowTools);


        }

        private void CustomizeDesign()
        {
            //btnCloseChildForm.Visible = false;

            pnlPlaylistsSubMenu.Visible = false;
            pnlMusicianSubMenu.Visible = false;
            pnlToolsSubMenu.Visible = false;
            pnlEditSubMenu.Visible = false;

            this.Text = string.Empty;
            this.ControlBox = false;
            this.DoubleBuffered = true;
            this.MaximizedBounds = Screen.FromHandle(this.Handle).WorkingArea;

        }

        /// <summary>
        /// Open child form in pnlContent and activate the button
        /// </summary>
        /// <param name="childForm"></param>
        /// <param name="btnSender"></param>
        private void OpenChildForm(Form childForm, Object btnSender)
        {
            activeForm?.Close();
            
            ActivateButton(btnSender);
            activeForm = childForm;
            childForm.TopLevel = false;
            childForm.FormBorderStyle = FormBorderStyle.None;   
            childForm.Dock = DockStyle.Fill;
            pnlContent.Controls.Add(childForm);
            pnlContent.Tag = childForm;
            childForm.BringToFront();
            childForm.Show();
            lblTitle.Text = childForm.Text;

        }

        private void ActivateButton(object btnSender)
        {
            if (btnSender != null)
            {
                if (currentButton != (Button)btnSender)
                {                    
                    currentButton = (Button)btnSender;

                    if ((string)currentButton.Tag == "mainMenu")
                    {
                        DisableButton();

                        Color color = SelectThemeColor();

                        currentButton.BackColor = color;
                        currentButton.ForeColor = Color.White;
                        //currentButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));

                        pnlTitleBar.BackColor = color;
                        btnHome.BackColor = ThemeColors.ChangeColorBrightness(color, -0.3);
                        ThemeColors.PrimaryColor = color;
                        ThemeColors.SecondaryColor = ThemeColors.ChangeColorBrightness(color, -0.3);

                        // Change image of buttons with submenu arrows
                        if (currentButton == btnPlaylists)
                            imgArrowPlaylists.Image = Properties.Resources.arrowdown_white9;
                        else if (currentButton == btnEdit)
                            imgArrowEdit.Image = Properties.Resources.arrowdown_white9;
                        else if (currentButton == btnMusician)
                            imgArrowMusician.Image = Properties.Resources.arrowdown_white9;
                        else if (currentButton == btnTools)
                            imgArrowTools.Image = Properties.Resources.arrowdown_white9;

                    }
                    else if ((string)currentButton.Tag == "subMenu")
                    {
                        DisableSubButtons();
                        
                        Color color = Color.LightBlue;
                        currentButton.BackColor = color;
                        currentButton.ForeColor = Color.White;
                        //currentButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                        
                    }
                }
                
            }
        }

        // Methods
        private Color SelectThemeColor()
        {
            int index = random.Next(ThemeColors.ColorList.Count);
            while (tempIndex == index)
            {
                index = random.Next(ThemeColors.ColorList.Count);
            }
            tempIndex = index;
            string color = ThemeColors.ColorList[index];
            return ColorTranslator.FromHtml(color);
        }


        

        private void DisableButton()
        {
            Panel[] subMenus = { pnlSideMenu, pnlEditSubMenu, pnlMusicianSubMenu, pnlPlaylistsSubMenu, pnlToolsSubMenu };
            foreach (Panel panel in subMenus)
            {
                foreach (Control previousBtn in panel.Controls)
                {
                    if (previousBtn.GetType() == typeof(Button))
                    {
                        // Reset button to default state depending if it is located in the main side menu or in a submenu
                        // Maybe refactor this later to avoid the if-else with tags ?                        
                        if ((string)previousBtn.Tag == "subMenu")
                        {
                            //previousBtn.BackColor = Color.FromArgb(39, 39, 58);
                            previousBtn.BackColor = Color.FromArgb(96, 125, 139);

                        }
                        else if ((string)previousBtn.Tag == "mainMenu")
                        {
                            previousBtn.BackColor = Color.FromArgb(51, 51, 76);
                            
                        }
                                                
                        
                        previousBtn.ForeColor = Color.Gainsboro;
                        //previousBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));

                        // Restore image of buttons with submenu arrows
                        if (previousBtn == btnPlaylists)
                            imgArrowPlaylists.Image = Properties.Resources.arrowright_white9;
                        else if (previousBtn == btnEdit)
                            imgArrowEdit.Image = Properties.Resources.arrowright_white9;
                        else if (previousBtn == btnMusician)
                            imgArrowMusician.Image = Properties.Resources.arrowright_white9;
                        else if (previousBtn == btnTools)
                            imgArrowTools.Image = Properties.Resources.arrowright_white9;

                    }
                }
            }        
        }


        private void DisableSubButtons()
        {
            Panel[] subMenus = { pnlEditSubMenu, pnlMusicianSubMenu, pnlPlaylistsSubMenu, pnlToolsSubMenu };
            foreach (Panel panel in subMenus)
            {
                foreach (Control previousBtn in panel.Controls)
                {
                    if (previousBtn.GetType() == typeof(Button))
                    {
                        // Reset button to default state depending if it is located in the main side menu or in a submenu
                        // Maybe refactor this later to avoid the if-else with tags ?                        
                        if ((string)previousBtn.Tag == "subMenu")
                        {
                            //previousBtn.BackColor = Color.FromArgb(39, 39, 58);
                            previousBtn.BackColor = Color.FromArgb(96, 125, 139);
                        }
                        

                    }

                }
            }
        }

        private void HideSubMenu()
        {
            if ( this.pnlPlaylistsSubMenu.Visible == true)
                pnlPlaylistsSubMenu.Visible = false;
            if (this.pnlMusicianSubMenu.Visible == true)
                pnlMusicianSubMenu.Visible = false;
            if (pnlToolsSubMenu.Visible == true)
                pnlToolsSubMenu.Visible = false;
            if (pnlEditSubMenu.Visible == true)
                pnlEditSubMenu.Visible = false;                                        
        }
        
        private void ShowSubMenu(Panel subMenu)
        {
            if (subMenu.Visible == false)
            {
                HideSubMenu();
                subMenu.Visible = true;
            }
            else
                subMenu.Visible = false;
        }


        private void btnHome_Click(object sender, EventArgs e)
        {
            activeForm?.Close();

            HideSubMenu();
            Reset();
        }



        private void btnFiles_Click(object sender, EventArgs e)
        {
            HideSubMenu();
            OpenChildForm(new Forms.frmFiles(), sender);
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            HideSubMenu();
            OpenChildForm(new Forms.frmSearch(), sender);
        }

        private void btnArtists_Click(object sender, EventArgs e)
        {
            HideSubMenu();
            OpenChildForm(new Forms.frmArtists(), sender);
        }

        private void btnPlay_Click(object sender, EventArgs e)
        {
            HideSubMenu();
            OpenChildForm(new Forms.frmPlayer(), sender);
        }


        #region Playlists

        private void btnPlaylists_Click(object sender, EventArgs e)
        {            
            if (pnlPlaylistsSubMenu.Visible)
            {
                HideSubMenu();
                imgArrowPlaylists.Image = Properties.Resources.arrowright_white9;

                return;
            }
                

            ActivateButton(sender);
            ShowSubMenu(pnlPlaylistsSubMenu);
            EnsureVisibility(pnlPlaylistsSubMenu);
            OpenChildForm(new Forms.frmPlaylists(), sender);
        }

        private void btnPlaylistsNewPl_Click(object sender, EventArgs e)
        {

        }

        #endregion Playlists
    

        #region Edition
        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (pnlEditSubMenu.Visible)
            {
                HideSubMenu();
                imgArrowEdit.Image = Properties.Resources.arrowright_white9;
                return;
            }

            ActivateButton(sender);
            ShowSubMenu(pnlEditSubMenu);
            EnsureVisibility(pnlEditSubMenu);
            OpenChildForm(new Forms.frmFiles(), sender);
        }

        #endregion Edition


        #region Musician
        private void btnMusician_Click(object sender, EventArgs e)
        {
            if (pnlMusicianSubMenu.Visible)
            {
                HideSubMenu();
                imgArrowMusician.Image = Properties.Resources.arrowright_white9;
                return;
            }

            ActivateButton(sender);
            ShowSubMenu(pnlMusicianSubMenu);
            EnsureVisibility(pnlMusicianSubMenu);
            OpenChildForm(new Forms.frmFiles(), sender);
        }

        private void btnMusicianChords_Click(object sender, EventArgs e)
        {
            ActivateButton(sender);
            OpenChildForm(new Forms.frmChords(), sender);

        }

        private void btnMusicianPiano_Click(object sender, EventArgs e)
        {
            ActivateButton(sender);
            OpenChildForm(new Forms.frmPiano(), sender);
        }

        private void btnMusicianGuitar_Click(object sender, EventArgs e)
        {
            ActivateButton(sender);
            OpenChildForm(new Forms.frmGuitar(), sender);
        }

        #endregion Musician


        #region Tools
        /// <summary>
        /// Opens the Tools form and shows the Tools submenu
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnTools_Click(object sender, EventArgs e)
        {
            if (pnlToolsSubMenu.Visible)
            {
                HideSubMenu();
                imgArrowTools.Image = Properties.Resources.arrowright_white9;
                return;
            }

            ActivateButton(sender);
            ShowSubMenu(pnlToolsSubMenu);

            // ensure that the bottom of the panel is visible
            EnsureVisibility(pnlToolsSubMenu);

            OpenChildForm(new Forms.frmFiles(), sender);
        }

        private void btnToolsSettings_Click(object sender, EventArgs e)
        {
            ActivateButton(sender);
            OpenChildForm(new Forms.frmToolsSettings(), sender);
        }

        private void btnToolsLibrary_Click(object sender, EventArgs e)
        {
            ActivateButton(sender);
            OpenChildForm(new Forms.frmToolsLibrary(), sender);
        }

        private void btnToolsMidi_Click(object sender, EventArgs e)
        {
            ActivateButton(sender);
            OpenChildForm(new Forms.frmToolsMidi(), sender);
        }

        #endregion Tools


        private void EnsureVisibility(Panel panel)
        {
            // ensure that the bottom of the panel is visible
            int ybottom = panel.Top + panel.Height;
            if (ybottom > pnlSideMenu.Height)
            {
                pnlSideMenu.AutoScrollPosition = new Point(0, ybottom - pnlSideMenu.Height);
            }
        }


        private void Reset()
        {            
            DisableButton();            
            lblTitle.Text = "HOME";
            pnlTitleBar.BackColor = Color.FromArgb(0, 150, 136);            
            btnHome.BackColor = Color.FromArgb(39, 39, 58);
            currentButton = null;
            
        }

       


        private void pnlTitleBar_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Clicks == 2)
            {
                if (MouseButtons == MouseButtons.Left)
                {
                    if (WindowState == FormWindowState.Normal)
                        WindowState = FormWindowState.Maximized;
                    else
                        WindowState = FormWindowState.Normal;
                }
            }
            else
            {
                ReleaseCapture();
                SendMessage(this.Handle, 0x112, 0xf012, 0);
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnMaximize_Click(object sender, EventArgs e)
        {
            if(WindowState == FormWindowState.Normal)
                WindowState = FormWindowState.Maximized;
            else
                WindowState = FormWindowState.Normal;
        }

        private void btnMinimize_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;   
        }

        

       
    }
}
