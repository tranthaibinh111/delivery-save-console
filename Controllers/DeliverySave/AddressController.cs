using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;

using Service;
using DeliverySave.Service;
using DeliverySave.Model;

namespace DeliverySave.Controller
{
  public class AddressController
  {
    private AddressService _addressService;

    public AddressController()
    {
      _addressService = FactoryService.getInstance<AddressService>();
    }

    public bool getAllProvince()
    {
      Console.WriteLine("Bat dau lay thong tin tinh / thanh pho tu API Giao Hang Tiet Kiem");
      var result = false;
      var url = "https://khachhang.giaohangtietkiem.vn/khach-hang/services/list-dia-chi-public";

      // Excute API
      HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
      request.Method = "GET";

      var response = (HttpWebResponse)request.GetResponse();

      if (response != null && response.StatusCode == HttpStatusCode.OK)
      {
        try
        {
          using (var reader = new StreamReader(response.GetResponseStream()))
          {
            var data = JsonConvert.DeserializeObject<List<Address>>(reader.ReadToEnd());

            foreach (var item in data)
            {
              var task = _addressService.insert(item);

              if (task != null)
                Console.WriteLine("Success: " + JsonConvert.SerializeObject(item));
              else
                Console.WriteLine("Failed: " + JsonConvert.SerializeObject(item));
            }

            result = true;
          }
        }
        catch (Exception e)
        {
          Console.WriteLine("Error: Lay thong tin tinh / thanh pho");
          Console.WriteLine(e);

          result = false;
        }
      }
      else
      {
        Console.WriteLine("Error: Lay thong tin tinh / thanh pho");
        Console.WriteLine("Get API: That bai");

        result = false;
      }

      Console.WriteLine("Ket thuc lay thong tin tinh / thanh pho tu API Giao Hang Tiet Kiem");
      return result;
    }

    public bool getAllDistrict()
    {
      var taskProvinces = _addressService.getAllProvince();

      if (taskProvinces == null || taskProvinces.Count == 0)
      {
        Console.WriteLine("Hien tai trong database khong co tinh / thanh pho nao het ^-^.");
        return true;
      }

      Console.WriteLine("Bat dau lay thong tin quan / huyen tu API Giao Hang Tiet Kiem");
      var result = false;
      var api = "https://khachhang.giaohangtietkiem.vn/khach-hang/services/list-dia-chi-public?type=3";

      foreach (var province in taskProvinces)
      {
        Console.WriteLine(String.Format("Tinh / Thanh Pho: {0}", province.name));

        // Excute API
        var url = String.Format("{0}&parentId={1}", api, province.id);
        HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
        request.Method = "GET";

        var response = (HttpWebResponse)request.GetResponse();

        if (response != null && response.StatusCode == HttpStatusCode.OK)
        {
          try
          {
            using (var reader = new StreamReader(response.GetResponseStream()))
            {
              var data = JsonConvert.DeserializeObject<List<Address>>(reader.ReadToEnd());

              foreach (var item in data)
              {
                var task = _addressService.insert(item);

                if (task != null)
                  Console.WriteLine("Success: " + JsonConvert.SerializeObject(item));
                else
                  Console.WriteLine("Failed: " + JsonConvert.SerializeObject(item));
              }

              result = true;
            }
          }
          catch (Exception e)
          {
            Console.WriteLine("Error: Lay thong tin quan / huyen");
            Console.WriteLine(e);

            result = false;
          }
        }
        else
        {
          Console.WriteLine("Error: Lay thong tin quan / huyen");
          Console.WriteLine("Get API: That bai");

          result = false;
        }
      }
      Console.WriteLine("Ket thuc lay thong tin quan / huyen tu API Giao Hang Tiet Kiem");
      return result;
    }

    public bool getAllWard()
    {
      var taskProvinces = _addressService.getAllProvince();

      if (taskProvinces == null || taskProvinces.Count == 0)
      {
        Console.WriteLine("Hien tai trong database khong co tinh / thanh pho nao het ^-^.");
        return true;
      }

      var districts = new List<Address>();
      foreach (var province in taskProvinces)
      {
        var taskDistrict = _addressService.getAllDistrict(province.id);

        if (taskDistrict != null && taskDistrict.Count > 0)
          districts.AddRange(taskDistrict);
      }

      if (districts.Count == 0)
      {
        Console.WriteLine("Hien tai trong database khong co quan / huyen nao het ^-^.");
        return true;
      }

      Console.WriteLine("Bat dau lay thong tin phuong / xa tu API Giao Hang Tiet Kiem");
      var result = false;
      var api = "https://khachhang.giaohangtietkiem.vn/khach-hang/services/list-dia-chi-public?type=1";

      foreach (var district in districts)
      {
        Console.WriteLine(String.Format("Quan / Huyen: {0}", district.name));

        // Excute API
        var url = String.Format("{0}&parentId={1}", api, district.id);
        HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
        request.Method = "GET";

        var response = (HttpWebResponse)request.GetResponse();

        if (response != null && response.StatusCode == HttpStatusCode.OK)
        {
          try
          {
            using (var reader = new StreamReader(response.GetResponseStream()))
            {
              var data = JsonConvert.DeserializeObject<List<Address>>(reader.ReadToEnd());

              foreach (var item in data)
              {
                var task = _addressService.insert(item);

                if (task != null)
                  Console.WriteLine("Success: " + JsonConvert.SerializeObject(item));
                else
                  Console.WriteLine("Failed: " + JsonConvert.SerializeObject(item));
              }

              result = true;
            }
          }
          catch (Exception e)
          {
            Console.WriteLine("Error: Lay thong tin phuong / xa");
            Console.WriteLine(e);

            result = false;
          }
        }
        else
        {
          Console.WriteLine("Error: Lay thong tin phuong / xa");
          Console.WriteLine("Get API: That bai");

          result = false;
        }
      }
      Console.WriteLine("Ket thuc lay thong tin phuong / xa tu API Giao Hang Tiet Kiem");
      return result;
    }

    public bool getAllHamlet()
    {
      var taskProvinces = _addressService.getAllProvince();

      if (taskProvinces == null || taskProvinces.Count == 0)
      {
        Console.WriteLine("Hien tai trong database khong co tinh / thanh pho nao het ^-^.");
        return true;
      }

      var taskDistricts = new List<Address>();
      foreach (var province in taskProvinces)
      {
        var taskDistrict = _addressService.getAllDistrict(province.id);

        if (taskDistrict != null && taskDistrict.Count > 0)
          taskDistricts.AddRange(taskDistrict);
      }

      if (taskDistricts.Count == 0)
      {
        Console.WriteLine("Hien tai trong database khong co quan / huyen nao het ^-^.");
        return true;
      }

      var wards = new List<Address>();
      foreach (var province in taskDistricts)
      {
        var taskWard = _addressService.getAllWard(province.id);

        if (taskWard != null && taskWard.Count > 0)
          wards.AddRange(taskWard);
      }

      if (wards.Count == 0)
      {
        Console.WriteLine("Hien tai trong database khong co phuong / xa nao het ^-^.");
        return true;
      }

      Console.WriteLine("Bat dau lay thong tin thôn / ấp / xóm / tổ/ ... tu API Giao Hang Tiet Kiem");
      var result = false;
      var api = "https://khachhang.giaohangtietkiem.vn/khach-hang/services/list-dia-chi-public?type=17";

      foreach (var ward in wards)
      {
        Console.WriteLine(String.Format("Phường / Xã: {0}", ward.alias));

        // Excute API
        var url = String.Format("{0}&parentId={1}", api, ward.id);
        HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
        request.Method = "GET";

        var response = (HttpWebResponse)request.GetResponse();

        if (response != null && response.StatusCode == HttpStatusCode.OK)
        {
          try
          {
            using (var reader = new StreamReader(response.GetResponseStream()))
            {
              var data = JsonConvert.DeserializeObject<List<Address>>(reader.ReadToEnd());

              foreach (var item in data)
              {
                var task = _addressService.insert(item);

                if (task != null)
                  Console.WriteLine("Success: " + JsonConvert.SerializeObject(item));
                else
                  Console.WriteLine("Failed: " + JsonConvert.SerializeObject(item));
              }

              result = true;
            }
          }
          catch (Exception e)
          {
            Console.WriteLine("Error: Lay thong tin thôn / ấp / xóm / tổ/ ...");
            Console.WriteLine(e);

            result = false;
          }
        }
        else
        {
          Console.WriteLine("Error: Lay thong tin thôn / ấp / xóm / tổ/ ...");
          Console.WriteLine("Get API: That bai");

          result = false;
        }
      }
      Console.WriteLine("Ket thuc lay thong tin thôn / ấp / xóm / tổ/ ... tu API Giao Hang Tiet Kiem");
      return result;
    }
  }
}