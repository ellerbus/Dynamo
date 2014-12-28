using System;
using System.Collections.Generic;
using System.Linq;
using Sample.Core.Repositories;
using Sample.Core.Models;

namespace Sample.Core.Services
{
	#region Service interface
	
	/// <summary>
	/// Service Interface for MemberRole
	/// </summary>
	public interface IMemberRoleService 
	{
		/// <summary>
		/// Gets a list of MemberRoles
		/// </summary>
		/// <returns></returns>
		IList<MemberRole> GetList();

		/// <summary>
		/// Gets a singe MemberRole based on the given primary key
		/// </summary>
		MemberRole Get(int memberId, int roleId);
		
		/// <summary>
		/// Saves a MemberRole
		/// </summary>
		void Save(MemberRole memberRole);
		
		/// <summary>
		/// Saves a list of MemberRole
		/// </summary>
		void Save(IEnumerable<MemberRole> memberRoles);
		
		///// <summary>
		///// Deletes a MemberRole
		///// </summary>
		//void Delete(MemberRole memberRole);
		//
		/// <summary>
		///// Deletes a list of MemberRole
		///// </summary>
		//void Delete(IEnumerable<MemberRole> memberRoles);
	}
	
	#endregion

	/// <summary>
	/// Service Implementation for MemberRole
	/// </summary>
	public class MemberRoleService : IMemberRoleService 
	{
		#region Members

		private IMemberRoleRepository _repository;

		#endregion

		#region Constructors

		/// <summary>
		/// Creates a new instance
		/// </summary>
		public MemberRoleService(IMemberRoleRepository repository)
		{
			_repository = repository;
		}

		#endregion

		#region Methods

		/// <summary>
		/// Gets a list of MemberRoles
		/// </summary>
		/// <returns></returns>
		public IList<MemberRole> GetList()
		{
			return _repository.GetList();
		}

		/// <summary>
		/// Gets a singe MemberRole based on the given primary key
		/// </summary>
		public MemberRole Get(int memberId, int roleId)
		{
			MemberRole memberRole = _repository.Get(memberId, roleId);
			
			return memberRole;
		}
		
		/// <summary>
		/// Saves a MemberRole
		/// </summary>
		/// <returns></returns>
		public void Save(MemberRole memberRole)
		{
			_repository.Save(memberRole);
		}
		
		/// <summary>
		/// Saves a list of MemberRoles
		/// </summary>
		/// <returns></returns>
		public void Save(IEnumerable<MemberRole> memberRoles)
		{
			_repository.Save(memberRoles);
		}
		
		/// <summary>
		/// Deletes a MemberRole
		/// </summary>
		public void Delete(MemberRole memberRole)
		{
			_repository.Delete(memberRole);
		}
		
		/// <summary>
		/// Deletes a list of MemberRole
		/// </summary>
		public void Delete(IEnumerable<MemberRole> memberRoles)
		{
			_repository.Delete(memberRoles);
		}

		#endregion
	}
}