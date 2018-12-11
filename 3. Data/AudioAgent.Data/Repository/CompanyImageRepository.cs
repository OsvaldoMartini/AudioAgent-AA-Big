using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using AudioAgent.Data.IRepository;
using AudioAgent.Data.Utils;
using MySql.Data.MySqlClient;

namespace AudioAgent.Data.Repository
{
    public class CompanyImageRepository : GenericRepository<CompanyImageEntity>, ICompanyImageRepository
    {
        /// <summary>
        /// Method responsible for simulating data load;
        /// </summary>
        /// <returns></returns>
        public List<CompanyImageEntity> GetCompanyImageByUrl(string typeCompany, string url)
         {
             List<CompanyEntity> listCompanies = new List<CompanyEntity>()
             {
                 new CompanyEntity(){CompanyID = 1,CompanyType = "aero"},
                 new CompanyEntity(){CompanyID = 2,CompanyType = "toys"},
                 new CompanyEntity(){CompanyID = 3,CompanyType = "bags"},
                 new CompanyEntity(){CompanyID = 4,CompanyType = "metal"},
                 new CompanyEntity(){CompanyID = 5,CompanyType = "hats"},


             };

             var companies = new int[] { 1, 2, 3, 4, 5 };

            if (!string.IsNullOrEmpty(typeCompany) && typeCompany != "all")
             {
                 List<CompanyEntity> list = listCompanies.Where(v => v.CompanyType.Equals(typeCompany)).ToList();
                 if (list.Count == 0)
                     return new List<CompanyImageEntity>();

                 Array.Resize(ref companies, list.Count);
                 for (int x=0; x <= list.Count-1; x++)
                     companies[x] = list[x].CompanyID;
             }

             var deckCompanies = CreateShuffledDeck(companies);

            var fileExtesion = new string[] { "jpg", "bmp", "png", "ctv", "avv", "ttf", "pdf", "mdg"};
            var extension = new int[] { 0, 1, 2, 3, 4, 5, 6 ,7 };
             var deckExtension = CreateShuffledDeck(extension);
             var sizeMB = new int[] { 40, 55, 1024, 2048, 5045, 50404, 34555 };
             var deckSizeMB = CreateShuffledDeck(sizeMB);

            var imageUrlIdx = new int[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9};
            var imageUrl = new string[] {
                 "https://www.countries-ofthe-world.com/flags-normal/flag-of-United-Kingdom.png",
                 "https://www.countries-ofthe-world.com/flags-normal/flag-of-Eswatini.png",
                 "https://www.countries-ofthe-world.com/flags-normal/flag-of-Denmark.png",
                 "https://www.countries-ofthe-world.com/flags-normal/flag-of-Romania.png",
                 "https://www.countries-ofthe-world.com/flags-normal/flag-of-Canada.png",
                 "https://www.countries-ofthe-world.com/flags-normal/flag-of-Cameroon.png",
                 "https://www.countries-ofthe-world.com/flags-normal/flag-of-Brunei.png",
                 "https://www.countries-ofthe-world.com/flags-normal/flag-of-Nepal.png",
                 "https://www.countries-ofthe-world.com/flags-normal/flag-of-Monaco.png",
                 "https://www.countries-ofthe-world.com/flags-normal/flag-of-Australia.png"};
             var deckImageUrl = CreateShuffledDeck(imageUrlIdx);



            List<CompanyImageEntity> listCorpSaleByType = new List<CompanyImageEntity>();

             int loop = 35;
            while (loop > 0)
            {
                var extensionRange = -1;
                if (deckExtension.Count > 0)
                    extensionRange = deckExtension.Pop();
                else
                {
                    //Refil deckMonths
                    deckExtension = CreateShuffledDeck(extension);
                    extensionRange = deckExtension.Pop();
                }

                 var companyRange = -1;
                if (deckCompanies.Count > 0)
                     companyRange = deckCompanies.Pop();
                 else
                 {
                    //Refil deckCompanies
                     deckCompanies = CreateShuffledDeck(companies);
                     companyRange = deckCompanies.Pop();
                }

                var sizeMBRange = -1;
                if (deckSizeMB.Count > 0)
                    sizeMBRange = deckSizeMB.Pop();
                else
                {
                    //Refil deckCompanies
                    deckSizeMB = CreateShuffledDeck(sizeMB);
                    sizeMBRange = deckSizeMB.Pop();
                }

                var imageUrlRange = -1;
                if (deckImageUrl.Count > 0)
                    imageUrlRange = deckImageUrl.Pop();
                else
                {
                    //Refil deckCompanies
                    deckImageUrl = CreateShuffledDeck(imageUrlIdx);
                    imageUrlRange = deckImageUrl.Pop();
                }



                listCorpSaleByType.Add(new CompanyImageEntity()
                 {
                     CompanyImageID = 35 - loop + 1,
                     ImageSizeMB = sizeMBRange,
                     ImageExtension = fileExtesion[extensionRange],
                     ImageUrl = imageUrl[imageUrlRange],
                     DateCreated = DateTime.Now.AddYears(-5),
                     TCompany = listCompanies[companyRange-1]
                 });

                loop--;

            }

            if (!string.IsNullOrEmpty(url) && typeCompany == null)
            {
                return listCorpSaleByType.Where(v => v.ImageUrl.Equals(url)).ToList();
            }

            return listCorpSaleByType;
        }

        private Stack<T> CreateShuffledDeck<T>(IEnumerable<T> values)
        {
            var rand = new Random();

            var list = new List<T>(values);
            var stack = new Stack<T>();
             
            while (list.Count > 0)
            {
                // Get the next item at random.
                var index = rand.Next(0, list.Count);
                var item = list[index];

                // Remove the item from the list and push it to the top of the deck.
                list.RemoveAt(index);
                stack.Push(item);
            }

            return stack;
        }

    }
}   