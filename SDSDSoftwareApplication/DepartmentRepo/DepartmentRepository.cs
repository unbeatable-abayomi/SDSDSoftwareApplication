using Microsoft.EntityFrameworkCore;
using SDSDSoftwareApplication.Data;
using SDSDSoftwareApplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SDSDSoftwareApplication.DepartmentRepo
{
	public class DepartmentRepository : IDepartment
	{

		private readonly ApplicationDbContext context;
		//public IEnumerable<Department> Departments => return context.EmployeeeTable();
		
		public IEnumerable<Department> Departments
		{
			get
			{
				return context.Departments.ToList();
			}
		}

		public DepartmentRepository(ApplicationDbContext cont)
		{
			context = cont;
		}

		//Add Department
		public void Add(Department department)
		{
			context.Departments.Add(department);
			context.SaveChanges();
		}

		public Department Delete(Guid DepartmentId)
		{
			Department department = context.Departments.Find(DepartmentId);
			if (department != null)
			{
				context.Departments.Remove(department);
				context.SaveChanges();
			}

			return department;
		}

		public Department GetDepartment(Guid DepartmentId)
		{
			return context.Departments.Find(DepartmentId);
		}

		public void EditDepartment(Department department)
		{
			context.Entry(department).State = EntityState.Modified;
			context.SaveChanges();
		}
	}
}
