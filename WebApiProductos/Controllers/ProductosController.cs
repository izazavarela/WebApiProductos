using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Unipluss.Sign.ExternalContract.Entities;
using WebApiProductos.Data;
using WebApiProductos.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApiProductos.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductosController : ControllerBase
    {
        private readonly AppDbContext _Context;

        public ProductosController(AppDbContext Context)
        {
            this._Context = Context;
        }

        // GET: api/<ProductosController>
        [HttpGet]
        public async Task<ActionResult<Productos>> GetProducts()
        {
            var List = await _Context.TblProductos.ToListAsync();
            return Ok(List);
        }

        // GET api/<ProductosController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Productos>> GetProduct(int id)
        {
            var ListP = await _Context.TblProductos.FindAsync(id);

            if (ListP == null)
            {
                return NotFound();
            }
            return Ok(ListP);
        }
        //[HttpGet("{id}")]
        //public async Task<ActionResult> GetP(int id)
        //{
        //    var List = await _Context.TblProductos.FirstOrDefaultAsync(P => P.PkCodigo == id);

        //    if(List == null)
        //    {
        //        return NotFound();
        //    }
        //    return Ok(List);
        //}

        // POST api/<ProductosController>
        [HttpPost]
        public async Task<ActionResult<Productos>> New(Productos Product)
        {
            _Context.TblProductos.Add(Product);
            await _Context.SaveChangesAsync();

            return CreatedAtAction("GetProduct", new { id = Product.PkCodigo }, Product);
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> Edit (int id, Productos Product)
        {
            if (id != Product.PkCodigo)
            {
                return BadRequest();
            }

            _Context.Entry(Product).State = EntityState.Modified;

            try
            {
                await _Context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (id != Product.PkCodigo)
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Productos>> DeleteProduct(int id)
        {
            var product = await _Context.TblProductos.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }

            _Context.TblProductos.Remove(product);
            await _Context.SaveChangesAsync();

            return product;
        }
    }
}

    
 

