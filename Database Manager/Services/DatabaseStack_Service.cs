using Database_Manager.Model;
using Database_Manager.Views;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using Windows.Storage;

namespace Database_Manager.Services
{
    internal class DatabaseStack_Service
    {







        public bool AddNewCard(DatabaseCard newCard)
        {

            var localSettings = ApplicationData.Current.LocalSettings;


            List<DatabaseCard> stackCards = GetCards();

            bool hasCard = false;

            foreach (DatabaseCard card in stackCards)
            {
                if (card.Name == newCard.Name) hasCard = true;
            }

            if (!hasCard) stackCards.Add(newCard);

            string newList = JsonConvert.SerializeObject(stackCards);
            localSettings.Values["DatabaseStack01"] = newList;

            Database_stack.Database_stackContext.updateStack();

            return hasCard;

        }


        public void RemoveCard(DatabaseCard delCard)
        {


            var localSettings = ApplicationData.Current.LocalSettings;
            List<DatabaseCard> stackCards = GetCards();
            List<DatabaseCard> filteredList = stackCards.FindAll(x => x.URI != delCard.URI);


            string newList = JsonConvert.SerializeObject(filteredList);
            localSettings.Values["DatabaseStack01"] = newList;



            Database_stack.Database_stackContext.updateStack();


            MainPage.MainPageContext.Frame.Navigate(typeof(MainPage));



        }

        public List<DatabaseCard> GetCards()
        {

            try
            {
                var localSettings = ApplicationData.Current.LocalSettings;

                string list = (string)localSettings.Values["DatabaseStack01"];

                List<DatabaseCard> deserilizedCardList = JsonConvert.DeserializeObject<List<DatabaseCard>>(list);


                return deserilizedCardList;

            }
            catch 
            {

      

                return new List<DatabaseCard>();
            }
        }

        public void RunCard(DatabaseCard runCard)
        {

            var localSettings = ApplicationData.Current.LocalSettings;
            List<DatabaseCard> stackCards = GetCards();
            DatabaseCard currentCard = stackCards.Find(x => x.URI == runCard.URI);

            DatabaseCard newCard = new DatabaseCard();


            if (currentCard.Status == "Stopped")
            {
                currentCard.Status = "Running";
            }
            else
            {
                currentCard.Status = "Stopped";
            }

            string newList = JsonConvert.SerializeObject(stackCards);
            localSettings.Values["DatabaseStack01"] = newList;
            Database_stack.Database_stackContext.updateStack();


        }



    }
}
