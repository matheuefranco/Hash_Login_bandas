using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CadastroBandas
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            ConectaBanco con = new ConectaBanco();
            if (con.consultaUsuario(txtuser.Text, txtpass.Text) == true)
            {
                this.Hide();
                Sistema s = new Sistema();
                s.Show();
                
            }// fim if
            else
                MessageBox.Show("Usuário ou senha incorretos :(");
            
        }
    }
}
