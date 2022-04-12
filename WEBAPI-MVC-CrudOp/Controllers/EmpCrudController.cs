using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WEBAPI_MVC_CrudOp.Models;

namespace WEBAPI_MVC_CrudOp.Controllers
{
    public class EmpCrudController : ApiController
    {
        WebApiDBEntities we = new WebApiDBEntities();
        public IHttpActionResult getemp()
        {
          
            var results = we.Persons.ToList();
            return Ok(results);
        }
        [HttpPost]
        public IHttpActionResult EmpInsert(Person empinsert)
        {
            we.Persons.Add(empinsert);
            we.SaveChanges();
            return Ok();

        }
        public IHttpActionResult GetEmpId(int id)
        {
            EmpClass empdetails = null;
            empdetails = we.Persons.Where(x => x.PersonID == id).Select(x => new EmpClass()
            {
                PersonID = x.PersonID,
                LastName = x.LastName,
                FirstName = x.FirstName,
                Address = x.Address,
                City = x.City

            }).FirstOrDefault <EmpClass>();
            if (empdetails == null)
            {
                return NotFound();
            }
            return Ok(empdetails);
        }

        public IHttpActionResult Put(EmpClass ec) {
            var updateEmp = we.Persons.Where(x => x.PersonID == ec.PersonID).FirstOrDefault<Person>();
            if(updateEmp != null)
            {
                updateEmp.PersonID = ec.PersonID;
                updateEmp.LastName = ec.LastName;
                updateEmp.FirstName = ec.FirstName;
                updateEmp.Address = ec.Address;
                updateEmp.City = ec.City;
                we.SaveChanges();
            }
            else
            {
                return NotFound();
            }
            return Ok(updateEmp);

        }
        public IHttpActionResult Delete(int id) {
            var empdel = we.Persons.Where(x => x.PersonID == id).FirstOrDefault();
            we.Entry(empdel).State = System.Data.Entity.EntityState.Deleted;
            we.SaveChanges();
            return Ok();


        }

    }
}
