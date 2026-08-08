using System; 
 
namespace Practical2 
{ 
    interface ISalary 
    { 
        void ShowSalary(); 
    } 
 
    class Employee 
    { 
        public int EmpId;         public string Name;         public string Department; 
        public string Designation; 
 
        public Employee(int id, string name, string dept, string desig) 
        { 
            EmpId = id; 
            Name = name; 
            Department = dept; 
            Designation = desig; 
        } 
    } 
 
    class Manager : Employee, ISalary 
    { 
        public Manager(int id, string name, string dept, string desig) 
            : base(id, name, dept, desig) 
        { 
        } 
 
        public void ShowSalary() 
        { 
            Console.WriteLine("----- Employee Payroll Details -----"); 
            Console.WriteLine("Employee ID : " + EmpId); 
            Console.WriteLine("Name        : " + Name); 
            Console.WriteLine("Department  : " + Department); 
            Console.WriteLine("Designation : " + Designation); 
            Console.WriteLine("Salary      : 10,000$"); 
        } 
    } 
 
    class Program 
    { 
        static void Main(string[] args) 
        { 
            Manager m = new Manager(101, "Yatharthsinh", "CE", "computer engineer");             
m.ShowSalary(); 
        } 
    } 
} 
