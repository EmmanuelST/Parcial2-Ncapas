using BLL;
using Entidades;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Parcial2
{
    public partial class rEstudiante : Form
    {
        //TODO: Valida perfectamente el formulario de Estudiante
        public rEstudiante()
        {
            InitializeComponent();
        }

        private void Nuevobutton_Click(object sender, EventArgs e)
        {
            Limpiar();
        }

        private void Limpiar()
        {
            IdnumericUpDown.Value = 0;
            NombretextBox.Text = string.Empty;
            FechadateTimePicker.Value = DateTime.Now;
            BalancetextBox.Text = "0";

        }

        private void Guadarbutton_Click(object sender, EventArgs e)
        {

            if (!validar())
                return;

            try
            {
                Estudiantes estudiante = new Estudiantes();
                estudiante = LlenarClase();
                RepositorioBase<Estudiantes> db = new RepositorioBase<Estudiantes>();

                if (IdnumericUpDown.Value == 0)
                {
                    db.Guardar(estudiante);
                    MessageBox.Show("Guardado Correctamente", "Atencion!!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    db.Modificar(estudiante);
                    MessageBox.Show("Modificado Correctamente", "Atencion!!", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }

                Limpiar();

            }
            catch (Exception)
            {
                MessageBox.Show("Hubo un error", "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
        private bool validar()
        {
            bool paso = true;
            errorProvider.Clear();

            if (string.IsNullOrWhiteSpace(NombretextBox.Text))
            {
                errorProvider.SetError(NombretextBox, "Este campo no puede estar vacio");
                paso = false;
            }


            if (FechadateTimePicker.Value > DateTime.Now)
            {
                errorProvider.SetError(FechadateTimePicker, "La fecha no puede ser mayor que la de hoy");
                paso = false;
            }

            var regex = new Regex(@"^[A-Za-z ]+$");
            if (!(regex.IsMatch(NombretextBox.Text)))
            {
                errorProvider.SetError(NombretextBox, "El nombre solo debe contener letras");
                paso = false;
            }


            return paso;
        }

        private Estudiantes LlenarClase()
        {
            Estudiantes estudiante = new Estudiantes();

            estudiante.Nombre = NombretextBox.Text;
            estudiante.EstudianteId = (int)IdnumericUpDown.Value;
            estudiante.FechaIngreso = FechadateTimePicker.Value;
            estudiante.Balance = Decimal.Parse(BalancetextBox.Text);

            return estudiante;
        }

        private void Eliminarbutton_Click(object sender, EventArgs e)
        {
            RepositorioBase<Estudiantes> db = new RepositorioBase<Estudiantes>();
            errorProvider.Clear();

            try
            {
                if (IdnumericUpDown.Value > 0)
                {
                    if (db.Buscar((int)IdnumericUpDown.Value).Balance > 0)
                    {
                        MessageBox.Show("No se puede eliminar este estudiante por que tiene balance pendiente", "Atencion!!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                    if (db.Elimimar((int)IdnumericUpDown.Value))
                    {
                        MessageBox.Show("Eliminado Correctamente", "Atencion!!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        Limpiar();
                    }
                    else
                    {
                        MessageBox.Show("No se puede eliminar", "Atencion!!", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                    }

                }
                else
                {
                    errorProvider.SetError(IdnumericUpDown, "Este campo no puede ser cero");
                }


            }
            catch (Exception)
            {
                MessageBox.Show("Hubo un error eliminando", "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Buscarbutton_Click(object sender, EventArgs e)
        {
            RepositorioBase<Estudiantes> db = new RepositorioBase<Estudiantes>();
            errorProvider.Clear();
            try
            {

                if (IdnumericUpDown.Value > 0)
                {
                    Estudiantes estudiante = new Estudiantes();
                    if ((estudiante = db.Buscar((int)IdnumericUpDown.Value)) != null)
                    {
                        Limpiar();
                        LlenarCampos(estudiante);
                    }
                    else
                    {
                        MessageBox.Show("No se pudo encontrar", "Atencion!!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                else
                {
                    errorProvider.SetError(IdnumericUpDown, "Este valor no puede ser cero");
                }

            }
            catch (Exception)
            {
                MessageBox.Show("Hubo un error al buscar", "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void LlenarCampos(Estudiantes estudiante)
        {

            IdnumericUpDown.Value = estudiante.EstudianteId;
            NombretextBox.Text = estudiante.Nombre;
            FechadateTimePicker.Value = estudiante.FechaIngreso;
            BalancetextBox.Text = estudiante.Balance.ToString();

        }
    }
}
