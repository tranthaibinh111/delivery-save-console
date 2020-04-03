using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;

using ANNShop.Model;

using Service;

namespace ANNShop.Service
{
  public class DeliverySaveAddressService : IService
  {
    #region Khởi tạo
    public DeliverySaveAddress insert(DeliverySaveAddress data)
    {
      using (var con = new ANNShopContext())
      {
        var address = con.DeliverySaveAddresses
					.Where(x => x.id == data.id)
					.FirstOrDefault();

        if (address != null)
        {
          address.name = data.name;
          address.pid = data.pid;
          address.type = data.type;
          address.region = data.region;
          address.alias = data.alias;
          address.is_picked = data.is_picked;
          address.is_delivered = data.is_delivered;
          con.SaveChanges();

          return address;
        }
        else
        {
          con.DeliverySaveAddresses.Add(data);
          con.SaveChanges();

          return data;
        }
      }
    }
    #endregion
  }
}