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
    public class AsistenciasController : ApiController
    {
        private EventosEntities db = new EventosEntities();
        /*
        // GET: api/Asistencias
        public IQueryable<ASIST_LISTAEVENTO> GetASIST_LISTAEVENTO()
        {
            return db.ASIST_LISTAEVENTO;
        }

        // GET: api/Asistencias/5
        [ResponseType(typeof(ASIST_LISTAEVENTO))]
        public IHttpActionResult GetASIST_LISTAEVENTO(int id)
        {
            ASIST_LISTAEVENTO aSIST_LISTAEVENTO = db.ASIST_LISTAEVENTO.Find(id);
            if (aSIST_LISTAEVENTO == null)
            {
                return NotFound();
            }

            return Ok(aSIST_LISTAEVENTO);
        }
        */
        // PUT: api/Asistencias/5
        [HttpPut]
        [ResponseType(typeof(void))]
        public IHttpActionResult PutASIST_LISTAEVENTO(int id, ASIST_LISTAEVENTO aSIST_LISTAEVENTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != aSIST_LISTAEVENTO.ID_LISTA)
            {
                return BadRequest();
            }
            //hace cambios de todos. 
            //db.Entry(aSIST_LISTAEVENTO).State = EntityState.Modified;
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
        
        // POST: api/Asistencias
        [ResponseType(typeof(ASIST_LISTAEVENTO))]
        public IHttpActionResult PostASIST_LISTAEVENTO(ASIST_LISTAEVENTO aSIST_LISTAEVENTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.ASIST_LISTAEVENTO.Add(aSIST_LISTAEVENTO);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = aSIST_LISTAEVENTO.ID_LISTA }, aSIST_LISTAEVENTO);
        }

        // DELETE: api/Asistencias/5
        [ResponseType(typeof(ASIST_LISTAEVENTO))]
        public IHttpActionResult DeleteASIST_LISTAEVENTO(int id)
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

        private bool ASIST_LISTAEVENTOExists(int id)
        {
            return db.ASIST_LISTAEVENTO.Count(e => e.ID_LISTA == id) > 0;
        }
    }
}