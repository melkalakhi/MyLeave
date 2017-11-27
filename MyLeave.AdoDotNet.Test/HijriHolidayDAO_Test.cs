using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;

namespace MyLeave.AdoDotNet.Test
{
    public class HijriHolidayDAO_Test
    {
        [TestCase(4, 1, 2015)]
        public void TestHijriHolidayDAO(int day, int month, int year)
        {
            // La liste des jours fériers hijri
            IList<DTO.HijriHolidayDTO> hijriHolidays = DAO.HijriHolidayDAO.Instance.GetAll();
            int count = hijriHolidays != null ? hijriHolidays.Count : 0;

            // Ajouter un jour férie hijri
            DTO.HijriHolidayDTO hijriHolidayDto = new DTO.HijriHolidayDTO();
            hijriHolidayDto.Day = day;
            hijriHolidayDto.Month = month;
            hijriHolidayDto.Year = year;
            DAO.HijriHolidayDAO.Instance.Add(hijriHolidayDto);

            // La liste des jours fériers hijri
            hijriHolidays = DAO.HijriHolidayDAO.Instance.GetAll();
            int count2 = hijriHolidays != null ? hijriHolidays.Count : 0;
            Assert.AreEqual(count2, count + 1);

            // Récupérer le jour férie hijri ajouté
            hijriHolidayDto = hijriHolidays[count];
            int id = hijriHolidayDto.ID;

            // Récupérer le jour férie Hijri par ID
            hijriHolidayDto = DAO.HijriHolidayDAO.Instance.GetById(id);
            Assert.NotNull(hijriHolidayDto);

            // Modifier le jour férie
            hijriHolidayDto.Day = 5;
            hijriHolidayDto.Month = 9;
            hijriHolidayDto.Year = 2020;
            DAO.HijriHolidayDAO.Instance.Update(hijriHolidayDto);

            // Supprimer le jour férie Hijri
            DAO.HijriHolidayDAO.Instance.Delete(hijriHolidayDto);

        }
    }
}
