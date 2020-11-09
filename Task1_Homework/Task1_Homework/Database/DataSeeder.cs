using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Task1_Homework.Business.Models;

namespace Task1_Homework.Business.Database
{
    public class DataSeeder
    {
        private readonly ResaleContext context;

        private static readonly List<Event> events = new List<Event>();

        private static readonly List<Venue> venues = new List<Venue>();

        private static readonly List<City> cities = new List<City>();

        private static readonly List<Ticket> tickets = new List<Ticket>();

        private static readonly List<Order> orders = new List<Order>();

        public DataSeeder(ResaleContext context)
        {
            this.context = context;
            cities.AddRange(
                   new[]
                   {
                       new City { Name = "Minsk" },
                       new City { Name = "Moscow" },
                       new City { Name = "London" },
                       new City { Name = "New-York" },
                       new City { Name = "Tokyo" },
                       new City { Name = "Homel" },
                   });

            venues.AddRange(
                new[]
                {
                    new Venue { Name = "Prime Hall", Adress = "Somewhere in Minsk", City = cities[0] },
                    new Venue { Name = "Area 1.2", Adress = "Somewhere in Minsk", City = cities[0] },
                    new Venue { Name = "Area 2.1", Adress = "Somewhere in Moscow", City = cities[1] },
                    new Venue { Name = "Area 2.2", Adress = "Somewhere in Moscow", City = cities[1] },
                    new Venue { Name = "Area 3.1", Adress = "Somewhere in London", City = cities[2] },
                    new Venue { Name = "Area 3.2", Adress = "Somewhere in London", City = cities[2] },
                    new Venue { Name = "Area 4.1", Adress = "Somewhere in New-York", City = cities[3] },
                    new Venue { Name = "Area 4.2", Adress = "Somewhere in New-York", City = cities[3] },
                    new Venue { Name = "Area 5.1", Adress = "Somewhere in Tokyo", City = cities[4] },
                    new Venue { Name = "Area 5.2", Adress = "Somewhere in Tokyo", City = cities[4] },
                    new Venue { Name = "Area 6.1", Adress = "Somewhere in Homel", City = cities[5] },
                    new Venue { Name = "Area 6.2", Adress = "Somewhere in Homel", City = cities[5] },
                });

            events.AddRange(
                  new[]
                  {
                        new Event {  Date = new DateTime(2020, 09, 10, 19, 0, 0), Name = "Rock Hits", Venue = venues[4], Banner = "1.jpg", Description = "<div><p>Рок-Культура — явление молодежной субкультуры, возникшей в Великобритании и США в 60 х гг." +
                        " вокруг нового муз. стиля и выражающей нонконформистский пафос. “Рок это больше, чем просто музыка, это энергетич. центр новой культуры и молодежной революции”.</p></div>" },

                        new Event {  Date = new DateTime(2020, 09, 13, 18, 30, 0), Name = "Artur Pirozhok",Venue = venues[2], Banner = "2.jpeg", Description = "<div><p>Алекса́ндр Влади́мирович Ре́вва (укр. Олександр Володимирович Ревва; род. " +
                        "10 сентября 1974, Донецк, Украинская ССР, СССР) — российский шоумен, комедийный актёр, телеведущий, певец. Бывший игрок команды КВН «Утомлённые солнцем». " +
                        "Резидент юмористического шоу «Comedy Club». Как певец выступает под псевдонимом Артур Пирожков.</p></div>"},

                        new Event {  Date = new DateTime(2020, 10, 29, 16, 0, 0), Name = "B-2", Venue = venues[9], Banner = "3.jpg", Description = "<div><p>Би-2 — советская, белорусская и российская рок-группа, образованная в " +
                        "1988 году в Бобруйске. Основатели и бессменные участники — Шура Би-2 (гитара, вокал) и Лёва Би-2 (основной вокал). " +
                        "В состав команды также входят Андрей Звонков (гитара), Макс Лакмус (бас-гитара), Борис Лифшиц (ударные, перкуссия) и " +
                        "Ян Николенко (бэк-вокал, клавишные, флейта, перкуссия). В 2017 году Би-2 завершили работу " +
                        "над десятым студийным альбомом «Горизонт событий».</p></div>" },

                        new Event {  Date = new DateTime(2020, 11, 14, 20, 0, 0), Name = "Rock Concert", Venue = venues[9], Banner = "4.jpg", Description = "<div><p>Рок-Культура — явление молодежной субкультуры, возникшей в Великобритании и США в 60 х гг." +
                        " вокруг нового муз. стиля и выражающей нонконформистский пафос. “Рок это больше, чем просто музыка, это энергетич. центр новой культуры и молодежной революции”.</p></div>" },

                        new Event { Date = new DateTime(2020, 11, 28, 20, 10, 0), Name = "Ivan Dorn", Venue = venues[0], Banner = "5.jpg", Description = "<div><p>Ива́н Алекса́ндрович Дорн (укр. Іва́н Олекса́ндрович Дорн; род. 17 октября 1988, Челябинск, РСФСР, СССР) — украинский певец, музыкант, " +
                        "автор песен, продюсер, диджей, актёр и телеведущий. " +
                        "Основатель и директор студии звукозаписи Masterskaya. Бывший участник группы «Пара нормальных».</p></div>" },

                        new Event {Date = new DateTime(2020, 10, 21, 19, 30, 0), Name = "Ivan Abramov",Venue = venues[1], Banner = "6.jpg", Description = "<div><p>Иван Абрамов – комик-интеллигент, музыкант, резидент шоу «Stand Up», бывший КВНщик (играл в команде МГИМО «Парапапарам»). " +
                        "Он много шутит о знаменитостях, политиках и повседневной жизни, и в каждом его стендапе есть музыкальная составляющая.</p></div>"},

                        new Event { Date = new DateTime(2020, 10, 29, 18, 0, 0), Name = "NoizeMc XVI", Venue = venues[2], Banner = "7.jpg", Description = "<div><p>Ива́н Алекса́ндрович Алексе́ев, более известный под сценическим псевдонимом Noize MС" +
                        " — российский музыкант, рэп-рок-исполнитель. </p></div>" },

                        new Event { Date = new DateTime(2020, 10, 28, 17, 0, 0), Name = "Poets Of The Fall", Venue = venues[3], Banner = "8.jpg", Description = "<div><p>Poets of the Fall — финская рок-группа, записывающаяся на собственном лейбле «Insomniac». Она образовалась в Хельсинки в 2003 году из дуэта старых друзей:" +
                        " вокалиста Марко Сааресто и гитариста Олли Тукиайнена, а также клавишника и продюсера Маркуса Каарлонена.</p></div>" },

                        new Event {  Date = new DateTime(2020, 12, 29, 20, 30, 0), Name = "Nickelback", Venue = venues[7], Banner = "9.jpg", Description = "<div><p>Nickelback — канадская альтернативная рок-группа, основанная в 1995 году в городе Ханна. " +
                        "Группа состоит из гитариста и вокалиста Чеда Крюгера; гитариста, клавишника и бэк-вокалиста Райана Пик; басиста Майка Крюгера и барабанщика Дэниеля Адэра. </p></div>" },

                        new Event {  Date = new DateTime(2020, 11, 14, 20, 0, 0), Name = "Maks Korzh", Venue = venues[9], Banner = "10.jpg", Description = "<div><p>Макс Корж - молодой музыкант из Минска, выходдец из тусовки MuSkool, " +
                        "с лёгкостью смешивающий в своём творчестве электронную музыку, рэп и задушевное пение.</p></div>" },
                  });

            tickets.AddRange(
                new[]
                {
                     new Ticket { Event = events[0], Price = 100, SellerName = "user2@mail.ru", Status = TicketSaleStatus.Sale },
                     new Ticket { Event = events[1], Price = 110, SellerName= "user2@mail.ru",  Status = TicketSaleStatus.Sale },
                     new Ticket { Event = events[0], Price = 110, SellerName= "user2@mail.ru", Status = TicketSaleStatus.Sale },
                     new Ticket { Event = events[1], Price = 120, SellerName= "user2@mail.ru", Status = TicketSaleStatus.Sale },
                     new Ticket { Event = events[7], Price = 120, SellerName= "user2@mail.ru", Status = TicketSaleStatus.Sale },
                     new Ticket { Event = events[3], Price = 220, SellerName= "user2@mail.ru", Status = TicketSaleStatus.Sale },
                     new Ticket { Event = events[6], Price = 130,  Status = TicketSaleStatus.Sale },
                     new Ticket { Event = events[3], Price = 130,  Status = TicketSaleStatus.Sale },
                     new Ticket { Event = events[4], Price = 180,  Status = TicketSaleStatus.Sale },
                     new Ticket { Event = events[3], Price = 110,  Status = TicketSaleStatus.Sale },
                     new Ticket { Event = events[0], Price = 360,  Status = TicketSaleStatus.Sale },
                     new Ticket { Event = events[4], Price = 180,  Status = TicketSaleStatus.Sale },
                     new Ticket { Event = events[8], Price = 206,  Status = TicketSaleStatus.Sale },
                     new Ticket { Event = events[3], Price = 120,  Status = TicketSaleStatus.Sale },
                     new Ticket { Event = events[4], Price = 160,  Status = TicketSaleStatus.Sale },
                     new Ticket { Event = events[8], Price = 230,  Status = TicketSaleStatus.Sale },
                     new Ticket { Event = events[5], Price = 100,  Status = TicketSaleStatus.Sale },
                     new Ticket { Event = events[6], Price = 610,  Status = TicketSaleStatus.Sale },
                     new Ticket { Event = events[5], Price = 410,  Status = TicketSaleStatus.Sale },
                     new Ticket { Event = events[8], Price = 120,  Status = TicketSaleStatus.Sale },
                     new Ticket { Event = events[8], Price = 120,  Status = TicketSaleStatus.Sale },
                     new Ticket { Event = events[7], Price = 420,  Status = TicketSaleStatus.Sale },
                     new Ticket { Event = events[7], Price = 130,  Status = TicketSaleStatus.Sale },
                     new Ticket { Event = events[8], Price = 730,  Status = TicketSaleStatus.Sale },
                });



        }

        public async Task SeedDataAsync()
        {
            await context.Database.EnsureCreatedAsync();
            if (!context.Events.Any())
            {
                await context.Events.AddRangeAsync(events);
            }

            if (!context.Cities.Any())
            {
                await context.Cities.AddRangeAsync(cities);
            }

            if (!context.Venues.Any())
            {
                await context.Venues.AddRangeAsync(venues);
            }

            if (!context.Tickets.Any())
            {
                await context.Tickets.AddRangeAsync(tickets);
            }

            if (!context.Orders.Any())
            {
                await context.Orders.AddRangeAsync(orders);
            }

            await context.SaveChangesAsync();
        }
    }
}  

