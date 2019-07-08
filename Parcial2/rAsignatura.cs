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
    public partial class rAsignatura : Form
    {
       

        public rAsignatura()
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
            DescripciontextBox.Text = string.Empty;
            CreditosnumericUpDown.Value = 0;
            errorProvider.Clear();

        }

        private void Guardarbutton_Click(object sender, EventArgs e)
        {

            if (!Validar())
                return;

            RepositorioBase<Asignaturas> db = new RepositorioBase<Asignaturas>();
            RepositorioBase<Asignaturas> temp = new RepositorioBase<Asignaturas>();
            Asignaturas asigantura = new Asignaturas();
            DescripciontextBox.Text = DescripciontextBox.Text.Trim();
            asigantura = LlenarClase();

            try
            {


                if (IdnumericUpDown.Value == 0)
                {
                    if (db.Repetido(A => A.Descripcion == DescripciontextBox.Text))
                    {
                        MessageBox.Show("Esta asignatura ya esta registrada", "Atencion!!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    if (db.Guardar(asigantura))
                    {
                        MessageBox.Show("Guardado correctamente", "Atencion!!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        Limpiar();
                    }
                    else
                    {
                        MessageBox.Show("No se pudo guardar", "Atencion!!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                else
                {
                    var asigantura2 = temp.Buscar((int)IdnumericUpDown.Value);

                    if (DescripciontextBox.Text != asigantura2.Descripcion)
                    {
                        if (db.Repetido(A => A.Descripcion == DescripciontextBox.Text))
                        {
                            MessageBox.Show("Esta asignatura ya esta registrada", "Atencion!!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                    }

                    if (db.Modificar(asigantura))
                    {
                        MessageBox.Show("Modificado correctamente", "Atencion!!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        Limpiar();
                    }
                    else
                    {
                        MessageBox.Show("No se pudo moficar", "Atencion!!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }

            }
            catch (Exception)
            {
                MessageBox.Show("Hubo un error", "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }




        }

        private bool Validar()
        {
            bool paso = true;
            errorProvider.Clear();

            if (string.IsNullOrWhiteSpace(DescripciontextBox.Text))
            {
                errorProvider.SetError(DescripciontextBox, "Este campo no puede estar vacio");
                paso = false;
            }

            if (CreditosnumericUpDown.Value == 0)
            {
                errorProvider.SetError(CreditosnumericUpDown, "Los creditos no pueden ser cero");
                paso = false;

            }

            if (CreditosnumericUpDown.Value > 10)
            {
                errorProvider.SetError(CreditosnumericUpDown, "Una Asignatura no puede tener mas de 5 creditos");
                paso = false;

            }


            return paso;
        }

        private void LlenarCampos(Asignaturas asignatura)
        {
            IdnumericUpDown.Value = asignatura.AsignaturaId;
            DescripciontextBox.Text = asignatura.Descripcion;
            CreditosnumericUpDown.Value = asignatura.Creditos;

        }

        private Asignaturas LlenarClase()
        {
            Asignaturas asignatura = new Asignaturas();

            asignatura.AsignaturaId = (int)IdnumericUpDown.Value;
            asignatura.Descripcion = DescripciontextBox.Text;
            asignatura.Creditos = (int)CreditosnumericUpDown.Value;

            return asignatura;

        }

        private void Eliminarbutton_Click(object sender, EventArgs e)
        {
            RepositorioBase<Asignaturas> db = new RepositorioBase<Asignaturas>();
            errorProvider.Clear();

            try
            {
                if (IdnumericUpDown.Value > 0)
                {
                    if (db.Elimimar((int)IdnumericUpDown.Value))
                    {
                        Limpiar();
                        MessageBox.Show("Eliminado correctamente", "Atencion!!", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    }
                    else
                    {
                        MessageBox.Show("No se pudo eliminar", "Atencion!!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }

                }
                else
                {
                    errorProvider.SetError(IdnumericUpDown, "Este campo no puede ser cero");
                    ;
                }


            }
            catch (Exception)
            {
                MessageBox.Show("Hubo un error", "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void Buscarbutton_Click(object sender, EventArgs e)
        {
            RepositorioBase<Asignaturas> db = new RepositorioBase<Asignaturas>();
            Asignaturas asignatura;
            errorProvider.Clear();

            try
            {

                if (IdnumericUpDown.Value > 0)
                {
                    if ((asignatura = db.Buscar((int)IdnumericUpDown.Value)) != null)
                    {
                        Limpiar();
                        LlenarCampos(asignatura);
                    }
                    else
                    {
                        MessageBox.Show("No se encontro la asignatura", "Atencion!!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }

                }
                else
                {
                    errorProvider.SetError(IdnumericUpDown, "Este campo no puede ser cero");
                }


            }
            catch (Exception)
            {
                MessageBox.Show("Hubo un error", "Atencion!!", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }
    }
}
