using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;

namespace EDurianstore.Models
{
    public class DataAkses
    {
        public Response daftar(Penggunas penggunas, SqlConnection connection)
        {
            Response response = new Response();
            SqlCommand command = new SqlCommand("sp_daftar", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@Namapengguna", penggunas.Namapengguna);
            command.Parameters.AddWithValue("@Katasandi", penggunas.Katasandi);
            command.Parameters.AddWithValue("@Email", penggunas.Email);
            command.Parameters.AddWithValue("@Telepon", penggunas.Telepon);
            command.Parameters.AddWithValue("@Alamat", penggunas.Alamat);
            command.Parameters.AddWithValue("@Dana", 0);
            command.Parameters.AddWithValue("@Type", "Penggunas");
            command.Parameters.AddWithValue("@Type", "Pending");
            connection.Open();
            int i = command.ExecuteNonQuery();
            connection.Close();
            if (i > 0)
            {
                response.KodeStatus = 200;
                response.StatusPesan = "Pengguna Berhasil Didaftarkan";
            }
            else
            {
                response.KodeStatus = 100;
                response.StatusPesan = "Pendaftaran Pengguna Gagal";
            }


            return response;
        }

        public Response masuk (Penggunas penggunas, SqlConnection connection)
        {
            SqlDataAdapter dataAdapter = new SqlDataAdapter("sp_masuk", connection);
            dataAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            dataAdapter.SelectCommand.Parameters.AddWithValue("@Email", penggunas.Email);
            dataAdapter.SelectCommand.Parameters.AddWithValue("@Katasandi", penggunas.Katasandi);
            DataTable dataTable = new DataTable();
            dataAdapter.Fill(dataTable);
            Response response = new Response();

            if(dataTable.Rows.Count>0)
            {
                penggunas.ID = Convert.ToInt32(dataTable.Rows[0]["ID"]);
                penggunas.Namapengguna = Convert.ToString(dataTable.Rows[0]["Namapengguna"]);
                penggunas.Email = Convert.ToString(dataTable.Rows[0]["Email"]);
                penggunas.Type = Convert.ToString(dataTable.Rows[0]["Type"]);
                response.KodeStatus = 200;
                response.StatusPesan = "Pengguna Ditemukan";
                response.pengguna = penggunas;
            }
            else
            {
                response.KodeStatus = 100;
                response.StatusPesan = "Pengguna Tidak Ditemukan";
                response.pengguna = null;
            }
            return response;
        }

        public Response tampilanuser(Penggunas penggunas, SqlConnection connection)
        {
            SqlDataAdapter dataAdapter = new SqlDataAdapter("p_tampilanuser", connection);
            dataAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            dataAdapter.SelectCommand.Parameters.AddWithValue("@ID", penggunas.ID);
            DataTable dataTable = new DataTable();
            dataAdapter.Fill(dataTable);
            Response response = new Response();
            if (dataTable.Rows.Count > 0)
            {
                response.KodeStatus = 200;
                response.StatusPesan = "Pengguna Ada";
            }
            else{
                response.KodeStatus = 100;
                response.StatusPesan = "Pengguna Tidak Ada";
            }
            return response;
        }
    }
}