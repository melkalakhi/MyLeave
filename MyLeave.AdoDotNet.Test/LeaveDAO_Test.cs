using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using System.Data.SqlClient;

namespace MyLeave.AdoDotNet.Test
{
    public class LeaveDAO_Test
    {
        [TestCase("Congé été", 2014, 08, 11, 2014, 08, 25)]
        public void AddLeaveWithOutCompany(string description, int startDateYear, int startDateMonth, int startDateDay,
            int endDateYear, int endDateMonth, int endDateDay)
        {
            DTO.LeaveDTO leaveDto = new DTO.LeaveDTO();
            leaveDto.StartDate = new DateTime(startDateYear, startDateMonth, startDateDay);
            leaveDto.EndDate = new DateTime(endDateYear, endDateMonth, endDateDay);
            leaveDto.Description = description;
            int id = 0;
            DTO.CompanyDTO companyDto = null;
            do
            {
                id++;
                companyDto = DAO.CompanyDAO.Instance.GetById(id);
            } while (companyDto != null);
            companyDto = new DTO.CompanyDTO();
            companyDto.ID = id;
            leaveDto.Company = companyDto;

            SqlException sqlException = Assert.Throws<SqlException>(delegate() { DAO.LeaveDAO.Instance.Add(leaveDto); });
            Assert.AreEqual(sqlException.Number, 547);

        }

        [TestCase("Archos Technology", 2011, 7, 1, 2013, 5, 3, "Congé été", 2014, 08, 11, 2014, 08, 25)]
        [TestCase("Visiativ Maroc", 2013, 5, 13, null, null, null, "Congé été", 2014, 08, 11, 2014, 08, 25)]
        public void TestLeaveDao(string name, int yearRecruitementDate, int monthRecruitementDate, int dayRecruitementDate,
            int? yearEndOfMissionDate, int? monthEndOfMissionDate, int? dayEndOfMissionDate,
            string description, int startDateYear, int startDateMonth, int startDateDay,
            int? endDateYear, int? endDateMonth, int? endDateDay)
        {
            // Ajouter une entreprise
            DTO.CompanyDTO companyDto = new DTO.CompanyDTO();
            companyDto.Name = name;
            companyDto.RecruitementDate = new DateTime(yearRecruitementDate, monthRecruitementDate, dayRecruitementDate);
            if (yearEndOfMissionDate != null && monthEndOfMissionDate != null && dayEndOfMissionDate != null)
            {
                companyDto.EndOfMissionDate = new DateTime((int)yearEndOfMissionDate, (int)monthEndOfMissionDate, (int)dayEndOfMissionDate);
            }

            DAO.CompanyDAO.Instance.Add(companyDto);

            // Récupérer l'entreprise ajoutée
            IList<DTO.CompanyDTO> companies = DAO.CompanyDAO.Instance.GetAll();
            int count = companies.Count;
            companyDto = companies[count - 1];

            // Ajouter le congé
            DTO.LeaveDTO leaveDto = new DTO.LeaveDTO();
            leaveDto.StartDate = new DateTime(startDateYear, startDateMonth, startDateDay);
            leaveDto.EndDate = new DateTime((int)endDateYear, (int)endDateMonth, (int)endDateDay);
            leaveDto.Description = description;
            leaveDto.Company = companyDto;

            DAO.LeaveDAO.Instance.Add(leaveDto);

            // Supprimer l'entreprise ajoutée
            SqlException sqlException = Assert.Throws<SqlException>(delegate() { DAO.CompanyDAO.Instance.Delete(companyDto); });
            Assert.AreEqual(sqlException.Number, 547);

            // Récupérer le congé ajouté
            IList<DTO.LeaveDTO> leaves = DAO.LeaveDAO.Instance.GetAll();
            count = leaves.Count;
            leaveDto = leaves[count - 1];

            // Récupérer le congé par son ID
            int id = leaveDto.ID;
            leaveDto = DAO.LeaveDAO.Instance.GetById(id);

            // Modifier le congé
            leaveDto.Description = "XXX";
            leaveDto.StartDate = DateTime.Now;
            leaveDto.EndDate = null;

            // Ajouter une entreprise
            DTO.CompanyDTO companyDto2 = new DTO.CompanyDTO();
            companyDto2.Name = name;
            companyDto2.RecruitementDate = new DateTime(yearRecruitementDate, monthRecruitementDate, dayRecruitementDate);
            if (yearEndOfMissionDate != null && monthEndOfMissionDate != null && dayEndOfMissionDate != null)
            {
                companyDto2.EndOfMissionDate = new DateTime((int)yearEndOfMissionDate, (int)monthEndOfMissionDate, (int)dayEndOfMissionDate);
            }

            DAO.CompanyDAO.Instance.Add(companyDto2);

            // Récupérer l'entreprise ajoutée
            companies = DAO.CompanyDAO.Instance.GetAll();
            count = companies.Count;
            companyDto2 = companies[count - 1];

            leaveDto.Company = companyDto2;

            DAO.LeaveDAO.Instance.Update(leaveDto);

            // Supprimer le congé ajouté
            DAO.LeaveDAO.Instance.Delete(leaveDto);

            // Supprimer l'entreprise ajoutée
            DAO.CompanyDAO.Instance.Delete(companyDto);
            DAO.CompanyDAO.Instance.Delete(companyDto2);
        }
    
    }
}
