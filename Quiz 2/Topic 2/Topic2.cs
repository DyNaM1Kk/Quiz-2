using System.Text;

namespace Quiz2
{
    public class Topic2
    {
        //StringBuider preference
        //Difference

        public static void Showcase()
        {
            StringBuilder newString = new StringBuilder("Bootcamp");

            //Append method
            newString.Append(" / .NET");

            //AppendLine method
            newString.AppendLine();

            //AppendFormat method
            newString.AppendFormat("\tName: {0}, Age: {1}", "mariami", 21);
            newString.AppendLine();
            newString.Append("\tStudies at Caucasus University");

            Console.WriteLine(newString);
            Console.ReadKey(true);

            //Replace method 
            newString.Replace("\tStudies at Caucasus University", "\tStatus:  Student");
            newString.AppendLine();
            newString.Append('-', 15);

            //Insert method
            newString.AppendLine();
            newString.Insert(78, "Access code- NJJ78");

            newString.AppendLine();
            newString.Append("Membership is valid until 18.12.24");
            newString.AppendLine();
            newString.Append("Active");

            Console.WriteLine(newString);
            Console.ReadKey(true);

            //Remove method ( student status is removed)
            newString.Remove(38, 50);
            Console.WriteLine(newString);
            Console.ReadKey(true);

            //Clear method 
            newString.Clear();
            Console.WriteLine(newString);
            Console.ReadKey(true);

            //Combination of methods
            StringBuilder anotherString = new StringBuilder("Member info:");
            anotherString.AppendLine().Append('-', 10).AppendLine().Append("Name:").AppendLine().Append("Surname:");
            Console.WriteLine(anotherString);
            Console.ReadKey(true);
        }
    }
}