using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace person_files
{
    [Serializable]
    class Person
    {
        public string Name { get; set; }
        public int Age { get; set; }
        public DateTime Birthday { get; set; }
        public string Position { get; set; }

        public Person()
        {
            Name = "Person";
            Birthday = DateTime.Now;
        }
        public Person(string name, DateTime date, string position)
        {
            this.Name = name;
            this.Birthday = date;
            setAge(date);
            Position = position;
        }

        public void setAge(DateTime date)
        {
            DateTime today = DateTime.Today;
            Age = today.Year - Birthday.Year;
            if (Birthday > today.AddYears(-Age) && Age > 0) Age--;
        }

    }
}
