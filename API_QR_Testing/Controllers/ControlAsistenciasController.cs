using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using API_QR_Testing.Models;

namespace API_QR_Testing.Controllers
{
    public class ControlAsistenciasController : ApiController
    {
        private EntitiesOracle db = new EntitiesOracle();

        // GET: api/ControlAsistencias
        public IQueryable<ASIST_LISTAEVENTO> GetASIST_LISTAEVENTO()
        {
            return db.ASIST_LISTAEVENTO;
        }

        // GET: api/ControlAsistencias/5
        [ResponseType(typeof(ASIST_LISTAEVENTO))]
        public IHttpActionResult GetASIST_LISTAEVENTO(decimal id)
        {
            ASIST_LISTAEVENTO aSIST_LISTAEVENTO = db.ASIST_LISTAEVENTO.Find(id);
            if (aSIST_LISTAEVENTO == null)
            {
                return NotFound();
            }

            return Ok(aSIST_LISTAEVENTO);
        }

        // PUT: api/ControlAsistencias/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutASIST_LISTAEVENTO(decimal id, ASIST_LISTAEVENTO aSIST_LISTAEVENTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != aSIST_LISTAEVENTO.ID_LISTA)
            {
                return BadRequest();
            }

            //db.Entry(aSIST_LISTAEVENTO).State = EntityState.Modified;
            //hace cambios de todos.
            var consulta = (from p in db.ASIST_LISTAEVENTO
                            where p.ID_LISTA == id
                            select p).SingleOrDefault();
            consulta.ASISTE = "S";

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ASIST_LISTAEVENTOExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/ControlAsistencias
        [ResponseType(typeof(ASIST_LISTAEVENTO))]
        public IHttpActionResult PostASIST_LISTAEVENTO(ASIST_LISTAEVENTO aSIST_LISTAEVENTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.ASIST_LISTAEVENTO.Add(aSIST_LISTAEVENTO);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (ASIST_LISTAEVENTOExists(aSIST_LISTAEVENTO.ID_LISTA))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = aSIST_LISTAEVENTO.ID_LISTA }, aSIST_LISTAEVENTO);
        }

        // DELETE: api/ControlAsistencias/5
        [ResponseType(typeof(ASIST_LISTAEVENTO))]
        public IHttpActionResult DeleteASIST_LISTAEVENTO(decimal id)
        {
            ASIST_LISTAEVENTO aSIST_LISTAEVENTO = db.ASIST_LISTAEVENTO.Find(id);
            if (aSIST_LISTAEVENTO == null)
            {
                return NotFound();
            }

            db.ASIST_LISTAEVENTO.Remove(aSIST_LISTAEVENTO);
            db.SaveChanges();

            return Ok(aSIST_LISTAEVENTO);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ASIST_LISTAEVENTOExists(decimal id)
        {
            return db.ASIST_LISTAEVENTO.Count(e => e.ID_LISTA == id) > 0;
        }
    }
}