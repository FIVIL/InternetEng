using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InternetProject
{
    public class SessionProvider
    {
        public Guid AdminToken = Guid.Empty;
        public ConcurrentDictionary<Guid, DateTime> Sessions { get; set; }
        public SessionProvider()
        {
            Sessions = new ConcurrentDictionary<Guid, DateTime>();
        }
        public async Task Set(Guid id)
        {
            await Task.Run(() =>
            {
                Sessions.TryAdd(id, DateTime.Now);
            });
            Console.WriteLine(Sessions.Count+"  "+id);
        }
        public async Task<bool> Get(Guid ID)
        {
            return await Task.Run(() =>
            {
                if (Sessions.ContainsKey(ID))
                {
                    Sessions[ID] = DateTime.Now;
                    Console.WriteLine("T");
                    return true;
                }
                Console.WriteLine("F");
                return false;
            });
        }
        public async Task<bool> Clear(Guid id)
        {
            return await Task.Run(() =>
            {
                return Sessions.TryRemove(id, out var dt);
            });
        }
        public async void Clear()
        {
            await Task.Run(() =>
            {
                var p = Sessions.Where(x => x.Value < DateTime.Now.AddMinutes(1)).Select(x => x.Key);
                foreach (var item in p)
                {
                    Sessions.Remove(item, out var retValue);
                }
            });
        }
    }
}
