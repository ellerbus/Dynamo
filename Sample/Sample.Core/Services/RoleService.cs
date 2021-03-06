using System;
using System.Collections.Generic;
using System.Linq;
using Sample.Core.Repositories;
using Sample.Core.Models;

namespace Sample.Core.Services
{
	#region Service interface
	
	/// <summary>
	/// Service Interface for Role
	/// </summary>
	public interface IRoleService 
	{
		/// <summary>
		/// Gets a list of Roles
		/// </summary>
		/// <returns></returns>
		IList<Role> Get();

		/// <summary>
		/// Gets a singe Role based on the given primary key
		/// </summary>
		Role Get(int id);
		
		/// <summary>
		/// Saves a Role
		/// </summary>
		void Save(Role role);
		
		/// <summary>
		/// Saves a list of Role
		/// </summary>
		void Save(IEnumerable<Role> roles);
		
		/// <summary>
		/// Inserts a Role
		/// </summary>
		void Insert(Role role);

		/// <summary>
		/// Inserts a list of Role
		/// </summary>
		void Insert(IEnumerable<Role> roles);
		
		/// <summary>
		/// Updates a Role
		/// </summary>
		void Update(Role role);

		/// <summary>
		/// Updates a list of Role
		/// </summary>
		void Update(IEnumerable<Role> roles);
		
		/// <summary>
		/// Deletes a Role
		/// </summary>
		void Delete(Role role);

		/// <summary>
		/// Deletes a list of Role
		/// </summary>
		void Delete(IEnumerable<Role> roles);
	}
	
	#endregion

	/// <summary>
	/// Service Implementation for Role
	/// </summary>
	public class RoleService : IRoleService 
	{
		#region Members

		private IRoleRepository _repository;

		#endregion

		#region Constructors

		/// <summary>
		/// Creates a new instance
		/// </summary>
		public RoleService(IRoleRepository repository)
		{
			_repository = repository;
		}

		#endregion

		#region Methods

		/// <summary>
		/// Gets a list of Roles
		/// </summary>
		/// <returns></returns>
		public IList<Role> Get()
		{
			return _repository.Get();
		}

		/// <summary>
		/// Gets a singe Role based on the given primary key
		/// </summary>
		public Role Get(int id)
		{
			Role role = _repository.Get(id);
			
			return role;
		}
		
		/// <summary>
		/// Saves a Role
		/// </summary>
		/// <returns></returns>
		public void Save(Role role)
		{
			_repository.Save(role);
		}
		
		/// <summary>
		/// Saves a list of Roles
		/// </summary>
		/// <returns></returns>
		public void Save(IEnumerable<Role> roles)
		{
			_repository.Save(roles);
		}
		
		/// <summary>
		/// Inserts a Role
		/// </summary>
		public void Insert(Role role)
		{
			_repository.Insert(role);
		}
		
		/// <summary>
		/// Inserts a list of Role
		/// </summary>
		public void Insert(IEnumerable<Role> roles)
		{
			_repository.Insert(roles);
		}
		
		/// <summary>
		/// Updates a Role
		/// </summary>
		public void Update(Role role)
		{
			_repository.Update(role);
		}
		
		/// <summary>
		/// Updates a list of Role
		/// </summary>
		public void Update(IEnumerable<Role> roles)
		{
			_repository.Update(roles);
		}
		
		/// <summary>
		/// Deletes a Role
		/// </summary>
		public void Delete(Role role)
		{
			_repository.Delete(role);
		}
		
		/// <summary>
		/// Deletes a list of Role
		/// </summary>
		public void Delete(IEnumerable<Role> roles)
		{
			_repository.Delete(roles);
		}

		#endregion
	}
}