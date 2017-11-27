using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;

namespace MyLeave.AdoDotNet.Test
{
    public class NationalHolidayDAO_Test
    {
        [TestCase(1, 5)]
        [TestCase(1, 1)]
        public void TestNationalHolidayDAO(int day, int month)
        {
            // Récupérer la liste des jours féries nationals
            IList<DTO.NationalHolidayDTO> nationalHolidays = DAO.NationalHolidayDAO.Instance.GetAll();
            int count = nationalHolidays != null ? nationalHolidays.Count : 0;

            // Ajouter un jour férie
            DTO.NationalHolidayDTO nationalHolidayDto = new DTO.NationalHolidayDTO();
            nationalHolidayDto.Day = day;
            nationalHolidayDto.Month = month;
            DAO.NationalHolidayDAO.Instance.Add(nationalHolidayDto);

            // Récupérer la liste des jours féries nationals
            nationalHolidays = DAO.NationalHolidayDAO.Instance.GetAll();
            int count2 = nationalHolidays != null ? nationalHolidays.Count : 0;
            Assert.AreEqual(count2, count + 1);

            // Récupérer le jour férie
            nationalHolidayDto = nationalHolidays[count];

            // Récupérer le jour férie par ID
            nationalHolidayDto = DAO.NationalHolidayDAO.Instance.GetById(nationalHolidayDto.ID);
            Assert.NotNull(nationalHolidayDto);

            nationalHolidayDto.Day = 18;
            nationalHolidayDto.Month = 11;
            DAO.NationalHolidayDAO.Instance.Update(nationalHolidayDto);

            // Récupérer le jour férie par ID
            nationalHolidayDto = DAO.NationalHolidayDAO.Instance.GetById(nationalHolidayDto.ID);
            Assert.NotNull(nationalHolidayDto);
            Assert.AreEqual(nationalHolidayDto.Month, 11);

            // Supprimer le jour férie
            DAO.NationalHolidayDAO.Instance.Delete(nationalHolidayDto);
        }
    }
}
