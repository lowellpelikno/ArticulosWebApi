using ArticulosWebApi.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Utilerias;
using static System.Runtime.InteropServices.JavaScript.JSType;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ArticulosWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArticulosController : ControllerBase
    {
        // GET: api/<ArticulosController>
        [HttpGet]
        public ResultList<Articulo> Get()
        {
            ResultList<Articulo> resultList = new();

            try
            {
                using (PruebasApiCoreContext cnt = new())
                {
                    resultList.Data = [.. cnt.Articulos];
                }
                resultList.Success = true;

            }
            catch (Exception er)
            {
                //resultList.Error = er;
                resultList.Success = false;
            }
            return resultList;
        }

        // GET api/<ArticulosController>/5
        [HttpGet("{id}")]
        public async Task<ResultObject<Articulo>> Get(int id)
        {
            ResultObject<Articulo> resultObj = new();

            try
            {
                using PruebasApiCoreContext cnt = new();
                resultObj.Data = await cnt.Articulos.FirstOrDefaultAsync(x => x.Id == id);
                resultObj.Success = resultObj.Data != null;

            }
            catch (Exception er)
            {
                //resultObj.Error = er;
                resultObj.Success = false;
            }
            return resultObj;
        }

        // POST api/<ArticulosController>
        [HttpPost]
        public async Task<ResultObject<Articulo>> Post([FromBody] Articulo data)
        {
            ResultObject<Articulo> resultObj = new();
            try
            {
                if (data.Id == 0)
                {
                    resultObj.Mensaje = "Alerta: ingrese un código de artículo diferente de cero...";
                    resultObj.Success = false;
                    return resultObj;
                }
                data.Id = 0;
                using PruebasApiCoreContext cnt = new();
                {
                    resultObj.Data = await cnt.Articulos.FirstOrDefaultAsync(x => x.Descripcion == data.Descripcion);
                    if (resultObj.Data != null && resultObj.Data.Id != data.Id)
                    {

                        resultObj.Mensaje = "Alerta: ya existe un articulo con esa descripcion, y con un codigo diferente...";
                        resultObj.Success = false;
                        return resultObj;
                    }

                    await cnt.Articulos.AddAsync(data);
                    await cnt.SaveChangesAsync();
                    resultObj.Mensaje = "Exito: artículo guardado...";
                    resultObj.Success = true;
                }
            }
            catch (Exception er)
            {
                //resultObj.Error = er;
                resultObj.Success = false;
            }
            return resultObj;
        }

        // PUT api/<ArticulosController>/5
        [HttpPut]
        public async Task<ResultObject<Articulo>> Put([FromBody] Articulo data)
        {
            ResultObject<Articulo> resultObj = new();
            try
            {
                if (data.Id == 0)
                {
                    resultObj.Mensaje = $"Alerta: no se encontro el código de artículo:<S1> = {data.Id}...";
                    resultObj.Success = false;
                    return resultObj;
                }

                using PruebasApiCoreContext cnt = new();
                {
                    resultObj.Data = await cnt.Articulos.FirstOrDefaultAsync(x => x.Id == data.Id);
                    Articulo? ar = await cnt.Articulos.FirstOrDefaultAsync(x => x.Descripcion == data.Descripcion);
                    if (resultObj.Data != null && ar != null)
                    {
                        if (resultObj.Data.Id != ar.Id)
                        {
                            resultObj.Mensaje = "Alerta: ya existe un articulo con esa descripcion, y con un codigo diferente...";
                            resultObj.Success = false;
                            return resultObj;
                        }
                    }else if(resultObj.Data == null)
                    {
                        resultObj.Mensaje = $"Alerta: no se encontro el código de artículo:<S2> = {data.Id}...";
                        resultObj.Success = false;
                        return resultObj;
                    }
                    resultObj.Data!.Descripcion = data.Descripcion;
                    resultObj.Data.Precio = data.Precio;
                    cnt.Entry(resultObj.Data).State = EntityState.Modified;
                    await cnt.SaveChangesAsync();
                    resultObj.Mensaje = "Exito: artículo Modificado...";
                    resultObj.Success = true;
                }
            }
            catch (Exception er)
            {
                //resultObj.Error = er;
                resultObj.Success = false;
            }
            return resultObj;
        }

        // DELETE api/<ArticulosController>/5
        [HttpDelete("{Id}")]
        public async Task<ResultBase> Delete(int Id)
        {
            ResultBase resultBase = new();
            try
            {
                if (Id == 0)
                {
                    resultBase.Mensaje = $"Alerta: no se encontro el código de artículo:<S1> = {Id}...";
                    resultBase.Success = false;
                    return resultBase;
                }

                using PruebasApiCoreContext cnt = new();
                {
                    Articulo? articulo = await cnt.Articulos.FirstOrDefaultAsync(x => x.Id == Id);
                    if (articulo == null)
                    {
                        resultBase.Mensaje = $"Alerta: no se encontro el código de artículo:<S2> = {Id}...";
                        resultBase.Success = false;
                        return resultBase;

                    }
                    cnt.Remove(articulo);
                    await cnt.SaveChangesAsync();
                    resultBase.Mensaje = "Exito: artículo eliminado...";
                    resultBase.Success = true;
                }
            }
            catch (Exception er)
            {
                //resultBase.Error = er;
                resultBase.Success = false;
            }
            return resultBase;
        }
    }
}
