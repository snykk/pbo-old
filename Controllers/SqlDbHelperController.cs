using Microsoft.AspNetCore.Mvc;
using Npgsql;
using Project_PBO.Models;
using System.Configuration;
using System.Data;
using System.Diagnostics;

namespace Project_PBO.Controllers
{
    public class SqlDbHelperController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public bool ExecuteCUDQuery(ref DataTable dt, string sql, params NpgsqlParameter[] parameters)
        {
            string conn = $"Host=;Username=;Password=;Database=";
            using (NpgsqlConnection connStr = new NpgsqlConnection(conn))
            using (NpgsqlCommand cmd = new NpgsqlCommand(sql, connStr))
            {
                cmd.CommandType = CommandType.Text;
                try
                {

                    foreach (var item in parameters)
                    {
                        cmd.Parameters.Add(item);
                    }

                    cmd.Connection?.Open();
                    new NpgsqlDataAdapter(cmd).Fill(dt);
                    Console.WriteLine("Process ended successfuly");
                    return true;

                }
                catch (NpgsqlException ex)
                {
                    Console.WriteLine(ex.Message);
                    return false;
                }
            }
        }

        public bool ExecuteRQuery(ref DataSet ds, string sql, params NpgsqlParameter[] parameters)
        {
            string conn = $"Host=;Username=;Password=;Database=";
            using (NpgsqlConnection connStr = new NpgsqlConnection(conn))
            using (NpgsqlCommand cmd = new NpgsqlCommand(sql, connStr))
            {
                cmd.CommandType = CommandType.Text;
                try
                {
                    if (parameters.Length > 0)
                    {
                        foreach (var item in parameters)
                        {
                            cmd.Parameters.Add(item);
                        }
                    }

                    cmd.Connection?.Open();
                    new NpgsqlDataAdapter(cmd).Fill(ds);
                    Console.WriteLine("Process ended successfuly");
                    return true;
                }
                catch (NpgsqlException ex)
                {
                    Console.WriteLine(ex.Message);
                    return false;
                }
            }
        }

    }
}
