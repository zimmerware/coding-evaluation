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
    /// Use these for unit tests....they WORK
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

            if (title == root.GetTitle() && !root.IsFilled())
            {
                root.SetEmployee(employee);
                Position position = new Position(title, employee);
                return position;
            }
            ImmutableList<Position> positions = root.GetDirectReports();
            foreach (Position pos1 in positions)
            {
                if (title == pos1.GetTitle() && !pos1.IsFilled())
                {
                    Position position = new Position(title, employee);
                    pos1.SetEmployee(employee);
                    return position;
                }
                ImmutableList<Position> pos2 = pos1.GetDirectReports();
                foreach (Position pos3 in pos2)
                {
                    if (title == pos3.GetTitle() && !pos3.IsFilled())
                    {
                        Position position = new Position(title, employee);
                        pos3.SetEmployee(employee);
                        return position;
                    }
                    ImmutableList<Position> pos4 = pos3.GetDirectReports();
                    foreach (Position pos5 in pos4)
                    {
                        if (title == pos5.GetTitle() && !pos5.IsFilled())
                        {
                            Position position = new Position(title, employee);
                            pos5.SetEmployee(employee);
                            return position;
                        }
                    }
                }

            }

            return null; //not found
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
