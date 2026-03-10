using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;


namespace ModernUI
{
    public partial class frmMain : Form
    {

        // Fields
        private Button currentButton;        
        private int tempIndex;
        private Form activeForm;

        PictureBox imgArrowPlaylists;
        PictureBox imgArrowEdit;
        PictureBox imgArrowMusician;
        PictureBox imgArrowTools;

        // Source https://colorkit.co/palette/1abc9c-16a085-2ecc71-27ae60-3498db-2980b9-9b59b6-8e44ad-34495e-2c3e50-f1c40f-f39c12-e67e22-d35400-e74c3c-c0392b-ecf0f1-bdc3c7-95a5a6-7f8c8d/


        Panel[] subMenus;

        #region colors
        // Define colors for each main menu button
        private Color TitleBarHomeColor = Color.FromArgb(44, 62, 80); // 
        private Color HomeColor = Color.FromArgb(29, 29, 29); // Dark gray #1d1d1d
        private Color HomeTextColor = Color.FromArgb(255, 196, 13); // Yellow #ffc40d

        private Color MainMenuDefaultColor = Color.FromArgb(44, 62, 80); // Dark blue gray #2c3e50

        private Color ExplorerColor = Color.FromArgb(46, 204, 113); // Green #2ecc71 
        private Color SearchColor = Color.FromArgb(52, 152, 219); // 
        private Color ArtistsColor = Color.FromArgb(96, 60, 186); // Purple #9b59b6
        private Color PlayColor = Color.FromArgb(127, 140, 141); // Gray #7f8c8d

        private Color PlaylistsColor= Color.FromArgb(192, 57, 43); // Red #c0392b
        private Color EditColor = Color.FromArgb(26, 188, 156); // Turquoise #1abc9c
        private Color MusicianColor = Color.FromArgb(211, 84, 0); // Dark orange #d35400
        private Color ToolsColor = Color.FromArgb(41, 128, 185); // Blue #2980b9

        // Define colors for submenu buttons
        private Color SubMenuColor = Color.FromArgb(189, 195, 199); // #bdc3c7        
        private Color SubMenuDefaultColor = Color.FromArgb(127, 140, 141); // #7f8c8d

        #endregion colors


        #region dll
        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();
        
        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr hWnd, int wMsg, int wParam, int lParam);

        #endregion dll


        // Constructor
        public frmMain()
        {
            InitializeComponent();

            subMenus = new Panel[] { pnlPlaylistsSubMenu, pnlEditSubMenu, pnlMusicianSubMenu, pnlToolsSubMenu };
            

            
                 
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
                BackColor = Color.Transparent,
                Enabled = false,
            };            
            btnPlaylists.Controls.Add(imgArrowPlaylists);

            imgArrowEdit = new PictureBox()
            {
                Image = Properties.Resources.arrowright_white9,
                Size = new Size(9, 9),
                SizeMode = PictureBoxSizeMode.CenterImage,
                Location = new Point(btnEdit.Width - 30, (btnEdit.Height - 9) / 2),
                BackColor = Color.Transparent,
                Enabled = false,
            };
            btnEdit.Controls.Add(imgArrowEdit);
            
            imgArrowMusician = new PictureBox()
            {
                Image = Properties.Resources.arrowright_white9,
                Size = new Size(9, 9),
                SizeMode = PictureBoxSizeMode.CenterImage,
                Location = new Point(btnMusician.Width - 30, (btnMusician.Height - 9) / 2),
                BackColor = Color.Transparent,
                Enabled = false,
            };
            btnMusician.Controls.Add(imgArrowMusician);
            
            imgArrowTools = new PictureBox()
            {
                Image = Properties.Resources.arrowright_white9,
                Size = new Size(9, 9),
                SizeMode = PictureBoxSizeMode.CenterImage,
                Location = new Point(btnTools.Width - 30, (btnTools.Height - 9) / 2),
                BackColor = Color.Transparent,
                Enabled = false,
            };
            btnTools.Controls.Add(imgArrowTools);
        }

        private void CustomizeDesign()
        {            
            // Panels under buttons
            pnlMusicianSubMenu.Visible = false;
            pnlToolsSubMenu.Visible = false;
            pnlEditSubMenu.Visible = false;

            // Panels on the right of the buttons

            this.Text = string.Empty;

            this.ControlBox = false;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.FormBorderStyle = FormBorderStyle.Sizable;

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

                        Color color = HomeColor;
                        // Select color according to selected menu
                        if (currentButton == btnFiles)
                            color = ExplorerColor;
                        else if (currentButton == btnSearch)
                            color = SearchColor;
                        else if (currentButton == btnArtists)
                            color = ArtistsColor;
                        else if (currentButton == btnPlay)
                            color = PlayColor;
                        else if (currentButton == btnPlaylists)
                            color = PlaylistsColor;
                        else if (currentButton == btnEdit)
                            color = EditColor;
                        else if (currentButton == btnMusician)
                            color = MusicianColor;
                        else if (currentButton == btnTools)
                            color = ToolsColor;

                        // Select random color                        
                        currentButton.BackColor = color;
                        currentButton.ForeColor = Color.White;
                        btnHome.ForeColor = Color.Gainsboro;                        

                        pnlTitleBar.BackColor = color;                        
                        btnHome.BackColor = ThemeColors.ChangeColorBrightness(color, -0.3);
                        btnHome.Image = Properties.Resources.micro_white32;

                        ThemeColors.PrimaryColor = color;
                        ThemeColors.SecondaryColor = ThemeColors.ChangeColorBrightness(color, -0.3);

                    }
                    else if ((string)currentButton.Tag == "subMenu")
                    {
                        DisableSubButtons();

                        Color color = SubMenuColor;
                        currentButton.BackColor = color;
                        currentButton.ForeColor = Color.White;                        
                    }
                }                                
            }
        }


        // Methods
        /*
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
        */

        private void DisableButton()
        {
            Panel[] subMenus = { pnlSideMenu, pnlPlaylistsSubMenu, pnlEditSubMenu, pnlMusicianSubMenu, pnlToolsSubMenu };
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
                            previousBtn.BackColor = SubMenuDefaultColor;
                        }
                        else if ((string)previousBtn.Tag == "mainMenu")
                        {
                            previousBtn.BackColor = MainMenuDefaultColor;                            
                        }                                                                        
                        previousBtn.ForeColor = Color.Gainsboro;                        
                    }
                }
            }        
        }

        /// <summary>
        /// Default color for submenu buttons
        /// </summary>
        private void DisableSubButtons()
        {
            Panel[] subMenus = { pnlEditSubMenu, pnlMusicianSubMenu, pnlToolsSubMenu };
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
                            previousBtn.BackColor = SubMenuDefaultColor;
                        }                        
                    }
                }
            }
        }


        /// <summary>
        /// Hides submenu panels by setting their visibility to false. Can optionally hide only submenus that are
        /// designated as right-aligned.
        /// </summary>
        /// <remarks>When submenus are hidden, the associated arrow images are updated to indicate the
        /// collapsed state. This method has no effect if the submenu collection is null.</remarks>
        /// <param name="bOnlyRight">true to hide only submenus tagged as right-aligned; false to hide all submenus.</param>
        private void HideSubMenus(bool bOnlyRightSubmenus = false)
        {
            if (subMenus == null) return;
            
            foreach (Panel panel in subMenus)
            {
                if (bOnlyRightSubmenus)
                {
                    if (panel.Tag.ToString() == "SubMenuRight")
                    {
                        panel.Visible = false;
                        // Only right-sided panels are masked. Right arrow only for them.
                        imgArrowPlaylists.Image = Properties.Resources.arrowright_white9;
                        imgArrowEdit.Image = Properties.Resources.arrowright_white9;
                    }
                }
                else
                {
                    panel.Visible = false;
                    // All the panels are masked => arrow picture is right arrow
                    imgArrowPlaylists.Image = Properties.Resources.arrowright_white9;
                    imgArrowEdit.Image = Properties.Resources.arrowright_white9;
                    imgArrowMusician.Image = Properties.Resources.arrowright_white9;
                    imgArrowTools.Image = Properties.Resources.arrowright_white9;

                }
            }                                              
        }
        
       
        /// <summary>
        /// Displays the specified submenu panel, ensuring that only one submenu is visible at a time. Hides the submenu
        /// if it is already visible.
        /// </summary>
        /// <remarks>This method updates the visibility of submenu panels and associated arrow images to
        /// reflect the current state of the user interface. If the submenu is already visible, it will be hidden
        /// instead of shown.</remarks>
        /// <param name="subMenu">The submenu panel to show or hide. Cannot be null.</param>
        /// <param name="btn">An optional button that, if provided and the submenu is positioned to the right, determines the vertical
        /// alignment of the submenu when displayed.</param>
        private void ShowSubMenu(Panel subMenu, Button btn = null)
        {
            if (subMenu.Visible == false)
            {
                HideSubMenus();                                
                subMenu.Visible = true;
                                
                if (subMenu.Tag.ToString() == "SubMenuRight" && btn != null)
                {
                    subMenu.BringToFront();
                    subMenu.Top = btn.Top;
                    subMenu.Left = 0;
                }
                EnsureVisibility(subMenu);
            }
            else
                subMenu.Visible = false;

            // Display arrows depending on the visibility of the submenu panel
            imgArrowPlaylists.Image = pnlPlaylistsSubMenu.Visible ? Properties.Resources.arrowdown_white9 : Properties.Resources.arrowright_white9;            
            imgArrowEdit.Image = pnlEditSubMenu.Visible ?  Properties.Resources.arrowdown_white9 : Properties.Resources.arrowright_white9;            
            imgArrowMusician.Image = pnlMusicianSubMenu.Visible ? Properties.Resources.arrowdown_white9 : Properties.Resources.arrowright_white9;
            imgArrowTools.Image = pnlToolsSubMenu.Visible ? Properties.Resources.arrowdown_white9 : Properties.Resources.arrowright_white9;
        }


        #region normalbuttons
        private void btnHome_Click(object sender, EventArgs e)
        {
            activeForm?.Close();

            HideSubMenus();
            Reset();
        }

        private void btnFiles_Click(object sender, EventArgs e)
        {
            HideSubMenus();
            OpenChildForm(new Forms.frmFiles(), sender);
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            HideSubMenus();
            OpenChildForm(new Forms.frmSearch(), sender);
        }

        private void btnArtists_Click(object sender, EventArgs e)
        {
            HideSubMenus();
            OpenChildForm(new Forms.frmArtists(), sender);
        }

        private void btnPlay_Click(object sender, EventArgs e)
        {
            HideSubMenus();
            OpenChildForm(new Forms.frmPlayer(), sender);
        }

        #endregion normal buttons


        #region buttons with submenus

        #region Playlists

        /// <summary>
        /// Show dropdown RIGHT menu for playlists
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnPlaylists_Click(object sender, EventArgs e)
        {            
            if (pnlPlaylistsSubMenu.Visible) return;
            
            ActivateButton(sender);            
            OpenChildForm(new Forms.frmPlaylists(), sender);
            
            // Show submenu after showwing child form
            ShowSubMenu(pnlPlaylistsSubMenu, btnPlaylists);                                        
        }


        #region mouse events
        private void btnPlaylists_MouseHover(object sender, EventArgs e)
        {
            if (currentButton == btnPlaylists && !pnlPlaylistsSubMenu.Visible)
            {
                // Show submenu after showwing child form
                ShowSubMenu(pnlPlaylistsSubMenu, btnPlaylists);
            }
        }      

        private void pnlPlaylistsSubMenu_MouseLeave(object sender, EventArgs e)
        {
            HideSubMenus(true);
        }

        private void btnPlaylists_MouseLeave(object sender, EventArgs e)
        {
            // If mouse leaves this button without going to pnlPlaylistsSubmenu panel
            if (btnPlaylists.RectangleToScreen(btnPlaylists.ClientRectangle).Contains(Cursor.Position))
                return;
           
            bool mouse_on_submenu = pnlPlaylistsSubMenu.RectangleToScreen(pnlPlaylistsSubMenu.ClientRectangle).Contains(Cursor.Position);
            if (!mouse_on_submenu)               
                HideSubMenus(true);
        }

        #endregion mouse events

        #region right submenu
        private void btnPlNew_Click(object sender, EventArgs e)
        {
            pnlPlaylistsSubMenu.Visible = false;
            imgArrowPlaylists.Image = Properties.Resources.arrowright_white9;
        }

        private void btnPlDelete_Click(object sender, EventArgs e)
        {
            pnlPlaylistsSubMenu.Visible = false;
            imgArrowPlaylists.Image = Properties.Resources.arrowright_white9;
        }

        private void btnPFolderlNew_Click(object sender, EventArgs e)
        {
            pnlPlaylistsSubMenu.Visible = false;
            imgArrowPlaylists.Image = Properties.Resources.arrowright_white9;
        }

        private void btnPlFolderDelete_Click(object sender, EventArgs e)
        {
            pnlPlaylistsSubMenu.Visible = false;
            imgArrowPlaylists.Image = Properties.Resources.arrowright_white9;
        }

        #endregion right submenu

        #endregion Playlists


        #region Edition

        /// <summary>
        /// Show dropdown RIGHT menu for edition
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnEdit_Click(object sender, EventArgs e)
        {            
            if (pnlEditSubMenu.Visible) return;
            
            ActivateButton(sender);
            OpenChildForm(new Forms.frmFiles(), sender);
            
            // Show submenu after showwing child form
            ShowSubMenu(pnlEditSubMenu, btnEdit);                                                    
        }

        #region mouse events
        private void btnEdit_MouseHover(object sender, EventArgs e)
        {
            if (currentButton == btnEdit && !pnlEditSubMenu.Visible)
            {
                // Show submenu after showwing child form
                ShowSubMenu(pnlEditSubMenu, btnEdit);
            }
        }


        private void pnlEditSubMenu_MouseLeave(object sender, EventArgs e)
        {
            HideSubMenus(true);
        }

        private void btnEdit_MouseLeave(object sender, EventArgs e)
        {
            // If mouse leaves this button without going to pnlEditSubmenu panel
            if (btnEdit.RectangleToScreen(btnEdit.ClientRectangle).Contains(Cursor.Position))
                return;

            bool mouse_on_submenu = pnlEditSubMenu.RectangleToScreen(pnlEditSubMenu.ClientRectangle).Contains(Cursor.Position);
            if (!mouse_on_submenu)
                HideSubMenus(true);
        }
        #endregion mouse events


        #region edit buttons

        private void btnEditModify_Click(object sender, EventArgs e)
        {
            pnlEditSubMenu.Visible = false;
            imgArrowEdit.Image = Properties.Resources.arrowright_white9;
        }

        private void btnEditNew_Click(object sender, EventArgs e)
        {
            pnlEditSubMenu.Visible = false;
            imgArrowEdit.Image = Properties.Resources.arrowright_white9;
        }

        private void btnEditAddToPl_Click(object sender, EventArgs e)
        {
            pnlEditSubMenu.Visible = false;
            imgArrowEdit.Image = Properties.Resources.arrowright_white9;
        }

        #endregion edit buttons

        #endregion Edition


        #region Musician

        /// <summary>
        /// Show dropdown BELOW menu for musician
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnMusician_Click(object sender, EventArgs e)
        {
            ActivateButton(sender);
            OpenChildForm(new Forms.frmFiles(), sender);
            ShowSubMenu(pnlMusicianSubMenu, btnMusician);                                       
        }

        #region musician buttons
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

        #endregion musician buttons

        #endregion Musician


        #region Tools
        /// <summary>
        /// Show dropdown BELOW menu for Tools
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnTools_Click(object sender, EventArgs e)
        {           
            ActivateButton(sender);
            ShowSubMenu(pnlToolsSubMenu, btnTools);                
            OpenChildForm(new Forms.frmFiles(), sender);                                 
        }

        #region tools buttons
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

        private void btnToolsHelp_Click(object sender, EventArgs e)
        {
            // Do nothing in the UI, just launch help
        }

        #endregion tools buttons

        #endregion Tools


        #endregion buttons with submenus


        /// <summary>
        /// Ensure that the bottom of the panel is visible by modifying AutoscrollPosition od pnlSideMenu
        /// </summary>
        /// <param name="panel"></param>
        private void EnsureVisibility(Panel panel)
        {
            // ensure that the bottom of the panel is visible
            int ybottom = panel.Top + panel.Height;
            if (ybottom > pnlSideMenu.Height)
            {
                pnlSideMenu.AutoScrollPosition = new Point(0, ybottom - pnlSideMenu.Height);
            }
        }


        /// <summary>
        /// Click on the Home button
        /// </summary>
        private void Reset()
        {            
            DisableButton();            
            lblTitle.Text = "HOME";
            pnlTitleBar.BackColor = TitleBarHomeColor;           
            btnHome.BackColor = HomeColor;
            btnHome.ForeColor = HomeTextColor;
            btnHome.Image = Properties.Resources.micro_yellow32;
            currentButton = null;            
        }

        #region Titlebar
        
        /// <summary>
        /// Make it possible to move the form and maximize or minimize it
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
        
        /// <summary>
        /// Exit application
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnClose_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        /// <summary>
        /// Modify windowstate
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnMaximize_Click(object sender, EventArgs e)
        {
            if(WindowState == FormWindowState.Normal)
                WindowState = FormWindowState.Maximized;
            else
                WindowState = FormWindowState.Normal;
        }

        /// <summary>
        /// Minimize form
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnMinimize_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;   
        }

        #endregion Titlebar


        #region form load close
        private void frmMain_KeyDown(object sender, KeyEventArgs e)
        {            
            // Hide right submenus with escape key
            if (e.KeyCode == Keys.Escape)
            {
                HideSubMenus(true);
            }
        }

        #endregion form load close

    }
}
