using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;

namespace MyLeave.AdoDotNet.DAO
{
    public class NationalHolidayDAO : EntityDAO<DTO.NationalHolidayDTO>
    {
        private static DAO.EntityDAO<DTO.NationalHolidayDTO> _Instance;
        /// <summary>
        /// Récupérer l'instance singleton de NationalHolidayDAO
        /// </summary>
        public static DAO.EntityDAO<DTO.NationalHolidayDTO> Instance
        {
            get
            {
                if (_Instance == null)
                {
                    _Instance = new NationalHolidayDAO();
                }
                return _Instance;
            }
        }

        /// <summary>
        /// Constructeur privé pour implementer le pattern singleton
        /// </summary>
        private NationalHolidayDAO() { }

        /// <summary>
        /// Ajouter un jour férie national
        /// </summary>
        /// <param name="nationalHolidayDto"></param>
        public override void Add(DTO.NationalHolidayDTO nationalHolidayDto)
        {
            try
            {
                IDbConnection connection = new SqlConnection(connectionString);
                
                IDbCommand commande = connection.CreateCommand();
                commande.CommandType = CommandType.Text;
                string query = "INSERT INTO PublicHoliday VALUES(@Day, @Month);SELECT CAST(scope_identity() AS int)";
                commande.CommandText = query;
                commande.Parameters.Add(new SqlParameter("Day", nationalHolidayDto.Day));
                commande.Parameters.Add(new SqlParameter("Month", nationalHolidayDto.Month));

                connection.Open();
                int newId = (int)commande.ExecuteScalar();

                commande = connection.CreateCommand();
                commande.CommandType = CommandType.Text;
                query = "INSERT INTO NationalHoliday VALUES(@ID)";
                commande.CommandText = query;
                commande.Parameters.Add(new SqlParameter("ID", newId));
                int intExecuteNonQuery = commande.ExecuteNonQuery();

                connection.Close();
                connection.Dispose();
            }
            catch (Exception)
            {
                
                throw;
            }
        }

        /// <summary>
        /// Supprimer le jour férie
        /// </summary>
        /// <param name="entity"></param>
        public override void Delete(DTO.NationalHolidayDTO nationalHolidayDto)
        {
            try
            {
                IDbConnection connection = new SqlConnection(connectionString);
                IDbCommand command = connection.CreateCommand();
                IDbCommand command2 = connection.CreateCommand();
                string query = "DELETE FROM NationalHoliday WHERE ID=@ID;";
                string query2 = "DELETE FROM PublicHoliday WHERE ID=@ID;";
                command.CommandText = query;
                command2.CommandText = query2;
                command.CommandType = CommandType.Text;
                command2.CommandType = CommandType.Text;
                command.Parameters.Add(new SqlParameter("ID", nationalHolidayDto.ID));
                command2.Parameters.Add(new SqlParameter("ID", nationalHolidayDto.ID));

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
        /// Modifier le jour férie
        /// </summary>
        /// <param name="nationalHolidayDto"></param>
        public override void Update(DTO.NationalHolidayDTO nationalHolidayDto)
        {
            try
            {
                IDbConnection connection = new SqlConnection(connectionString);
                IDbCommand command = connection.CreateCommand();
                string query = "UPDATE PublicHoliday SET Day=@Day, Month=@Month WHERE ID=@ID;";
                command.CommandType = CommandType.Text;
                command.CommandText = query;
                command.Parameters.Add(new SqlParameter("Day",nationalHolidayDto.Day));
                command.Parameters.Add(new SqlParameter("Month",nationalHolidayDto.Month));
                command.Parameters.Add(new SqlParameter("ID", nationalHolidayDto.ID));

                connection.Open();

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
        /// Récupérer le jour férie par ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public override DTO.NationalHolidayDTO GetById(int id)
        {
            try
            {
                IDbConnection connection = new SqlConnection(connectionString);
                IDbCommand command = connection.CreateCommand();
                string query = "SELECT PublicHoliday.ID as PublicHoliday_ID, PublicHoliday.Day as PublicHoliday_Day, " +
                    "PublicHoliday.Month as PublicHoliday_Month FROM NationalHoliday " +
                    "INNER JOIN PublicHoliday ON NationalHoliday.ID = PublicHoliday.ID " +
                    "WHERE NationalHoliday.ID=@ID";
                command.CommandText = query;
                command.CommandType = CommandType.Text;
                command.Parameters.Add(new SqlParameter("ID", id));

                DTO.NationalHolidayDTO nationalHolidayDto = null;

                connection.Open();
                IDataReader dataReader = command.ExecuteReader();
                if (dataReader.Read())
                {
                    nationalHolidayDto = new DTO.NationalHolidayDTO();
                    nationalHolidayDto.ID = (int)dataReader["PublicHoliday_ID"];
                    nationalHolidayDto.Day = (short)dataReader["PublicHoliday_Day"];
                    nationalHolidayDto.Month = (short)dataReader["PublicHoliday_Month"];
                }

                connection.Close();
                connection.Dispose();

                return nationalHolidayDto;
            }
            catch (Exception)
            {
                
                throw;
            }
        }

        /// <summary>
        /// Récupérer les jours féries nationals
        /// </summary>
        /// <returns></returns>
        public override IList<DTO.NationalHolidayDTO> GetAll()
        {
            try
            {
                IDbConnection connection = new SqlConnection(connectionString);
                IDbCommand command = connection.CreateCommand();
                string query = "SELECT PublicHoliday.ID as PublicHoliday_ID, PublicHoliday.Day as PublicHoliday_Day, " + 
                    "PublicHoliday.Month as PublicHoliday_Month FROM NationalHoliday " + 
                    "INNER JOIN PublicHoliday ON NationalHoliday.ID = PublicHoliday.ID";
                command.CommandText = query;
                command.CommandType = CommandType.Text;

                IList<DTO.NationalHolidayDTO> nationalHolidays = null;
                DTO.NationalHolidayDTO nationalHolidayDto = null;

                connection.Open();
                
                IDataReader dataReader = command.ExecuteReader();
                while (dataReader.Read())
                {
                    nationalHolidayDto = new DTO.NationalHolidayDTO();
                    nationalHolidayDto.ID = (int)dataReader["PublicHoliday_ID"];
                    nationalHolidayDto.Day = (short)dataReader["PublicHoliday_Day"];
                    nationalHolidayDto.Month = (short)dataReader["PublicHoliday_Month"];
                    nationalHolidays = nationalHolidays ?? new List<DTO.NationalHolidayDTO>();
                    nationalHolidays.Add(nationalHolidayDto);
                }

                connection.Close();
                connection.Dispose();

                return nationalHolidays;
            }
            catch (Exception)
            {
                
                throw;
            }
        }
    
    }
}
