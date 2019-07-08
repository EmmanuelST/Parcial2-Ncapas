using BLL;
using Entidades;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Parcial2
{
    public partial class cAsiganturas : Form
    {
        public cAsiganturas()
        {
            InitializeComponent();
            Buscar();
        }

        private void Buscarbutton_Click(object sender, EventArgs e)
        {
            Buscar();

        }

        private void Buscar()
        {
            var listado = new List<Asignaturas>();
            RepositorioBase<Asignaturas> db = new RepositorioBase<Asignaturas>();

            if (CriteriotextBox.Text.Trim().Length > 0)
            {

                try
                {
                    switch (FiltrocomboBox.SelectedIndex)
                    {
                        case 0://Todo
                            listado = db.GetList(A => true);
                            break;

                        case 1://ID
                            int id = Convert.ToInt32(CriteriotextBox.Text);
                            listado = db.GetList(p => p.AsignaturaId == id);
                            break;

                        case 2://Descripcion
                            listado = db.GetList(A => A.Descripcion.Contains(CriteriotextBox.Text));
                            break;

                    }
                }
                catch (Exception)
                {

                }

            }
            else
            {
                listado = db.GetList(p => true);
            }

            ConsultadataGridView.DataSource = null;
            ConsultadataGridView.DataSource = listado;
        }
    }
}
