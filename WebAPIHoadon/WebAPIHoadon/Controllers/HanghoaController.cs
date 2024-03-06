using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using WebAPIHoadon.Models;

namespace WebAPIHoadon.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HanghoaController : ControllerBase
    {
        private hoadonContext db = new hoadonContext();
        [HttpGet]
        public IActionResult getDSHanghoa()
        {
            try
            {
                return Ok(db.Hanghoas.ToList());
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }
        [HttpGet("{id}")]
        public IActionResult getHanghoa(string id)
        {
            try{
                Hanghoa a = db.Hanghoas.Find(id);
                if (a == null) return NotFound();
                else return Ok(a);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }
        [HttpDelete("{id}")]
        public IActionResult deleteHanghoa(string id)
        {
            try
            {
                Hanghoa a = db.Hanghoas.Find(id);
                if (a == null) return NotFound();
                else
                {
                    db.Hanghoas.Remove(a);
                    db.SaveChanges();
                    return Ok();
                }
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }
        [HttpPost]
        public IActionResult postHanghoa(Hanghoa h)
        {
            try
            {
                db.Hanghoas.Add(h);
                db.SaveChanges();
                return Ok();
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }
        [HttpPut]
        public IActionResult putHanghoa(Hanghoa h)
        {
            try
            {
                Hanghoa x = db.Hanghoas.Find(h.Mahang);
                if (x == null) 
                    return NotFound();
                x.Tenhang= h.Tenhang;
                x.Dvt= h.Dvt;
                x.Dongia= h.Dongia;

                db.SaveChanges();
                return Ok();
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }
        [HttpGet("Hoadon")]
        public IActionResult getDSHoadon()
        {
            try
            {
                return Ok(db.Hoadons.ToList());
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }
        [HttpPost("Hoadon")]
        public IActionResult postHoadon(Hoadon hd)
        {
            try
            {
                db.Hoadons.Add(hd);
                db.SaveChanges();
                return Ok();
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }
        [HttpGet("ChitietHoadon/{id}")]
        public IActionResult getDSChitietHoadon(string id)
        {
            try
            {
                List<Chitiethoadon> ds=db.Chitiethoadons.Where(t=>t.Sohd==id).ToList();
                if (ds == null) return NotFound();
                else return Ok(ds);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }
    }
}
