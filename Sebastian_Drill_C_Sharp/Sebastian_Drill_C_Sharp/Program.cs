using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;

namespace Sebastian_Drill_C_Sharp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("please enter ENOUGH 6 times to skip the 3 example persons of the drill");
            Maagar_loop maagr1 = new Maagar_loop();
            maagr1.TheDataOG = new List<Person>()
            {
                new Person("example",123,111,new Dictionary<string, int>(){{"hobby1",1},{"hobby2",2}},new List<string>(){"property1", "property2"}),
                new Person("example",456,111,new Dictionary<string, int>(){{"hobby3",3},{"hobby2",2}},new List<string>(){"property3", "property2"}),
                new Person("example",789,222,new Dictionary<string, int>(){{"hobby3",3},{"hobby4",4}},new List<string>(){"property3", "property4"})
            };
            maagr1.MaagarCommunication();
        }
    }
    class Person
    {
        public string Name { get; set; }
        public int Id { get; set; }
        public int Age {get;set;}
        public Dictionary<string, int> Hobbies = new();
        public List<string> PersonAdditionalProperties = new();

        public Person(string name, int id, int age)
        {
            Name = name;
            Id = id;
            Age = age;

            bool IsEnoughHobbies = false;
            bool IsEnoughAdditionalProperties = false;
            while (!IsEnoughHobbies)
            {
                IsEnoughHobbies = this.addHobby();
            }
            while (!IsEnoughAdditionalProperties)
            {
                IsEnoughAdditionalProperties = this.addAditionalProperty();
            }
        }
        public Person(string name, int id, int age, Dictionary<string, int> hobbies, List<string> personAdditionalProperties) : this(name, id, age)
        {
            Hobbies = hobbies;
            PersonAdditionalProperties = personAdditionalProperties;
        }
        public bool addHobby()
        {
            string newHobby;
            int investmentLevel = 1;
            Console.WriteLine("please enter the new hobby name OR the word ENOUGH to stop adding hobbies");
            newHobby = Console.ReadLine();
            if (newHobby == "ENOUGH")
            {
                return true;
            }
            Console.WriteLine("please enter how much ${0} is invest at new hobby from 1(not much) to 5(a lot)", this.Name);
            try
            {
                investmentLevel = int.Parse(Console.ReadLine());
                if (investmentLevel < 1 || investmentLevel > 5)
                {
                    Console.WriteLine("invesment level most be a number from 1 to 5!");
                    newHobby = null;
                    return false;
                }
            }
            catch
            {
                Console.WriteLine("invesment level most be a number from 1 to 5!");
                return false;
            }

            Hobbies.Add(newHobby, investmentLevel);
            return false;

        }
        public bool addAditionalProperty()
        {
            string newProperty;
            Console.WriteLine("please enter new property of the person OR enter ENOUGH to stop adding");
            newProperty = Console.ReadLine();
            if (newProperty == "ENOUGH")
            {
                return true;
            }
            this.PersonAdditionalProperties.Add(newProperty);
            return false;
        }
    }
    class Maagar_loop
    {
        public List<Person> TheDataOG { get; set; } //how do i do  to this?{ get; set; }    // = new List<Person>();
        /*TheDataOG = new List<Person>();*/
        string input;
        bool endDrill = false;
        public void MaagarCommunication()
        {
            List<Person> TheFilteredData = new List<Person>();
            TheFilteredData = TheDataOG;
            while (!endDrill)
            {
                
                Console.WriteLine("welcome to the 'maagar' \nplease enter one of the next options OR enter ENOUGH to end the drill");
                Console.WriteLine("add - to add new person to the 'maagar'");
                Console.WriteLine("remove - to remove person from the 'maagar'");
                Console.WriteLine("print all - to print all the data in 'maagar'");
                Console.WriteLine("find - to filter the data in 'maagar'");
                Console.WriteLine("print - to print the filtered data from 'maagar'");
                input = Console.ReadLine();
                Console.WriteLine();
                switch (input)
                {
                    case ("ENOUGH"):
                        endDrill = true;
                        break;
                    case ("add"):
                        addPerson();
                        break;
                    case ("remove"):
                        removePerson();
                        break ;
                    case ("print all"):
                        printTheData(TheDataOG);
                        break;
                    case ("find"):
                        TheFilteredData = filterListBy(TheDataOG);
                        break ;
                    case ("print"):
                            printTheData(TheFilteredData);
                        break ;
                    default:
                        Console.WriteLine("please enter valid text");
                        break;
                }
            }
        }
        public void addPerson()
        {
            string name;
            int age;
            int id;
            Console.WriteLine("please enter persons name");
            name = Console.ReadLine();
            Console.WriteLine("please enter persons age");
            age = int.Parse(Console.ReadLine());
            Console.WriteLine("please enter persons id");
            id = int.Parse(Console.ReadLine());
            try
            {
                if (TheDataOG.Find(x => x.Id == id)!= null)
                {
                    throw new Exception("this id already used");
                }
                TheDataOG.Add(new Person(name, id, age));

            }
            catch
            {
                Console.WriteLine("this id already used, please try anither");
            }

        }
        public void removePerson()
        {
            int id;
            Console.WriteLine("please enter persons id");
            id = int.Parse(Console.ReadLine());
            Person person = TheDataOG.Find(x=> x.Id == id);
            if (person==null)
            {
                Console.WriteLine("couldnt find this id");
                return;
            }
            TheDataOG.Remove(person);
        }
        public void printTheData(List<Person> theList)
        {
            foreach (Person theItem in theList)
            {
                Console.WriteLine($"name: {theItem.Name}, age: {theItem.Age}, ID: {theItem.Id}");
                /*Console.WriteLine(theItem.Age);
                Console.WriteLine(theItem.Id);*/
                Console.WriteLine("hobbies:");
                foreach (object keyAndValue in theItem.Hobbies)
                {
                    Console.WriteLine(keyAndValue.ToString());
                }

                Console.WriteLine("properties:");
                foreach (object property in theItem.PersonAdditionalProperties)
                {
                    Console.WriteLine(property.ToString());
                }
                Console.WriteLine();
            }
        }
        public List<Person> filterListBy(List<Person> OGList)
        {
            List<Person> filterdData = OGList;
            bool endFiltering = false;
            Console.WriteLine("please enter how do you want to filter the list");
            Console.WriteLine("name-N \nage-A  \nhobby-H \nhobbyInterstLvl-HIL \nproperty-P"); /*\nageRange - AR*/
            string filterdByString;
            string input = Console.ReadLine();
            do 
            {
                switch (input)
                {
                    case ("N"):
                        Console.WriteLine("please enter the name that you want to get");
                        filterdByString = Console.ReadLine();
                        filterdData = OGList
                            .Where(n => n.Name == filterdByString)
                            .ToList()
                            /*.Select(n=>n.Name)*/;
                        filterdData = filterAgain(filterdData);
                        endFiltering= true;
                        break;

                    case ("A"):
                        Console.WriteLine("please enter the name that you want to get");
                        filterdByString = Console.ReadLine();
                        try
                        {
                            int filterdByInt = int.Parse(filterdByString);
                            filterdData = OGList
                                .Where(n => n.Age == filterdByInt)
                                .ToList();
                            filterdData = filterAgain(filterdData);
                            endFiltering = true;
                            break;
                        }
                        catch
                        {
                            Console.WriteLine("invalid input");
                            break;
                        }
                    /*case ("AR"):*/

                    case ("H"):
                        Console.WriteLine("please enter the name of the hobby that you want to get");
                        filterdByString = Console.ReadLine();
                        filterdData = OGList
                            .Where(n => n.Hobbies.Keys.Any(k=> k == filterdByString))
                            .ToList();
                        filterdData = filterAgain(filterdData);
                        endFiltering = true;
                        break;

                    case ("HIL"):
                        Console.WriteLine("please enter the lvl of intrest that you want to get");
                        filterdByString = Console.ReadLine();
                        try
                        {
                            int filterdByInt = int.Parse(filterdByString);
                            if (filterdByInt > 5 || filterdByInt < 1)
                            {
                                throw new Exception();
                            }
                            filterdData = OGList
                                .Where(n => n.Hobbies.Values.Any(v => v == filterdByInt))
                                .ToList();
                            filterdData = filterAgain(filterdData);
                            endFiltering = true;
                            break;
                        }
                        catch
                        {
                            Console.WriteLine("invalid input");
                            break;
                        }
                    case ("P"):
                        Console.WriteLine("please enter the name of the property that you want to get");
                        filterdByString = Console.ReadLine();
                        filterdData = OGList
                            .Where(P => P.PersonAdditionalProperties.Any(k => k == filterdByString))
                            .ToList();
                        filterdData = filterAgain(filterdData);
                        endFiltering = true;
                        break;
                    default:
                        Console.WriteLine("invalid input");
                        break;
                }
                List<Person> FilterdList = new List<Person>();

            } while (!endFiltering);
            return filterdData;
        }
        public List<Person> filterAgain(List<Person> OGList)
        {
            string yesORno;
            bool endAsking = false;
            do
            {
                Console.WriteLine("would you like to filter more? \nenter Y for yes OR N for no.");
                yesORno = Console.ReadLine();
                switch (yesORno)
                {
                    case "N":
                        endAsking = true;
                        break;
                    case "Y":
                        return filterListBy(OGList);
                    default:
                        Console.WriteLine("invalid input");
                        break;
                }
            }while(!endAsking);
            return OGList;
        }
    }
}
