using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;

namespace MyLeave.AdoDotNet.DAO
{
    public class HijriHolidayDAO : EntityDAO<DTO.HijriHolidayDTO>
    {
        private static DAO.EntityDAO<DTO.HijriHolidayDTO> _Instance;
        /// <summary>
        /// Récupérer l'instance singleton de HijriHolidayDAO
        /// </summary>
        public static DAO.EntityDAO<DTO.HijriHolidayDTO> Instance
        {
            get
            {
                if (_Instance == null)
                {
                    _Instance = new HijriHolidayDAO();
                }
                return _Instance;
            }
        }

        /// <summary>
        /// Constructeur privé pour implementer le pattern singleton
        /// </summary>
        private HijriHolidayDAO() { }

        /// <summary>
        /// Ajouter un jour férie Hijri
        /// </summary>
        /// <param name="hijriHolidayDto"></param>
        public override void Add(DTO.HijriHolidayDTO hijriHolidayDto)
        {
            try
            {
                IDbConnection connection = new SqlConnection(connectionString);

                IDbCommand command = connection.CreateCommand();
                command.CommandType = CommandType.Text;
                string query = "INSERT INTO PublicHoliday VALUES(@Day, @Month);SELECT CAST(scope_identity() AS int)";
                command.CommandText = query;
                command.Parameters.Add(new SqlParameter("Day", hijriHolidayDto.Day));
                command.Parameters.Add(new SqlParameter("Month", hijriHolidayDto.Month));

                connection.Open();
                int newId = (int)command.ExecuteScalar();

                command = connection.CreateCommand();
                command.CommandType = CommandType.Text;
                query = "INSERT INTO HijriHoliday VALUES(@Year, @ID)";
                command.CommandText = query;
                command.Parameters.Add(new SqlParameter("Year", hijriHolidayDto.Year));
                command.Parameters.Add(new SqlParameter("ID", newId));
                int intExecuteNonQuery = command.ExecuteNonQuery();

                connection.Close();
                connection.Dispose();
            }
            catch (Exception)
            {

                throw;
            }
        }

        /// <summary>
        /// Supprimer le jour férie Hijri
        /// </summary>
        /// <param name="entity"></param>
        public override void Delete(DTO.HijriHolidayDTO hijriHolidayDto)
        {
            try
            {
                IDbConnection connection = new SqlConnection(connectionString);

                IDbCommand command = connection.CreateCommand();
                string query = "DELETE FROM HijriHoliday WHERE ID=@ID;";
                command.CommandText = query;
                command.CommandType = CommandType.Text;
                command.Parameters.Add(new SqlParameter("ID", hijriHolidayDto.ID));

                IDbCommand command2 = connection.CreateCommand();
                string query2 = "DELETE FROM PublicHoliday WHERE ID=@ID;";
                command2.CommandText = query2;
                command2.CommandType = CommandType.Text;
                command2.Parameters.Add(new SqlParameter("ID", hijriHolidayDto.ID));

                connection.Open();
                int intExecuteNonQuery = command.ExecuteNonQuery();
                intExecuteNonQuery = command2.ExecuteNonQuery();
                connection.Close();
                connection.Dispose();
            }
            catch (Exception)
            {

                throw;
            }
        }

        /// <summary>
        /// Modifier le jour férie Hijri
        /// </summary>
        /// <param name="hijriHolidayDto"></param>
        public override void Update(DTO.HijriHolidayDTO hijriHolidayDto)
        {
            try
            {
                IDbConnection connection = new SqlConnection(connectionString);

                IDbCommand command = connection.CreateCommand();
                string query = "UPDATE PublicHoliday SET Day=@Day, Month=@Month WHERE ID=@ID;";
                command.CommandType = CommandType.Text;
                command.CommandText = query;
                command.Parameters.Add(new SqlParameter("Day", hijriHolidayDto.Day));
                command.Parameters.Add(new SqlParameter("Month", hijriHolidayDto.Month));
                command.Parameters.Add(new SqlParameter("ID", hijriHolidayDto.ID));

                IDbCommand command2 = connection.CreateCommand();
                string query2 = "UPDATE HijriHoliday SET Year=@Year WHERE ID=@ID;";
                command2.CommandType = CommandType.Text;
                command2.CommandText = query2;
                command2.Parameters.Add(new SqlParameter("Year", hijriHolidayDto.Year));
                command2.Parameters.Add(new SqlParameter("ID", hijriHolidayDto.ID));

                connection.Open();
                int intExecuteNonQuery = command.ExecuteNonQuery();
                intExecuteNonQuery = command2.ExecuteNonQuery();
                connection.Close();
                connection.Dispose();
            }
            catch (Exception)
            {
                
                throw;
            }
        }

        /// <summary>
        /// Récupérer le jour férie Hijri par ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public override DTO.HijriHolidayDTO GetById(int id)
        {
            try
            {
                IDbConnection connection = new SqlConnection(connectionString);
                IDbCommand command = connection.CreateCommand();
                string query = "SELECT PublicHoliday.ID as PublicHoliday_ID, PublicHoliday.Day as PublicHoliday_Day, " +
                    "PublicHoliday.Month as PublicHoliday_Month, HijriHoliday.Year as HijriHoliday_Year FROM HijriHoliday " +
                    "INNER JOIN PublicHoliday ON HijriHoliday.ID = PublicHoliday.ID " +
                    "WHERE HijriHoliday.ID=@ID";
                command.CommandText = query;
                command.CommandType = CommandType.Text;
                command.Parameters.Add(new SqlParameter("ID", id));

                DTO.HijriHolidayDTO hijriHolidayDto = null;

                connection.Open();
                IDataReader dataReader = command.ExecuteReader();
                if (dataReader.Read())
                {
                    hijriHolidayDto = new DTO.HijriHolidayDTO();
                    hijriHolidayDto.ID = (int)dataReader["PublicHoliday_ID"];
                    hijriHolidayDto.Day = (short)dataReader["PublicHoliday_Day"];
                    hijriHolidayDto.Month = (short)dataReader["PublicHoliday_Month"];
                    hijriHolidayDto.Year = (short)dataReader["HijriHoliday_Year"];
                }

                connection.Close();
                connection.Dispose();

                return hijriHolidayDto;
            }
            catch (Exception)
            {

                throw;
            }
        }

        /// <summary>
        /// Récupérer la liste des jours féries Hijri
        /// </summary>
        /// <returns></returns>
        public override IList<DTO.HijriHolidayDTO> GetAll()
        {
            try
            {
                IDbConnection connection = new SqlConnection(connectionString);
                IDbCommand command = connection.CreateCommand();
                string query = "SELECT PublicHoliday.ID as PublicHoliday_ID, PublicHoliday.Day as PublicHoliday_Day, " +
                    "PublicHoliday.Month as PublicHoliday_Month, HijriHoliday.Year as HijriHoliday_Year FROM HijriHoliday " +
                    "INNER JOIN PublicHoliday ON HijriHoliday.ID = PublicHoliday.ID";
                command.CommandText = query;
                command.CommandType = CommandType.Text;

                IList<DTO.HijriHolidayDTO> hijriHolidays = null;
                DTO.HijriHolidayDTO hijriHolidayDto = null;

                connection.Open();
                
                IDataReader dataReader = command.ExecuteReader();
                while (dataReader.Read())
                {
                    hijriHolidayDto = new DTO.HijriHolidayDTO();
                    hijriHolidayDto.ID = (int)dataReader["PublicHoliday_ID"];
                    hijriHolidayDto.Day = (short)dataReader["PublicHoliday_Day"];
                    hijriHolidayDto.Month = (short)dataReader["PublicHoliday_Month"];
                    hijriHolidayDto.Year = (short)dataReader["HijriHoliday_Year"];
                    hijriHolidays = hijriHolidays ?? new List<DTO.HijriHolidayDTO>();
                    hijriHolidays.Add(hijriHolidayDto);
                }

                connection.Close();
                connection.Dispose();

                return hijriHolidays;
            }
            catch (Exception)
            {
                
                throw;
            }
        }
    
    }
}
