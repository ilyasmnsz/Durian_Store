using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using EDurianstore.Models;
using EDurianstore.Data;
using System;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EDurianstore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PenggunasController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        public PenggunasController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpPost]
        [Route("Daftar")]
        public Response daftar(Penggunas penggunas, SqlConnection connection)
        {
            Response response = new Response();
            DataAkses dataAkses = new DataAkses();
            SqlConnection connection1 = new SqlConnection(_configuration.GetConnectionString("Database"));
            // .ToString()
            response = dataAkses.daftar(penggunas, connection);

            return response;
        }

        [HttpPost]
        [Route("Masuk")]
        public Response masuk(Penggunas penggunas, SqlConnection connection)
        {
            DataAkses dataAkses = new DataAkses();
            SqlConnection connection1 = new SqlConnection(_configuration.GetConnectionString("Database"));
            Response response = dataAkses.masuk(penggunas, connection);
            return response;
        }
    }
}