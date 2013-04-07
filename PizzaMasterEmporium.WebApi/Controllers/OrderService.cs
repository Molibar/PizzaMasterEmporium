using System.Collections.Generic;
using System.Web.Http;
using PizzaMasterEmporium.WebApi.OrderContext.ViewModels;

namespace PizzaMasterEmporium.WebApi.Controllers
{
    public class OrderService : ApiController
    {
        // GET api/values
        public IEnumerable<OrderViewModel> Get()
        {
            return new [] { new OrderViewModel(), new OrderViewModel() };
        }

        // GET api/values/5
        public OrderViewModel Get(int id)
        {
            return new OrderViewModel();
        }

        // POST api/values
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
        }
    }
}