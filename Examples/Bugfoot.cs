using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace Examples
{
    public class Bugfoot
    {
        public static void Do()
        {
            var sessionManagementService = new SessionManagement();
            var users = Enumerable.Range(0, 900).Select(i => new User(sessionManagementService, i)).ToList();
            Console.ReadKey();
        }

        class User
        {
            Page _page;
            public User(SessionManagement sessionManagement, int counter, bool isAuto=true)
            {
                _page = new Page(counter, sessionManagement);
                new Thread(() =>
                {
                    while (true)
                    {
                        Thread.Sleep(1000);
                        _page.Refresh();
                    }
                }).Start();
            }
        }

        class Page
        {
            private Guid _sessionId;
            private SessionManagement _sessionManagementService;
            private int _counter = 0;

            public Page(int counter, SessionManagement sessionManagementService)
            {
                _sessionId = Guid.NewGuid();
                _sessionManagementService = sessionManagementService;
                _counter = counter;

                sessionManagementService.SaveSession(new Session
                {
                    Id = _sessionId,
                    LoggedInUserId = Guid.NewGuid(),
                    SessionInfo = "this is session " + counter
                });
            }

            public void Refresh()
            {
                var session =_sessionManagementService.GetSession(_sessionId);
                if (session.Id != _sessionId)
                {
                    Console.BackgroundColor=ConsoleColor.Red;
                }
                Console.WriteLine(session.SessionInfo);
                Console.BackgroundColor = ConsoleColor.Black;
            }
        }

        class SessionManagement
        {
            static Db _db = new Db();
            Session session;
            public Session GetSession(Guid sessionId)
            {
                session=_db.GetSession(sessionId);
                return session;
            }

            public void SaveSession(Session session)
            {
                _db.SaveSession(session);
            }
        }

        class Db
        {
            private const int dbMaxLatency = 50;
            readonly Dictionary<Guid,Session> _sessionsInDb = new Dictionary<Guid, Session>();
            
            public void SaveSession(Session session)
            {
                Session dbSession=null;
                if (_sessionsInDb.ContainsKey(session.Id))
                {
                    dbSession = _sessionsInDb[session.Id];
                }
                Thread.Sleep(GetLatency());
                if (dbSession != null)
                {
                    dbSession.SessionInfo = session.SessionInfo;
                }
                else
                {
                    _sessionsInDb.Add(session.Id, session);
                }
                Thread.Sleep(GetLatency());
            }

            public Session GetSession(Guid sessionId)
            {
                var session = _sessionsInDb[sessionId];
                Thread.Sleep(GetLatency());
                return session;
            }

            static int GetLatency()
            {
                return new Random().Next(dbMaxLatency);
            }
        }

        class Session
        {
            public Guid Id { get; set; }
            public Guid LoggedInUserId { get; set; }
            public string SessionInfo { get; set; }
        }
    }
}
