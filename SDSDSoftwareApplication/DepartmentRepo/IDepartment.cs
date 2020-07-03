using SDSDSoftwareApplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SDSDSoftwareApplication.DepartmentRepo
{
	public interface IDepartment
	{
		IEnumerable<Department> Departments { get; }
		public void Add(Department departments);
		Department Delete(Guid DepartmentId);
		Department GetDepartment(Guid DepartmentId);

		public void EditDepartment(Department departments);
	}
}
