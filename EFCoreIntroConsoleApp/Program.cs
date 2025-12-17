using EFCoreIntroConsoleApp.Contexts;
using Microsoft.EntityFrameworkCore;

namespace EFCoreIntroConsoleApp;

internal class Program
{
    static void Main(string[] args)
    {
        using AppDbContext context = new AppDbContext();

        Console.WriteLine("Welcome!");

start:
        Console.WriteLine("1. Create Department");
        Console.WriteLine("2. Delete Department");
        Console.WriteLine("3. Show All Departments");
        Console.WriteLine("4. Create Employee");
        Console.WriteLine("5. Delete Employee");
        Console.WriteLine("6. Show All Employees");
        Console.Write("Enter your choice: ");
        string? choice = Console.ReadLine();

        Console.Clear();

        switch (choice)
        {
            case "1":
departmentName:
                Console.Write("Please enter a department name: ");
                string departmentName = Console.ReadLine();

                if (departmentName == null || departmentName.Length < 5)
                {
                    Console.WriteLine("Please enter a valid name!");
                    goto departmentName;
                }

capacityInput:
                Console.Write("Please enter capacity of the department:");
                var isParsedCapacity = int.TryParse(Console.ReadLine(), out int capacity);

                if (!isParsedCapacity)
                {
                    Console.WriteLine("Enter a valid input..");
                    goto capacityInput;
                }

                Department department = new Department()
                {
                    Name = departmentName,
                    Capacity = capacity
                };

                context.Departments.Add(department);
                context.SaveChanges();

                Console.WriteLine("Department created successfully!");

                goto start;
            case "2":
                GetAllDepartments(context);
departmentIdInput:
                Console.Write("Enter id to delete a department: ");
                var isParsedDepartmentId = int.TryParse(Console.ReadLine(), out int departmentId);

                if (!isParsedDepartmentId)
                {
                    Console.WriteLine("Please enter a valid ID");
                    goto departmentIdInput;
                }

                var deletedDepartment = context.Departments.FirstOrDefault(x => x.Id == departmentId);

                if (deletedDepartment == null)
                {
                    Console.WriteLine("Department not found according to selected ID");
                    break;
                }

                context.Departments.Remove(deletedDepartment);
                context.SaveChanges();

                Console.WriteLine("Department deleted successfully!");
                goto start;
            case "3":
                GetAllDepartments(context);
                goto start;
            case "4":
nameInput:
                Console.Write("Enter name of the employee: ");
                string? nameInput = Console.ReadLine();

                if (nameInput == null || nameInput.Length < 3)
                {
                    Console.WriteLine("Please enter a valid input...");
                    goto nameInput;
                }

employeeSalaryInput:
                Console.Write("Enter salary of the employee: ");
                var isParsedEmployeeSalary = decimal.TryParse(Console.ReadLine(), out decimal employeeSalary);

                if (!isParsedEmployeeSalary)
                {
                    Console.WriteLine("Please enter a valid input...");
                    goto employeeSalaryInput;
                }

                if (employeeSalary < 0)
                {
                    Console.WriteLine("salary cannot be less than 0!");
                    goto employeeSalaryInput;
                }

                GetAllDepartments(context);

employeeDepartmentIdInput:
                Console.Write("Enter department Id of the employee: ");
                var isParsedEmployeeDepartmentId = int.TryParse(Console.ReadLine(), out int employeeDepartmentId);

                if (!isParsedEmployeeDepartmentId)
                {
                    Console.WriteLine("Please enter a valid input...");
                    goto employeeDepartmentIdInput;
                }

                var isExistedDepartment = context.Departments.FirstOrDefault(x => x.Id == employeeDepartmentId);


                if (isExistedDepartment == null)
                {
                    Console.WriteLine("Department not found!");
                    goto employeeDepartmentIdInput;
                }
                var employeesByDepartmentId = context.Employees.Where(x => x.DepartmentId == employeeDepartmentId).ToList();

                if (employeesByDepartmentId == null)
                {
                    Console.WriteLine("Employee not found in department");
                }

                if (employeesByDepartmentId.Count() >= isExistedDepartment.Capacity)
                {
                    Console.WriteLine("Department capacity is full! Employee cannot be created!");
                    goto start;
                }

                Employee employee = new Employee()
                {
                    Name = nameInput,
                    Salary = employeeSalary,
                    DepartmentId = employeeDepartmentId
                };

                context.Employees.Add(employee);
                context.SaveChanges();

                Console.WriteLine("Employee created successfully!");
                goto start;
            case "5":
                GetAllEmployees(context);
employeeIdInput:
                Console.Write("Enter id to delete a employee: ");
                var isParsedEmployeeId = int.TryParse(Console.ReadLine(), out int employeeId);

                if (!isParsedEmployeeId)
                {
                    Console.WriteLine("Please enter a valid ID");
                    goto employeeIdInput;
                }

                var deletedEmployee = context.Employees.FirstOrDefault(x => x.Id == employeeId);

                if (deletedEmployee == null)
                {
                    Console.WriteLine("Employee not found according to selected ID");
                    break;
                }

                context.Employees.Remove(deletedEmployee);
                context.SaveChanges();

                Console.WriteLine("Employee deleted successfully!");
                goto start;
            case "6":
                GetAllEmployees(context);
                goto start;
            default:
                Console.WriteLine("Invalid option. Please try again..");
                break;

        }
    }

    private static void GetAllEmployees(AppDbContext context)
    {
        Console.WriteLine("");
        Console.WriteLine("-------------------------------------");
        var employees = context.Employees.Include(x => x.Department).ToList();
        employees.ForEach(employee => Console.WriteLine(employee));
        Console.WriteLine("-------------------------------------");
        Console.WriteLine("");
    }

    private static void GetAllDepartments(AppDbContext context)
    {
        Console.WriteLine("");
        Console.WriteLine("-------------------------------------");
        var departments = context.Departments.ToList();
        departments.ForEach(department => Console.WriteLine(department));
        Console.WriteLine("-------------------------------------");
        Console.WriteLine("");
    }
}
