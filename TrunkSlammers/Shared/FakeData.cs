
using System;
using System.Collections.Generic;
using System.Linq;

namespace TrunkSlammers.Shared
{
    public static class FakeData
    {
        public static Random random = new Random();

        public static List<League> leagues { get; set; } = new List<League>();
        private static int leagueId { get; set; } = 1;
        public static bool initalizedPlayers = false;

        static FakeData()
        {
            for (int x = 0; x <= random.Next(2, 10); x++)
            {

                var userIds = new List<(string, string)>();
                userIds.Add(("User1", "11e902e9-3c1d-4d71-a9e2-a8d0ccf12a01"));
                userIds.Add(("user2", "12716ee5-3f65-4590-bffc-2fd929fe1237"));

                League tmpLeague = new League();
                tmpLeague.Name = "League" + RandomString(5);
                tmpLeague.Id = leagueId;
                leagueId += 1;

                int rand = random.Next(0, userIds.Count());
                tmpLeague.Players.Add(new Player
                {
                    UserId = userIds[rand].Item2
                });
                tmpLeague.Players.Add(new Player
                {
                    UserId = "12345678-9999-4444-aaaa-" + RandomString(12)
                });
                tmpLeague.Players.Add(new Player
                {
                    UserId = "12345678-9999-4444-aaaa-" + RandomString(12)
                });
                tmpLeague.Players.Add(new Player
                {
                    UserId = "12345678-9999-4444-aaaa-" + RandomString(12)
                });

                Event tmpEvent = new Event
                {
                    StartDate = new DateTime(2021, 12, 1),
                    Id = 1,
                    Location = "Boyne",
                    Name = "Name" + RandomString(4)
                };

                Event tmpEventt = new Event
                {
                    StartDate = new DateTime(2021, 10, 1),
                    Id = 2,
                    Location = "Shanny",
                    Name = "Name" + RandomString(4)
                };


                Team tmpTeamConner = new Team
                {
                    Name = "Conner"
                };
                tmpTeamConner.Members.Add(tmpLeague.Players.ElementAt(0));

                Team tmpTeamPat = new Team
                {
                    Name = "Pat"
                };
                tmpTeamPat.Members.Add(tmpLeague.Players.ElementAt(1));

                tmpEvent.Teams.Add(tmpTeamPat);
                tmpEvent.Teams.Add(tmpTeamConner);

                tmpEventt.Teams.Add(tmpTeamPat);
                tmpEventt.Teams.Add(tmpTeamConner);

                tmpLeague.Events.Add(tmpEvent);
                tmpLeague.Events.Add(tmpEventt);
                leagues.Add(tmpLeague);

            }
                //var ranNum = random.Next(0, 20);
                //for (int count = 0; count <= ranNum; count++)
                //{
                //    y.Members.Add(new Player());
                //}
        }

        public static string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }

    }

}
