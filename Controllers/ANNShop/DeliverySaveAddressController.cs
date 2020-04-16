using System;
using System.Linq;
using Newtonsoft.Json;

using ANNShop.Model;
using ANNShop.Service;

using DeliverySave.Model;
using DeliverySave.Service;

using Service;

namespace ANNShop.Controller
{
  public class DeliverySaveAddressController
  {
    private DeliverySaveAddressService _addressService;

    public DeliverySaveAddressController()
    {
      _addressService = FactoryService.getInstance<DeliverySaveAddressService>();
    }

    public void syncAddressSQLite()
    {
      Console.WriteLine("Bat dau lay thong tin Address tu data SQLite");
      var addressSQLite = FactoryService.getInstance<AddressService>();

      var addresses = addressSQLite.getAll()
        .Select(x =>
        {
          try {
            var item = new DeliverySaveAddress()
            {
              ID = Convert.ToInt32(x.id),
              Name = x.name,
              PID = String.IsNullOrEmpty(x.pid) ? (int?)null : Convert.ToInt32(x.pid),
              Type = Convert.ToInt32(x.type),
              Region = String.IsNullOrEmpty(x.region) ? (int?)null : Convert.ToInt32(x.region),
              Alias = x.alias,
              IsPicked = x.is_picked == "1" ? true : false,
              IsDelivered = x.is_delivered == "1" ? true : false
            };

            return item;
          } catch (Exception e) {
            throw e;
          }
        })
        .ToList();

      try
      {
        Console.WriteLine("Bat dau dong bo ho du lieu table DeliverySaveAdress");

        foreach (var item in addresses)
        {
          _addressService.insert(item);
          Console.WriteLine("Success: " + JsonConvert.SerializeObject(item));
        }

        Console.WriteLine("Bat ket thuc dong bo ho du lieu table DeliverySaveAdress");
      }
      catch (Exception e)
      {
        Console.WriteLine("Error - Delivery Save Address Controller");
        Console.WriteLine(e);
      }
    }
  }
}