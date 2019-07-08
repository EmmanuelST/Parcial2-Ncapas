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
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void RegistroDeEstudiantesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            rEstudiante registro = new rEstudiante();
            registro.MdiParent = this;
            registro.Show();
        }

        private void RegistroDeAsignaturasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            rAsignatura registro = new rAsignatura();
            registro.MdiParent = this;
            registro.Show();
        }

        private void InscripcionesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            rInscripciones registro = new rInscripciones();
            registro.MdiParent = this;
            registro.Show();
        }

        private void ConsultarAsignaturasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            cAsiganturas consulta = new cAsiganturas();
            consulta.MdiParent = this;
            consulta.Show();
        }

        private void ConsultarEstudiantesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            cEstudiantes consulta = new cEstudiantes();
            consulta.MdiParent = this;
            consulta.Show();
        }

        private void ConsultaDeInscripcionesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            cInscripciones consulta = new cInscripciones();
            consulta.MdiParent = this;
            consulta.Show();
        }
    }
}
