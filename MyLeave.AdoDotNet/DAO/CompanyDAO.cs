using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;

namespace MyLeave.AdoDotNet.DAO
{
    /// <summary>
    /// Représente une entreprise
    /// </summary>
    public class CompanyDAO : EntityDAO<DTO.CompanyDTO>
    {

        private static DAO.EntityDAO<DTO.CompanyDTO> _Instance;
        /// <summary>
        /// Récupérer l'instance singleton de CompanyDAO
        /// </summary>
        public static DAO.EntityDAO<DTO.CompanyDTO> Instance
        {
            get 
            {
                if (_Instance == null)
                {
                    _Instance = new CompanyDAO();
                }
                return _Instance;
            }
        }

        /// <summary>
        /// Constructeur privé pour implementer la pattern singleton
        /// </summary>
        private CompanyDAO() { }

        /// <summary>
        /// Ajouter une entreprise à la base de données
        /// </summary>
        /// <param name="companyDto"></param>
        public override void Add(DTO.CompanyDTO companyDto)
        {
            try
            {
                IDbConnection connexion = new SqlConnection(connectionString);
                IDbCommand commande = connexion.CreateCommand();
                string query = "INSERT INTO Company VALUES(@Name, @RecruitementDate, @EndOfMissionDate)";
                commande.CommandText = query;
                commande.CommandType = CommandType.Text;
                commande.Parameters.Add(new SqlParameter("Name", companyDto.Name));
                commande.Parameters.Add(new SqlParameter("RecruitementDate", companyDto.RecruitementDate));
                if (companyDto.EndOfMissionDate != null)
                {
                    commande.Parameters.Add(new SqlParameter("EndOfMissionDate", companyDto.EndOfMissionDate));
                }
                else
                {
                    commande.Parameters.Add(new SqlParameter("EndOfMissionDate", DBNull.Value));                       
                }
            
                connexion.Open();
                int intExecuteNonQuery = commande.ExecuteNonQuery();
                connexion.Close();
                connexion.Dispose();
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Supprimer l'entreprise en paramètre
        /// </summary>
        /// <param name="companyDto"></param>
        public override void Delete(DTO.CompanyDTO companyDto)
        {
            try
            {
                IDbConnection connexion = new SqlConnection(connectionString);
                IDbCommand commande = connexion.CreateCommand();
                string query = "DELETE FROM Company WHERE ID=@ID;";
                commande.CommandText = query;
                commande.CommandType = CommandType.Text;
                commande.Parameters.Add(new SqlParameter("ID", companyDto.ID));

                connexion.Open();
                int intExecuteNonQuery = commande.ExecuteNonQuery();
                connexion.Close();
                connexion.Dispose();
            }
            catch (Exception)
            {
                
                throw;
            }
        }

        /// <summary>
        /// Modifier l'entreprise en paramètre
        /// </summary>
        /// <param name="entity"></param>
        public override void Update(DTO.CompanyDTO companyDto)
        {
            try
            {
                IDbConnection connexion = new SqlConnection(connectionString);
                IDbCommand commande = connexion.CreateCommand();
                string query = "UPDATE Company SET Name=@Name, RecruitementDate=@RecruitementDate, EndOfMissionDate=@EndOfMissionDate WHERE ID=@ID;";
                commande.CommandText = query;
                commande.CommandType = CommandType.Text;
                commande.Parameters.Add(new SqlParameter("Name", companyDto.Name));
                commande.Parameters.Add(new SqlParameter("RecruitementDate", companyDto.RecruitementDate));
                if (companyDto.EndOfMissionDate != null)
                {
                    commande.Parameters.Add(new SqlParameter("EndOfMissionDate", companyDto.EndOfMissionDate));
                }
                else
                {
                    commande.Parameters.Add(new SqlParameter("EndOfMissionDate", DBNull.Value));
                }
                commande.Parameters.Add(new SqlParameter("ID", companyDto.ID));

                connexion.Open();
                int intExecuteNonQuery = commande.ExecuteNonQuery();
                connexion.Close();
                connexion.Dispose();
            }
            catch (Exception)
            {
                
                throw;
            }
        }

        /// <summary>
        /// Récupérer une entreprise par son ID
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public override DTO.CompanyDTO GetById(int Id)
        {
            try
            {

                IDbConnection connexion = new SqlConnection(connectionString);
                IDbCommand commande = connexion.CreateCommand();
                string query = "SELECT * FROM Company WHERE ID=@ID";
                commande.CommandText = query;
                commande.CommandType = CommandType.Text;
                commande.Parameters.Add(new SqlParameter("ID", Id));

                DTO.CompanyDTO companyDto = null;
                connexion.Open();
                IDataReader dataReader = commande.ExecuteReader();
                if (dataReader.Read())
                {
                    companyDto = new DTO.CompanyDTO();
                    companyDto.ID = (int)dataReader["ID"];
                    companyDto.Name = (string)dataReader["Name"];
                    companyDto.RecruitementDate = ((DateTimeOffset)dataReader["RecruitementDate"]).DateTime;
                    if (dataReader["EndOfMissionDate"] != DBNull.Value)
                    {
                        companyDto.EndOfMissionDate = ((DateTimeOffset)dataReader["EndOfMissionDate"]).DateTime;
                    }

                }
                return companyDto;

            }
            catch (Exception)
            {
                
                throw;
            }
        }

        /// <summary>
        /// Charger toutes les entreprises de la base de données
        /// </summary>
        /// <returns></returns>
        public override IList<DTO.CompanyDTO> GetAll()
        {
            try
            {
                IDbConnection connexion = new SqlConnection(connectionString);
                IDbCommand commande = connexion.CreateCommand();
                string query = "SELECT * FROM Company";
                commande.CommandText = query;
                commande.CommandType = CommandType.Text;

                connexion.Open();
                IDataReader dataReader = commande.ExecuteReader();
                IList<DTO.CompanyDTO> companies = null;

                while (dataReader.Read())
                {
                    DTO.CompanyDTO companyDto = new DTO.CompanyDTO();

                    companyDto.ID = (int)dataReader["ID"];
                    companyDto.Name = (string)dataReader["Name"];
                    companyDto.RecruitementDate = ((DateTimeOffset)dataReader["RecruitementDate"]).DateTime;
                    if (dataReader["EndOfMissionDate"] != DBNull.Value)
                    {
                        companyDto.EndOfMissionDate = ((DateTimeOffset)dataReader["EndOfMissionDate"]).DateTime;
                    }

                    companies = companies ?? new List<DTO.CompanyDTO>();

                    companies.Add(companyDto);
                }

                connexion.Close();
                connexion.Dispose();

                return companies;
            }
            catch (Exception)
            {
                
                throw;
            }
        }
    
    }
}
