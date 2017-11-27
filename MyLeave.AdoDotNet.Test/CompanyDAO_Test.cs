using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;

namespace MyLeave.AdoDotNet.Test
{
    public class CompanyDAO_Test
    {
        [TestCase("Archos Technology", 2011, 7, 1, 2013, 5, 3)]
        [TestCase("Visiativ Maroc", 2013, 5, 13, null, null, null)]
        public void TestCompanyDAO(string name, int yearRecruitementDate, int monthRecruitementDate, int dayRecruitementDate, 
            int? yearEndOfMissionDate, int? monthEndOfMissionDate, int? dayEndOfMissionDate)
        {

            // Récupérer la liste de entreprise à partir de la base de données
            IList<DTO.CompanyDTO> companies = DAO.CompanyDAO.Instance.GetAll();
            // Mémoriser le nombre des entreprises dans la base de données
            int count1 = companies != null ? companies.Count : 0;

            // Ajouter une nouvelle entreprise
            DTO.CompanyDTO companyDto = new DTO.CompanyDTO();
            companyDto.Name = name;
            companyDto.RecruitementDate = new DateTime(yearRecruitementDate,monthRecruitementDate,dayRecruitementDate);
            if (yearEndOfMissionDate != null && monthEndOfMissionDate != null && dayEndOfMissionDate != null)
            {
                companyDto.EndOfMissionDate = new DateTime((int)yearEndOfMissionDate, (int)monthEndOfMissionDate, (int)dayEndOfMissionDate);
            }

            DAO.CompanyDAO.Instance.Add(companyDto);

            // Récupérer la liste de entreprise à partir de la base de données
            companies = DAO.CompanyDAO.Instance.GetAll();
            // Vérifié si une entreprise à été ajouté à la base de données
            int count2 = companies != null ? companies.Count : 0;
            Assert.AreEqual(count2, count1 + 1);

            // Récupérer la dernière entreprise ajoutée à la base de données
            int Id = companies[count2 - 1].ID;
            companyDto = DAO.CompanyDAO.Instance.GetById(Id);
            Assert.NotNull(companyDto);

            // Modifier l'entreprise récupérée
            companyDto.Name = "XXX";
            DAO.CompanyDAO.Instance.Update(companyDto);

            // Récupérer une entreprise par son ID
            companies = DAO.CompanyDAO.Instance.Find(c => c.ID == Id);
            Assert.NotNull(companies);
            Assert.AreEqual(companies.Count, 1);

            companyDto = companies[0];
            Assert.AreEqual(companyDto.Name, "XXX");

            // Modifier l'entreprise récupérée
            companyDto.EndOfMissionDate = null;
            DAO.CompanyDAO.Instance.Update(companyDto);

            // Récupérer la dernière entreprise ajoutée à la base de données
            companyDto = DAO.CompanyDAO.Instance.GetById(Id);
            Assert.IsNull(companyDto.EndOfMissionDate);

            // Supprimer la dernière entreprise ajoutée à la base de données
            DAO.CompanyDAO.Instance.Delete(companyDto);
            // Récupérer la liste de entreprise à partir de la base de données
            companies = DAO.CompanyDAO.Instance.GetAll();
            // Vérifié l'entreprise à été supprimée à la base de données
            int count3 = companies != null ? companies.Count : 0;
            Assert.AreEqual(count1, count3);

        }
    }
}
