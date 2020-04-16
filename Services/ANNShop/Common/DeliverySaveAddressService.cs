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
        var address = con.DeliverySaveAddress
					.Where(x => x.ID == data.ID)
					.FirstOrDefault();

        if (address != null)
        {
          address.Name = data.Name;
          address.PID = data.PID;
          address.Type = data.Type;
          address.Region = data.Region;
          address.Alias = data.Alias;
          address.IsPicked = data.IsPicked;
          address.IsDelivered = data.IsDelivered;
          con.SaveChanges();

          return address;
        }
        else
        {
          con.DeliverySaveAddress.Add(data);
          con.SaveChanges();

          return data;
        }
      }
    }
    #endregion
  }
}