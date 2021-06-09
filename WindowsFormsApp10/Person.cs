using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp10
{
    [Serializable]
    class Person
    {
        [JsonRequired]
        public string Name { get; set; }
        [JsonRequired]
        public string Surname { get; set; }
        [JsonRequired]
        public string Phone { get; set; }
        [JsonRequired]
        public string Email { get; set; }
        [JsonRequired]
        public DateTime BirthDate { get; set; }
    }
}
