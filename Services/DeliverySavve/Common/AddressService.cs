using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;

using DeliverySave.Model;

using Service;

namespace DeliverySave.Service
{
  public class AddressService : IService
  {
    #region Lấy thông tin
    public List<Address> getAll()
    {
      using (var con = new DeliverySaveContext())
      {
        var address = con.Addresses.ToList();

        return address;
      }
    }

    public List<Address> getAllProvince()
    {
      using (var con = new DeliverySaveContext())
      {
        var provinces = con.Addresses.
          Where(x => x.pid == null)
          .ToList();

        return provinces;
      }
    }

    public List<Address> getAllDistrict(string provinceID)
    {
      using (var con = new DeliverySaveContext())
      {
        var districts = con.Addresses
          .Where(x => x.pid == provinceID)
          .ToList();

        return districts;
      }
    }

    public List<Address> getAllWard(string districtID)
    {
      using (var con = new DeliverySaveContext())
      {
        var wards = con.Addresses
          .Where(x => x.pid == districtID)
          .ToList();

        return wards;
      }
    }
    #endregion

    #region Khởi tạo
    public Address insert(Address data)
    {
      using (var con = new DeliverySaveContext())
      {
        var address = con.Addresses.Where(x => x.id == data.id).FirstOrDefault();

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
          con.Addresses.Add(data);
          con.SaveChanges();

          return data;
        }
      }
    }

    public List<Address> insert(List<Address> data)
    {
      using (var con = new DeliverySaveContext())
      {
        var addressNew = new List<Address>();

        foreach (var item in data)
        {
          var address = con.Addresses.Where(x => x.id == item.id).FirstOrDefault();

          if (address != null)
          {
            address.name = item.name;
            address.pid = item.pid;
            address.type = item.type;
            address.region = item.region;
            address.alias = item.alias;
            address.is_picked = item.is_picked;
            address.is_delivered = item.is_delivered;
            con.SaveChanges();
          }
          else
          {
            addressNew.Add(item);
          }
        }

        con.Addresses.AddRange(addressNew);
        con.SaveChanges();

        return data;
      }
    }
    #endregion
  }
}