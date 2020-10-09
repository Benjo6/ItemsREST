using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Items;
using ItemsREST.DBUtil;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ItemsREST.Controllers
{
    [Route("api/localItems")]
    [ApiController]
    public class ItemsController : ControllerBase
    {
        //DATA
        private static List<Item> _data = new List<Item>()
        {
            new Item(1, "tomat", 20),
            new Item(2, "Soda", 15),
            new Item(3, "Bread", 10),
            new Item(4, "Beer", 10),
            new Item(5, "Soda", 20),
            new Item(6, "Golf", 45)
        };

        private ManageItems mgr = new ManageItems();

        // GET: api/<ItemsController>
        /// <summary>
        /// Med den her funktion får du alle items i listen
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IEnumerable<Item> Get()
        {
            return mgr.Get();
        }
        // GET api/<ItemsController>/5
        /// <summary>
        /// Her skriver du et id nummer for at udskrive et item fra listen
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("{id}")]
        public Item Get(int id)
        {
            return mgr.Get(id);
        }

        // POST api/<ItemsController>
        /// <summary>
        /// Her indtaster du varibler til at skabe et item til listen
        /// </summary>
        /// <param name="value"></param>
        [HttpPost]
        public void Post([FromBody] Item value)
        {
            mgr.Opret(value);
        }

        // PUT api/<ItemsController>/5
        /// <summary>
        /// Her updater du dit item for et id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="value"></param>
        [HttpPut]
        [Route("{id}")]

        public void Put(int id, [FromBody] Item value)
        {
            mgr.OpdaterItem(id,value);
        }

        // DELETE api/<ItemsController>/5
        /// <summary>
        /// Her kan du vælger et item til at blive slette fra systemet
        /// </summary>
        /// <param name="id"></param>
        [HttpDelete]
        [Route("{id}")]

      
        public void Delete(int id)
        {
            mgr.DeleteItem(id);
        }
        /// <summary>
        /// Her indtaster du en string som finder alle items der indeholder bogstavet
        /// </summary>
        /// <param name="substring"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("Name/{substring}")]
        public IEnumerable<Item> GetFromSubstring(string substring)
        {
            var list = _data.FindAll(i => i.Desciption.ToLower().Contains(substring.ToLower()));
            return list;

        }


    }
}
