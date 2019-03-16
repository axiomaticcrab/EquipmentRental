using System.Collections.Generic;
using Newtonsoft.Json;
using StackExchange.Redis;
using KeyNotFoundException = EquipmentRental.Util.Repository.Exception.KeyNotFoundException;

namespace EquipmentRental.Util.Repository
{
    public class RedisRepository
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

        public T Get<T>(int id)
        {
            return Get<T>(id.ToString());
        }

        public T Get<T>(string keySuffix)
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

        public bool Exists(int id)
        {
            return Exists(id.ToString());
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
