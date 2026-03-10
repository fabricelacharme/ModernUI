using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ModernUI
{
    
    
    public partial class btnTopMenu : UserControl
    {

        public new event EventHandler Click
        {
            add
            {
                base.Click += value;
                foreach (Control control in Controls)
                {
                    control.Click += value;
                }
            }
            remove
            {
                base.Click -= value;
                foreach (Control control in Controls)
                {
                    control.Click -= value;
                }
            }
        }

        private Image imageRight;
        
        public Image ImageRight
        {
            get { return pictRight.Image; }
            set { pictRight.Image = value; }
        }

        public Image Image
        {
            get { return btn.Image; }
            set { btn.Image = value; 
                    
            }
        }


        public string ButtonText
        {
            get { return btn.Text; }
            set { btn.Text = value; }
        }

        public btnTopMenu()
        {
            InitializeComponent();
        }

      
    }
}
