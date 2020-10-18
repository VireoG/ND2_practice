using System;
using Task1_Homework.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Html;

namespace Task1_Homework.Business
{
    public class EventList 
    {
        private readonly List<Event> events = new List<Event>();
        private readonly VenueList venueList;

        public EventList(VenueList venueList)
        {
            this.venueList = venueList;

            events.AddRange(
            new[]
                {
                    new Event { Id = 1, Date = new DateTime(2020, 09, 10, 19, 0, 0), Name = "Rock Hits", VenueId = 5, Banner = "1.jpg", Description = "<div><p>Рок-Культура — явление молодежной субкультуры, возникшей в Великобритании и США в 60 х гг." +
                    " вокруг нового муз. стиля и выражающей нонконформистский пафос. “Рок это больше, чем просто музыка, это энергетич. центр новой культуры и молодежной революции”.</p></div>" },

                    new Event { Id = 2, Date = new DateTime(2020, 09, 13, 18, 30, 0), Name = "Artur Pirozhok", VenueId = 3, Banner = "2.jpeg", Description = "<div><p>Алекса́ндр Влади́мирович Ре́вва (укр. Олександр Володимирович Ревва; род. " +
                    "10 сентября 1974, Донецк, Украинская ССР, СССР) — российский шоумен, комедийный актёр, телеведущий, певец. Бывший игрок команды КВН «Утомлённые солнцем». " +
                    "Резидент юмористического шоу «Comedy Club». Как певец выступает под псевдонимом Артур Пирожков.</p></div>"},

                    new Event { Id = 3, Date = new DateTime(2020, 10, 29, 16, 0, 0), Name = "B-2", VenueId = 11, Banner = "3.jpg", Description = "<div><p>Би-2 — советская, белорусская и российская рок-группа, образованная в " +
                    "1988 году в Бобруйске. Основатели и бессменные участники — Шура Би-2 (гитара, вокал) и Лёва Би-2 (основной вокал). " +
                    "В состав команды также входят Андрей Звонков (гитара), Макс Лакмус (бас-гитара), Борис Лифшиц (ударные, перкуссия) и " +
                    "Ян Николенко (бэк-вокал, клавишные, флейта, перкуссия). В 2017 году Би-2 завершили работу " +
                    "над десятым студийным альбомом «Горизонт событий».</p></div>" },

                    new Event { Id = 4, Date = new DateTime(2020, 11, 14, 20, 0, 0), Name = "Rock Concert", VenueId = 10, Banner = "4.jpg", Description = "<div><p>Рок-Культура — явление молодежной субкультуры, возникшей в Великобритании и США в 60 х гг." +
                    " вокруг нового муз. стиля и выражающей нонконформистский пафос. “Рок это больше, чем просто музыка, это энергетич. центр новой культуры и молодежной революции”.</p></div>" },

                    new Event { Id = 5, Date = new DateTime(2020, 11, 28, 20, 10, 0), Name = "Ivan Dorn", VenueId = 1, Banner = "5.jpg", Description = "<div><p>Ива́н Алекса́ндрович Дорн (укр. Іва́н Олекса́ндрович Дорн; род. 17 октября 1988, Челябинск, РСФСР, СССР) — украинский певец, музыкант, " +
                    "автор песен, продюсер, диджей, актёр и телеведущий. " +
                    "Основатель и директор студии звукозаписи Masterskaya. Бывший участник группы «Пара нормальных».</p></div>" },

                    new Event { Id = 6, Date = new DateTime(2020, 10, 21, 19, 30, 0), Name = "Ivan Abramov", VenueId = 2, Banner = "6.jpg", Description = "<div><p>Иван Абрамов – комик-интеллигент, музыкант, резидент шоу «Stand Up», бывший КВНщик (играл в команде МГИМО «Парапапарам»). " +
                    "Он много шутит о знаменитостях, политиках и повседневной жизни, и в каждом его стендапе есть музыкальная составляющая."},

                    new Event { Id = 7, Date = new DateTime(2020, 10, 29, 18, 0, 0), Name = "NoizeMc XVI", VenueId = 1, Banner = "7.jpg", Description = "<div><p>Ива́н Алекса́ндрович Алексе́ев, более известный под сценическим псевдонимом Noize MС" +
                    " — российский музыкант, рэп-рок-исполнитель. </p></div>" },

                    new Event { Id = 8, Date = new DateTime(2020, 10, 28, 17, 0, 0), Name = "Poets Of The Fall", VenueId = 4, Banner = "8.jpg", Description = "<div><p>Poets of the Fall — финская рок-группа, записывающаяся на собственном лейбле «Insomniac». Она образовалась в Хельсинки в 2003 году из дуэта старых друзей:" +
                    " вокалиста Марко Сааресто и гитариста Олли Тукиайнена, а также клавишника и продюсера Маркуса Каарлонена.</p></div>" },

                    new Event { Id = 9, Date = new DateTime(2020, 12, 29, 20, 30, 0), Name = "Nickelback", VenueId = 8, Banner = "9.jpg", Description = "<div><p>Nickelback — канадская альтернативная рок-группа, основанная в 1995 году в городе Ханна. " +
                    "Группа состоит из гитариста и вокалиста Чеда Крюгера; гитариста, клавишника и бэк-вокалиста Райана Пик; басиста Майка Крюгера и барабанщика Дэниеля Адэра. </p></div>" },

                    new Event { Id = 10, Date = new DateTime(2020, 11, 14, 20, 0, 0), Name = "Maks Korzh", VenueId = 12, Banner = "10.jpg", Description = "<div><p>Макс Корж - молодой музыкант из Минска, выходдец из тусовки MuSkool, " +
                    "с лёгкостью смешивающий в своём творчестве электронную музыку, рэп и задушевное пение.</p></div>" },
                });

            GetVenueByVenueId();
        }

        private void GetVenueByVenueId()
        {
            foreach(var item in events)
            {
                item.Venue = venueList.GetVenueById(item.VenueId);
            }
        }

        public Event[] GetEvent()
        {
            return events.ToArray();
        }

        public Event GetEventById(int id)
        {
            return events.FirstOrDefault(e => e.Id == id);
        }
    }
}
