using DAL;
using Entidades;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class RepositorioInscripcion : RepositorioBase<Inscripcion>
    {
        public override bool Guardar(Inscripcion entity)
        {
            bool paso = false;
            Contexto db = new Contexto();
            RepositorioBase<Estudiantes> dbE = new RepositorioBase<Estudiantes>();

            try
            {


                if (db.Inscripcion.Add(entity) != null)
                {
                    var estudiante = dbE.Buscar(entity.EstudianteId);
                    entity.CalcularMonto();
                    estudiante.Balance += entity.Monto;

                    paso = db.SaveChanges() > 0;
                    dbE.Modificar(estudiante);
                }

            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                db.Dispose();
                dbE.Dispose();
            }


            return paso;
        }

        public override bool Modificar(Inscripcion entity)
        {
            bool paso = false;
            Contexto db = new Contexto();
            RepositorioBase<Estudiantes> dbE = new RepositorioBase<Estudiantes>();


            try
            {
                var estudiante = dbE.Buscar(entity.EstudianteId);

                var anterior = new RepositorioBase<Inscripcion>().Buscar(entity.InscripcionId);

                estudiante.Balance -= anterior.Monto;

                foreach (var item in anterior.Asignaturas)
                {
                    if (!entity.Asignaturas.Any(A => A.InscripcionDetallesId == item.InscripcionDetallesId))
                    {
                        db.Entry(item).State = EntityState.Deleted;

                    }

                }

                foreach (var item in entity.Asignaturas)
                {
                    if (item.InscripcionDetallesId == 0)
                    {
                        db.Entry(item).State = EntityState.Added;
                    }

                    else
                        db.Entry(item).State = EntityState.Modified;
                }


                entity.CalcularMonto();
                estudiante.Balance += entity.Monto;
                dbE.Modificar(estudiante);

                db.Entry(entity).State = EntityState.Modified;

                paso = db.SaveChanges() > 0;


            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                db.Dispose();
                dbE.Dispose();
            }


            return paso;
        }

        public override bool Elimimar(int id)
        {

            bool paso = false;
            Contexto db = new Contexto();

            try
            {

                var eliminar = db.Inscripcion.Find(id);

                db.Entry(eliminar).State = EntityState.Deleted;

                paso = db.SaveChanges() > 0;


            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                db.Dispose();
            }

            return paso;
        }

        public bool RestarBalance(int id, decimal monto)
        {
            bool paso = false;
            RepositorioBase<Estudiantes> db = new RepositorioBase<Estudiantes>();


            try
            {
                var estudiante = db.Buscar(id);
                estudiante.Balance -= monto;
                db.Modificar(estudiante);

            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                db.Dispose();
            }

            return paso;
        }

        public bool Existe(int id)
        {
            bool paso = false;
            Contexto db = new Contexto();

            try
            {
                if (db.Inscripcion.Find(id) != null)
                {
                    paso = true;
                }

            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                db.Dispose();
            }



            return paso;
        }





    }
}
