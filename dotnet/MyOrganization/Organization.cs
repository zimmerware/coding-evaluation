using MyOrganization;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace MyOrganization
{
    /// <summary>
    /// Use these for unit tests....they WORK as expeected
    ///     org.Hire(new Name("Slick", "William"), "Salesperson"); //duplicate test to make sure William doesn't replacee Willie
    ///     org.Hire(new Name("Slick", "William"), "Salespersonnnnnnnnnnnn");  //no such job test
    /// </summary>
    internal abstract class Organization
    {
        private Position root;

        public Organization()
        {
            root = CreateOrganization();
        }

        protected abstract Position CreateOrganization();

      
       
        /// <summary>
        ///   * hire the given person as an employee in the position that has that title
        ///   @return the newly filled position or empty if no position has that title
        /// </summary>
        /// <param name="person"></param>
        /// <param name="title"></param>
        /// <returns>nullable Position</returns>
        public Position? Hire(Name person, string title)
        {
            Employee employee = new Employee(Guid.NewGuid(), person);  //Use SQL Server identity instead of GUID later

            Position? position = LameSearcherSinceCouldntGetTreeSearchToWork(employee, title);

            if (position != null)
                return position;
            else
                return null; //not found

        }

        /// <summary>
        ///   Couldn't get more elegant way to work but still hoping for interview
        ///   since I have over 4 years C# and database experience at Frontier Airlines
        /// </summary>
        /// <param name="employee"></param>
        /// <param name="title"></param>
        /// <returns></returns>
        protected Position? LameSearcherSinceCouldntGetTreeSearchToWork(Employee employee, string title)
        {
            if (root == null)
                return null;

            //CEO only
            if (title == root.GetTitle() && !root.IsFilled())
            {
                root.SetEmployee(employee);
                Position position = new Position(title, employee);
                return position;
            }

            Position? p = SearcherRecursive(root, employee, title);

            if (p != null)
                return p;
            else 
                return null; //not found
        }

        protected Position? SearcherRecursive(Position listOfPositions, Employee employee, string title)
        {
            if (listOfPositions == null)
                return null;

            ImmutableList<Position> positions = listOfPositions.GetDirectReports();
           
            foreach (Position pos in positions)
            {

                if (title == pos.GetTitle() && !pos.IsFilled())
                {
                    Position position = new Position(title, employee);
                    pos.SetEmployee(employee);
                    return position;
                }
                else
                {
                    SearcherRecursive(pos, employee, title);
                }
            }

            return null;
        }


        override public string ToString()
        {
            return PrintOrganization(root, "");
        }

        private string PrintOrganization(Position pos, string prefix)
        {
            StringBuilder sb = new StringBuilder(prefix + "+-" + pos.ToString() + "\n");
            foreach (Position p in pos.GetDirectReports())
            {
                sb.Append(PrintOrganization(p, prefix + "  "));
            }
            return sb.ToString();
        }
    }
}
