using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;

using ANNShop.Controller;

using DeliverySave.Controller;

using Newtonsoft.Json;


namespace DeliverySave
{
  class Program
  {
    static void Main(string[] args)
    {
      if (!File.Exists("delivery_save.db"))
      {
        Console.WriteLine("Don't find the delivery_save.db");
        Console.ReadLine();
        return;
      }

      #region Lấy dữ liệu Address từ API Giao Hàng tiết kiệm
      bool resultGetProvince = false;
      var resultGetDistrict = false;
      var resultGetWard = false;

      var addressControll = new AddressController();

      resultGetProvince = addressControll.getAllProvince();
      if (!resultGetProvince)
      {
        Console.WriteLine("Error: Address Controller - Get ALL Provice");
        Console.ReadLine();
        return;
      }

      resultGetDistrict = addressControll.getAllDistrict();
      if (!resultGetDistrict)
      {
        Console.WriteLine("Error: Address Controller - Get ALL District");
        Console.ReadLine();
        return;
      }

      resultGetWard = addressControll.getAllWard();
      if (!resultGetWard)
      {
        Console.WriteLine("Error: Address Controller - Get ALL Ward");
        Console.ReadLine();
        return;
      }
      #endregion

      #region Thực hiện đồng bộ hoá dữ liệu address
      var addressANNController = new DeliverySaveAddressController();

      addressANNController.syncAddressSQLite();
      #endregion

      Console.ReadLine();
    }
  }
}
