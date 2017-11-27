using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;

namespace MyLeave.AdoDotNet.DAO
{
    /// <summary>
    /// Représente un congé
    /// </summary>
    public class LeaveDAO : EntityDAO<DTO.LeaveDTO>
    {

        private static DAO.EntityDAO<DTO.LeaveDTO> _Instance;
        /// <summary>
        /// Récupérer l'instance singleton de LeaveDAO
        /// </summary>
        public static DAO.EntityDAO<DTO.LeaveDTO> Instance
        {
            get
            {
                if (_Instance == null)
                {
                    _Instance = new LeaveDAO();
                }
                return _Instance;
            }
        }

        /// <summary>
        /// Constructeur privé pour implementer le pattern singleton
        /// </summary>
        private LeaveDAO() { }

        /// <summary>
        /// Enregistrer un congé dans la base de données
        /// </summary>
        public override void Add(DTO.LeaveDTO leaveDto)
        {
            IDbConnection connexion = null;
            try
            {
                connexion = new SqlConnection(connectionString);
                IDbCommand commande = connexion.CreateCommand();
                string query = "INSERT INTO Leave VALUES(@StartDate, @EndDate, @Description, @Company_ID)";
                commande.CommandText = query;
                commande.CommandType = CommandType.Text;
                commande.Parameters.Add(new SqlParameter("StartDate", leaveDto.StartDate));
                if (leaveDto.EndDate != null)
                {
                    commande.Parameters.Add(new SqlParameter("EndDate", leaveDto.EndDate));
                }
                else
                {
                    commande.Parameters.Add(new SqlParameter("EndDate", DBNull.Value));
                }
                commande.Parameters.Add(new SqlParameter("Description", leaveDto.Description));
                commande.Parameters.Add(new SqlParameter("Company_ID", leaveDto.Company.ID));

                connexion.Open();
                int intExecuteNonQuery = commande.ExecuteNonQuery();
                
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                if (connexion != null)
                {
                    connexion.Close();
                    connexion.Dispose();
                }
                
            }
        }

        /// <summary>
        /// Supprimer un congé de la base de données
        /// </summary>
        /// <param name="leaveDto"></param>
        public override void Delete(DTO.LeaveDTO leaveDto)
        {
            try
            {
                IDbConnection connexion = new SqlConnection(connectionString);
                IDbCommand commande = connexion.CreateCommand();
                string query = "DELETE FROM Leave WHERE ID=@ID;";
                commande.CommandText = query;
                commande.CommandType = CommandType.Text;
                commande.Parameters.Add(new SqlParameter("ID", leaveDto.ID));

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
        /// Modifier un congé
        /// </summary>
        /// <param name="leaveDto"></param>
        public override void Update(DTO.LeaveDTO leaveDto)
        {
            try
            {
                IDbConnection connexion = new SqlConnection(connectionString);
                IDbCommand commande = connexion.CreateCommand();
                string query = "UPDATE Leave SET StartDate=@StartDate, EndDate=@EndDate, " + 
                    "Description=@Description, Company_ID=@Company_ID WHERE ID=@ID;";
                commande.CommandText = query;
                commande.CommandType = CommandType.Text;
                commande.Parameters.Add(new SqlParameter("StartDate", leaveDto.StartDate));

                if (leaveDto.EndDate != null)
                {
                    commande.Parameters.Add(new SqlParameter("EndDate", leaveDto.EndDate));                
                }
                else
                {
                    commande.Parameters.Add(new SqlParameter("EndDate", DBNull.Value)); 
                }

                commande.Parameters.Add(new SqlParameter("Description", leaveDto.Description));
                commande.Parameters.Add(new SqlParameter("Company_ID", leaveDto.Company.ID));
                commande.Parameters.Add(new SqlParameter("ID", leaveDto.ID));

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
        /// Récupérer un congé par ID
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public override DTO.LeaveDTO GetById(int Id)
        {
            try
            {
                IDbConnection connexion = new SqlConnection(connectionString);
                IDbCommand commande = connexion.CreateCommand();
                string query = "SELECT * FROM Leave WHERE ID=@ID";
                commande.CommandText = query;
                commande.CommandType = CommandType.Text;
                commande.Parameters.Add(new SqlParameter("ID", Id));

                DTO.LeaveDTO leaveDao = null;
                DTO.CompanyDTO companyDao = null;

                connexion.Open();
                IDataReader dataReader = commande.ExecuteReader();
                if (dataReader.Read())
                {
                    leaveDao = new DTO.LeaveDTO();
                    leaveDao.ID = (int)dataReader["ID"];
                    leaveDao.StartDate = ((DateTimeOffset)dataReader["StartDate"]).DateTime;
                    if (dataReader["EndDate"] != DBNull.Value)
                    {
                        leaveDao.EndDate = ((DateTimeOffset)dataReader["EndDate"]).DateTime;
                    }
                    leaveDao.Description = (string)dataReader["Description"];

                    int companyId = (int)dataReader["Company_ID"];
                    companyDao = DAO.CompanyDAO.Instance.GetById(companyId);
                    leaveDao.Company = companyDao;

                }
                return leaveDao;
            }
            catch (Exception)
            {
                
                throw;
            }
        }

        /// <summary>
        /// Récupérer tous les congés dans la base de données
        /// </summary>
        /// <returns></returns>
        public override IList<DTO.LeaveDTO> GetAll()
        {
            try
            {
                IDbConnection connexion = new SqlConnection(connectionString);
                IDbCommand commande = connexion.CreateCommand();
                string query = "SELECT * FROM Leave";
                commande.CommandText = query;
                commande.CommandType = CommandType.Text;
                
                connexion.Open();
                IDataReader dataReader = commande.ExecuteReader();
                IList<DTO.LeaveDTO> leaves = null;
                DTO.CompanyDTO companyDao = null;

                while (dataReader.Read())
                {
                    DTO.LeaveDTO leaveDao = new DTO.LeaveDTO();
                    leaveDao.ID = (int)dataReader["ID"];
                    leaveDao.StartDate = ((DateTimeOffset)dataReader["StartDate"]).DateTime;
                    if (dataReader["EndDate"] != DBNull.Value)
                    {
                        leaveDao.EndDate = ((DateTimeOffset)dataReader["EndDate"]).DateTime;
                    }
                    leaveDao.Description = (string)dataReader["Description"];

                    int companyId = (int)dataReader["Company_ID"];
                    companyDao = DAO.CompanyDAO.Instance.GetById(companyId);
                    leaveDao.Company = companyDao;

                    leaves = leaves ?? new List<DTO.LeaveDTO>();

                    leaves.Add(leaveDao);
                }

                connexion.Close();
                connexion.Dispose();

                return leaves;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Retourne les plus longue congés
        /// </summary>
        /// <returns></returns>
        public IList<DTO.LeaveDTO> Max()
        {
            try
            {
                IList<DTO.LeaveDTO> Leaves = GetAll();

                int maxInt = Leaves.Max(LD => LD.Days);

                Leaves = Leaves.Where(LeaveDao => LeaveDao.Days == maxInt).ToList();

                return Leaves;
            }
            catch (Exception)
            {
                
                throw;
            }
        }

        /// <summary>
        /// Retourne les plus courts congés
        /// </summary>
        /// <returns></returns>
        public IList<DTO.LeaveDTO> Min()
        {
            try
            {
                IList<DTO.LeaveDTO> Leaves = GetAll();

                int minInt = Leaves.Min(LD => LD.Days);

                Leaves = Leaves.Where(LeaveDao => LeaveDao.Days == minInt).ToList();

                return Leaves;
            }
            catch (Exception)
            {
                
                throw;
            }
        }

        /// <summary>
        /// Retourne les congés qui ont la longeur moyenne
        /// </summary>
        /// <returns></returns>
        public IList<DTO.LeaveDTO> Average()
        {
            try
            {
                IList<DTO.LeaveDTO> Leaves = GetAll();

                double averageDb = Leaves.Average(LD => LD.Days);

                Leaves = Leaves.Where(LeaveDao => LeaveDao.Days == averageDb).ToList();

                return Leaves;
            }
            catch (Exception)
            {
                
                throw;
            }
        }

        /// <summary>
        /// Retourne la somme de tous les congés
        /// </summary>
        /// <returns></returns>
        public int Sum()
        {
            try
            {
                IList<DTO.LeaveDTO> Leaves = GetAll();

                int sumInt = Leaves.Sum(LD => LD.Days);

                return sumInt;
            }
            catch (Exception)
            {
                
                throw;
            }
        }
    
    }
}
