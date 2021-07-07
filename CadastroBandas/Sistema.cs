using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace CadastroBandas
{
    public partial class Sistema : Form
    {

        int idalterar;

        public Sistema()
        {
            InitializeComponent();
            
        }
        

        private void button2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnCadastra_Click(object sender, EventArgs e)
        {
            marcador.Height = btnCadastra.Height;
            marcador.Top = btnCadastra.Top;
            tabControl1.SelectedTab = tabControl1.TabPages[0];
            

        }

        

        private void btnBusca_Click(object sender, EventArgs e)
        {
            marcador.Height = btnBusca.Height;
            marcador.Top = btnBusca.Top;
            tabControl1.SelectedTab = tabControl1.TabPages[1];
        }

        

   


        private void btnConfirmaRemocao_Click(object sender, EventArgs e)
        {
            
            int linha = dgBandas.CurrentRow.Index;
            int id = 
                Convert.ToInt32(dgBandas.Rows[linha].Cells["idbandas"]
                                .Value.ToString());
            DialogResult resp = MessageBox.Show("Tem certeza da exclusão?",
                "Remover", MessageBoxButtons.OKCancel);
            if(resp == DialogResult.OK) { 
                ConectaBanco con = new ConectaBanco();
                bool retorno = con.deletaBanda(id);
                if (retorno)
                    MessageBox.Show("Banda excluída com sucesso :)");
                else
                    MessageBox.Show("Erro:", con.mensagem);

                listaBandas();
            }// fim if confirmação
        }

        private void BtnConfirmaCadastro_Click(object sender, EventArgs e)
        {
            if (txtnome.Text.Length < 2 ||
                txtgenero.Text.Length <2 )
                MessageBox.Show("Preencha todos os campos");
            else { 
            Banda b = new Banda();
            b.Nome = txtnome.Text;
            b.Genero = txtgenero.Text;
            b.Integrantes = Convert.ToInt32(txtintegrantes.Text);
            b.Ranking = Convert.ToInt32(txtranking.Text);
            ConectaBanco con = new ConectaBanco();
            bool r = con.insereBanda(b);
            if (r == true)
                MessageBox.Show("Inserido com sucesso :)");
            else
                MessageBox.Show("Erro:" + con.mensagem);
            
            listaBandas();
            limpaForm();
            }// fim else do cadastro
        }

        void limpaForm()
        {
            txtnome.Clear();
            txtgenero.Clear();
            txtintegrantes.Clear();
            txtranking.Clear();
            txtnome.Focus();
        }

        void listaBandas()
        {
            ConectaBanco con = new ConectaBanco();      
            dgBandas.DataSource = con.listaBandas();
            dgBandas.Columns["idbandas"].Visible = false;

        }


        private void Sistema_Load(object sender, EventArgs e)
        {
            listaBandas();
        }

        private void txtnomeBusca_TextChanged(object sender, EventArgs e)
        {
            (dgBandas.DataSource as DataTable).DefaultView.RowFilter =
                string.Format("nome like '%{0}%'", txtnomeBusca.Text);
        }

        private void btnAlterar_Click(object sender, EventArgs e)
        {
            int linha = dgBandas.CurrentRow.Index;
            idalterar =
                Convert.ToInt32(dgBandas.Rows[linha].Cells["idbandas"]
                                .Value.ToString());
            txtAlteraNome.Text = dgBandas.Rows[linha].Cells["nome"].Value.ToString();
            txtAlteraGenero.Text = dgBandas.Rows[linha].Cells["genero"].Value.ToString();
            txtAlteraIntegrantes.Text = dgBandas.Rows[linha].Cells["integrantes"].Value.ToString();
            txtAlteraRanking.Text = dgBandas.Rows[linha].Cells["ranking"].Value.ToString();
            tabControl1.SelectedTab = tabAlterar;
        }

        private void btnConfirmaAlteracao_Click(object sender, EventArgs e)
        {
            Banda b = new Banda();
            b.Nome = txtAlteraNome.Text;
            b.Genero = txtAlteraGenero.Text;
            b.Integrantes = Convert.ToInt32(txtAlteraIntegrantes.Text);
            b.Ranking = Convert.ToInt32(txtAlteraRanking.Text);
            ConectaBanco con = new ConectaBanco();
            bool ret = con.alteraBanda(b, idalterar);
            if (ret == true)
                MessageBox.Show("Alterado com sucesso :)");
            else
                MessageBox.Show("Erro:", con.mensagem);
            
            listaBandas();
            tabControl1.SelectedTab = tabBuscar;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
        }
    }
}
