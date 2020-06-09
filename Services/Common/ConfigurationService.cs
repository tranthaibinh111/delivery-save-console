using System;
using System.Collections.Generic;
using System.Linq;

using Microsoft.Extensions.Configuration;

namespace Service {
  public class ConfigurationService : IService
  {
    #region Parameters
    private string _envName;
    private string _connectionSQLServer;
    private string _connectionSQLite;
    private string _databaseSQLitePath;
    #endregion

    public ConfigurationService() {
      _loadEnvironment();
      _loadConnection();
    }

    #region Private
    // Load môi trường đang chạy
    private void _loadEnvironment() {
      _envName = Environment.GetEnvironmentVariable("CONSOLE_ENVIRONMENT");

      if (String.IsNullOrEmpty(_envName))
        _envName = "Production";
    }

    // Load các connect string
    private void _loadConnection() {
      IConfigurationRoot configuration = new ConfigurationBuilder()
              .SetBasePath(Environment.CurrentDirectory)
              .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
              .AddJsonFile($"appsettings.{_envName}.json", optional: true)
              .Build();
      _connectionSQLServer = configuration.GetValue("SQLServer:ConnectionString", String.Empty);
      _connectionSQLite = configuration.GetValue("SQLite:ConnectionString", String.Empty);
      _databaseSQLitePath = configuration.GetValue("SQLite:DataBasePath", String.Empty);
    }
    #endregion

    #region Public
    public string getWorkspaceFolder() {
      return Environment.CurrentDirectory;
    }

    // Kiểm tra xem phải môi trường Development
    public bool IsDEV() {
      return _envName == "Development";
    }
    // Lấy tên môi trường đang chạy
    public string getEnvironment() {
      return _envName;
    }

    // Lấy connection string của SQL Server
    public string getConnectionSQLServer() {
      return _connectionSQLServer;
    }

    // Lấy connection string của SQLite
    public string getConnectionSQLite() {
      return _connectionSQLite;
    }

    // Lấy path của Databse SQLite
    public string getDatabaseSQLitePath() {
      return _databaseSQLitePath;
    }
    #endregion
  }
}