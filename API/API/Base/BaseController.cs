using API.Repository.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace API.Base
{
    [Route("api/[controller]")]
    [ApiController]
    public class BaseController<Entity, Repository, Key> : ControllerBase
        where Entity : class
        where Repository : IRepository<Entity, Key>
    {
        private readonly Repository repository;
        public BaseController(Repository repository)
        {
            this.repository = repository;
        }

        [HttpGet]
        public ActionResult<Entity> Get()
        {
            var getEntity = repository.Get();
            if (getEntity.ToList().Count > 0)
            {
                return Ok(getEntity);
            }
            else
            {
                return NotFound(new { status = HttpStatusCode.NotFound, result = getEntity, message = "Tidak ada data di sini" });
            }
        }

        [HttpGet("{key}")]
        public ActionResult Get(Key key)
        {
            var ada = repository.Get(key);
            if (ada != null)
            {
                return Ok(ada);
            }
            else
            {
                return NotFound(new { status = HttpStatusCode.NotFound, result = ada, message = "Data tersebut tidak ditemukan" });
            }
        }

        [HttpPost]
        public ActionResult Post(Entity entity)
        {
            try
            {
                var result = repository.Insert(entity);
                return Ok(new { status = HttpStatusCode.OK, result = result, message = "Data berhasil dimasukkan" });
            }
            catch
            {
                return BadRequest(new { status = HttpStatusCode.BadRequest, message = "Data gagal dimasukkan: ID yang Anda masukkan sudah terdaftar!" });
            }
        }

        [HttpDelete("{key}")]
        public ActionResult Delete(Key key)
        {
            var exist = repository.Get(key);
            try
            {
                var result = repository.Delete(key);
                return Ok(new { status = HttpStatusCode.OK, result = exist, message = "Data berhasil dihapus" });
            }
            catch
            {
                return NotFound(new { status = HttpStatusCode.NotFound, result = exist, message = "Data tersebut tidak ditemukan" });
            }
        }

        [HttpPut("{key}")]
        public ActionResult Update(Entity entity, Key key)
        {
            try
            {
                var result = repository.Update(entity, key);
                /*return Ok(new { status = HttpStatusCode.OK, message = "Data tersebut berhasil diupdate" });*/
                return Ok(result);
            }
            catch
            {
                return NotFound(new { status = HttpStatusCode.NotFound, message = "Data tersebut tidak ditemukan" });
            }
        }
    }
}
