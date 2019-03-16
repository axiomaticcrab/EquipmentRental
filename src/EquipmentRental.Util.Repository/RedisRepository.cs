using System.Collections.Generic;
using Newtonsoft.Json;
using StackExchange.Redis;
using KeyNotFoundException = EquipmentRental.Util.Repository.Exception.KeyNotFoundException;

namespace EquipmentRental.Util.Repository
{
    public class Entity
    {
        public int EntityId { get; set; }
    }

    public interface IRepository<T>
    {
        T GetById(int id);
        List<T> GetMultiple(List<int> ids);
        bool Exists(int id);
        void Save(T item);
    }

    public class RedisRepository<T> : IRepository<T> where T : Entity
    {
        private readonly IConnectionMultiplexer _redisConnection;

        /// <summary>
        /// The Namespace is the first part of any key created by this Repository
        /// </summary>
        private readonly string _namespace;

        public RedisRepository(IConnectionMultiplexer redis, string nameSpace)
        {
            _redisConnection = redis;
            _namespace = nameSpace;
        }

        public T Get(int id)
        {
            return Get(id.ToString());
        }

        public T Get(string keySuffix)
        {
            var key = MakeKey(keySuffix);
            var database = _redisConnection.GetDatabase();
            var serializedObject = database.StringGet(key);
            if (serializedObject.IsNullOrEmpty) throw new KeyNotFoundException(key);
            return JsonConvert.DeserializeObject<T>(serializedObject.ToString());
        }

        public List<T> GetMultiple<T>(List<int> ids)
        {
            var database = _redisConnection.GetDatabase();
            List<RedisKey> keys = new List<RedisKey>();
            foreach (int id in ids)
            {
                keys.Add(MakeKey(id));
            }
            var serializedItems = database.StringGet(keys.ToArray(), CommandFlags.None);
            List<T> items = new List<T>();
            foreach (var item in serializedItems)
            {
                items.Add(JsonConvert.DeserializeObject<T>(item.ToString()));
            }
            return items;
        }

        public T GetById(int id)
        {
            return Get(id);
        }

        public List<T> GetMultiple(List<int> ids)
        {
            return GetMultiple<T>(ids);
        }

        public bool Exists(int id)
        {
            return Exists(id.ToString());
        }

        public void Save(T item)
        {
            Save(item.EntityId, item);
        }

        public bool Exists(string keySuffix)
        {
            var key = MakeKey(keySuffix);
            var database = _redisConnection.GetDatabase();
            var serializedObject = database.StringGet(key);
            return !serializedObject.IsNullOrEmpty;
        }

        public void Save(int id, object entity)
        {
            Save(id.ToString(), entity);
        }

        public void Save(string keySuffix, object entity)
        {
            var key = MakeKey(keySuffix);
            var database = _redisConnection.GetDatabase();
            database.StringSet(MakeKey(key), JsonConvert.SerializeObject(entity));
        }

        private string MakeKey(int id)
        {
            return MakeKey(id.ToString());
        }

        private string MakeKey(string keySuffix)
        {
            if (!keySuffix.StartsWith(_namespace + ":"))
            {
                return _namespace + ":" + keySuffix;
            }
            else return keySuffix;
        }
    }
}
